using System;
using System.Collections.Generic;
using System.Text;

namespace WalletRpc.RequestResults
{
    public class AddressIndex
    {
        public int index { get; set; }
    }

    public class Index
    {
        int major { get; set; }
        int minor { get; set; }
    }
}
