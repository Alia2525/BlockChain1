using BlockChain1.Services;

var blockChainService = new BlockChainService();
var blockChainDisplayService = new BlockChainDispleyService();

blockChainService.AddBlock("Alice send Bob 100 Coin");
blockChainService.AddBlock("Bob send Narta 50 Coin");

blockChainDisplayService.ShowBlockChain(blockChainService.Chain);
blockChainDisplayService.ShowValidationResult(blockChainService.IsValid());
