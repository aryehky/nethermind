//  Copyright (c) 2021 Demerzel Solutions Limited
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

using System;
using Nethermind.Core;
using Nethermind.Core.Specs;
using Nethermind.Crypto.Bls;

namespace Nethermind.Evm.Precompiles.Bls.Shamatar
{
    /// <summary>
    /// https://eips.ethereum.org/EIPS/eip-2537
    /// </summary>
    public class G1AddPrecompile : IPrecompile
    {
        public static IPrecompile Instance = new G1AddPrecompile();

        private G1AddPrecompile()
        {
        }

        public Address Address { get; } = Address.FromNumber(10);

        public long BaseGasCost(IReleaseSpec releaseSpec)
        {
            return 600L;
        }

        public long DataGasCost(in ReadOnlyMemory<byte> inputData, IReleaseSpec releaseSpec)
        {
            return 0L;
        }

        public (ReadOnlyMemory<byte>, bool) Run(in ReadOnlyMemory<byte> inputData, IReleaseSpec releaseSpec)
        {
            const int expectedInputLength = 4 * BlsParams.LenFp;
            if (inputData.Length != expectedInputLength)
            {
                return (Array.Empty<byte>(), false); 
            }
            
            // Span<byte> inputDataSpan = stackalloc byte[expectedInputLength];
            // inputData.PrepareEthInput(inputDataSpan);

            (byte[], bool) result;
            
            Span<byte> output = stackalloc byte[2 * BlsParams.LenFp];
            bool success = ShamatarLib.BlsG1Add(inputData.Span, output);
            if (success)
            {
                result = (output.ToArray(), true);
            }
            else
            {
                result = (Array.Empty<byte>(), false);
            }

            return result;
        }
    }
}
