using BlockChain1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain1.Services
{
    // Мясорубка для блоків. Вона приймає блок і повертає його хеш.
    public class HashingService
    {
        public string ComputeHash(Block block)
        {
            var input = $"{block.Index}{block.Timestamp.ToString("0")}{block.Data}{block.PreviousHash}";
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