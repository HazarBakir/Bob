using System;
using System.Buffers;
using AliMertCetin.Scripts.EnemyAI.States;
using AliMertCetin.Scripts.PlayerSystems;
using TheGame.FSM;
using UnityEngine;
using UnityEngine.AI;
using XIV.Core.Extensions;
using XIV.Core.TweenSystem;
using XIV.Core.Utils;
using XIV.DesignPatterns.Common.HealthSystem;
using XIV.Packages.ScriptableObjects.Channels;

namespace AliMertCetin.Scripts.EnemyAI
{
    public class EnemyFSM : StateMachine, IObserver<HealthChange>
    {
        [SerializeField] TransformChannelSO onPlayerLoadedChannel;
        [SerializeField] public float rotationSpeed = 50f;
        [field: SerializeField] public float groundCheckRadius { get; private set; } = 0.25f;
        [field: SerializeField] public float moveSpeed { get; private set; } = 20f;
        [field: SerializeField] public float attackCooldown { get; private set; } = 3f;
        [field: SerializeField] public float dealDamageAmount { get; private set; } = 5f;
        [field: SerializeField] public float attackRange { get; private set; } = 1.5f;
        [field: SerializeField] public float interactableRange { get; private set; } = 1.5f;
        [field: SerializeField] public float fieldOfViewAngle { get; private set; } = 5f;
        [field: SerializeField] public float fieldOfViewDistance { get; private set; } = 8f;
        [field: SerializeField] public Vector3 gravity { get; private set; } = Physics.gravity;
        
        public NavMeshAgent navMeshAgent { get; private set; }
        public Transform playerTransform { get; private set; }
        IDisposable unsubscribeContract;
        public Animator animator { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void OnEnable()
        {
            onPlayerLoadedChannel.Register(OnPlayerLoaded);
            unsubscribeContract?.Dispose();
            var damageable = GetComponentInChildren<DamageableComponent>() as IDamageable;
            unsubscribeContract = damageable.Subscribe(this);
        }

        void OnDisable()
        {
            onPlayerLoadedChannel.Unregister(OnPlayerLoaded);
            unsubscribeContract?.Dispose();
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
        void IObserver<HealthChange>.OnCompleted() { }

        void IObserver<HealthChange>.OnError(Exception error) { }

        void IObserver<HealthChange>.OnNext(HealthChange value)
        {
            transform.CancelTween();
            if (value.healthDataAfter.isDepleted)
            {
                transform.XIVTween()
                    .Scale(Vector3.one, Vector3.zero, 0.75f, EasingFunction.EaseInOutBounce)
                    .OnComplete(() =>
                    {
                        Destroy(this.gameObject);
                    })
                    .Start();
            }
            else
            {
                transform.XIVTween()
                    .Scale(Vector3.one, Vector3.one * 0.75f, 0.5f, EasingFunction.EaseOutExpo, true)
                    .Start();
            }
        }

#if UNITY_EDITOR

        void OnDrawGizmos()
        {
            if (Application.isPlaying) return;
            Gizmos.color = Color.green.SetA(0.2f);
            Gizmos.DrawSphere(transform.position, groundCheckRadius);
        }

#endif
    }
}
