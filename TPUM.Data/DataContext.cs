using System.Collections.Generic;
using TPUM.Data.Model;

namespace TPUM.Data
{
    public class DataContext
    {
        public List<Game> Games { get; set; }
        public List<Publisher> Publishers { get; set; }

        public DataContext()
        {
            Games = new List<Game>();
            Publishers = new List<Publisher>();
        }
    }
}