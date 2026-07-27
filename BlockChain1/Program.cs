using System;
using System.Diagnostics;
using BlockChain1.Models;
using BlockChain1.Services;

var blockchain = new BlockChainService();
var hashingService = new HashingService();
var miningService = new MiningService(hashingService);

Console.WriteLine("========== LEVEL 1 ==========");
Console.WriteLine();

// Створюємо 5 блоків
blockchain.AddBlock("Alice send Bob 100 Coin", "Alice");
blockchain.AddBlock("Bob send Marta 50 Coin", "Bob");
blockchain.AddBlock("Marta send Ivan 20 Coin", "Marta");
blockchain.AddBlock("Ivan send Kate 10 Coin", "Ivan");
blockchain.AddBlock("Kate send Alice 5 Coin", "Kate");

// Вивід ланцюга
blockchain.PrintChain();

Console.WriteLine();
Console.WriteLine($"Blockchain valid: {blockchain.IsValid()}");


//====================================================
// LEVEL 2
//====================================================

Console.WriteLine();
Console.WriteLine("========== LEVEL 2 ==========");
Console.WriteLine();

Console.WriteLine("-----------------------------------------------");
Console.WriteLine("| Difficulty | Nonce | Time (ms) |");
Console.WriteLine("-----------------------------------------------");

for (int difficulty = 1; difficulty <= 5; difficulty++)
{
    Block block = new Block(
        0,
        DateTime.UtcNow,
        "Benchmark",
        "System",
        "0");

    Stopwatch sw = Stopwatch.StartNew();

    miningService.MineBlock(block, difficulty);

    sw.Stop();

    Console.WriteLine($"|     {difficulty}      | {block.Nonce,6} | {sw.ElapsedMilliseconds,8} |");
}

Console.WriteLine("-----------------------------------------------");


//====================================================
// LEVEL 3
//====================================================

Console.WriteLine();
Console.WriteLine("========== LEVEL 3 ==========");
Console.WriteLine();

blockchain.HackChain(2, "Fake transaction: Alice -> Hacker 1000000");

Console.WriteLine();

blockchain.PrintChain();

Console.WriteLine();
Console.WriteLine($"Blockchain valid after hack: {blockchain.IsValid()}");

Console.WriteLine();
Console.WriteLine("Press any key...");
Console.ReadKey();