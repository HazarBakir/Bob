using AliMertCetin.Scripts.PlayerSystems;
using TheGame.FSM;
using UnityEngine;
using XIV.Core.Utils;
using XIV.Core.XIVMath;

namespace AliMertCetin.Scripts.EnemyAI.States
{
    public class EnemyWaitAttackCooldownState : State<EnemyFSM, EnemyStateFactory>
    {
        Timer timer;

        public EnemyWaitAttackCooldownState(EnemyFSM stateMachine, EnemyStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            var animationName = PickAnimation(out int id);
            var duration = stateMachine.animator.GetClipLength(animationName);
            stateMachine.animator.Play(id);
            timer = new Timer(duration);
        }

        protected override void OnStateFixedUpdate()
        {
            var t = Mathf.PingPong(XIVMathf.Remap(timer.NormalizedTime, 0f, 1f, 0f, 2f), 1f);
            t = EasingFunction.EaseOutExpo(t);
            stateMachine.animator.SetLayerWeight(1, t);
        }

        string PickAnimation(out int id)
        {
            var rnd = Random.value;
            id = rnd > 0.5f ? 
                AnimationConstants.PlayerController.Clips.PlayerController_PunchingLeftHash :
                AnimationConstants.PlayerController.Clips.PlayerController_PunchingRightHash;
            return rnd > 0.5f ?
                AnimationConstants.PlayerController.Clips.PlayerController_PunchingLeft :
                AnimationConstants.PlayerController.Clips.PlayerController_PunchingRight;
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