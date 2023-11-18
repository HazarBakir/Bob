using TheGame.FSM;
using UnityEngine;
using XIV.Core.Utils;
using XIV.DesignPatterns.Common.HealthSystem;

namespace AliMertCetin.Scripts.EnemyAI.States
{
    public class EnemyAttackState : State<EnemyFSM, EnemyStateFactory>
    {
        IDamageable damageablePlayer;
        Timer cooldownTimer;
        const float ERROR = 0.25f;
        
        public EnemyAttackState(EnemyFSM stateMachine, EnemyStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            stateMachine.navMeshAgent.enabled = false;
            damageablePlayer = stateMachine.playerTransform.GetComponent<IDamageable>();
            cooldownTimer = new Timer(stateMachine.attackCooldown);
        }

        protected override void OnStateUpdate()
        {
            if (cooldownTimer.Update(Time.deltaTime) == false) return;
            cooldownTimer.Restart();
            damageablePlayer.ReceiveDamage(stateMachine.dealDamageAmount);
        }

        protected override void CheckTransitions()
        {
            var pos = transform.position;
            var playerPos = stateMachine.playerTransform.position;
            var distance = Vector3.Distance(playerPos, pos);
            if (distance > stateMachine.attackRange + ERROR)
            {
                ChangeStateFromChild(factory.GetState<EnemyMoveState>());
                return;
            }
        }
    }
}