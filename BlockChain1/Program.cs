using BlockChain1.Services;
using BlockChain1.Models;

var hashingService = new HashingService();
var blockChainService = new BlockChainService();
var blockChainDisplayService = new BlockChainDispleyService();
var miningService = new MiningService(hashingService);

//blockChainService.AddBlock("Alice send Bob 100 Coin", "Alice");
//blockChainService.AddBlock("Bob send Marta 50 Coin", "Bob");
//blockChainService.AddBlock("Katia send Marta 50 Coin", "Katia");

//Console.WriteLine("=== Start BlockChain ===");

//blockChainDisplayService.ShowBlockChain(blockChainService.Chain);
//blockChainDisplayService.ShowValidationResult(blockChainService.IsValid());

//Console.WriteLine();
//Console.WriteLine("=== After fake ===");

//// змінюємо перший блок після Genesis
//blockChainService.Chain[1].Data = "Alice send Bob 1000000 Coin";

//// перерахунок хеша
//blockChainService.Chain[1].Hash =
//    hashingService.ComputeHash(blockChainService.Chain[1]);

//// оновлення наступних блоків
//for (int i = 2; i < blockChainService.Chain.Count; i++)
//{
//    blockChainService.Chain[i].PreviousHash =
//        blockChainService.Chain[i - 1].Hash;

//    blockChainService.Chain[i].Hash =
//        hashingService.ComputeHash(blockChainService.Chain[i]);
//}

//Console.WriteLine();

//blockChainDisplayService.ShowBlockChain(blockChainService.Chain);
//blockChainDisplayService.ShowValidationResult(blockChainService.IsValid());

//Console.ReadKey();


//Console.WriteLine("Menu");
//Console.WriteLine("1. Add Block");
//Console.WriteLine("2. Show BlockChain");
//Console.WriteLine("3. Validate BlockChain");
//Console.WriteLine("4. Change Difficulty ++");
//Console.WriteLine("5. Change Difficulty --");
//Console.WriteLine("6. Exit");

//string select;
//while (true)
//{
//    select = Console.ReadLine();
//    switch (select)
//    {
//        case "1":
//            Console.WriteLine("Block Added");
//            blockChainService.AddBlock("Alice send Bob 100 Coin", "Alice");
//            break;
//        case "2":
//            blockChainDisplayService.ShowBlockChain(blockChainService.Chain);
//            break;
//        case "3":
//            blockChainDisplayService.ShowValidationResult(blockChainService.IsValid());
//            break;
//        case "4":
//            blockChainService.Difficulty++;
//            Console.WriteLine($"Difficulty changed to {blockChainService.Difficulty}");
//            break;
//        case "5":
//            if (blockChainService.Difficulty > 1)
//            {
//                blockChainService.Difficulty--;
//                Console.WriteLine($"Difficulty changed to {blockChainService.Difficulty}");
//            }
//            else
//            {
//                Console.WriteLine("Difficulty cannot be less than 1");
//            }
//            break;
//        case "6":
//            Environment.Exit(0);
//            break;
//        default:
//            Console.WriteLine("Invalid selection");
//            break;
//    }
//}
Console.Write("Print your surname: ");
string surname = Console.ReadLine();

var block = new Block(
    1,
    DateTime.Now,
    surname,
    surname,
    "0"
);

block.Hash = hashingService.ComputeHash(block);

Console.WriteLine("Searching for hash with prefix cafe...");
Console.WriteLine();

miningService.MineBlock(block, "cafe");

Console.WriteLine();
Console.WriteLine("========== RESULT ==========");
Console.WriteLine($"Data : {block.Data}");
Console.WriteLine($"Hash : {block.Hash}");
Console.WriteLine($"Nonce: {block.Nonce}");

Console.ReadKey();