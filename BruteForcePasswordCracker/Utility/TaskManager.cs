using System;
using System.Collections.Generic;

namespace BruteForcePasswordCracker.Utility
{
    public class TaskManager
    {
        private static object lockObject = new object();
        private char[] charset;
        private int idx;

        public TaskManager(List<char> charset)
        {
            this.charset = charset.ToArray();
        }

        public string GetWork()
        {
            string startingChar = null;
            if (idx < charset.Length)
            {
                lock (lockObject)
                {
                    if (idx < charset.Length)
                    {
                        startingChar = charset[idx].ToString();
                        idx++;
                    }
                }
            }
            return startingChar;
        }
    }
}
