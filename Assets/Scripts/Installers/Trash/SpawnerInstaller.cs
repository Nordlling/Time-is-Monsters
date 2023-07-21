using UnityEngine;
using Zenject;

public class SpawnerInstaller : MonoInstaller
{
    [SerializeField] private Spawner spawner;
    
    public override void InstallBindings()
    {
        Container.Bind<Spawner>().FromInstance(spawner).AsSingle();
    }
}