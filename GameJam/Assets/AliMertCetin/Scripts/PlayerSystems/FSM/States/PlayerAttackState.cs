using System.Buffers;
using AliMertCetin.Scripts.InteractionSystem;
using TheGame.FSM;
using UnityEngine;
using XIV.DesignPatterns.Common.HealthSystem;

namespace AliMertCetin.Scripts.PlayerSystems.FSM.States
{
    public class PlayerAttackState : State<PlayerFSM, PlayerStateFactory>
    {
        public PlayerAttackState(PlayerFSM stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateUpdate()
        {
            if (stateMachine.inputReader.isAttackPressed == false) return;
            
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

        protected override void CheckTransitions()
        {
            if (stateMachine.inputReader.isAttackPressed)
            {
                ChangeStateFromChild(factory.GetState<PlayerWaitAttackCooldownState>());
                return;
            }
        }
    }
}