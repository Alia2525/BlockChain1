using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChain1.Models;

namespace BlockChain1.Services
{
    //Цей клас відповідає за процес майнінгу блоків у блокчейні. Він використовує сервіс хешування для обчислення хешів блоків та знаходження правильного значення Nonce, яке задовольняє умові складності (difficulty).
    public class MiningService
    {
        private readonly HashingService _hashingService;

        public MiningService(HashingService hashingService)
        {
            _hashingService = hashingService;
        }
        // Метод MineBlock приймає блок та рівень складності (difficulty) як параметри. Він генерує цільовий рядок, що складається з певної кількості нулів, відповідно до рівня складності. Потім він виконує цикл, в якому збільшує значення Nonce блоку та обчислює його хеш, поки хеш не починається з цільового рядка. Коли умова виконана, метод повертає знайдене значення Nonce.
        public long MineBlock(Block block, string targetPrefix)
        {
            long foundNonce = -1;

            Parallel.For(0, Environment.ProcessorCount, (threadId, state) =>
            {
                long nonce = threadId;

                while (!state.IsStopped)
                {
                    Block tempBlock = new Block(
                        block.Index,
                        block.Timestamp,
                        block.Data,
                        block.Author,
                        block.PreviousHash);

                    tempBlock.Nonce = (int)nonce;
                    tempBlock.Hash = _hashingService.ComputeHash(tempBlock);

                    if (tempBlock.Hash.StartsWith(targetPrefix))
                    {
                        lock (block)
                        {
                            block.Nonce = tempBlock.Nonce;
                            block.Hash = tempBlock.Hash;
                        }

                        foundNonce = nonce;

                        state.Stop();
                        return;
                    }

                    nonce += Environment.ProcessorCount;
                }
            });

            Console.WriteLine($"Hash : {block.Hash}");
            Console.WriteLine($"Nonce: {block.Nonce}");

            return foundNonce;
        }
    }
}
