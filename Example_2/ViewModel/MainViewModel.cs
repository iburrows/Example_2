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
        private ObservableCollection<string> chatMessages;

        public Server theServer;
        private bool isConnected = false;

        private const int port = 8055;
        private const string ip = "127.0.0.1";

        public string Name { get; set; }

        public string SelectedUser { get; set; }

        public ObservableCollection<string> ChatMessages
        {
            get { return chatMessages; }
            set { chatMessages = value;
                RaisePropertyChanged(); }
        }

        public MainViewModel()
        {
            StartBtnClicked = new RelayCommand(()=> { StartServer(); },()=> { return !isConnected; });
            //UsersList = new ObservableCollection<UserVM>();
            UsersList = new ObservableCollection<string>();
            ChatMessages = new ObservableCollection<string>();
            //DemoData();
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
                if (message.Contains(":"))
                {
                    string[] newUser = message.Split(':');
                    UsersList.Add(newUser[1]);
                }
                else
                {
                    ChatMessages.Add(message);
                }
                
                //UsersList.Add(new UserVM(message, new ChatHistoryVM(message)));
            });
            
        }
    }
}