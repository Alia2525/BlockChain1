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
        public List<Block> Chain { get; set; } // список блоків у ланцюзі
        private readonly HashingService _hashingService;  // сервіс для обчислення хешів

        public BlockChainService()
        {
            _hashingService = new HashingService();
            Chain = new List<Block>(); 
            CreateGenesisBlock();// створюємо генезис-блок
        }

        private void CreateGenesisBlock()
        {
            var genesisBlock = new Block(
                0,
                DateTime.UtcNow,
                "Genesis Block",
                "System",
                "0");

            genesisBlock.Hash = _hashingService.ComputeHash(genesisBlock);

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

            newBlock.Hash = _hashingService.ComputeHash(newBlock);

            Chain.Add(newBlock);
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                var currentBlock = Chain[i];
                var previousBlock = Chain[i - 1];
                // Перевіряємо, чи хеш поточного блоку правильний
                if (currentBlock.Hash != _hashingService.ComputeHash(currentBlock))
                {
                    return false;
                }
                // Перевіряємо, чи попередній хеш поточного блоку збігається з хешем попереднього блоку
                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
