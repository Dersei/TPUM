using System;

namespace TPUM.Communication.DTO
{
    public class UserDTO
    {
        public UserDTO(Guid id, string username, string password)
        {
            ID = id;
            Username = username;
            Password = password;
        }

        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public override string ToString() => Username;
    }
}