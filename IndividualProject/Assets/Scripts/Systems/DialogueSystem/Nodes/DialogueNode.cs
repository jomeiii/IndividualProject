using System.Collections.Generic;
using UnityEngine;

namespace Systems.DialogueSystem.Nodes
{
    [CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/DialogueNode")]
    public class DialogueNode : ScriptableObject
    {
        [TextArea] public string promtText;
        [TextArea] public string npcText;
        public List<PlayerReply> playerReplies;
        public bool isEndNode;
        public bool needMovement;

        public string PromtText
        {
            get
            {
                if (string.IsNullOrEmpty(promtText))
                {
                    return
                        "Игрок дал ответ на предыдущее сообщение. " +
                        "Ответь ему, учитывая весь контекст диалога, и постарайся сохранить суть исходного промта.";
                }

                return promtText;
            }
        }
    }

    [System.Serializable]
    public class PlayerReply
    {
        public string replyText;
        public DialogueNode nextNode;
        public bool allowCustomInput;
    }
}