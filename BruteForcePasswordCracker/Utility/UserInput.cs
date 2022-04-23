using System;
using System.Collections.Generic;

namespace BruteForcePasswordCracker.Utility
{
    public class UserInput
    {
        public string Password { get; set; }
        public List<char> Charset { get; set; }
        public int NumberOfThreads { get; set; }
    }
}
