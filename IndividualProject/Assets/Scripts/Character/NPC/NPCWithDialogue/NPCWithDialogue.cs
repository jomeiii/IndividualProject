using Character.Player;
using Controllers;
using Systems.DialogueSystem;
using Systems.DialogueSystem.Nodes;
using UnityEngine;

namespace Character.NPC.NPCWithDialogue
{
    public class NPCWithDialogue : NPC
    {
        [Header("Dialogue Settings")]
        [SerializeField] private DialogueNode _dialogueNode;
        
        [Header("References")] [SerializeField]
        private TriggerController _triggerController;

        [SerializeField] private Transform _dialogueCameraPoint;

        public DialogueNode DialogueNode
        {
            get => _dialogueNode;
            set => _dialogueNode = value;
        }
        public Transform DialogueCameraPoint => _dialogueCameraPoint;
        
        private DialogueManager DialogueManager => DialogueManager.Instance;

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
        }

        private void TriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                playerController.TriggerEnterNPCWithDialogue(this);
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