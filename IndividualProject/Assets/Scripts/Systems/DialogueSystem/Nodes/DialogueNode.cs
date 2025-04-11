using System.Collections.Generic;
using UnityEngine;

namespace Systems.DialogueSystem.Nodes
{
    [CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/DialogueNode")]
    public class DialogueNode : ScriptableObject
    {
        [TextArea] public string npcText;
        public List<PlayerReply> playerReplies;
        public bool isEndNode;
    }

    [System.Serializable]
    public class PlayerReply
    {
        public string replyText;
        public DialogueNode nextNode;
        public bool allowCustomInput;
    }
}