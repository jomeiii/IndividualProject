using System;
using System.Collections;
using UnityEngine;

namespace Systems.DialogueSystem.CameraController
{
    public class DialogueCameraSwitcherPresenter : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private float _trantionTime;

        [Header("References")] [Tooltip("Image for creating a camera transition")] [SerializeField]
        private CanvasGroup _canvasGroup;

        public IEnumerator SwitchCameraAnim(Action canCameraSwitch)
        {
            float f = 0f;
            while (f < _trantionTime)
            {
                f += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(0f, 1f, f);
                yield return null;
            }

            _canvasGroup.alpha = 1f;
            canCameraSwitch?.Invoke();
            f = 0f;
            while (f < _trantionTime)
            {
                f += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(1f, 0f, f);
                yield return null;
            }

            _canvasGroup.alpha = 0f;
        }
    }
}