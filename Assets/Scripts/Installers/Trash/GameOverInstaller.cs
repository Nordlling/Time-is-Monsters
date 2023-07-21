using UnityEngine;
using Zenject;

public class GameOverInstaller : MonoInstaller
{
    [SerializeField] private GameOverNotifier gameOverNotifier;
    
    public override void InstallBindings()
    {
        Container.Bind<GameOverNotifier>().FromInstance(gameOverNotifier).AsSingle().NonLazy();
    }
}