using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TPUM.GUI.Interfaces;
using TPUM.GUI.ViewModel.Commands;
using TPUM.Logic;
using TPUM.Logic.DTO;
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
            Work();
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
            if (iView?.DataContext is GameCreationViewModel gcvm && gcvm?.CreatedGame != null)
            {
                AddGame(gcvm.CreatedGame);
            }
        }

        private void AddGame(GameDTO game)
        {
            if (!Games.Contains(game))
            {
                Games.Add(game);
                _gamesSystem.AddGame(game);
            }
        }
    }
}