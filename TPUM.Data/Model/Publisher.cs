using System;

namespace TPUM.Data.Model
{
    public class Publisher : BaseModel
    {
        public Publisher(string name, string country)
        {
            Name = name;
            Country = country;
        }

        public string Name { get; private set; }
        public string Country { get; private set; }
    }
}