using BruteForcePasswordCracker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BruteForcePasswordCracker.Logic
{
    class Generator
    {
        private List<char> charset;
        private string password;
        private int passwordLength;

        public Generator(UserInput userInput)
        {
            charset = userInput.Charset;
            password = userInput.Password;
            passwordLength = password.Length;
        }

        public void GenerateAndSearchPassword(TaskManager taskManager, CancellationTokenSource cts)
        {
            string startingChar = taskManager.GetWork();
            while (startingChar != null && !cts.IsCancellationRequested)
            {
                RecursiveSearch(startingChar, passwordLength - 1, cts);
                startingChar = taskManager.GetWork();
            }
        }

        private void RecursiveSearch(string possiblePassword, int remainingPasswordLength, CancellationTokenSource cts)
        {
            if (cts.IsCancellationRequested)
            {
                return;
            }

            if (remainingPasswordLength == 0)
            {
                // Console.WriteLine(possiblePassword);
                if (password == possiblePassword)
                {
                    Console.WriteLine("Password found: " + possiblePassword);
                    cts.Cancel();
                }
                return;
            }

            for (int i = 0; i < charset.Count; ++i)
            {
                string newPossiblePassword = possiblePassword + charset[i];
                RecursiveSearch(newPossiblePassword, remainingPasswordLength - 1, cts);

                if (cts.IsCancellationRequested)
                {
                    return;
                }
            }
        }
    }
}
