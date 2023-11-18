using AliMertCetin.Scripts.EnemyAI.Extensions;
using TheGame.FSM;
using UnityEngine;
using XIV.Core.Extensions;

namespace AliMertCetin.Scripts.EnemyAI.States
{
    public class EnemyMoveState : State<EnemyFSM, EnemyStateFactory>
    {
        public EnemyMoveState(EnemyFSM stateMachine, EnemyStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            stateMachine.navMeshAgent.enabled = true;
            stateMachine.navMeshAgent.speed = stateMachine.moveSpeed;
            stateMachine.navMeshAgent.stoppingDistance = stateMachine.attackRange;
        }

        protected override void OnStateUpdate()
        {
            var targetPosition = stateMachine.playerTransform.position.SetY(transform.position.y);
            stateMachine.navMeshAgent.SetDestination(targetPosition);
        }

        protected override void CheckTransitions()
        {
            if (stateMachine.navMeshAgent.IsReachedDestination())
            {
                ChangeStateFromChild(factory.GetState<EnemyAttackState>());
                return;
            }
        }
    }
}