using AliMertCetin.Scripts.EnemyAI.States;
using TheGame.FSM;

namespace AliMertCetin.Scripts.EnemyAI
{
    public class EnemyStateFactory : StateFactory<EnemyFSM>
    {
        public EnemyStateFactory(EnemyFSM stateMachine) : base(stateMachine)
        {
            AddState(new EnemyGroundedState(stateMachine, this)); // parent state
            AddState(new EnemyMoveState(stateMachine, this)); // child state
            AddState(new EnemyAttackState(stateMachine, this)); // child state
            AddState(new EnemySearchGunState(stateMachine, this)); // child state
            AddState(new EnemyWanderState(stateMachine, this)); // child state
            AddState(new EnemyFallingState(stateMachine, this)); // parent state
        }
    }
}