using System;
using System.Collections.ObjectModel;
using TPUM.Data.Model;

namespace TPUM.GUI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Game> Games { get; set; }

        public MainViewModel()
        {
            Games = new ObservableCollection<Game>
            {
                new Game("Life is Strange", new Publisher("Dontnod", "F"), 10, new DateTime(2015, 1, 30), new[] {Genre.Adventure}),
                new Game("Dragon Age 2", new Publisher("Bioware", "CA"), 10, new DateTime(2011, 3, 8), new[] {Genre.RPG}),
                new Game("Mass Effect 2", new Publisher("Bioware", "CA"), 10, new DateTime(2010, 1, 26), new[] {Genre.RPG, Genre.TPS})
            };
        }
    }
}