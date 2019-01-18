using Nethereum.Hex.HexTypes;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace EthereumLib
{
    public static class Converter
    {
        public static decimal AtomicToDecimal(HexBigInteger value)
        {
            return Nethereum.Web3.Web3.Convert.FromWei(value.Value);
        }

        public static decimal AtomicToDecimal(long value)
        {
            return AtomicToDecimal(new BigInteger(value));
        }

        public static decimal AtomicToDecimal(BigInteger value)
        {
            return Nethereum.Web3.Web3.Convert.FromWei(value);
        }

        public static HexBigInteger DecimalToAtomicUnit(decimal value)
        {
            return new HexBigInteger(DecimalToBigInt(value));
        }

        public static BigInteger DecimalToBigInt(decimal value)
        {
            return Nethereum.Web3.Web3.Convert.ToWei(value);
        }
    }
}
