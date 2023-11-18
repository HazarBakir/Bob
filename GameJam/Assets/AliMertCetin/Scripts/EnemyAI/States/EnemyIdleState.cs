using TheGame.FSM;
using UnityEngine;

namespace AliMertCetin.Scripts.EnemyAI.States
{
    public class EnemyIdleState : State<EnemyFSM, EnemyStateFactory>
    {
        public EnemyIdleState(EnemyFSM stateMachine, EnemyStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }
        
        protected override void CheckTransitions()
        {
            if (Vector3.Distance(transform.position, stateMachine.playerTransform.position) > stateMachine.attackRange)
            {
                ChangeStateFromChild(factory.GetState<EnemyMovementState>());
                return;
            }
        }
    }
}