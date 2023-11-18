using TheGame.FSM;
using UnityEngine;

namespace AliMertCetin.Scripts.PlayerSystems.FSM.States
{
    public class PlayerFallState : State<PlayerFSM, PlayerStateFactory>
    {
        Vector3 movement;
        
        public PlayerFallState(PlayerFSM stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            // Play Fall animation
            // stateMachine.animator.SetBool(AnimationConstants.PlayerController.Parameters.PlayerController_IsJumping_BoolID, true);
            // stateMachine.yVelocity += Mathf.Sqrt(stateMachine.jumpHeight * -3.0f * stateMachine.gravity);
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
            // Stop Fall animation
            // stateMachine.animator.SetBool(AnimationConstants.PlayerController.Parameters.PlayerController_IsJumping_BoolID, false);
        }

        protected override void CheckTransitions()
        {
            if (CheckIsGrounded())
            {
                ChangeStateFromRoot(factory.GetState<PlayerGroundedState>());
                return;
            }
        }

        protected override void InitializeChildStates()
        {
            AddChildState(factory.GetState<PlayerOnAirMovementState>());
        }

        bool CheckIsGrounded()
        {
            var radius = stateMachine.radius;
            var ground = stateMachine.ground;
            var mask = stateMachine.groundMask;
            return Physics.CheckSphere(ground.position, radius, mask);
        }
    }
}