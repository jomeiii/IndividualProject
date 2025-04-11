using System;
using System.Collections.Generic;
using Systems.DialogueSystem.Nodes;
using UnityEngine;

namespace Systems.DialogueSystem.Presenter
{
    public class DialogueButtonController : MonoBehaviour
    {
        public event Action<int> QuestionClickedEvent;
        public event Action UpdatedQuestionEvent;

        [Header("Settings")] [SerializeField] private string allowCustomInputText = "Свой выбор...";

        [Header("References")] [SerializeField]
        private List<DialogueButton> _dialogueButtons;

        public void UpdateQuestionButtonText(List<PlayerReplyInstance> playerReplies)
        {
            if (playerReplies == null || playerReplies.Count > _dialogueButtons.Count) return;

            for (int i = 0; i < _dialogueButtons.Count; i++)
            {
                if (i < playerReplies.Count)
                {
                    _dialogueButtons[i].gameObject.SetActive(true);
                    var iLocal = i;
                    _dialogueButtons[i].buttonText.text = playerReplies[i].allowCustomInput
                        ? allowCustomInputText
                        : playerReplies[i].replyText;

                    _dialogueButtons[i].button.onClick.RemoveAllListeners();
                    _dialogueButtons[i].button.onClick.AddListener(() => QuestionButtonHandler(iLocal));
                }
                else
                {
                    _dialogueButtons[i].gameObject.SetActive(false);
                }
            }
            
            UpdatedQuestionEvent?.Invoke();
        }

        public void OnUpdatedQuestionEvent()
        {
            UpdatedQuestionEvent?.Invoke();
        }

        private void QuestionButtonHandler(int index)
        {
            QuestionClickedEvent?.Invoke(index);
        }
    }
}