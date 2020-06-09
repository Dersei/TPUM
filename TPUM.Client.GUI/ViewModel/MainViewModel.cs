using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TPUM.Client.GUI.Interfaces;
using TPUM.Client.GUI.View;
using TPUM.Client.GUI.ViewModel.Commands;
using TPUM.Client.Logic;
using TPUM.Communication;
using TPUM.Communication.DTO;

namespace TPUM.Client.GUI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isUserLoggedIn;
        private bool _isOtherUsersSelected;
        public ObservableCollection<GameDTO> Games { get; }
        public ObservableCollection<string> Users { get; }
        public GameDTO? ChosenGame { get; set; }

        private string _textLog = string.Empty;
        private readonly CancellationTokenSource _tokenSource;
        private readonly StringBuilder _sb = new StringBuilder();

        public bool IsUserLoggedIn
        {
            get => _isUserLoggedIn;
            set
            {
                _isUserLoggedIn = value;
                RaisePropertyChanged();
            }
        }

        public bool IsOtherUsersSelected
        {
            get => _isOtherUsersSelected;
            set
            {
                _isOtherUsersSelected = value;
                if (value) GetOtherUsers();
                RaisePropertyChanged();
            }
        }

        public string TextLog
        {
            get => _textLog;
            set
            {
                _textLog = value;
                _textLog += _sb.ToString();
                _sb.Clear();
                RaisePropertyChanged();
            }
        }

        public ICommand DoLogIn { get; }
        public ICommand DoLogOut { get; }
        public CancelEventHandler DoClose { get; }
        public ICommand DoDelete { get; }
        public ICommand DoCreateView { get; }
        public ICommand DoCancelLog { get; }

        private IClientLogic? _clientLogic;

        private void RunDispatcher(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        private void CreateLogic()
        {
            _clientLogic = ClientLogic.Instance;

            _clientLogic.Log += s => Debug.WriteLine(s);
            _clientLogic.OnLoginResponse += (succeeded, token) =>
            {
                if (succeeded)
                {
                    _token = token;
                    MessageBox.Show("Log in successful");
                }
                else
                {
                    RunDispatcher(() => CreateView(typeof(UserLoginWindow)));
                }
            };
            _clientLogic.OnCreateGameResponse += (success, game) =>
            {
                if (success && game != null)
                {
                    RunDispatcher(() => Games.Add(game));
                }
                else
                {
                    MessageBox.Show("Game exists");
                }
            };
            _clientLogic.OnGetOtherUsersResponse += list =>
            {
                if (list is null) return;
                RunDispatcher(() =>
                {
                    foreach (string s in list)
                    {
                        Users.Add(s);
                    }
                });
            };
            _clientLogic.OnGetAllGamesResponse += dtos =>
            {
                if (dtos is null) return;
                RunDispatcher(() =>
                {
                    foreach (GameDTO s in dtos)
                    {
                        Games.Add(s);
                    }
                });
            };
        }



        public MainViewModel()
        {
            _tokenSource = new CancellationTokenSource();
            Games = new ObservableCollection<GameDTO>();
            Users = new ObservableCollection<string>();

            DoLogIn = new RelayCommand(LogIn);
            DoLogOut = new RelayCommand(LogOut);
            DoDelete = new RelayCommand(Delete);
            DoCreateView = new ParameterCommand<Type>(CreateView);
            DoCancelLog = new RelayCommand(CancelLog);
            DoClose = Close;
            CreateLogic();
            Connect();
            CreateView(typeof(UserLoginWindow));
            _clientLogic?.GetAllGames();
        }

        private void Close(object sender, CancelEventArgs e)
        {
            _clientLogic?.Logout(_token);
            foreach (IView view in _views)
            {
                view.Close();
            }
        }

        private void GetOtherUsers()
        {
            _clientLogic?.GetOtherUsers();
        }

        public void Connect()
        {
            Task.Run(() => _clientLogic?.Connect(new Uri("ws://localhost:8081/")));
        }

        private void LogIn() => IsUserLoggedIn = true;

        private void LogOut() => IsUserLoggedIn = false;
        private void Delete() => Games.Remove(ChosenGame!);
        private void CancelLog() => _tokenSource.Cancel();

        private readonly HashSet<IView> _views = new HashSet<IView>();

        private void CreateView(Type view)
        {
            if (!(Activator.CreateInstance(view) is IView iView)) return;
            _views.Add(iView);
            iView.ShowDialog();
            switch (iView.DataContext)
            {
                case GameCreationViewModel { CreatedGame: GameDTO createdGame }:
                    AddGame(createdGame);
                    break;
                case UserLoginViewModel { UserCredentials: UserDTO credentials }:
                    LogIn(credentials);
                    break;
            }
        }

        private SessionToken _token;

        private void LogIn(UserDTO user)
        {
            _clientLogic?.TryLogin(user);
        }

        private void AddGame(GameDTO game)
        {
            if (!Games.Contains(game))
            {
                _clientLogic?.CreateGame(game);
            }
        }
    }
}