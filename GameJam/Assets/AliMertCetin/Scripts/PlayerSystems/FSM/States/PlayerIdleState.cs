using TheGame.FSM;
using TheGame.UISystems;
using UnityEngine;

namespace AliMertCetin.Scripts.PlayerSystems.FSM.States
{
    public class PlayerIdleState : State<PlayerFSM, PlayerStateFactory>
    {
        Vector3 inputNormalized;
        bool isJumpPressed;
        
        public PlayerIdleState(PlayerFSM stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            isJumpPressed = false;
            inputNormalized = Vector3.zero;
            var movementInput = GameInput.Get<GameInput.PlayerMovement>();
            movementInput.Enable();
            movementInput.onAxisPressed += OnAxisPressed;
            movementInput.onJumpPressed += OnJumpPressed;
        }

        protected override void OnStateExit()
        {
            var movementInput = GameInput.Get<GameInput.PlayerMovement>();
            movementInput.Enable();
            movementInput.onAxisPressed -= OnAxisPressed;
            movementInput.onJumpPressed -= OnJumpPressed;
        }

        void OnAxisPressed(Vector3 obj) => inputNormalized = obj;
        void OnJumpPressed() => isJumpPressed = true;

        protected override void CheckTransitions()
        {
            if (inputNormalized.sqrMagnitude > Mathf.Epsilon)
            {
                ChangeStateFromChild(factory.GetState<PlayerMovementState>());
                return;
            }

            if (isJumpPressed)
            {
                ChangeStateFromChild(factory.GetState<PlayerJumpState>());
                return;
            }
        }
    }
}