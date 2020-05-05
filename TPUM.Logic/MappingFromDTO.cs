using TPUM.Data.Model;
using TPUM.Logic.DTO;

namespace TPUM.Logic
{
    public static class MappingFromDTO
    {
        public static Publisher MapPublisherDTO(PublisherDTO publisher)
        {
            return new Publisher(publisher.Name, publisher.Country);
        }
        
        public static Game MapGameDTO(GameDTO game)
        {
            return new Game(game.Title, MapPublisherDTO(game.Publisher), game.Rating, game.Premiere, game.Genres);
        }
    }
}