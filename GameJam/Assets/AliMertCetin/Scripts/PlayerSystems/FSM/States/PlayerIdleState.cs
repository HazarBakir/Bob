using TheGame.FSM;
using UnityEngine;

namespace AliMertCetin.Scripts.PlayerSystems.FSM.States
{
    public class PlayerIdleState : State<PlayerFSM, PlayerStateFactory>
    {
        public PlayerIdleState(PlayerFSM stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void CheckTransitions()
        {
            if (stateMachine.isGrounded == false)
            {
                // fall state
            }

            if (stateMachine.inputReader.inputNormalized.sqrMagnitude > Mathf.Epsilon)
            {
                ChangeStateFromChild(factory.GetState<PlayerMovementState>());
                return;
            }

            if (stateMachine.inputReader.IsJumpPressed)
            {
                ChangeStateFromChild(factory.GetState<PlayerJumpState>());
                return;
            }
        }
    }
}