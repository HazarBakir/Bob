using TheGame.FSM;
using UnityEngine;
using XIV.DesignPatterns.Common.HealthSystem;

namespace AliMertCetin.Scripts.PlayerSystems.FSM.States
{
    public class PlayerAttackState : State<PlayerFSM, PlayerStateFactory>
    {
        IDamageable target;
        bool didAttacked;
        
        public PlayerAttackState(PlayerFSM stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateUpdate()
        {
            didAttacked = Input.GetMouseButtonDown(0);
            if (didAttacked == false) return;
            // int punchAnimation = Random.value > 0.5f ? AnimationConstants.RightPunch : AnimationConstants.LeftPunch;
            
        }

        protected override void CheckTransitions()
        {
            if (didAttacked)
            {
                ChangeStateFromChild(factory.GetState<PlayerWaitAttackCooldownState>());
                return;
            }
        }
    }
}