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
            var genesisBlock = new Block(0, DateTime.UtcNow, "Genesis Block", "0"); // створюємо генезис-блок з індексом 0, поточним часом, даними "Genesis Block" та попереднім хешем "0"
            genesisBlock.Hash = _hashingService.ComputeHash(genesisBlock);// обчислюємо хеш генезис-блоку
            Chain.Add(genesisBlock);
        }

        public void AddBlock(string data)
        {
            var previousBlock = Chain.Last(); // отримуємо останній блок у ланцюзі
            var newIndex = previousBlock.Index + 1; // індекс нового блоку на 1 більший за попередній
            var newTimestamp = DateTime.UtcNow; // поточний час
            var newPreviousHash = previousBlock.Hash; // хеш попереднього блоку
            var newBlock = new Block(newIndex, newTimestamp, data, newPreviousHash); // створюємо новий блок з індексом на 1 більшим за попередній, поточним часом, переданими даними та хешем попереднього блоку
            newBlock.Hash = _hashingService.ComputeHash(newBlock); // обчислюємо хеш нового блоку
            Chain.Add(newBlock); // додаємо новий блок до ланцюга
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
