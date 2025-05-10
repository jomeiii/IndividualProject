using Systems.DialogueSystem;
using UnityEngine;

namespace Character.NPC.NPCWithDialogue
{
    public class NPCWithDialogueMovement : NPCWithDialogue
    {
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private int _currentIndex;

        private DialogueManager DialogueManager => DialogueManager.Instance;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            DialogueManager.NPCMovementStartedEvent += OnNPCMovementStart;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            DialogueManager.NPCMovementStartedEvent -= OnNPCMovementStart;
        }

        private void OnNPCMovementStart(NPCWithDialogue npcWithDialogue)
        {
            if (npcWithDialogue != this) return;
            
            Debug.Log("NPCWithDialogueMovement.OnNPCMovementStart");
        }
    }
}