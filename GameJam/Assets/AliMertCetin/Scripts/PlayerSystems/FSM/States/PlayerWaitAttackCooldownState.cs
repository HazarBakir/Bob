using TheGame.FSM;
using UnityEngine;
using XIV.Core.Utils;

namespace AliMertCetin.Scripts.PlayerSystems.FSM.States
{
    public class PlayerWaitAttackCooldownState : State<PlayerFSM, PlayerStateFactory>
    {
        Timer timer;
        
        public PlayerWaitAttackCooldownState(PlayerFSM stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            var duration = stateMachine.animator.GetCurrentAnimatorStateInfo(0).length;
            timer = new Timer(duration);
        }

        protected override void OnStateUpdate()
        {
            timer.Update(Time.deltaTime);
        }

        protected override void CheckTransitions()
        {
            if (timer.IsDone)
            {
                ChangeStateFromChild(previousState);
                return;
            }
        }
    }
}