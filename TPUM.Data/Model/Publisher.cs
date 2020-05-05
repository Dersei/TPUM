using System;

namespace TPUM.Data.Model
{
    public sealed class Publisher : IdItem, IEquatable<Publisher>
    {
        public Publisher(string name, string country)
        {
            Name = name;
            Country = country;
        }

        public string Name { get; set; }
        public string Country { get; set; }

        public bool Equals(Publisher? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Country == other.Country;
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is Publisher other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }

        public override string ToString()
        {
            return Name + " - " + Country;
        }
    }
}