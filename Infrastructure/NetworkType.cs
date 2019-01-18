// <copyright file="NetworkType.cs" company="HiTech Service, Inc.">
//     Copyright (c) 2018, Graft Blockchain, Inc. All rights reserved.
// </copyright>

namespace Graft.Infrastructure
{
    /// <summary>
    /// Represents possible Graft networks available, production and test networks.
    /// </summary>
    public enum NetworkType
    {
        /// <summary>
        /// The only production network.
        /// </summary>
        MainNet,
        /// <summary>
        /// Public testing network.
        /// </summary>
        PublicTestnet,
        /// <summary>
        /// Public real time authorization test network.
        /// </summary>
        PublicRTATestnet
    }
}