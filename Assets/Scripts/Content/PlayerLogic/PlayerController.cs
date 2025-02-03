using Zenject;

namespace Assets.Scripts.Content
{
    public sealed class PlayerController : IEntity, IHealable
    {
        private readonly PLayerDamageHandler _damageHandler;
        private readonly PlayerHealHandler _healHandler;

        public PlayerController(PLayerDamageHandler damageHandler, PlayerHealHandler playerHealHandler)
        {
            _damageHandler = damageHandler;
            _healHandler = playerHealHandler;
        }

        public class Factory : PlaceholderFactory<PlayerController> { }

        public void Heal(float healPoints)
        {
            _healHandler.Heal(healPoints);
        }

        public T ProvideComponent<T>() where T : class
        {
            //Взаимодействие с классом как с агрегатором
            if (typeof(T) == typeof(IDamageable))
                return _damageHandler as T;

            //Взаимодействие с классом как с фасадом
            if (typeof(T) == typeof(IHealable))
                return this as T;

            return null;
        }
    }
}