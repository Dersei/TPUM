using System;
using System.Collections.Generic;

namespace TPUM.Logic.DTO
{
    public class UserDTO
    {
        public UserDTO(Guid id, string username, string password, List<GameDTO> favouriteGames)
        {
            ID = id;
            Username = username;
            Password = password;
            FavouriteGames = favouriteGames;
        }

        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<GameDTO> FavouriteGames { get; set; }
    }
}