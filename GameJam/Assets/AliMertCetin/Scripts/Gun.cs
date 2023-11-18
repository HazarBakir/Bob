using System.Buffers;
using UnityEngine;
using XIV.DesignPatterns.Common.HealthSystem;

namespace AliMertCetin.Scripts.EnemyAI
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] Transform gunTip;
        [SerializeField] float damageAmount;
        public Transform owner { get; private set; }

        public void SetOwner(Transform newOwner)
        {
            this.owner = newOwner;
        }

        public virtual void Fire(float distance, int layerMask)
        {
            var buffer = ArrayPool<RaycastHit>.Shared.Rent(4);
            var gunTipPosition = gunTip.position;
            var hitCount = Physics.RaycastNonAlloc(gunTipPosition, gunTip.forward, buffer, distance, layerMask);
            var closest = GetClosest(buffer, gunTipPosition, hitCount);
            if (closest.transform)
            {
                var damageable = closest.transform.GetComponent<IDamageable>();
                if (damageable != default && damageable.CanReceiveDamage()) damageable.ReceiveDamage(damageAmount);
            }

            ArrayPool<RaycastHit>.Shared.Return(buffer);
        }

        RaycastHit GetClosest(RaycastHit[] searchArray, Vector3 position, int length)
        {
            var distance = float.MaxValue;
            if (length == 0) return default;

            RaycastHit selected = default;

            for (int i = 0; i < length; i++)
            {
                var current = searchArray[i];
                var dist = Vector3.Distance(position, current.transform.position);
                if (dist < distance)
                {
                    distance = dist;
                    selected = current;
                }
            }
            
            return selected;
        }

        public bool HasOwner()
        {
            return owner;
        }
    }
}