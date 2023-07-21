using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    // [Inject]
    // private EnemyFactory enemyFactory;

    [Inject] private DiContainer diContainer;
    [Inject] private GameOverNotifier gameOverNotifier;
    [Inject] private DifficultyManager difficultyManager;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform point;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float minSpawnTime = 3;
    [SerializeField] private float maxSpawnTime = 8;
    [SerializeField] private float difficultyСoef = 0.95f;
    [SerializeField] private float freezeTime = 3f;
    [SerializeField] private int enemyCountToLose = 10;

    private int enemyCount;
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
        minSpawnTime *= difficultyСoef;
        maxSpawnTime *= difficultyСoef;
    }

    public void KillEnemy()
    {
        enemyCount--;
    }

    private void Spawn()
    {
        Debug.Log($"FIRST enemyCount = {enemyCount}");
        if (enemyCount >= enemyCountToLose)
        {
            return;
        } 
        Vector3 randomPoint = point.position + UnityEngine.Random.insideUnitSphere * radius;
        randomPoint.y = point.position.y;

        diContainer.InstantiatePrefab(enemyPrefab, randomPoint, Quaternion.identity, null);
        Debug.Log("SPAWN");
        enemyCount++;
        if (enemyCount >= enemyCountToLose)
        {
            gameOverNotifier.GameOver();
        }
        Debug.Log($"LAST enemyCount = {enemyCount}");

        // Instantiate(enemyPrefab, randomPoint, Quaternion.identity);
        // GameObject newEnemy = enemyFactory.Create();
        // newEnemy.transform.position = randomPoint;
    }

    public void FreezeSpawn()
    {
        _freeze = true;
    }
}
