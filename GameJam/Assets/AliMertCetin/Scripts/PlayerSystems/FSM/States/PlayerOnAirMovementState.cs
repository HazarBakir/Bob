using TheGame.FSM;
using UnityEngine;

namespace AliMertCetin.Scripts.PlayerSystems.FSM.States
{
    public class PlayerOnAirMovementState : State<PlayerFSM, PlayerStateFactory>
    {
        public PlayerOnAirMovementState(PlayerFSM stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateUpdate()
        {
            ref var speed = ref stateMachine.speed;
            float horizontal = stateMachine.inputReader.inputNormalized.x;
            float vertical = stateMachine.inputReader.inputNormalized.z;

            Vector3 direction = (transform.right * horizontal + transform.forward * vertical).normalized;
            var inputMovement = direction * (speed * Time.deltaTime);
            stateMachine.controller.Move(inputMovement);
        }
    }
}