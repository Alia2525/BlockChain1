using BlockChain1.Services;

var blockChainService = new BlockChainService();
var blockChainDisplayService = new BlockChainDispleyService();

blockChainService.AddBlock("Alice send Bob 100 Coin");
blockChainService.AddBlock("Bob send Marta 50 Coin");
blockChainService.AddBlock("Katia send Marta 50 Coin");
blockChainService.AddBlock("Marta send Alice 50 Coin");
blockChainService.AddBlock("Katia send Bob 50 Coin");


blockChainDisplayService.ShowBlockChain(blockChainService.Chain);
blockChainDisplayService.ShowValidationResult(blockChainService.IsValid());
