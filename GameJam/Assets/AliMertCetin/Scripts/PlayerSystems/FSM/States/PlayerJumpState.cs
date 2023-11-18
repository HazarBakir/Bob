using TheGame.FSM;
using UnityEngine;

namespace AliMertCetin.Scripts.PlayerSystems.FSM.States
{
    public class PlayerJumpState : State<PlayerFSM, PlayerStateFactory>
    {
        Vector3 movement;
        
        public PlayerJumpState(PlayerFSM stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            stateMachine.animator.SetBool(AnimationConstants.PlayerController.Parameters.PlayerController_IsJumping_BoolID, true);
            stateMachine.yVelocity += Mathf.Sqrt(stateMachine.jumpHeight * -3.0f * stateMachine.gravity);
        }

        protected override void OnStateUpdate()
        {
            ref var yVelocity = ref stateMachine.yVelocity;
            var gravity = stateMachine.gravity;

            yVelocity += gravity * Time.deltaTime;
            movement = Vector3.up * (yVelocity * Time.deltaTime);
            stateMachine.controller.Move(movement);
        }

        protected override void OnStateExit()
        {
            stateMachine.animator.SetBool(AnimationConstants.PlayerController.Parameters.PlayerController_IsJumping_BoolID, false);
        }

        protected override void CheckTransitions()
        {
            if (movement.y < 0f)
            {
                ChangeStateFromRoot(factory.GetState<PlayerFallState>());
                return;
            }
        }

        protected override void InitializeChildStates()
        {
            AddChildState(factory.GetState<PlayerOnAirMovementState>());
        }
    }
}