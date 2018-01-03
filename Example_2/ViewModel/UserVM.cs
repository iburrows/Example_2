using System.Collections.ObjectModel;

namespace Example_2.ViewModel
{
    public class UserVM
    {
        public string name;
        private ObservableCollection<ChatHistoryVM> usersMessages;

        public ObservableCollection<ChatHistoryVM> UsersMessages
        {
            get { return usersMessages; }
            set { usersMessages = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public UserVM(string name, ChatHistoryVM item)
        {
            usersMessages = new ObservableCollection<ChatHistoryVM>();
            this.Name = name;
            usersMessages.Add(item);
        }

        public UserVM(string name)
        {
            
            this.Name = name;
            
        }

    }
}