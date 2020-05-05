using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TPUM.Data.Model;
using TPUM.GUI.Interfaces;
using TPUM.GUI.ViewModel.Commands;
using TPUM.Logic;
using TPUM.Logic.DTO;

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
                RaisePropertyChanged();
            }
        }

        public ICommand DoLogIn { get; }
        public ICommand DoLogOut { get; }
        public ICommand DoDelete { get; }
        public ICommand DoCreateView { get; }

        public MainViewModel()
        {
            _gamesSystem = new GamesSystem();
            _usersSystem = new UsersSystem();
            Games = new ObservableCollection<GameDTO>(_gamesSystem.GetAllGames());
            Users = new ObservableCollection<UserDTO>(_usersSystem.GetAllUsers());

            DoLogIn = new RelayCommand(LogIn);
            DoLogOut = new RelayCommand(LogOut);
            DoDelete = new RelayCommand(Delete);
            DoCreateView = new ParameterCommand<IView>(CreateView);

            Work();
        }

        private async void Work()
        {
            await foreach (string s in _usersSystem.Simulate())
            {
                TextLog += s;
            }
        }

        private void LogIn() => IsUserLoggedIn = true;
        private void LogOut() => IsUserLoggedIn = false;
        private void Delete() => Games.Remove(ChosenGame!);

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