using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteForcePasswordCracker.Utility
{
    public class UserInput
    {
        public string Password { get; set; }
        public List<char> Charset { get; set; }
        public int NumberOfThreads { get; set; }
    }
}
