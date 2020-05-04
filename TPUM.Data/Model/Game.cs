using System;

namespace TPUM.Data.Model
{
    public class Game : BaseModel
    {
        public Game(string title, Publisher publisher, decimal rating, DateTime premiere, Genre[]? genres)
        {
            Title = title;
            Publisher = publisher;
            Rating = rating;
            Genres = genres;
            Premiere = premiere;
        }

        public string Title { get; private set; }
        public Publisher Publisher { get; private set; }
        public decimal Rating { get; private set; }
        public DateTime Premiere { get; private set; }
        public Genre[]? Genres { get; private set; }
    }
}
