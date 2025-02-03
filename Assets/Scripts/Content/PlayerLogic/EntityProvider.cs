using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content
{
    public class EntityProvider : MonoBehaviour, IEntity
    {
        private PlayerController _player;
        private PlayerController _playerController;
        [Inject]
        private void Construct(PlayerController player)
        {
            _player = player;
        }

        public T ProvideComponent<T>() where T : class
        {
            _player.ProvideComponent<T>();

            return null;
        }
    }
}