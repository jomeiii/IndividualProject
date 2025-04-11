using System;
using UnityEngine;

namespace Systems
{
    public class InputManager : Singleton<InputManager>
    {
        public event Action InteractionButtonPressedEvent;

        [SerializeField] private KeyCode _interactionKeyCode = KeyCode.E;

        private void Update()
        {
            if (Input.GetKeyDown(_interactionKeyCode))
            {
                InteractionButtonHandler();
            }
        }

        public void InteractionButtonHandler()
        {
            Dynamic.DynamicDebug.Debug(nameof(InputManager), nameof(InteractionButtonHandler),
                "Interaction button pressed");
            InteractionButtonPressedEvent?.Invoke();
        }
    }
}