using AliMertCetin.Scripts.EnemyAI.States;
using TheGame.FSM;

namespace AliMertCetin.Scripts.EnemyAI
{
    public class EnemyStateFactory : StateFactory<EnemyFSM>
    {
        public EnemyStateFactory(EnemyFSM stateMachine) : base(stateMachine)
        {
            AddState(new EnemyGroundedState(stateMachine, this)); // parent state
            AddState(new EnemyIdleState(stateMachine, this)); // child state
            AddState(new EnemyMovementState(stateMachine, this)); // child state
            AddState(new EnemyAttackState(stateMachine, this)); // child state
            AddState(new EnemyWaitAttackCooldownState(stateMachine, this)); // child state
            AddState(new EnemyFallingState(stateMachine, this)); // parent state
        }
    }
}