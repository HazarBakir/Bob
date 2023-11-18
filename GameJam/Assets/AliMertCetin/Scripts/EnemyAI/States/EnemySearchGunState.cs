using System.Buffers;
using TheGame.FSM;
using UnityEngine;
using XIV.Core.Extensions;
using XIV.DesignPatterns.Common.FOV;

namespace AliMertCetin.Scripts.EnemyAI.States
{
    public class EnemySearchGunState : State<EnemyFSM, EnemyStateFactory>
    {
        const int BUFFER_LENGTH = 16;
        Gun gunToReach;
        bool isStuck;
        
        public EnemySearchGunState(EnemyFSM stateMachine, EnemyStateFactory stateFactory) : base(stateMachine, stateFactory)
        {
        }

        protected override void OnStateEnter(State comingFrom)
        {
            stateMachine.navMeshAgent.enabled = true;
            stateMachine.navMeshAgent.stoppingDistance = stateMachine.interactableRange;
            stateMachine.navMeshAgent.SetDestination(transform.position);
            isStuck = false;
        }

        protected override void OnStateUpdate()
        {
            HandleTargetSelection();
            int obstacleLayerMask = ~(1 << PhysicsConstants.InteractableLayer);
            if (gunToReach == false || 
                FOVHelper.ValidateTargetPosition(gunToReach.transform.position, GetFovData(transform.position), obstacleLayerMask) == false)
            {
                isStuck = true;
                return;
            }

            var gunPosition = gunToReach.transform.position;
            stateMachine.navMeshAgent.SetDestination(gunPosition);

            var distance = Vector3.Distance(transform.position, gunPosition);
            if (distance < stateMachine.interactableRange)
            {
                stateMachine.gunUser.SetGun(gunToReach);
            }
        }

        protected override void CheckTransitions()
        {
            if (stateMachine.gunUser.HasGun())
            {
                ChangeStateFromChild(factory.GetState<EnemyMoveState>());
                return;
            }

            if (isStuck)
            {
                ChangeStateFromChild(factory.GetState<EnemyWanderState>());
                return;
            }
        }

        void HandleTargetSelection()
        {
            if (TryFindTarget(out var closestGun) == false) return;
            
            if (gunToReach == closestGun) return;

            if (gunToReach == false)
            {
                gunToReach = closestGun;
                return;
            }

            // Compare them if we have target already
            var position = transform.position;
            var distanceToCurrentTarget = Vector3.Distance(gunToReach.transform.position, position);
            var distanceToClosest = Vector3.Distance(closestGun.transform.position, position);
            if (distanceToClosest < distanceToCurrentTarget) gunToReach = closestGun;
        }

        bool TryFindTarget(out Gun closestGun)
        {
            var colliderBuffer = ArrayPool<Collider>.Shared.Rent(BUFFER_LENGTH);
            var gunBuffer = ArrayPool<Gun>.Shared.Rent(BUFFER_LENGTH);

            var position = transform.position;
            var fovData = GetFovData(position);
            int targetLayerMask = 1 << PhysicsConstants.InteractableLayer;
            int obstacleLayerMask = ~targetLayerMask;
            int hitCount = FOVHelper.GetTargetsInsideFOVNonAlloc(colliderBuffer, fovData, targetLayerMask, obstacleLayerMask);
            hitCount = Filter(colliderBuffer, hitCount, gunBuffer);
            closestGun = gunBuffer.GetClosest(position, hitCount);
            
            ArrayPool<Collider>.Shared.Return(colliderBuffer);
            ArrayPool<Gun>.Shared.Return(gunBuffer);
            return closestGun;
        }

        FieldOfViewData GetFovData(Vector3 position)
        {
            var forward = transform.forward;
            var fovAngle = stateMachine.fieldOfViewAngle;
            var fovDistance = stateMachine.fieldOfViewDistance;
            return new FieldOfViewData(position, forward, fovAngle, fovDistance);
        }

        int Filter(Collider[] arr, int length, Gun[] bufferToWrite)
        {
            int bufferToWriteLength = bufferToWrite.Length;
            int count = 0;
            for (int i = 0; i < length && i < bufferToWriteLength; i++)
            {
                var collider = arr[i];
                var gun = collider.GetComponent<Gun>();
                if (gun && gun.HasOwner() == false)
                {
                    bufferToWrite[count++] = gun;
                }
            }

            return count;
        }
    }
}