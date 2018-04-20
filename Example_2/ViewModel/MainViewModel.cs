using Example_2.TheServer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Example_2.ViewModel
{

    public class MainViewModel : ViewModelBase
    {

        public RelayCommand StartBtnClicked { get; set; }
        //public ObservableCollection<UserVM> UsersList { get; set; }
        public ObservableCollection<string> UsersList { get; set; }
        //private ObservableCollection<string> chatMessages;
        private ObservableCollection<ChatHistoryVM> chatMessages;

        public ObservableCollection<ChatHistoryVM> SelectedUsersMessages { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }

        public Server theServer;
        private bool isConnected = false;

        private const int port = 8055;
        private const string ip = "127.0.0.1";

        public string Name { get; set; }
        private string selectedUser = "";
        public string SelectedUser {
            get {return selectedUser; }
            set {
                selectedUser = value;
                RaisePropertyChanged();
                GetMessages(selectedUser);
            }
        }

        private void GetMessages(string selectedUser)
        {
            if (SelectedUsersMessages.Count > 0)
            {
                SelectedUsersMessages.Clear();
            }

            foreach (var item in chatMessages)
            {
                string[] Message = item.Message.Split(':');

                string UserName = Message[0].Trim();

                if (UserName.Contains(selectedUser.Trim()))
                {
                    SelectedUsersMessages.Add(item);
                }
            }
        }

        //public ObservableCollection<string> ChatMessages
        //{
        //    get { return chatMessages; }
        //    set { chatMessages = value;
        //        RaisePropertyChanged(); }
        //}

        public ObservableCollection<ChatHistoryVM> ChatMessages
        {
            get { return chatMessages; }
            set
            {
                chatMessages = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            SelectedUsersMessages = new ObservableCollection<ChatHistoryVM>();
            StartBtnClicked = new RelayCommand(()=> { StartServer(); },()=> { return !isConnected; });
            //UsersList = new ObservableCollection<UserVM>();
            UsersList = new ObservableCollection<string>();
            //ChatMessages = new ObservableCollection<string>();
            ChatMessages = new ObservableCollection<ChatHistoryVM>();
            
        }


        private void StartServer()
        {
            theServer = new Server(ip, port, NewMessagesReceived);
            isConnected = true;
            //theServer.StartReceiving();
        }

        private void NewMessagesReceived(string message)
        {
            App.Current.Dispatcher.Invoke(() => 
            {
                if (message.Contains("New User"))
                {
                    string[] newUser = message.Split(':');
                    UsersList.Add(newUser[1]);
                }
                else
                {
                    ChatMessages.Add(new ChatHistoryVM(message, DateTime.Now));
                }
                
                //UsersList.Add(new UserVM(message, new ChatHistoryVM(message)));
            });
            
        }
    }
}