using System.Collections.Generic;
using TPUM.Client.Data.Interfaces;
using TPUM.Communication.TransferModel;

namespace TPUM.Client.Data
{
    public class DataContext
    {
        public List<TransferGame> Games { get; }
        public List<TransferPublisher> Publishers { get; }
        public List<TransferUser> Users { get; }

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
            Games = new List<TransferGame>();
            Publishers = new List<TransferPublisher>();
            Users = new List<TransferUser>();
        }
    }
}