using System;
using TPUM.Data;
using TPUM.Data.Model;
using TPUM.Data.Repositories;

namespace TPUM.Logic
{
    public class GamesSystem
    {
        public GamesSystem()
        {
            Repository.Add(new Game("Life is Strange", new Publisher("Dontnod", "F"), 10, new DateTime(2015, 1, 30),
                new[] {Genre.Adventure}));
            Repository.Add(new Game("Dragon Age 2", new Publisher("Bioware", "CA"), 10, new DateTime(2011, 3, 8),
                new[] {Genre.RPG}));
            Repository.Add(new Game("Mass Effect 2", new Publisher("Bioware", "CA"), 10, new DateTime(2010, 1, 26),
                new[] {Genre.RPG, Genre.TPS}));
        }

        public GameRepository Repository { get; } = new GameRepository(new DataContext());
    }
}
