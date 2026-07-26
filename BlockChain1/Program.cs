using BlockChain1.Services;

var hashingService = new HashingService();
var blockChainService = new BlockChainService();
var blockChainDisplayService = new BlockChainDispleyService();

blockChainService.AddBlock("Alice send Bob 100 Coin", "Alice");
blockChainService.AddBlock("Bob send Marta 50 Coin", "Bob");
blockChainService.AddBlock("Katia send Marta 50 Coin", "Katia");

Console.WriteLine("=== Start BlockChain ===");

blockChainDisplayService.ShowBlockChain(blockChainService.Chain);
blockChainDisplayService.ShowValidationResult(blockChainService.IsValid());

Console.WriteLine();
Console.WriteLine("=== After fake ===");

// змінюємо перший блок після Genesis
blockChainService.Chain[1].Data = "Alice send Bob 1000000 Coin";

// перерахунок хеша
blockChainService.Chain[1].Hash =
    hashingService.ComputeHash(blockChainService.Chain[1]);

// оновлення наступних блоків
for (int i = 2; i < blockChainService.Chain.Count; i++)
{
    blockChainService.Chain[i].PreviousHash =
        blockChainService.Chain[i - 1].Hash;

    blockChainService.Chain[i].Hash =
        hashingService.ComputeHash(blockChainService.Chain[i]);
}

Console.WriteLine();

blockChainDisplayService.ShowBlockChain(blockChainService.Chain);
blockChainDisplayService.ShowValidationResult(blockChainService.IsValid());

Console.ReadKey();