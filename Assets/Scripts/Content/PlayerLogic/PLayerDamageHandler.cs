using UnityEngine;

namespace Assets.Scripts.Content
{
    public sealed class PLayerDamageHandler : IDamageable
    {
        public void TakeDamage(float damage)
        {
            Debug.Log(damage + " damage was taken");
        }
    }
}