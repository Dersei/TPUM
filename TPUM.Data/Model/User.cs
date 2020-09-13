using System;
using System.Collections.Generic;

namespace TPUM.Data.Model
{
    public sealed class User : IEquatable<User>
    {
        public User(string username, string password)
        {
            ID = Guid.NewGuid().GetHashCode();
            Username = username;
            Password = password;
        }

        public int ID { get; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool Equals(User? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Username == other.Username && Password == other.Password;
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is User other && Equals(other);
        }

        public override int GetHashCode() => HashCode.Combine(ID);

        public override string ToString() => Username;
    }
}