using System.Collections.Generic;
using TPUM.Data.Interfaces;
using TPUM.Data.Model;

namespace TPUM.Data
{
    public class DataContext
    {
        public List<Game> Games { get; }
        public List<Publisher> Publishers { get; }
        public List<User> Users { get; }

        private static DataContext? _instance;
        
        private static readonly object InstanceLock = new object();
        
        public static DataContext Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    _instance ??= new DataContext();
                }
                return _instance;
            }
        }

        public DataContext FillData(IDataContextFiller filler) => _instance = filler.Fill();
        
        private DataContext()
        {
            Games = new List<Game>();
            Publishers = new List<Publisher>();
            Users = new List<User>();
        }
    }
}