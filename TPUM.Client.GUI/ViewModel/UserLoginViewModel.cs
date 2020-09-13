using System;
using System.Windows.Input;
using TPUM.Client.GUI.ViewModel.Commands;
using TPUM.Client.Logic.DTO;

namespace TPUM.Client.GUI.ViewModel
{
    internal class UserLoginViewModel : BaseViewModel
    {
        private string? _username;
        private string? _password;
        private string? _info;

        public ICommand DoLogIn { get; }

        public UserLoginViewModel()
        {
            DoLogIn = new RelayCommand(LogIn);
        }

        public UserDTO? UserCredentials { get; private set; }

        public string Username
        {
            get => _username ?? string.Empty;
            set
            {
                _username = value;
                RaisePropertyChanged();
            }
        }
        public string Password
        {
            get => _password ?? string.Empty;
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        public string Info
        {
            get => _info ?? string.Empty;
            set
            {
                _info = value;
                RaisePropertyChanged();
            }
        }

        private void LogIn()
        {
            UserCredentials = new UserDTO(Guid.Empty, Username, Password);
        }

    }
}
