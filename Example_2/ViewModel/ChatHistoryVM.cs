using System;

namespace Example_2.ViewModel
{
    public class ChatHistoryVM
    {
        public string Message { get; set; }
        public string Time { get; set; }

        public ChatHistoryVM(string message, DateTime time)
        {
            this.Message = message;
            Time = time.ToShortTimeString();
        }
    }
}