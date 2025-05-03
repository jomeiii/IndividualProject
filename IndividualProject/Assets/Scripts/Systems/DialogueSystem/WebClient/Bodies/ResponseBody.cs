using System.Collections.Generic;

namespace Systems.DialogueSystem.WebClient.Bodies
{
    [System.Serializable]
    public class ResponseBody
    {
        public List<Choice> choices;
        public long created;
        public string model;
        public string @object;
        public Usage usage;
    }

    [System.Serializable]
    public class Choice
    {
        public Message message;
        public int index;
        public string finish_reason;
    }

    [System.Serializable]
    public class Usage
    {
        public int prompt_tokens;
        public int completion_tokens;
        public int total_tokens;
        public int precached_prompt_tokens;
    }
}