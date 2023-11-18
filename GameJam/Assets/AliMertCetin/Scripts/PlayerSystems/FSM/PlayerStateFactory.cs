using AliMertCetin.Scripts.PlayerSystems.FSM.States;
using TheGame.FSM;

namespace AliMertCetin.Scripts.PlayerSystems.FSM
{
    public class PlayerStateFactory : StateFactory<PlayerFSM>
    {
        public PlayerStateFactory(PlayerFSM stateMachine) : base(stateMachine)
        {
            AddState(new PlayerGroundedState(base.stateMachine, this));
            AddState(new PlayerIdleState(base.stateMachine, this));
            AddState(new PlayerMovementState(base.stateMachine, this));
            AddState(new PlayerAttackState(base.stateMachine, this));
            AddState(new PlayerWaitAttackCooldownState(base.stateMachine, this));
            AddState(new PlayerOnAirMovementState(base.stateMachine, this));
            AddState(new PlayerJumpState(base.stateMachine, this));
            AddState(new PlayerFallState(base.stateMachine, this));
        }
    }
}