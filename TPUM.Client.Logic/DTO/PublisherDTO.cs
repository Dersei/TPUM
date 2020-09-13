using System;

namespace TPUM.Communication.DTO
{
    public class PublisherDTO
    {
        public PublisherDTO(string name, string country)
        {
            Name = name;
            Country = country;
        }

        public string Name { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return Name + " - " + Country;
        }
    }
}