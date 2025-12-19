using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.DialogueSystem
{
    public class DialogueInputFieldController : MonoBehaviour
    {
        public event Action<string> InputFieldAcceptedEvent;
        
        [Header("References")]
        [SerializeField] private GameObject _dialogueInputFieldUI;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _acceptButton;

        private void OnEnable()
        {
            _acceptButton.onClick.AddListener(AcceptButtonHandler);
        }

        private void OnDisable()
        {
            _acceptButton.onClick.RemoveListener(AcceptButtonHandler);
        }

        public void SetActiveDialogueInputFieldUI(bool active)
        {
            _dialogueInputFieldUI.SetActive(active);
        }

        private void AcceptButtonHandler()
        {
            InputFieldAcceptedEvent?.Invoke(_inputField.text);
            _inputField.text = "";
        }
    }
}