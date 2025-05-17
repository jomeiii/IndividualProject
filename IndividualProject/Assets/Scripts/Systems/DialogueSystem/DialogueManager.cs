using System;
using Character.NPC.NPCWithDialogue;
using Character.Player;
using Systems.DialogueSystem.CameraController;
using Systems.DialogueSystem.Nodes;
using Systems.DialogueSystem.Presenter;
using Systems.DialogueSystem.WebClient;
using UnityEngine;

namespace Systems.DialogueSystem
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        public event Action<NPCWithDialogue> NPCMovementStartedEvent;

        [Header("Settings")] [SerializeField] [TextArea]
        private string _historyPromt;

        [Header("References")] [SerializeField]
        private DialogueCameraController _dialogueCameraController;

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private DialoguePresenter _dialoguePresenter;
        [SerializeField] private DialogueButtonController _dialogueButtonController;
        [SerializeField] private DialogueInputFieldController _dialogueInputFieldController;

        [Header("Dialogue value")] [SerializeField]
        private bool _isDialogueRunning;

        [SerializeField] private bool _canDialogue;
        [SerializeField] private NPCWithDialogue _currentNPCWithDialogue;

        private DialogueWebClient _dialogueWebClient;
        private bool _isAIDialogueRunning;
        private DialogueNodeInstance _currentNodeInstance;

        public NPCWithDialogue CurrentNPCWithDialogue => _currentNPCWithDialogue;
        public DialogueButtonController DialogueButtonController => _dialogueButtonController;
        public DialogueNodeInstance CurrentNodeInstance => _currentNodeInstance;

        private InputManager InputManager => InputManager.Instance;

        private void OnEnable()
        {
            _dialogueButtonController.QuestionClickedEvent += OnQuestionClickedButton;
            _dialogueInputFieldController.InputFieldAcceptedEvent += AIAcceptButtonHandler;
            InputManager.InteractionButtonPressedEvent += InteractionButtonHandler;
        }

        private void OnDisable()
        {
            _dialogueButtonController.QuestionClickedEvent -= OnQuestionClickedButton;
            _dialogueInputFieldController.InputFieldAcceptedEvent -= AIAcceptButtonHandler;
            InputManager.InteractionButtonPressedEvent -= InteractionButtonHandler;
        }

        protected override async void Awake()
        {
            base.Awake();

            try
            {
                _dialogueWebClient = new DialogueWebClient();
                await _dialogueWebClient.GetAccessToken();
            }
            catch
            {
                // ignored
            }
        }

        public void StartDialogue(NPCWithDialogue npcWithDialogue)
        {
            if (npcWithDialogue.CanDialogue)
            {
                _currentNPCWithDialogue = npcWithDialogue;
                _currentNodeInstance = new DialogueNodeInstance(npcWithDialogue.DialogueNode);
                _dialogueCameraController.StartAnimCamera(true, _currentNPCWithDialogue, () =>
                    {
                        _dialoguePresenter.SetActiveDialogueUI(true);
                        _dialogueButtonController.UpdateQuestionButtonText(_currentNodeInstance.playerReplies);
                    },
                    () => { }, () => { });
            }
        }

        public void StopDialogue(Action onFinish)
        {
            _isDialogueRunning = false;
            _dialogueCameraController.StartAnimCamera(false, _currentNPCWithDialogue,
                () => { _dialoguePresenter.SetActiveDialogueUI(false); },
                () => { },
                () => { onFinish?.Invoke(); });
        }

        public void TriggerEnterNPCWithDialogue(NPCWithDialogue npcWithDialogue)
        {
            _canDialogue = true;
            _currentNPCWithDialogue = npcWithDialogue;
        }

        public void TriggerExitNPCWithDialogue()
        {
            _canDialogue = false;
            _currentNPCWithDialogue = null;
        }

        private void OnQuestionClickedButton(int indexPlayerReplay)
        {
            if (_currentNPCWithDialogue.DialogueNode.playerReplies[indexPlayerReplay].allowCustomInput ||
                _isAIDialogueRunning)
            {
                _dialogueInputFieldController.SetActiveDialogueInputFieldUI(true);
                _isAIDialogueRunning = true;
                return;
            }

            _currentNodeInstance = new DialogueNodeInstance(
                _currentNodeInstance.playerReplies[indexPlayerReplay].nextNode);

            if (_currentNodeInstance.isEndNode)
            {
                StopDialogue(() =>
                {
                    if (_currentNodeInstance.needMovement)
                    {
                        NPCMovementStartedEvent?.Invoke(_currentNPCWithDialogue);
                        _currentNPCWithDialogue = null;
                    }
                });
            }
            else
            {
                _dialogueButtonController.UpdateQuestionButtonText(_currentNodeInstance.playerReplies);
            }
        }

        private async void AIAcceptButtonHandler(string input)
        {
            string promt = "";
            if (string.IsNullOrEmpty(_currentNPCWithDialogue.DialogueNode.PromtText))
            {
                promt = "Игрок дал ответ на предыдущее сообщение. " +
                        "Ответь ему, учитывая весь контекст диалога, и постарайся сохранить суть исходного промта.";
            }
            else
            {
                promt = _currentNPCWithDialogue.DialogueNode.PromtText + "\n" + _historyPromt;
            }

            var output = await _dialogueWebClient.GetText(input, promt, _currentNPCWithDialogue.npcName);
            _currentNodeInstance.npcText = output;
            _dialogueButtonController.OnUpdatedQuestionEvent();
            _dialogueInputFieldController.SetActiveDialogueInputFieldUI(false);
            _isAIDialogueRunning = false;
        }

        private void InteractionButtonHandler()
        {
            if (_canDialogue && !_isDialogueRunning)
            {
                StartDialogue(_currentNPCWithDialogue);
                _isDialogueRunning = true;
            }
        }
    }
}