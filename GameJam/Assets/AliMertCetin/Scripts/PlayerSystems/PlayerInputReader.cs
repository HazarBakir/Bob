using UnityEngine;

namespace AliMertCetin.Scripts.PlayerSystems
{
    public class PlayerInputReader : MonoBehaviour
    {
        public Vector3 inputNormalized { get; private set; }
        public bool IsJumpPressed { get; private set; }
        public bool IsRunPressed { get; private set; }
        
        const string HORIZONTAL_AXIS = "Horizontal";
        const string VERTICAL_AXIS = "Vertical";
        
        void Update()
        {
            var horizontal = Input.GetAxisRaw(HORIZONTAL_AXIS);
            var vertical = Input.GetAxisRaw(VERTICAL_AXIS);
            inputNormalized = new Vector3(horizontal, 0f, vertical);
            IsJumpPressed = Input.GetKeyDown(KeyCode.Space);
            IsRunPressed = Input.GetKey(KeyCode.LeftShift);
        }
    }
}