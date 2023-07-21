// using UnityEngine;
// using Zenject;
//
// public class EnemyInstaller : MonoInstaller
// {
//     [SerializeField] private GameObject enemyPrefab;
//
//     public override void InstallBindings()
//     {
//         // Биндинг DifficultyManager в контейнер зависимостей
//         Container.Bind<DifficultyManager>().FromComponentInHierarchy().AsSingle();
//
//         // Биндинг EnemyFactory в контейнер зависимостей
//         Container.BindFactory<GameObject, EnemyFactory>()
//             .FromComponentInNewPrefab(enemyPrefab)
//             .WithGameObjectName("Enemy")
//             .UnderTransformGroup("Enemies")
//             .AsSingle();
//     }
//     
//     
//     // public GameObject enemyPrefab;
//     //
//     // public override void InstallBindings()
//     // {
//     //     Container.BindFactory<GameObject, EnemyFactory>()
//     //         .FromComponentInNewPrefab(enemyPrefab)
//     //         .AsSingle();
//     // }
// }