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
        public int Difficulty { get; set; } = 3;

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

            _miningService.MineBlock(newBlock, Difficulty);

            Chain.Add(newBlock);
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block current = Chain[i];
                Block previous = Chain[i - 1];

                string hash = _hashingService.ComputeHash(current);

                if (current.Hash != hash)
                    return false;

                if (current.PreviousHash != previous.Hash)
                    return false;

                if (!current.Hash.StartsWith(new string('0', Difficulty)))
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