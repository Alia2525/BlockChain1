using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BlockChain1.Models;

namespace BlockChain1.Services
{
    public class BlockChainService
    {
        public List<Block> Chain { get; set; }

        private readonly HashingService _hashingService;
        private readonly MiningService _miningService;

        // Складність майнінгу
        public int Difficulty { get; set; } = 1;
        private readonly int _targetTimePerBlock = 2000; // 2 секунди
        private readonly int _adjustmentInterval = 2; // Кількість блоків для корекції складності
        public BlockChainService()
        {
            Chain = new List<Block>();

            _hashingService = new HashingService();
            _miningService = new MiningService(_hashingService);

            CreateGenesisBlock();
        }

        private void CreateGenesisBlock()
        {
            Block genesis = new Block(
                0,
                DateTime.UtcNow,
                "Genesis Block",
                "System",
                "0");

            _miningService.MineBlock(genesis, Difficulty);

            Chain.Add(genesis);
        }

        public void AddBlock(string data, string author)
        {
            Block previous = Chain.Last();

            Block newBlock = new Block(
                previous.Index + 1,
                DateTime.UtcNow,
                data,
                author,
                previous.Hash);

            newBlock.Difficulty = Difficulty;
            _miningService.MineBlock(newBlock, Difficulty);

            Chain.Add(newBlock);

            if (newBlock.Index % _adjustmentInterval == 0)
            {
                AdjustDifficulty();
            }
        }

        private void AdjustDifficulty()
        {
            var recentBlocks = Chain.Skip(Chain.Count - _adjustmentInterval).Take(_adjustmentInterval).ToList();
            var avgTime = recentBlocks.Average(b => b.MiningDuration);

            if (avgTime < _targetTimePerBlock)
            {
                Difficulty++;
                Console.WriteLine($"Difficulty increased to {Difficulty}");
            }
            else if (avgTime > _targetTimePerBlock)
            {
                Difficulty--;
                
            }
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previous = Chain[i - 1];

                string hash = _hashingService.ComputeHash(currentBlock);

                if (currentBlock.Hash != hash)
                    return false;

                if (currentBlock.PreviousHash != previous.Hash)
                    return false;

                if (!currentBlock.Hash.StartsWith(new string('0', currentBlock.Difficulty)))
                    return false;
            }

            return true;
        }

        public void PrintChain()
        {
            Console.WriteLine();
            Console.WriteLine("========== BLOCKCHAIN ==========");

            foreach (var block in Chain)
            {
                Console.WriteLine($"Index: {block.Index}");
                Console.WriteLine($"Timestamp: {block.Timestamp}");
                Console.WriteLine($"Author: {block.Author}");
                Console.WriteLine($"Data: {block.Data}");
                Console.WriteLine($"Nonce: {block.Nonce}");
                Console.WriteLine($"Hash: {block.Hash}");
                Console.WriteLine($"Previous Hash: {block.PreviousHash}");
                Console.WriteLine(new string('-', 60));
            }
        }

        // -------------------------
        // Рівень 3
        // -------------------------
        public void HackChain(int index, string fakeData)
        {
            Stopwatch sw = Stopwatch.StartNew();

            // Змінюємо дані блоку
            Chain[index].Data = fakeData;

            // Повторний майнінг зміненого блоку
            Chain[index].Nonce = 0;
            _miningService.MineBlock(Chain[index], Difficulty);

            // Перерахунок усіх наступних блоків
            for (int i = index + 1; i < Chain.Count; i++)
            {
                Chain[i].PreviousHash = Chain[i - 1].Hash;
                Chain[i].Nonce = 0;

                _miningService.MineBlock(Chain[i], Difficulty);
            }

            sw.Stop();

            Console.WriteLine();
            Console.WriteLine($"Hack completed in {sw.ElapsedMilliseconds} ms");
        }
    }
}