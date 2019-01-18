// <copyright file="WalletInfo.cs" company="HiTech Service, Inc.">
//     Copyright (c) 2018, Graft Blockchain, Inc. All rights reserved.
// </copyright>

namespace Graft.Infrastructure
{
    /// <summary>
    /// Represents restored wallet information needed for DAPI calls Sale & GetSaleStatus.
    /// </summary>
    public class WalletInfo
    {
        /// <summary>
        /// Gets/sets wallet address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets/sets (private) key needed to invoke Sale dapi method.
        /// This key is available when restoring Graft account, see 
        /// </summary>
        public string ViewKey { get; set; }

        /// <summary>
        /// Gets/sets network type (production/test).
        /// </summary>
        public NetworkType Network { get; set; }
    }
}
