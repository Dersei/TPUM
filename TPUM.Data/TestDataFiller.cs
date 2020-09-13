using System;
using TPUM.Data.Interfaces;
using TPUM.Data.Model;

namespace TPUM.Data
{
    public class TestDataFiller : IDataContextFiller
    {
        public DataContext Fill()
        {
            if (IDataContextFiller.WasUsed) return DataContext.Instance;
            IDataContextFiller.WasUsed = true;
            DataContext dc = DataContext.Instance;
            Publisher bioware = new Publisher("Bioware", "CA");
            Publisher dontnod = new Publisher("Dontnod", "FR");
            Publisher arkane = new Publisher("Arkane Studios", "FR");
            Publisher nightSchool = new Publisher("Night School Studio", "USA");
            dc.Publishers.Add(bioware);
            dc.Publishers.Add(dontnod);
            dc.Publishers.Add(arkane);
            dc.Publishers.Add(nightSchool);

            dc.Games.Add(new Game("Life is Strange", dontnod, 10, new DateTime(2015, 1, 30),
                Genre.Adventure));
            dc.Games.Add(new Game("Dragon Age 2", bioware, 10, new DateTime(2011, 3, 8),
                Genre.RPG));
            dc.Games.Add(new Game("Mass Effect 2", bioware, 10, new DateTime(2010, 1, 26),
                Genre.RPG | Genre.TPS));
            dc.Games.Add(new Game("Dragon Age: Inquisition", bioware, 10, new DateTime(2014, 11, 18),
                Genre.RPG));
            dc.Games.Add(new Game("Dishonored", arkane, 8, new DateTime(2012, 10, 9),
                 Genre.Action | Genre.ImmersiveSim));
            dc.Games.Add(new Game("Dishonored 2", arkane, 10, new DateTime(2016, 11, 11),
                 Genre.Action | Genre.ImmersiveSim));
            dc.Games.Add(new Game("Oxenfree", nightSchool, 10, new DateTime(2016, 1, 15),
                Genre.Adventure | Genre.WalkingSim));

            dc.Users.Add(new User("Dersei", "password1234"));
            dc.Users.Add(new User("q", "q"));
            dc.Users.Add(new User("Dealiner", "1234098pass"));
            dc.Users.Add(new User("Shepard", "me123Andromeda"));
            dc.Users.Add(new User("FHawke", "merrill2"));
            dc.Users.Add(new User("SerInq", "bre4ch"));

            return dc;
        }
    }
}