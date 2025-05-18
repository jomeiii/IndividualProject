using System;
using Character.NPC.NPCWithDialogue;
using Character.Player;
using Managers;
using UnityEngine;

namespace Systems.DialogueSystem.CameraController
{
    public class DialogueCameraController : MonoBehaviour
    {
        private readonly string MainCameraTag = "MainCamera";
        private readonly string CameraTag = "Camera";

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
        public void StartAnimCamera(bool state, NPCWithDialogue npcWithDialogue, Action onCameraSwitch, Action onFinish,
            Action onCameraSwitchFinish)
        {
            Action action = () => { onCameraSwitch?.Invoke(); };
            if (state)
                action += () =>
                {
                    _cameraTransformTemp = npcWithDialogue.CameraTransform;
                    npcWithDialogue.CameraTransform = npcWithDialogue.DialogueCameraPoint;
                    // npcWithDialogue.NeedInvert = false;
                    SwitchToDialogueCamera(npcWithDialogue.DialogueCameraPoint,
                        DialogueManager.Instance.CurrentNPCWithDialogue.transform);
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

        private void SwitchToDialogueCamera(Transform dialogueCameraPoint, Transform lookAtCameraPoint)
        {
            if (_mainCamera != null)
            {
                _mainCamera.enabled = false;
                _mainCamera.tag = CameraTag;
            }

            if (_dialogueCamera != null)
            {
                _dialogueCamera.enabled = true;
                _dialogueCamera.transform.localPosition = dialogueCameraPoint.position;
                _dialogueCamera.transform.LookAt(DialogueManager.Instance.CurrentNPCWithDialogue.transform);
                var angle = Quaternion.Euler(0f, _dialogueCamera.transform.eulerAngles.y, 0f);
                _dialogueCamera.transform.rotation = angle;
                _dialogueCamera.tag = MainCameraTag;
            }
        }

        private void SwitchToMainCamera()
        {
            if (_mainCamera != null)
            {
                _mainCamera.enabled = true;
                _mainCamera.tag = MainCameraTag;
            }

            if (_dialogueCamera != null)
            {
                _dialogueCamera.enabled = false;
                _dialogueCamera.tag = CameraTag;
            }
        }
    }
}