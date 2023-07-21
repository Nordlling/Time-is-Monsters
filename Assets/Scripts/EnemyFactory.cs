using UnityEngine;
using Zenject;

public class EnemyFactory : PlaceholderFactory<GameObject>
{
    private readonly DiContainer container;
    private readonly GameObject enemyPrefab;

    public EnemyFactory(DiContainer container, GameObject enemyPrefab)
    {
        this.container = container;
        this.enemyPrefab = enemyPrefab;
    }

    public GameObject Create()
    {
        return container.InstantiatePrefab(enemyPrefab);
    }
}