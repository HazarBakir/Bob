using TheGame.FSM;
using UnityEngine;

namespace AliMertCetin.Scripts.EnemyAI.States
{
    public class EnemyFallingState : State<EnemyFSM, EnemyStateFactory>
    {
        public EnemyFallingState(EnemyFSM stateMachine, EnemyStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            stateMachine.navMeshAgent.enabled = false;
        }

        protected override void OnStateUpdate()
        {
            var gravity = stateMachine.gravity;
            transform.position += gravity * Time.deltaTime;
        }

        protected override void CheckTransitions()
        {
            if (stateMachine.IsGrounded(out _))
            {
                ChangeStateFromRoot(factory.GetState<EnemyGroundedState>());
                return;
            }
        }
    }
}