using BlockChain1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain1.Services
{
    public class BlockChainDispleyService
    {
        public void ShowBlockChain(List<Block> chain)
        {
            foreach (var block in chain)
            {
                Console.WriteLine($"Index: {block.Index}");
                Console.WriteLine($"Timestamp: {block.Timestamp}");
                Console.WriteLine($"Data: {block.Data}");
                Console.WriteLine($"Previous Hash: {block.PreviousHash}");
                Console.WriteLine($"Hash: {block.Hash}");
                Console.WriteLine(new string('-', 50));
            }
        }
        public void ShowValidationResult(bool isValid)
        {
            if (isValid)
            {
                Console.WriteLine("The blockchain is valid.");
            }
            else
            {
                Console.WriteLine("The blockchain is NOT valid.");
            }
        }
    }
}
