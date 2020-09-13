using System;

namespace TPUM.Communication.TransferModel
{
    public class TransferUser
    {
        public TransferUser(int id, string username, string password)
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