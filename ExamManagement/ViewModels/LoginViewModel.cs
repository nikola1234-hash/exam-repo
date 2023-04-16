using EasyTestMaker.Constants;
using EasyTestMaker.Models;
using EasyTestMaker.PubSub;
using EasyTestMaker.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace EasyTestMaker.ViewModels
{
    public class LoginViewModel : ViewModelBase, IDisposable
    {

        private string _userName;

        public string Username
        {
            get { return _userName; }
            set => SetProperty(ref _userName, value);
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set => SetProperty(ref _password, value);
        }

        private string _infoMessage;

        public string InfoMessage
        {
            get { return _infoMessage; }
            set=> SetProperty(ref _infoMessage, value);
        }


        public ICommand Login { get; private set;}
        public ICommand Loaded { get; private set;}

        private bool _isVIsible;

        public bool IsVisible
        {
            get { return _isVIsible; }
            set => SetProperty(ref _isVIsible, value);
        }
        private bool _isInfoVisible;

        public bool IsInfoVisible
        {
            get { return _isInfoVisible; }
            set=> SetProperty(ref _isInfoVisible, value);
        }
        private IAuthService _auth => App.GetService<IAuthService>();
        private IEventAggregator @event = App.GetService<IEventAggregator>();
        public LoginViewModel()
        {
            InitializeCommands();
            InitializeModel();

        }

        private void InitializeCommands()
        {
            Login = new DelegateCommand(OnLogin);
            Loaded = new DelegateCommand(OnLoad);
        }

        private void OnLoad()
        {
            
        }

        private void InitializeModel()
        {
            IsVisible = true;
        }

        private async void OnLogin()
        {
            User user = new User(Username, Password);
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                MessageBox.Show("You have to populate username and password");
                return;
            }
            IsVisible = false;
            IsInfoVisible = true;
            InfoMessage = "Please wait contacting server...";

            bool success = await _auth.Login(user);
            if (success)
            {
                InfoMessage = "Login success";
                Thread.Sleep(1000);
             
            }
            else
            {
                IsVisible =true;
                IsInfoVisible = false;
                MessageBox.Show("Wrong username or password");
            }
        }
        private void ChangeView()
        {
            @event.GetEvent<ViewChangeEvent>().Publish(this);
        }
        public void Dispose()
        {
            
        }
    }
}
