using AliMertCetin.Scripts.PlayerSystems;
using TheGame.FSM;
using UnityEngine;
using XIV.Core.Utils;
using XIV.DesignPatterns.Common.HealthSystem;

namespace AliMertCetin.Scripts.EnemyAI.States
{
    public class EnemyAttackState : State<EnemyFSM, EnemyStateFactory>
    {
        IDamageable damageable;
        
        public EnemyAttackState(EnemyFSM stateMachine, EnemyStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
        }

        protected override void OnStateUpdate()
        {
            if (damageable == default && stateMachine.playerTransform)
            {
                damageable = stateMachine.playerTransform.GetComponent<IDamageable>();
            }
            var playerTransformPosition = stateMachine.playerTransform.position;
            var distance = Vector3.Distance(transform.position, playerTransformPosition);
            if (distance < stateMachine.attackRange == false) return;

            var dirToPlayer = (playerTransformPosition - transform.position);
            var dot = Vector3.Dot(transform.forward, dirToPlayer);
            if (dot < 0.9f)
            {
                var angle = Vector3.SignedAngle(transform.forward, dirToPlayer, Vector3.up);
                var rot = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), 50f * Time.deltaTime);
                transform.rotation *= rot;
            }
            
            damageable.ReceiveDamage(stateMachine.dealDamageAmount);
        }

        protected override void CheckTransitions()
        {
            var distance = Vector3.Distance(transform.position, stateMachine.playerTransform.position);
            if (distance < stateMachine.attackRange)
            {
                ChangeStateFromChild(factory.GetState<EnemyWaitAttackCooldownState>());
                return;
            }
        }
    }
}