using Microsoft.Extensions.Configuration;
using System;

namespace Graft.Infrastructure
{
    public static class ConfigUtils
    {
        public static string GetConnectionString(IConfiguration configuration, string prefix)
        {
            string server = configuration["DB:Server"];
            string port = configuration["DB:Port"];
            string db = configuration["DB:DbName"];
            string user = configuration["DB:UserName"];
            string password = configuration["DB:Password"];

            return $"server={server};port={port};database={db};uid={user};pwd={password}";
        }
    }

    public static class GraftConvert
    {
        public static ulong ToAtomicUnits(decimal amount)
        {
            return (ulong)Math.Round(amount * 10_000_000_000);
        }

        public static decimal FromAtomicUnits(ulong amount)
        {
            return (decimal)amount / 10_000_000_000;
        }
    }

    public static class StringExtensions
    {
        public static string EllipsisString(this string str, int startLength = 5, int endLength = 5)
        {
            if (str == null || str.Length <= (startLength + endLength + "...".Length))
                return str;
            return $"{str.Substring(0, startLength)}...{str.Substring(str.Length - endLength)}";
        }

        public static bool ContainsStr(this string str, string value)
        {
            if (str == null)
                return false;
            return str.Contains(value);
        }

        public static byte[] HexStringToBytes(this string hex)
        {
            if (hex == null) return null;
            if (hex.Length == 0) return new byte[0];

            int l = hex.Length / 2;
            var b = new byte[l];
            for (int i = 0; i < l; ++i)
            {
                b[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return b;
        }
    }

    public static class ByteArrayExtensions
    {
        public static bool ByteArrayCompare(this byte[] a1, ReadOnlySpan<byte> a2)
        {
            return ((ReadOnlySpan<byte>)a1).SequenceEqual(a2);
        }
    }
}
