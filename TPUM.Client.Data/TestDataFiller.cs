using System;
using TPUM.Client.Data.Interfaces;
using TPUM.Communication.TransferModel;

namespace TPUM.Client.Data
{
    public class TestDataFiller : IDataContextFiller
    {
        public DataContext Fill()
        {
            if(IDataContextFiller.WasUsed) return DataContext.Instance;
            IDataContextFiller.WasUsed = true;
            DataContext dc = DataContext.Instance;
            TransferPublisher bioware = new TransferPublisher("Bioware", "CA");
            TransferPublisher dontnod = new TransferPublisher("Dontnod", "FR");
            TransferPublisher arkane = new TransferPublisher("Arkane Studios", "FR");
            TransferPublisher nightSchool = new TransferPublisher("Night School Studio", "USA");
            dc.Publishers.Add(bioware);
            dc.Publishers.Add(dontnod);
            dc.Publishers.Add(arkane);
            dc.Publishers.Add(nightSchool);

            dc.Games.Add(new TransferGame("Life is Strange", dontnod, 10, new DateTime(2015, 1, 30),
                new[] {Genre.Adventure}));
            dc.Games.Add(new TransferGame("Dragon Age 2", bioware, 10, new DateTime(2011, 3, 8),
                new[] {Genre.RPG}));
            dc.Games.Add(new TransferGame("Mass Effect 2", bioware, 10, new DateTime(2010, 1, 26),
                new[] {Genre.RPG, Genre.TPS}));
            dc.Games.Add(new TransferGame("Dragon Age: Inquisition", bioware, 10, new DateTime(2014, 11, 18),
                new[] {Genre.RPG}));
            dc.Games.Add(new TransferGame("Dishonored", arkane, 8, new DateTime(2012, 10, 9),
                new[] {Genre.Action, Genre.ImmersiveSim}));
            dc.Games.Add(new TransferGame("Dishonored 2", arkane, 10, new DateTime(2016, 11, 11),
                new[] {Genre.Action, Genre.ImmersiveSim}));
            dc.Games.Add(new TransferGame("Oxenfree", nightSchool, 10, new DateTime(2016, 1, 15),
                new[] {Genre.Adventure, Genre.WalkingSim}));
            
            dc.Users.Add(new TransferUser(Guid.NewGuid(), "Dersei", "password1234"));
            dc.Users.Add(new TransferUser(Guid.NewGuid(), "q", "q"));
            dc.Users.Add(new TransferUser(Guid.NewGuid(), "Dealiner", "1234098pass"));
            dc.Users.Add(new TransferUser(Guid.NewGuid(), "Shepard", "me123Andromeda"));
            dc.Users.Add(new TransferUser(Guid.NewGuid(), "FHawke", "merrill2"));
            dc.Users.Add(new TransferUser(Guid.NewGuid(), "SerInq", "bre4ch"));

            return dc;
        }
    }
}