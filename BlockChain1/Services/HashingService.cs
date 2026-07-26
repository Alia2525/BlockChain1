using BlockChain1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain1.Services
{
    public class HashingService
    {
        // Мясорубка для блоків. Вона приймає блок і повертає його хеш.
        public string ComputeHash(Block block)
        {
            var input =
                $"{block.Index}" +
                $"{block.Timestamp:o}" +
                $"{block.Data}" +
                $"{block.Author}" +
                $"{block.PreviousHash}" +
                $"{block.Nonce}";

            return ComputeHash(input);
        }

        // Мясорубка для рядків. Вона приймає рядок і повертає його хеш.
        public string ComputeHash(string input)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}