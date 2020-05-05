using System;
using System.Collections.Generic;
using System.Windows.Input;
using TPUM.Data.Model;
using TPUM.GUI.ViewModel.Commands;

namespace TPUM.GUI.ViewModel
{
    public class GameCreationViewModel : BaseViewModel
    {
        private string? _title;
        private List<Publisher>? _publisher;
        private Publisher? _selectedPublisher;
        private decimal _rating;
        private DateTime _premiere;
        private Genre[]? _genres;

        public ICommand DoCreate { get; }

        public GameCreationViewModel()
        {
            Title = "";
            Publisher = new List<Publisher>(){ new Publisher("Bioware", "CA"), new Publisher("Dontnod", "F") };
            Rating = 0;
            Premiere = DateTime.Now;
            Genres = new Genre[0];
            DoCreate = new RelayCommand(Create);
        }

        public Game? CreatedGame { get; private set; }

        public string Title
        {
            get => _title!;
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public List<Publisher> Publisher
        {
            get => _publisher!;
            set
            {
                _publisher = value;
                RaisePropertyChanged();
            }
        }

        public Publisher SelectedPublisher
        {
            get => _selectedPublisher!;
            set
            {
                _selectedPublisher = value;
                RaisePropertyChanged();
            }
        }

        public decimal Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                RaisePropertyChanged();
            }
        }

        public DateTime Premiere
        {
            get => _premiere;
            set
            {
                _premiere = value;
                RaisePropertyChanged();
            }
        }

        public Genre[] Genres
        {
            get => _genres!;
            set
            {
                _genres = value;
                RaisePropertyChanged();
            }
        }

        private void Create()
        {
            CreatedGame = new Game(Title, SelectedPublisher, Rating, Premiere, Genres);
        }
    }
}