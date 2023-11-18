using TheGame.FSM;

namespace AliMertCetin.Scripts.EnemyAI.States
{
    public class EnemyGroundedState : State<EnemyFSM, EnemyStateFactory>
    {
        public EnemyGroundedState(EnemyFSM stateMachine, EnemyStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            if (stateMachine.IsGrounded(out var closestGroundPosition))
            {
                transform.position = closestGroundPosition;
            }
        }

        protected override void InitializeChildStates()
        {
            base.AddChildState(factory.GetState<EnemyIdleState>());
            base.AddChildState(factory.GetState<EnemyAttackState>());
        }

        protected override void CheckTransitions()
        {
            if (stateMachine.IsGrounded(out _) == false)
            {
                ChangeStateFromRoot(factory.GetState<EnemyFallingState>());
                return;
            }
        }
    }
}