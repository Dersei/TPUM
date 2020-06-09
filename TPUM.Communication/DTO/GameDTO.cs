using System;
using TPUM.Data.Model;

namespace TPUM.Communication.DTO
{
    public class GameDTO
    {
        public GameDTO(Guid id, string title, PublisherDTO publisher, decimal rating, DateTime premiere, Genre[]? genres)
        {
            ID = id;
            Title = title;
            Publisher = publisher;
            Rating = rating;
            Premiere = premiere;
            Genres = genres;
        }

        public Guid ID { get; set; }
        public string Title { get; set; }
        public PublisherDTO Publisher { get; set; }
        public decimal Rating { get; set; }
        public DateTime Premiere { get; set; }
        public Genre[]? Genres { get; set; }

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