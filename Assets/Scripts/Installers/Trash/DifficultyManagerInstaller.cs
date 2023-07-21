using UnityEngine;
using Zenject;

public class DifficultyManagerInstaller : MonoInstaller
{
    [SerializeField] private DifficultyManager difficultyManager;
    
    public override void InstallBindings()
    {
        Container.Bind<DifficultyManager>().FromInstance(difficultyManager).AsSingle();
        // Container.Bind<DifficultyManager>().FromInstance(difficultyManager).AsSingle().NonLazy();
    }
}