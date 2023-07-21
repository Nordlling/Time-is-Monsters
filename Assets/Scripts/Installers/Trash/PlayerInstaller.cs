// using UnityEngine;
// using Zenject;
//
// public class PlayerInstaller : MonoInstaller
// {
//     [SerializeField] private PlayerMovement playerMovement;
//     
//     public override void InstallBindings()
//     {
//         Container.Bind<PlayerMovement>().FromInstance(playerMovement).AsSingle();
//         Container.QueueForInject(playerMovement);
//         
//         PlayerHealth playerHealth = playerMovement.GetComponent<PlayerHealth>();
//         Container.Bind<PlayerHealth>().FromInstance(playerHealth).AsSingle();
//         Container.QueueForInject(playerHealth);
//     }
// }