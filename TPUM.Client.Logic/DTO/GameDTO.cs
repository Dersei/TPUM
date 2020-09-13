using System;

namespace TPUM.Client.Logic.DTO
{
    public class GameDTO
    {
        public GameDTO(string title, PublisherDTO publisher, decimal rating, DateTime premiere, Genre[]? genres)
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