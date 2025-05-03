using System.Collections.Generic;

namespace Systems.DialogueSystem.WebClient.Bodies
{
    public class RequestBody
    {
        public string model;
        public List<Message> messages;
        public int n;
        public bool stream;
        public int max_tokens;
        public int repetition_penalty;
        public int update_interval;

        public RequestBody(string model, List<Message> messages, int n, bool stream, int maxTokens,
            int repetitionPenalty, int updateInterval)
        {
            this.model = model;
            this.messages = messages;
            this.n = n;
            this.stream = stream;
            this.max_tokens = maxTokens;
            this.repetition_penalty = repetitionPenalty;
            this.update_interval = updateInterval;
        }
    }
}