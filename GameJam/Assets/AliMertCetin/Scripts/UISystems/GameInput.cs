using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TheGame.UISystems
{
    public static class GameInput
    {
        public interface IInput
        {
            bool isEnabled { get; }
            event Action onEnabled;
            event Action onDisabled;
            void Update();
            void Enable();
            void Disable();
        }

        public abstract class InputBase : IInput
        {
            public bool isEnabled { get; private set; }
            public event Action onEnabled = delegate {  };
            public event Action onDisabled = delegate {  };
            
            public abstract void Update();

            public void Enable()
            {
                if (isEnabled) return;
                isEnabled = true;
                onEnabled.Invoke();
            }

            public void Disable()
            {
                if (isEnabled == false) return;
                isEnabled = false;
                onDisabled.Invoke();
            }
        }
        
        public class UI : InputBase
        {
            public event Action onPausePerformed = delegate {  };

            public override void Update()
            {
                if (IsPressedThisFrame(KeyCode.Escape)) onPausePerformed.Invoke();
            }
        }

        public class PlayerMovement : InputBase
        {
            public event Action<Vector3> onAxisPressed = delegate {  };
            public event Action onJumpPressed = delegate {  };
            public event Action onRunPressed = delegate {  };
            
            const string HORIZONTAL = "Horizontal";
            const string VERTICAL = "Vertical";

            public override void Update()
            {
                var horizontal = Input.GetAxisRaw(HORIZONTAL);
                var vertical = Input.GetAxisRaw(VERTICAL);
                var inputVec = new Vector3(horizontal, 0f, vertical).normalized;
                if (inputVec.sqrMagnitude > Mathf.Epsilon) onAxisPressed.Invoke(inputVec);
                if (IsPressedThisFrame(KeyCode.Space)) onJumpPressed.Invoke();
                if (IsPressing(KeyCode.LeftShift)) onRunPressed.Invoke();
            }
        }

        public class PlayerAttack : InputBase
        {
            public event Action onAttackPressed = delegate {  };
            
            public override void Update()
            {
                if (IsPressedThisFrame(KeyCode.Mouse0)) onAttackPressed.Invoke();
            }
        }

        class GameInputHelper : MonoBehaviour
        {
            void Update()
            {
                if (GameInput.isEnabled == false) return;

                int count = inputs.Count;
                for (int i = 0; i < count; i++)
                {
                    var input = inputs[i];
                    if (input.isEnabled == false) continue;
                    input.Update();
                }
            }
        }
        
        public static bool isEnabled { get; private set; }
        public static event Action onDisabled;
        public static event Action onEnabled;
        
        static List<IInput> inputs;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            var go = new GameObject("--- GAME INPUT HELPER ---");
            go.AddComponent<GameInputHelper>();
            Object.DontDestroyOnLoad(go);
            isEnabled = false;
            onDisabled = delegate { };
            onEnabled = delegate { };

            inputs = new List<IInput>
            {
                new UI(),
                new PlayerMovement(),
                new PlayerAttack(),
            };
        }

        public static T Get<T>() where T : IInput
        {
            int count = inputs.Count;
            for (int i = 0; i < count; i++)
            {
                if (inputs[i] is T t) return t;
            }

            return default;
        }

        /// <summary>
        /// Enables the input updates.
        /// </summary>
        public static void Enable()
        {
            if (isEnabled) return;
            isEnabled = true;
            onEnabled.Invoke();
        }
        
        /// <summary>
        /// Disables the input updates.
        /// </summary>
        public static void Disable()
        {
            if (isEnabled == false) return;
            isEnabled = false;
            onDisabled.Invoke();
        }
        
        /// <summary>
        /// Disables all individual inputs.
        /// </summary>
        public static void DisableAll()
        {
            Disable();
            int inputsCount = inputs.Count;
            for (var i = 0; i < inputsCount; i++)
            {
                inputs[i].Disable();
            }
        }

        public static bool IsPressedThisFrame(KeyCode key)
        {
            return Input.GetKeyDown(key) || Input.GetKeyUp(key);
        }

        public static bool IsPressing(KeyCode key)
        {
            return Input.GetKey(key) || Input.GetKeyUp(key);
        }
    }
}