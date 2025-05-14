using Controllers;
using Systems.DialogueSystem.Nodes;
using UnityEngine;

namespace Character.NPC.NPCWithDialogue.Types
{
    public class NPCWithDialogueStartLocation : NPCWithDialogueMovement
    {
        [SerializeField] private GoldenKeyController _goldenKey;
        [SerializeField] private int _goldenKeyIndex;
        
        [SerializeField] private DialogueNode _dialogueNodeAfterGoldenKey;
        
        protected override void OnEnable()
        {
            base.OnEnable();

            CurrentIndexChangedEvent += OnCurrentIndexChange;
            _goldenKey.GoldenKeyPressedEvent += OnPlayerTriggerEnterGoldenKey;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            CurrentIndexChangedEvent -= OnCurrentIndexChange;
            _goldenKey.GoldenKeyPressedEvent -= OnPlayerTriggerEnterGoldenKey;
        }

        private void OnCurrentIndexChange(int index)
        {
            if (index == _goldenKeyIndex)
            {
                _goldenKey.gameObject.SetActive(true);
            }
        }

        private void OnPlayerTriggerEnterGoldenKey()
        {
            _dialogueNode = _dialogueNodeAfterGoldenKey;
            
            DialogueManager.TriggerEnterNPCWithDialogue(this);
            DialogueManager.StartDialogue(this);
        }
    }
}