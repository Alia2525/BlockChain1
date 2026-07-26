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
        public long MineBlock(Block block, int difficulty)
        {
            string target = new string('0', difficulty); // "0000"
            var startTime = Stopwatch.StartNew();
            while (block.Hash.StartsWith(target) != true)
            {
                block.Nonce++;
                block.Hash = _hashingService.ComputeHash(block);

                if (block.Nonce % 100000 == 0)
                {
                    Console.Write($",");
                }
            }
            startTime.Stop();
            Console.WriteLine(startTime.ElapsedMilliseconds/1000.0);
            return block.Nonce;
        }
    }
}
