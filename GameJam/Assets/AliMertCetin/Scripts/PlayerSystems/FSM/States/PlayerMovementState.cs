using TheGame.FSM;
using UnityEngine;

namespace AliMertCetin.Scripts.PlayerSystems.FSM.States
{
    public class PlayerMovementState : State<PlayerFSM, PlayerStateFactory>
    {
        public PlayerMovementState(PlayerFSM stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            stateMachine.animator.SetBool(AnimationConstants.PlayerController.Parameters.PlayerController_IsWalking_BoolID, true);
        }

        protected override void OnStateUpdate()
        {
            float speed = stateMachine.speed;
            float horizontal = stateMachine.inputReader.inputNormalized.x;
            float vertical = stateMachine.inputReader.inputNormalized.z;

            Vector3 direction = (transform.right * horizontal + transform.forward * vertical).normalized;
            var inputMovement = direction * (speed * Time.deltaTime);
            stateMachine.controller.Move(inputMovement);
        }

        protected override void OnStateExit()
        {
            stateMachine.animator.SetBool(AnimationConstants.PlayerController.Parameters.PlayerController_IsWalking_BoolID, false);
        }

        protected override void CheckTransitions()
        {
            if (stateMachine.inputReader.inputNormalized.sqrMagnitude < Mathf.Epsilon)
            {
                ChangeStateFromChild(factory.GetState<PlayerIdleState>());
                return;
            }
        }
    }
}