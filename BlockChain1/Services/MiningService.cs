using System;
using BlockChain1.Models;

namespace BlockChain1.Services
{
    public class MiningService
    {
        private readonly HashingService _hashingService;

        public MiningService(HashingService hashingService)
        {
            _hashingService = hashingService;
        }

        /// <summary>
        /// Майнінг блоку методом Proof of Work.
        /// Повертає кількість спроб (Nonce).
        /// </summary>
        public long MineBlock(Block block, int difficulty)
        {
            string target = new string('0', difficulty);

            block.Nonce = 0;

            while (true)
            {
                block.Hash = _hashingService.ComputeHash(block);

                if (block.Hash.StartsWith(target))
                {
                    break;
                }

                block.Nonce++;
            }
            block.MiningDuration = startTime.ElapsedMilliseconds;
            return block.Nonce;
            
        }
    }
}