using TheGame.FSM;
using UnityEngine;

namespace AliMertCetin.Scripts.PlayerSystems.FSM.States
{
    public class PlayerGroundedState : State<PlayerFSM, PlayerStateFactory>
    {
        public PlayerGroundedState(PlayerFSM stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            stateMachine.yVelocity = 0f;
        }

        protected override void OnStateUpdate()
        {
            var radius = stateMachine.radius;
            var ground = stateMachine.ground;
            var mask = stateMachine.groundMask;
            stateMachine.isGrounded = Physics.CheckSphere(ground.position, radius, mask);
        }

        protected override void InitializeChildStates()
        {
            AddChildState(factory.GetState<PlayerIdleState>());
        }

        protected override void CheckTransitions()
        {
            if (stateMachine.isGrounded == false)
            {
                // fall state
                ChangeStateFromRoot(factory.GetState<PlayerFallState>());
            }

            if (stateMachine.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                ChangeStateFromRoot(factory.GetState<PlayerJumpState>());
                return;
            }
        }
    }
}