using BlockChain1.Models;
using System.Security.Cryptography;
using BlockChain1.Models;
using System.Security.Cryptography;
using System.Text;

namespace BlockChain1.Services
{
    public class HashingService
    {
        public string ComputeHash(Block block)
        {
            string input =
                $"{block.Index}" +
                $"{block.Timestamp:o}" +
                $"{block.Data}" +
                $"{block.Author}" +
                $"{block.PreviousHash}" +
                $"{block.Nonce}" +
                $"{block.Difficulty}" ;

            using SHA256 sha256 = SHA256.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha256.ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();

            foreach (byte b in hash)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}