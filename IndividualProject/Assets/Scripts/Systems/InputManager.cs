using System;
using UnityEngine;

namespace Systems
{
    public class InputManager : Singleton<InputManager>
    {
        public event Action InteractionButtonPressedEvent;
        public event Action AttackButtonPressedEvent;

        [SerializeField] private KeyCode _interactionKeyCode = KeyCode.E;

        private void Update()
        {
            if (Input.GetKeyDown(_interactionKeyCode)) InteractionButtonHandler();
            if (Input.GetMouseButtonDown(0)) AttackButtonHandler();
        }

        public void AttackButtonHandler()
        {
            Dynamic.DynamicDebug.Debug(nameof(InputManager), nameof(AttackButtonHandler), "Attack button pressed");
            AttackButtonPressedEvent?.Invoke();
        }

        public void InteractionButtonHandler()
        {
            Dynamic.DynamicDebug.Debug(nameof(InputManager), nameof(InteractionButtonHandler),
                "Interaction button pressed");
            InteractionButtonPressedEvent?.Invoke();
        }
    }
}