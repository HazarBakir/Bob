using System;
using System.Buffers;
using AliMertCetin.Scripts.EnemyAI.States;
using TheGame.FSM;
using UnityEngine;
using UnityEngine.AI;
using XIV.Core.Extensions;
using XIV.Packages.ScriptableObjects.Channels;

namespace AliMertCetin.Scripts.EnemyAI
{
    public class EnemyFSM : StateMachine
    {
        [SerializeField] TransformChannelSO onPlayerLoadedChannel;
        [SerializeField] public float rotationSpeed = 50f;
        [field: SerializeField] public Transform model { get; private set; }
        [field: SerializeField] public float groundCheckRadius { get; private set; } = 0.25f;
        [field: SerializeField] public float moveSpeed { get; private set; } = 20f;
        [field: SerializeField] public float attackCooldown { get; private set; } = 3f;
        [field: SerializeField] public float dealDamageAmount { get; private set; } = 5f;
        [field: SerializeField] public float attackRange { get; private set; } = 1.5f;
        [field: SerializeField] public float interactableRange { get; private set; } = 1.5f;
        [field: SerializeField] public float fieldOfViewAngle { get; private set; } = 5f;
        [field: SerializeField] public float fieldOfViewDistance { get; private set; } = 8f;
        [field: SerializeField] public Vector3 gravity { get; private set; } = Physics.gravity;
        
        public GunUser gunUser { get; private set; }
        public NavMeshAgent navMeshAgent { get; private set; }
        public Transform playerTransform { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            gunUser = GetComponent<GunUser>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void OnEnable()
        {
            onPlayerLoadedChannel.Register(OnPlayerLoaded);
        }

        void OnDisable()
        {
            onPlayerLoadedChannel.Unregister(OnPlayerLoaded);
        }

        void OnPlayerLoaded(Transform playerTransform)
        {
            this.playerTransform = playerTransform;
        }

        protected override State GetInitialState()
        {
            return new EnemyStateFactory(this).GetState<EnemyGroundedState>();
        }

        public bool IsGrounded(out Vector3 closestGroundPosition)
        {
            var groundCollisionPos = transform.position;
            var buffer = ArrayPool<Collider>.Shared.Rent(2);
            int hitCount = Physics.OverlapSphereNonAlloc(groundCollisionPos, groundCheckRadius, buffer, 1 << PhysicsConstants.GroundLayer);
            bool isGrounded = hitCount > 0;
            closestGroundPosition = default;
            if (isGrounded)
            {
                closestGroundPosition = buffer[0].ClosestPoint(groundCollisionPos);
            }
            
            ArrayPool<Collider>.Shared.Return(buffer);
            return isGrounded;
        }

#if UNITY_EDITOR

        void Reset()
        {
            if (model) return;
            model = transform.GetChild(0);
        }

        void OnDrawGizmos()
        {
            if (Application.isPlaying) return;
            Gizmos.color = Color.green.SetA(0.2f);
            Gizmos.DrawSphere(transform.position, groundCheckRadius);
        }

#endif
    }
}
