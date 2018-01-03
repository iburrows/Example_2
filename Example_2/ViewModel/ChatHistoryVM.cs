namespace Example_2.ViewModel
{
    public class ChatHistoryVM
    {
        public string Message { get; set; }

        public ChatHistoryVM(string message)
        {
            this.Message = message;
        }
    }
}