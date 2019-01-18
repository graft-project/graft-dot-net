namespace BitcoinLib
{
    public static class BitcoinConvert
    {
        public static decimal FromAtomicUnits(long amount)
        {
            return (decimal)amount / 100_000_000;
        }

        public static long ToAtomicUnits(decimal amount)
        {
            return (long)amount * 100_000_000;
        }
    }
}
