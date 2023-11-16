using System.Buffers;
using UnityEngine;
using XIV.Core;
using XIV.Core.Extensions;

namespace AliMertCetin.Scripts.InteractionSystem
{
    public class InteractionHandler
    {
        readonly Camera cam;
        readonly IInteractor interactor;
        Transform currentInteractable;

        public InteractionHandler(Camera cam, IInteractor interactor)
        {
            this.cam = cam;
            this.interactor = interactor;
        }
        
        public void Update()
        {
            var camTransform = cam.transform;
            var pos = camTransform.position;
            var dir = camTransform.forward;
            var buffer = ArrayPool<RaycastHit>.Shared.Rent(4);
            int layerMask = 1 << PhysicsConstants.InteractableLayer;
            var distance = interactor.GetInteractorSettings().distance;
            int hitCount = Physics.RaycastNonAlloc(pos, dir, buffer, distance + 1f, layerMask);
            int currentIndexOfInteractable = IndexOf(buffer, hitCount, currentInteractable);
            // Interactable is still in range, no need to search
            // Range is increased by 1 meter otherwise it will enter Select/Deselect loop (this happens when object grows when selected)
            if (currentIndexOfInteractable > -1)
            {
#if UNITY_EDITOR
                XIVDebug.DrawLine(pos, pos + (dir * (distance + 1)), Color.red);
#endif
                ArrayPool<RaycastHit>.Shared.Return(buffer);
                return;
            }
            
            // Since previous interactable is not in range, look again for possible interactables
            hitCount = Physics.RaycastNonAlloc(pos, dir, buffer, distance, layerMask);

            // Interactable is not in range, Look for new interactable
            DeselectCurrent();
            var closest = GetClosest(buffer, hitCount, interactor.GetComponent<Transform>().position);
            Select(closest);
            
#if UNITY_EDITOR
            if (currentInteractable) XIVDebug.DrawLine(pos, pos + (dir * distance), Color.green, 0.25f);
#endif
            
            ArrayPool<RaycastHit>.Shared.Return(buffer);
        }

        /// <summary>
        /// Returns true if there is selected interactable object, false otherwise
        /// </summary>
        public bool IsInteractableSelected() => (bool)currentInteractable;

        public IInteractable GetCurrentInteractable()
        {
            if ((bool)currentInteractable == false) return default;
            
            return currentInteractable.GetComponent<IInteractable>();
        }

        void Select(RaycastHit closest)
        {
            // There might be nothing to select
            if (closest.transform)
            {
                closest.transform.GetComponent<IInteractable>().Select();
                Debug.Log(("Selected : " + closest.transform.gameObject.name).ToColorGreen());
            }
            
            currentInteractable = closest.transform;
        }

        void DeselectCurrent()
        {
            if (currentInteractable == false) return;
            currentInteractable.GetComponent<IInteractable>().Deselect();
            Debug.Log(("Deselected : " + currentInteractable.gameObject.name).ToColorRed());
        }

        RaycastHit GetClosest(RaycastHit[] buffer, int hitCount, Vector3 position)
        {
            RaycastHit current = default;
            var distance = float.MaxValue;
            for (int i = 0; i < hitCount; i++)
            {
                var dist = buffer[i].transform.position.sqrMagnitude - position.sqrMagnitude;
                if (dist < distance)
                {
                    distance = dist;
                    current = buffer[i];
                }
            }

            return current;
        }

        int IndexOf(RaycastHit[] buffer, int hitCount, Transform transform)
        {
            for (int i = 0; i < hitCount; i++)
            {
                if (buffer[i].transform == transform) return i;
            }

            return -1;
        }
    }
}