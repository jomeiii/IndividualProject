using Character.Player;
using Controllers;
using Systems.DialogueSystem.Nodes;
using UnityEngine;

namespace Character.NPC.NPCWithDialogue
{
    public class NPCWithDialogue : NPC
    {
        [Header("Dialogue Settings")] [SerializeField]
        private DialogueNode _dialogueNode;

        [Header("References")] [SerializeField]
        private TriggerController _triggerController;

        [SerializeField] private Transform _dialogueCameraPoint;

        public DialogueNode DialogueNode => _dialogueNode;
        public Transform DialogueCameraPoint => _dialogueCameraPoint;

        protected override void OnEnable()
        {
            base.OnEnable();

            _triggerController.OnTriggerEnterEvent += TriggerEnter;
            _triggerController.OnTriggerExitEvent += TriggerExit;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _triggerController.OnTriggerEnterEvent -= TriggerEnter;
            _triggerController.OnTriggerExitEvent -= TriggerExit;
        }

        protected override void Awake()
        {
            base.Awake();

            _npcInfoPresenter.OnHealthChanged(-1, -1);
        }

        private void TriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                playerController.TriggerEnterNPCWithDialogue(this);
                Debug.Log("flms;glksd;gk");
            }
        }

        private void TriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                playerController.TriggerExitNPCWithDialogue();
            }
        }
    }
}