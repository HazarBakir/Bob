using UnityEngine;

namespace AliMertCetin.Scripts.EnemyAI
{
    public class GunUser : MonoBehaviour
    {
        [SerializeField] Transform hand;
        public Gun gun { get; private set; }

        public void SetGun(Gun gun)
        {
            this.gun = gun;
            if (gun == false) return;
            this.gun.SetOwner(transform);
            Transform gunTransform = this.gun.transform;
            gunTransform.SetParent(hand);
            gunTransform.localPosition = Vector3.zero;
        }
        
        public bool HasGun()
        {
            return gun;
        }
    }
}