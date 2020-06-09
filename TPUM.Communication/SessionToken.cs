using System;

namespace TPUM.Communication
{
    public struct SessionToken : IEquatable<SessionToken>
    {
        public SessionToken(int value) => Value = value;

        public int Value { get; set; }

        public bool Equals(SessionToken other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            return obj is SessionToken other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}