using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PLayerHorizontalMoveHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerVerticalMoveHandler>().AsSingle();
            Container.Bind<PlayerData>().FromComponentOnRoot();
            Container.Bind<CharacterController>().FromComponentOnRoot();
        }
    }
}
