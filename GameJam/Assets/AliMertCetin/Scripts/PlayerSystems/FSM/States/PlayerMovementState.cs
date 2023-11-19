using TheGame.FSM;
using TheGame.UISystems;
using UnityEngine;

namespace AliMertCetin.Scripts.PlayerSystems.FSM.States
{
    public class PlayerMovementState : State<PlayerFSM, PlayerStateFactory>
    {
        bool isRunPressed;
        Vector3 inputNormalized;
        
        public PlayerMovementState(PlayerFSM stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            stateMachine.animator.SetBool(AnimationConstants.PlayerController.Parameters.PlayerController_IsWalking_BoolID, true);
            var movementInput = GameInput.Get<GameInput.PlayerMovement>();
            movementInput.Enable();
            movementInput.onAxisPressed += OnAxisPressed;
            movementInput.onRunPressed += OnRunPressed;
        }

        protected override void OnStateUpdate()
        {
            float speed = isRunPressed ? stateMachine.runSpeed : stateMachine.speed;

            var normalizedSpeed = speed / stateMachine.runSpeed;
            stateMachine.animator.SetFloat(AnimationConstants.PlayerController.Parameters.PlayerController_MovementSpeed01_Float, normalizedSpeed);

            Vector3 direction = (transform.right * inputNormalized.x + transform.forward * inputNormalized.z).normalized;
            var inputMovement = direction * (speed * Time.deltaTime);
            stateMachine.controller.Move(inputMovement);
        }

        protected override void OnStateExit()
        {
            stateMachine.animator.SetBool(AnimationConstants.PlayerController.Parameters.PlayerController_IsWalking_BoolID, false);
            var movementInput = GameInput.Get<GameInput.PlayerMovement>();
            movementInput.Disable();
            movementInput.onAxisPressed -= OnAxisPressed;
            movementInput.onRunPressed -= OnRunPressed;
        }

        void OnAxisPressed(Vector3 obj) => inputNormalized = obj;

        void OnRunPressed() => isRunPressed = true;

        protected override void CheckTransitions()
        {
            if (inputNormalized.sqrMagnitude < Mathf.Epsilon)
            {
                ChangeStateFromChild(factory.GetState<PlayerIdleState>());
                return;
            }
        }
    }
}