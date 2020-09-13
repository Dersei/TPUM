using System;
using System.Collections.Generic;
using System.Windows.Input;
using TPUM.Client.GUI.ViewModel.Commands;
using TPUM.Client.Logic;
using TPUM.Communication.DTO;

namespace TPUM.Client.GUI.ViewModel
{
    public class GameCreationViewModel : BaseViewModel
    {
        private string? _title;
        private List<PublisherDTO>? _publisher;
        private PublisherDTO? _selectedPublisher;
        private decimal _rating;
        private DateTime _premiere;
        private Genre[]? _genres;

        public ICommand DoCreate { get; }

        public async void GetPublishers()
        {
            await ClientLogic.Instance.GetAllPublishers();
        }

        public GameCreationViewModel()
        {
            Title = "";
            Publisher = new List<PublisherDTO>();
            Rating = 0;
            Premiere = DateTime.Now;
            Genres = new Genre[0];
            DoCreate = new RelayCommand(Create);

            ClientLogic.Instance.OnGetAllPublishersResponse += dtos =>
            {
                if (dtos is null) return;
                Publisher = dtos;
            };
            GetPublishers();
        }

        public GameDTO? CreatedGame { get; private set; }

        public string Title
        {
            get => _title!;
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public List<PublisherDTO> Publisher
        {
            get => _publisher!;
            set
            {
                _publisher = value;
                RaisePropertyChanged();
            }
        }

        public PublisherDTO SelectedPublisher
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
            CreatedGame = new GameDTO(Title, SelectedPublisher, Rating, Premiere, Genres);
        }
    }
}