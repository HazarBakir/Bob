using TheGame.FSM;
using UnityEngine;
using XIV.Core.Extensions;

namespace AliMertCetin.Scripts.EnemyAI.States
{
    public class EnemyMoveState : State<EnemyFSM, EnemyStateFactory>
    {
        public EnemyMoveState(EnemyFSM stateMachine, EnemyStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateUpdate()
        {
            var pos = transform.position;
            var playerPos = stateMachine.playerTransform.position.SetY(pos.y);
            
            pos = Vector3.MoveTowards(pos, playerPos, stateMachine.moveSpeed * Time.deltaTime);
            transform.position = pos;
            var forward = transform.forward;
            var lookDir = (playerPos - pos).normalized;
            var axis = Vector3.up;
            var smoothRot = Vector3.MoveTowards(forward, lookDir, stateMachine.rotationSpeed * Time.deltaTime);
            var angle = Vector3.SignedAngle(forward, smoothRot, axis);
            var rot = Quaternion.AngleAxis(angle, axis);
            transform.rotation *= rot;
        }

        protected override void CheckTransitions()
        {
            var pos = transform.position;
            var playerPos = stateMachine.playerTransform.position;
            if ((playerPos - pos).sqrMagnitude < stateMachine.attackRange)
            {
                ChangeStateFromChild(factory.GetState<EnemyAttackState>());
                return;
            }
        }
    }
}