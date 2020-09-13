using System;

namespace TPUM.Communication.TransferModel
{
    public class TransferGame
    {
        public TransferGame(string title, TransferPublisher publisher, decimal rating, DateTime premiere, Genre[]? genres)
        {
            Title = title;
            Publisher = publisher;
            Rating = rating;
            Premiere = premiere;
            Genres = genres;
        }

        public string Title { get; set; }
        public TransferPublisher Publisher { get; set; }
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