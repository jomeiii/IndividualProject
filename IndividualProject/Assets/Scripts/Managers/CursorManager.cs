using System;
using UnityEngine;

namespace Managers
{
    public class CursorManager : MonoBehaviour
    {
        public static event Action CursorEnabledEvent;
        public static event Action CursorDisableEvent;
        
        private static bool CursorEnebled;

        private void Awake()
        {
            DisableCursor();
        }

        public static void EnableCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            CursorEnebled = true;
            CursorEnabledEvent?.Invoke();
        }

        public static void DisableCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            CursorEnebled = false;
            CursorDisableEvent?.Invoke();
        }
    }
}