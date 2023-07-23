using System;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{

    [Inject] private DiContainer diContainer;
    [Inject] private GameOverNotifier gameOverNotifier;
    [Inject] private DifficultyManager difficultyManager;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform point;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float minSpawnTime = 3;
    [SerializeField] private float maxSpawnTime = 8;
    [SerializeField] private float coef = 0.95f;
    [SerializeField] private float freezeTime = 3f;
    [SerializeField] private int enemyCountToLose = 10;

    public event Action<int> OnUpdateDead;
    public event Action<int> OnUpdateAlive;

    public int _aliveEnemies;
    private int _deadEnemies;
    private bool _freeze;
    private float _leftFreezeTime;
    private float _currentTime;
    
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
        Vector3 randomPoint = point.position + UnityEngine.Random.insideUnitSphere * radius;
        randomPoint.y = point.position.y;

        diContainer.InstantiatePrefab(enemyPrefab, randomPoint, Quaternion.identity, null);
        _aliveEnemies++;
        OnUpdateAlive?.Invoke(_aliveEnemies);
        
        if (_aliveEnemies >= enemyCountToLose)
        {
            gameOverNotifier.GameOver(_deadEnemies);
            SaveManager.SaveHighScore(_deadEnemies);
        }
    }

    public void FreezeSpawn()
    {
        _freeze = true;
    }
}
