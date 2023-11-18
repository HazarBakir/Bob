using AliMertCetin.Scripts.EnemyAI.Extensions;
using TheGame.FSM;
using XIV.Core.Extensions;

namespace AliMertCetin.Scripts.EnemyAI.States
{
    public class EnemyMovementState : State<EnemyFSM, EnemyStateFactory>
    {
        public EnemyMovementState(EnemyFSM stateMachine, EnemyStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            stateMachine.navMeshAgent.enabled = true;
            stateMachine.navMeshAgent.speed = stateMachine.moveSpeed;
            stateMachine.navMeshAgent.stoppingDistance = stateMachine.attackRange;
            stateMachine.animator.SetBool(AnimationConstants.PlayerController.Parameters.PlayerController_IsWalking_BoolID, true);
        }

        protected override void OnStateUpdate()
        {
            var targetPosition = stateMachine.playerTransform.position.SetY(transform.position.y);
            stateMachine.navMeshAgent.SetDestination(targetPosition);
        }

        protected override void OnStateExit()
        {
            stateMachine.animator.SetBool(AnimationConstants.PlayerController.Parameters.PlayerController_IsWalking_BoolID, false);
        }

        protected override void CheckTransitions()
        {
            if (stateMachine.navMeshAgent.IsReachedDestination())
            {
                ChangeStateFromChild(factory.GetState<EnemyIdleState>());
                return;
            }
        }
    }
}