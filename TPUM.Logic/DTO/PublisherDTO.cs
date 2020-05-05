using System;

namespace TPUM.Logic.DTO
{
    public class PublisherDTO
    {
        public PublisherDTO(Guid id, string name, string country)
        {
            ID = id;
            Name = name;
            Country = country;
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return Name + " - " + Country;
        }
    }
}