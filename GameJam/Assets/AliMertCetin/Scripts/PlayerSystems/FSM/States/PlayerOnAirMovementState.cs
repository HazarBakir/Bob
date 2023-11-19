using TheGame.FSM;
using TheGame.UISystems;
using UnityEngine;

namespace AliMertCetin.Scripts.PlayerSystems.FSM.States
{
    public class PlayerOnAirMovementState : State<PlayerFSM, PlayerStateFactory>
    {
        Vector3 inputNormalized;
        
        public PlayerOnAirMovementState(PlayerFSM stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            var moveInput = GameInput.Get<GameInput.PlayerMovement>();
            moveInput.onAxisPressed += OnAxisPressed;
        }

        protected override void OnStateUpdate()
        {
            Vector3 direction = (transform.right * inputNormalized.x + transform.forward * inputNormalized.z).normalized;
            var inputMovement = direction * (stateMachine.speed * Time.deltaTime);
            stateMachine.controller.Move(inputMovement);
        }

        void OnAxisPressed(Vector3 obj) => inputNormalized = obj;

        protected override void OnStateExit()
        {
            var moveInput = GameInput.Get<GameInput.PlayerMovement>();
            moveInput.onAxisPressed -= OnAxisPressed;
        }
    }
}