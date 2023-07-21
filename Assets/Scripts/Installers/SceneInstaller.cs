using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    
    [SerializeField] private Boosters boosters;
    [SerializeField] private Spawner spawner;
    [SerializeField] private DifficultyManager difficultyManager;
    [SerializeField] private GameOverNotifier gameOverNotifier;

    public override void InstallBindings()
    {
        Container.Bind<Boosters>().FromInstance(boosters).AsSingle();
        Container.Bind<Spawner>().FromInstance(spawner).AsSingle();
        Container.Bind<DifficultyManager>().FromInstance(difficultyManager).AsSingle();
        Container.Bind<GameOverNotifier>().FromInstance(gameOverNotifier).AsSingle();
    }
}