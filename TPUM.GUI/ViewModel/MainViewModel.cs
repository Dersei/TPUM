using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TPUM.Client.Logic;
using TPUM.Communication.DTO;
using TPUM.GUI.Interfaces;
using TPUM.GUI.View;
using TPUM.GUI.ViewModel.Commands;
using TPUM.Logic;
using TPUM.Logic.Systems;

namespace TPUM.GUI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isUserLoggedIn;
        public ObservableCollection<GameDTO> Games { get; }
        public ObservableCollection<UserDTO> Users { get; }
        public GameDTO? ChosenGame { get; set; }

        private readonly GamesSystem _gamesSystem;
        private readonly UsersSystem _usersSystem;
        private string _textLog = string.Empty;
        private readonly CancellationTokenSource _tokenSource;
        private readonly StringLogSender _userSender;
        private readonly StringLogSender _gameSender;
        private readonly UserLogger _userLogger;
        private readonly GameLogger _gameLogger;
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
            _clientLogic = new ClientLogic()
            {
                Log = s => Debug.WriteLine(s),
                OnLoginResponse = (succeeded) =>
                {
                    if (succeeded)
                    {
                        Debug.WriteLine("Success");
                        MessageBox.Show("Log in succesful");
                    }
                    else
                    {
                        Debug.WriteLine("Fail");
                        RunDispatcher(() => CreateView(new UserLoginWindow()));
                    }
                },
               OnCreateGameResponse = (success, game) =>
               {
                   if (success)
                   {
                       RunDispatcher(() => Games.Add(game));
                   }
                   {
                       Debug.WriteLine("Game exists");
                   }
               }
            };
        }


        public MainViewModel()
        {
            _tokenSource = new CancellationTokenSource();
            _gamesSystem = new GamesSystem();
            _usersSystem = new UsersSystem();
            Games = new ObservableCollection<GameDTO>(_gamesSystem.GetAllGames());
            Users = new ObservableCollection<UserDTO>(_usersSystem.GetAllUsers());

            _userSender = new StringLogSender(_usersSystem, TimeSpan.FromSeconds(10));
            _gameSender = new StringLogSender(_gamesSystem, TimeSpan.FromSeconds(10));
            Task.Run(() => _userSender.SendReport());
            Task.Run(() => _gameSender.SendReport());
            _userLogger = new UserLogger();
            _gameLogger = new GameLogger(_sb);
            _userLogger.Subscribe(_userSender);
            _gameLogger.Subscribe(_gameSender);

            DoLogIn = new RelayCommand(LogIn);
            DoLogOut = new RelayCommand(LogOut);
            DoDelete = new RelayCommand(Delete);
            DoCreateView = new ParameterCommand<IView>(CreateView);
            DoCancelLog = new RelayCommand(CancelLog);
            CreateLogic();
            Work();
            Connect();
            CreateView(new UserLoginWindow());
        }

        public void Connect()
        {
            Task.Run(() => _clientLogic?.Connect());
        }

       
        private async void Work()
        {
            PeriodicTask<string> task = new PeriodicTask<string>(TimeSpan.FromSeconds(5));
            await foreach (string s in task.Start((() => "Logging..." + Environment.NewLine), _tokenSource.Token))
            {
                TextLog += s;
            }
        }

        private void LogIn() => IsUserLoggedIn = true;
        private void LogOut() => IsUserLoggedIn = false;
        private void Delete() => Games.Remove(ChosenGame!);
        private void CancelLog() => _tokenSource.Cancel();

        private void CreateView(IView view)
        {
            IView? iView = Activator.CreateInstance(view.GetType()) as IView;
            iView?.ShowDialog();
            switch (iView?.DataContext)
            {
                case GameCreationViewModel { CreatedGame: GameDTO createdGame }:
                    AddGame(createdGame);
                    break;
                case UserLoginViewModel { UserCredentials: UserDTO credentials }:
                    LogIn(credentials);
                    break;
            }
        }

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