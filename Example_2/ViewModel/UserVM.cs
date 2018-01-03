using System.Collections.ObjectModel;

namespace Example_2.ViewModel
{
    public class UserVM
    {

        public string Name { get; set; }
        public ObservableCollection<ChatHistoryVM> UsersMessages{get;set;}

        public UserVM(string name)
        {
            this.Name = name;
        }

        public void AddMsg(ChatHistoryVM msg)
        {
            if (UsersMessages == null)
            {
                UsersMessages = new ObservableCollection<ChatHistoryVM>();
            }

            UsersMessages.Add(msg);
        }

    }
}