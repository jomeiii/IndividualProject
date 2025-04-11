using System;
using System.Collections.Generic;

namespace Systems.DialogueSystem.Nodes
{
    [Serializable]
    public class DialogueNodeInstance
    {
        public string npcText;
        public List<PlayerReplyInstance> playerReplies;
        public bool isEndNode;

        public DialogueNodeInstance(DialogueNode node)
        {
            npcText = node.npcText;
            isEndNode = node.isEndNode;
            playerReplies = new List<PlayerReplyInstance>();

            foreach (var reply in node.playerReplies)
            {
                playerReplies.Add(new PlayerReplyInstance(reply));
            }
        }
    }

    [Serializable]
    public class PlayerReplyInstance
    {
        public string replyText;
        public DialogueNode nextNode;
        public bool allowCustomInput;

        public PlayerReplyInstance(PlayerReply original)
        {
            replyText = original.replyText;
            nextNode = original.nextNode;
            allowCustomInput = original.allowCustomInput;
        }
    }
}