namespace Systems.DialogueSystem.WebClient.Bodies
{
    [System.Serializable]
    public struct AccessTokenResponseBody
    {
        public string access_token;
        public int expiresAt;
    }
}