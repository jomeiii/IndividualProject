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

        [SerializeField] protected bool _canDialogue = true;

        [Header("References")] [SerializeField]
        private TriggerController _triggerController;

        [SerializeField] private Transform _dialogueCameraPoint;

        public bool CanDialogue => _canDialogue;
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
        }
        
        protected override void Start()
        {
            base.Start();
        
            _npcInfoPresenter.OnHealthChanged(-1, -1);
        }

        protected virtual void TriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                playerController.TriggerEnterNPCWithDialogue(this);
            }
        }

        protected virtual void TriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                playerController.TriggerExitNPCWithDialogue();
            }
        }
    }
}