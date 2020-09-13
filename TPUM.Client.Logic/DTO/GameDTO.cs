using System;

namespace TPUM.Client.Logic.DTO
{
    public class GameDTO : IEquatable<GameDTO>
    {
        public GameDTO(string title, PublisherDTO publisher, decimal rating, DateTime premiere, Genre genres)
        {
            Title = title;
            Publisher = publisher;
            Rating = rating;
            Premiere = premiere;
            Genres = genres;
        }

        public string Title { get; set; }
        public PublisherDTO Publisher { get; set; }
        public decimal Rating { get; set; }
        public DateTime Premiere { get; set; }
        public Genre Genres { get; set; }

        public override string ToString()
        {
            string info = "";
            info += "Title: " + Title + Environment.NewLine;
            info += "Publisher: " + Publisher + Environment.NewLine;
            info += "Premiere: " + Premiere;
            return info;
        }

        public bool Equals(GameDTO? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Title == other.Title && Publisher.Equals(other.Publisher) && Rating == other.Rating && Premiere.Equals(other.Premiere);
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is GameDTO other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title);
        }

    }
}