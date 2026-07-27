using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChain1.Models; // <-- add this if Block is in BlockChain1.Models

namespace BlockChain1.Services
{
    public class BlockChainService
    {
        public List<Block> Chain { get; set; }

        private readonly HashingService _hashingService;
        private readonly MiningService _miningService;

        // Префікс, який має починатися хеш
        public string TargetPrefix { get; set; } = "cafe";

        public BlockChainService()
        {
            _hashingService = new HashingService();
            _miningService = new MiningService(_hashingService);

            Chain = new List<Block>();

            CreateGenesisBlock();
        }

        private void CreateGenesisBlock()
        {
            var genesisBlock = new Block(
                0,
                DateTime.UtcNow,
                "Genesis Block",
                "System",
                "0");

            _miningService.MineBlock(genesisBlock, TargetPrefix);

            Chain.Add(genesisBlock);
        }

        public void AddBlock(string data, string author)
        {
            var previousBlock = Chain.Last();

            var newBlock = new Block(
                previousBlock.Index + 1,
                DateTime.UtcNow,
                data,
                author,
                previousBlock.Hash);

            _miningService.MineBlock(newBlock, TargetPrefix);

            Chain.Add(newBlock);
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                var currentBlock = Chain[i];
                var previousBlock = Chain[i - 1];

                if (currentBlock.Hash != _hashingService.ComputeHash(currentBlock))
                    return false;

                if (currentBlock.PreviousHash != previousBlock.Hash)
                    return false;

                if (!currentBlock.Hash.StartsWith(TargetPrefix))
                    return false;
            }

            return true;
        }
    }
}
