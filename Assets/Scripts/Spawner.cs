using System;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{

    [Inject] private DiContainer diContainer;
    [Inject] private GameOverNotifier gameOverNotifier;
    [Inject] private DifficultyManager difficultyManager;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Collider platformCollider;
    [SerializeField] private float minSpawnTime = 3;
    [SerializeField] private float maxSpawnTime = 8;
    [SerializeField] private float coef = 0.95f;
    [SerializeField] private float freezeTime = 3f;
    [SerializeField] private int enemyCountToLose = 10;

    [SerializeField] private AudioSource spawnEffect;

    public event Action<int> OnUpdateDead;
    public event Action<int> OnUpdateAlive;
    public event Action OnActivateLeftBacklight;
    public event Action OnActivateRightBacklight;

    private int _aliveEnemies;
    private int _deadEnemies;
    private bool _freeze;
    private float _leftFreezeTime;
    private float _currentTime;
    
    private float spawnMinX;
    private float spawnMaxX;
    private float spawnMinZ;
    private float spawnMaxZ;
    
    private void OnEnable()
    {
        difficultyManager.OnIncreaseLevel += IncreaseDifficult;
    }
    private void OnDisable()
    {
        difficultyManager.OnIncreaseLevel -= IncreaseDifficult;
    }

    private void Start()
    {
        _currentTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime + 1);
        _leftFreezeTime = freezeTime;
        
        var platformBounds = platformCollider.bounds;
        var offset = 2f;
        spawnMinX = platformBounds.min.x + offset;
        spawnMaxX = platformBounds.max.x - offset;
        spawnMinZ = platformBounds.min.z + offset;
        spawnMaxZ = platformBounds.max.z - offset;
    }


    private void Update()
    {
        if (_freeze)
        {
            _leftFreezeTime -= Time.deltaTime;
            if (_leftFreezeTime < 0)
            {
                _freeze = false;
                _leftFreezeTime = freezeTime;
            }
            return;
        }

        _currentTime -= Time.deltaTime;
        if (_currentTime < 0)
        {
            Spawn();
            _currentTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime + 1);
        }
    }

    private void IncreaseDifficult()
    {
        minSpawnTime *= coef;
        maxSpawnTime *= coef;
    }

    public void KillEnemy()
    {
        _aliveEnemies--;
        _deadEnemies++;
        if (_aliveEnemies < 0)
        {
            _aliveEnemies = 0;
        }
        OnUpdateDead?.Invoke(_deadEnemies);
        OnUpdateAlive?.Invoke(_aliveEnemies);
    }

    private void Spawn()
    {
        if (_aliveEnemies >= enemyCountToLose)
        {
            return;
        }
        spawnEffect.Play();
        
        Vector3 spawnPosition = createSpawnPosition();
        checkEnemyBeyondCameraView(spawnPosition);

        _aliveEnemies++;
        OnUpdateAlive?.Invoke(_aliveEnemies);
        
        diContainer.InstantiatePrefab(enemyPrefab, spawnPosition, Quaternion.identity, null);

        if (_aliveEnemies >= enemyCountToLose)
        {
            gameOverNotifier.GameOver(_deadEnemies);
            SaveManager.SaveHighScore(_deadEnemies);
        }
    }

    private void checkEnemyBeyondCameraView(Vector3 spawnPosition)
    {
        float xPosition = Camera.main.WorldToViewportPoint(spawnPosition).x;
        if (xPosition < 0f)
        {
            OnActivateLeftBacklight?.Invoke();
        } else if (xPosition > 1f)
        {
            OnActivateRightBacklight?.Invoke(); 
        }
    }

    private Vector3 createSpawnPosition()
    {
        float randomX = UnityEngine.Random.Range(spawnMinX, spawnMaxX);
        float randomZ = UnityEngine.Random.Range(spawnMinZ, spawnMaxZ);
        return new Vector3(randomX, transform.position.y, randomZ);
    }

    public void FreezeSpawn()
    {
        _freeze = true;
    }
}
