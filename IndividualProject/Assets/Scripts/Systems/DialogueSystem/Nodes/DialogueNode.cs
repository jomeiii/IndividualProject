using System.Collections.Generic;
using UnityEngine;

namespace Systems.DialogueSystem.Nodes
{
    [CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/DialogueNode")]
    public class DialogueNode : ScriptableObject
    {
        [TextArea(3, 25)] public string promtText;
        [TextArea(3, 10)] public string npcText;
        public List<PlayerReply> playerReplies;
        public bool isEndNode;
        public bool needMovement;

        public string PromtText => promtText;
    }

    [System.Serializable]
    public class PlayerReply
    {
        public string replyText;
        public DialogueNode nextNode;
        public bool allowCustomInput;
    }
}