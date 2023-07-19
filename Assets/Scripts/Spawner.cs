using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform point;
    [SerializeField] private float radius = 3f;
    [SerializeField] private int minSpawnTime = 3;
    [SerializeField] private int maxSpawnTime = 8;

    private float currentTime;

    private void Start()
    {
        currentTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime + 1);
    }


    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            Spawn();
            currentTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime + 1);
        }
    }

    private void Spawn()
    {
        Vector3 randomPoint = point.position + UnityEngine.Random.insideUnitSphere * radius;
        randomPoint.y = point.position.y;
        Instantiate(enemyPrefab, randomPoint, Quaternion.identity);
    }
}
