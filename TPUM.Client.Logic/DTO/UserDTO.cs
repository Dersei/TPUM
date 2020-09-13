using System;

namespace TPUM.Client.Logic.DTO
{
    public class UserDTO
    {
        public UserDTO(int id, string username, string password)
        {
            ID = id;
            Username = username;
            Password = password;
        }

        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public override string ToString() => Username;
    }
}