namespace Base.Mailing.Content
{
    public class SimpleMessageContent : IMessageContent
    {
        public string Content { get; private set; }

        public SimpleMessageContent(string content)
        {
            Content = content;
        }
    }
}
