using AliMertCetin.Scripts.EnemyAI.Extensions;
using TheGame.FSM;
using UnityEngine;
using XIV.Core.Extensions;
using XIV.Core.Utils;

namespace AliMertCetin.Scripts.EnemyAI.States
{
    public class EnemyWanderState : State<EnemyFSM, EnemyStateFactory>
    {
        const float DISTANCE = 16f;
        Timer wonderDuration;
        
        public EnemyWanderState(EnemyFSM stateMachine, EnemyStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            wonderDuration = new Timer(2f);
        }

        protected override void OnStateUpdate()
        {
            wonderDuration.Update(Time.deltaTime);
            if (stateMachine.navMeshAgent.IsReachedDestination())
            {
                var playerPosition = stateMachine.playerTransform.position;
                var pos = playerPosition + Random.insideUnitSphere.SetY(0f) * DISTANCE;
                stateMachine.navMeshAgent.SetDestination(pos);
            }
        }

        protected override void CheckTransitions()
        {
            if (Vector3.Distance(stateMachine.playerTransform.position, transform.position) < stateMachine.attackRange)
            {
                ChangeStateFromChild(factory.GetState<EnemyAttackState>());
                return;
            }
            
            if (wonderDuration.IsDone)
            {
                ChangeStateFromChild(previousState);
                return;
            }
        }

        // bool TryFindTarget(out Gun closestGun)
        // {
        //     const int BUFFER_LENGTH = 8;
        //     var colliderBuffer = ArrayPool<Collider>.Shared.Rent(BUFFER_LENGTH);
        //     var gunBuffer = ArrayPool<Gun>.Shared.Rent(BUFFER_LENGTH);
        //
        //     var position = transform.position;
        //     var forward = transform.forward;
        //     var fovAngle = stateMachine.fieldOfViewAngle;
        //     var fovDistance = stateMachine.fieldOfViewDistance;
        //     var fovData = new FieldOfViewData(position, forward, fovAngle, fovDistance);
        //     int targetLayerMask = 1 << PhysicsConstants.InteractableLayer;
        //     int obstacleLayerMask = ~targetLayerMask;
        //     int hitCount = FOVHelper.GetTargetsInsideFOVNonAlloc(colliderBuffer, fovData, targetLayerMask, obstacleLayerMask);
        //     hitCount = Filter(colliderBuffer, hitCount, gunBuffer);
        //     closestGun = gunBuffer.GetClosest(position, hitCount);
        //     
        //     ArrayPool<Collider>.Shared.Return(colliderBuffer);
        //     ArrayPool<Gun>.Shared.Return(gunBuffer);
        //     return closestGun;
        // }
        //
        // int Filter(Collider[] arr, int length, Gun[] bufferToWrite)
        // {
        //     int bufferToWriteLength = bufferToWrite.Length;
        //     int count = 0;
        //     for (int i = 0; i < length && i < bufferToWriteLength; i++)
        //     {
        //         var collider = arr[i];
        //         var gun = collider.GetComponent<Gun>();
        //         if (gun && gun.HasOwner() == false)
        //         {
        //             bufferToWrite[count++] = gun;
        //         }
        //     }
        //
        //     return count;
        // }
    }
}