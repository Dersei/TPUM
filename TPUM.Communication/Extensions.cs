using System;
using System.Text;

namespace TPUM.Communication
{
    public static class Extensions
    {
        public static ArraySegment<byte> GetArraySegment(this string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            return new ArraySegment<byte>(buffer);
        }
    }
}
