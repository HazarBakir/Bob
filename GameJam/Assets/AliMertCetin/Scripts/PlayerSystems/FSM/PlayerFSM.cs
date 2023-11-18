using AliMertCetin.Scripts.PlayerSystems.FSM.States;
using TheGame.FSM;
using UnityEngine;

namespace AliMertCetin.Scripts.PlayerSystems.FSM
{
    public class PlayerFSM : StateMachine
    {
        public Animator animator;
        public Transform ground;
        [Tooltip("Ground check radius")]
        public float radius = 0.1f;
        public float speed = 8f;
        public float jumpHeight = 2f;
        public float gravity = -50f;
        public LayerMask groundMask;
        [HideInInspector] public bool isGrounded;
        [HideInInspector] public float yVelocity;

        float currentSpeed;
        public PlayerInputReader inputReader { get; private set; }
        public CharacterController controller { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            inputReader = gameObject.AddComponent<PlayerInputReader>();
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            controller.skinWidth = 0.1f;
        }

        protected override State GetInitialState()
        {
            return new PlayerStateFactory(this).GetState<PlayerGroundedState>();
        }
    }
}
