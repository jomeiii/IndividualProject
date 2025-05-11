using System;
using Character.NPC.NPCWithDialogue;
using Character.Player;
using Managers;
using UnityEngine;

namespace Systems.DialogueSystem.CameraController
{
    public class DialogueCameraController : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private Vector3 _cameraOffset;

        [Header("Cameras")] [SerializeField] private Camera _mainCamera;
        [SerializeField] private Camera _dialogueCamera;

        [Header("References")] [SerializeField]
        private DialogueCameraSwitcherPresenter _dialogueCameraSwitcherPresenter;

        [SerializeField] PlayerController _playerController;

        private Transform _cameraTransformTemp;

        private void Awake()
        {
            SwitchToMainCamera();
        }

        /// <summary>
        /// Switch camera with animation
        /// </summary>
        /// <param name="isDialogueCamera">If is dialogue camera - true or if is main camera - false</param>
        public void StartAnimCamera(bool state, NPCWithDialogue npcWithDialogue, Action onCameraSwitch, Action onFinish, Action onCameraSwitchFinish)
        {
            Action action = () => { onCameraSwitch?.Invoke(); };
            if (state)
                action += () =>
                {
                    _cameraTransformTemp = npcWithDialogue.CameraTransform;
                    npcWithDialogue.CameraTransform = npcWithDialogue.DialogueCameraPoint;
                    npcWithDialogue.NeedInvert = false;
                    SwitchToDialogueCamera(npcWithDialogue.DialogueCameraPoint);
                    _playerController.DialogueStart();
                    CursorManager.EnableCursor();
                };
            else
                action += () =>
                {
                    npcWithDialogue.CameraTransform = _cameraTransformTemp;
                    npcWithDialogue.NeedInvert = true;
                    SwitchToMainCamera();
                    _playerController.DialogueEnd();
                    CursorManager.DisableCursor();
                };

            StartCoroutine(_dialogueCameraSwitcherPresenter.SwitchCameraAnim(action, onCameraSwitchFinish));

            onFinish?.Invoke();
        }

        private void SwitchToDialogueCamera(Transform dialogueCameraPoint)
        {
            if (_mainCamera != null) _mainCamera.enabled = false;
            if (_dialogueCamera != null)
            {
                _dialogueCamera.enabled = true;
                _dialogueCamera.transform.position = dialogueCameraPoint.position + _cameraOffset;
            }
        }

        private void SwitchToMainCamera()
        {
            if (_mainCamera != null) _mainCamera.enabled = true;
            if (_dialogueCamera != null) _dialogueCamera.enabled = false;
        }
    }
}