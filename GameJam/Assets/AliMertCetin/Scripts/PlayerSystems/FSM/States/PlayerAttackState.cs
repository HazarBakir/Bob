using System.Buffers;
using TheGame.FSM;
using TheGame.UISystems;
using UnityEngine;
using XIV.DesignPatterns.Common.HealthSystem;

namespace AliMertCetin.Scripts.PlayerSystems.FSM.States
{
    public class PlayerAttackState : State<PlayerFSM, PlayerStateFactory>
    {
        bool attackPressed;
        
        public PlayerAttackState(PlayerFSM stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            attackPressed = false;
            var attackInput = GameInput.Get<GameInput.PlayerAttack>();
            attackInput.Enable();
            attackInput.onAttackPressed += OnAttackPressed;
        }

        protected override void OnStateUpdate()
        {
            if (attackPressed == false) return;
            
            var buffer = ArrayPool<Collider>.Shared.Rent(4);
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position + transform.forward, 0.5f, buffer, 1 << PhysicsConstants.EnemyLayer);

            for (int i = 0; i < hitCount; i++)
            {
                var coll = buffer[i];
                if (coll.TryGetComponent(out IDamageable damageable) && damageable.CanReceiveDamage())
                {
                    damageable.ReceiveDamage(stateMachine.punchDamageAmount);
                }
            }
                
            ArrayPool<Collider>.Shared.Return(buffer);
        }

        protected override void OnStateExit()
        {
            var attackInput = GameInput.Get<GameInput.PlayerAttack>();
            attackInput.Disable();
            attackInput.onAttackPressed -= OnAttackPressed;
        }

        void OnAttackPressed()
        {
            attackPressed = true;
        }

        protected override void CheckTransitions()
        {
            if (attackPressed)
            {
                ChangeStateFromChild(factory.GetState<PlayerWaitAttackCooldownState>());
                return;
            }
        }
    }
}