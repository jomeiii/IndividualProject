using Character.NPC.NPCWithDialogue;
using StarterAssets;
using Systems;
using Systems.DialogueSystem;
using UnityEngine;

namespace Character.Player
{
    public class PlayerController : Character
    {
        [Header("Settings")] [SerializeField] private Transform _spawnPosition;

        [Header("References")] [SerializeField]
        private ThirdPersonController _thirdPersonController;

        [SerializeField] private GameObject _visuals;

        public ThirdPersonController ThirdPersonController => _thirdPersonController;

        private InputManager InputManager => InputManager.Instance;
        private DialogueManager DialogueManager => DialogueManager.Instance;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        public void DialogueStart()
        {
            _thirdPersonController.CanMove = false;
            _visuals.SetActive(false);
        }

        public void DialogueEnd()
        {
            _thirdPersonController.CanMove = true;
            _visuals.SetActive(true);
        }

        public void TriggerEnterNPCWithDialogue(NPCWithDialogue npcWithDialogue)
        {
            DialogueManager.TriggerEnterNPCWithDialogue(npcWithDialogue);
        }

        public void TriggerExitNPCWithDialogue()
        {
            DialogueManager.TriggerExitNPCWithDialogue();
        }

        protected override void Die()
        {
            _thirdPersonController.Teleport(_spawnPosition.position);
        }
    }
}