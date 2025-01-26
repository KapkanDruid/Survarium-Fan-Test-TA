using Assets.Scripts.Architecture.CustomEventBus;
using Assets.Scripts.Architecture.Input;
using Assets.Scripts.Content;
using Assets.Scripts.Content.PlayerLogic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture.Installers
{
    public class MainServicesInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;

        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle();
            Container.Bind<InputEventHandler>().AsSingle();
            Container.Bind<InputSystem_Actions>().AsSingle();

            Container.BindFactory<PlayerController, PlayerController.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<PlayerInstaller>(_playerPrefab);
        }
    }
}