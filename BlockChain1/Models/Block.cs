using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain1.Models
{
    public class Block
    {
        public int Index { get; set; }

        public DateTime Timestamp { get; set; }

        public string Data { get; set; }

        public string Author { get; set; }

        public string Hash { get; set; }

        public string PreviousHash { get; set; }

        public Block(
            int index,
            DateTime timestamp,
            string data,
            string author,
            string previousHash)
        {
            Index = index;
            Timestamp = timestamp;
            Data = data;
            Author = author;
            PreviousHash = previousHash;
        }
    }
}
