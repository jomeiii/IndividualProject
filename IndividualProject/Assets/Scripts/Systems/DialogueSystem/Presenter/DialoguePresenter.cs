using TMPro;
using UnityEngine;

namespace Systems.DialogueSystem.Presenter
{
    public class DialoguePresenter : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private GameObject _dialogueUI;

        [SerializeField] private TextMeshProUGUI _outputText;

        private DialogueManager DialogueManager => DialogueManager.Instance;
        private DialogueButtonController DialogueButtonController => DialogueManager.DialogueButtonController;

        private void OnEnable()
        {
            DialogueButtonController.UpdatedQuestionEvent += OnQuestionButtonClick;
        }

        private void OnDisable()
        {
            DialogueButtonController.UpdatedQuestionEvent -= OnQuestionButtonClick;
        }

        public void SetActiveDialogueUI(bool active)
        {
            _dialogueUI.SetActive(active);
        }

        private void OnQuestionButtonClick()
        {
            _outputText.text = DialogueManager.Instance.CurrentNodeInstance.npcText;
        }
    }
}