using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TPUM.Data.Model;
using TPUM.GUI.Interfaces;
using TPUM.GUI.ViewModel.Commands;
using TPUM.Logic;

namespace TPUM.GUI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isUserLoggedIn;
        public ObservableCollection<Game> Games { get; set; }
        public Game? ChosenGame { get; set; }

        private readonly GamesSystem _gamesSystem;

        public bool IsUserLoggedIn
        {
            get => _isUserLoggedIn;
            set
            {
                _isUserLoggedIn = value;
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
            Games = new ObservableCollection<Game>(_gamesSystem.Repository.GetAll());

            DoLogIn = new RelayCommand(LogIn);
            DoLogOut = new RelayCommand(LogOut);
            DoDelete = new RelayCommand(Delete);
            DoCreateView = new ParameterCommand<IView>(CreateView);
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

        private void AddGame(Game game)
        {
            if (!Games.Contains(game))
            {
                Games.Add(game);
            }
        }
    }
}