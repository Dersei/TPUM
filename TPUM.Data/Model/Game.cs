using System;

namespace TPUM.Data.Model
{
    public sealed class Game : IEquatable<Game>
    {
        public Game(string title, Publisher publisher, decimal rating, DateTime premiere, Genre genres)
        {
            ID = Guid.NewGuid().GetHashCode();
            Title = title;
            Publisher = publisher;
            Rating = rating;
            Genres = genres;
            Premiere = premiere;
        }

        public int ID { get; }

        public string Title { get; set; }
        public Publisher Publisher { get; set; }
        public decimal Rating { get; set; }
        public DateTime Premiere { get; set; }
        public Genre Genres { get; set; }

        public bool Equals(Game? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Title == other.Title && Publisher.Equals(other.Publisher) && Rating == other.Rating && Premiere.Equals(other.Premiere);
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is Game other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }

        public override string ToString()
        {
            string info = "";
            info += "Title: " + Title + Environment.NewLine;
            info += "Publisher: " + Publisher + Environment.NewLine;
            info += "Premiere: " + Premiere;
            return info;
        }
    }
}
