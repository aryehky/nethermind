﻿//  Copyright (c) 2021 Demerzel Solutions Limited
//  This file is part of the Nethermind library.
// 
//  The Nethermind library is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  The Nethermind library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with the Nethermind. If not, see <http://www.gnu.org/licenses/>.

using Nethermind.Core.Specs;
using Nethermind.Int256;
using Nethermind.Specs.Forks;

namespace Nethermind.Specs
{
    public class SingleReleaseSpecProvider : ISpecProvider
    {
        private long? _theMergeBlock = null;

        public void UpdateMergeTransitionInfo(long? blockNumber, UInt256? terminalTotalDifficulty = null)
        {
            if (blockNumber != null)
                _theMergeBlock = blockNumber;
            if (terminalTotalDifficulty != null)
                TerminalTotalDifficulty = terminalTotalDifficulty;
        }

        public long? MergeBlockNumber => _theMergeBlock;
        public UInt256? TerminalTotalDifficulty { get; set; }
        public ulong ChainId { get; }
        public long[] TransitionBlocks { get; } = {0};

        private readonly IReleaseSpec _releaseSpec;

        public SingleReleaseSpecProvider(IReleaseSpec releaseSpec, ulong networkId)
        {
            ChainId = networkId;
            _releaseSpec = releaseSpec;
            if (_releaseSpec == Dao.Instance)
            {
                DaoBlockNumber = 0;
            }
        }

        public IReleaseSpec GenesisSpec => _releaseSpec;

        public IReleaseSpec GetSpec(long blockNumber) => _releaseSpec;

        public long? DaoBlockNumber { get; }
    }
}
