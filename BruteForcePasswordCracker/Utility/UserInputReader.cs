using System;
using System.Collections.Generic;

namespace BruteForcePasswordCracker.Utility
{
    class UserInputReader
    {
        public UserInputReader() { }

        public UserInput GetValidatedUserInput()
        {
            int charsetCode;
            bool passwordIsValid = false;
            UserInput userInput = new UserInput();

            do
            {
                userInput.Password = ReadPassword();
                charsetCode = ReadCharsetCode();
                userInput.Charset = DetermineSelectedCharset(charsetCode);
                passwordIsValid = CheckPassword(userInput);

            } while (!passwordIsValid);

            userInput.NumberOfThreads = GetNumberOfThreads();

            return userInput;
        }

        private string ReadPassword()
        {
            string password;
            do
            {
                Console.WriteLine("Enter the password:");
                password = Console.ReadLine();

            } while (password == "");
            
            return password;
        }

        private int ReadCharsetCode()
        {
            int charsetCode = 0;
            do
            {
                Console.WriteLine("Choose the password charset: \n [1] - Only lowercase characters \n [2] - Lowercase characters and numbers \n [3] - ASCII");
                charsetCode = Convert.ToInt32(Console.ReadLine());

            } while (charsetCode < 1 || charsetCode > 3);

            return charsetCode;
        }

        private List<char> DetermineSelectedCharset(int selectedCharset)
        {
            
            List<char> charset = new List<char>();
            for (int i = 97; i < 123; i++)
            {
                charset.Add(Convert.ToChar(i));
            }

            if(selectedCharset >= 2)
            {
                for (int i = 48; i < 58; i++)
                {
                    charset.Add(Convert.ToChar(i));
                }
            }

            if (selectedCharset == 3)
            {
                for (int i = 33; i < 48; i++)
                {
                    charset.Add(Convert.ToChar(i));
                }
                for (int i = 58; i < 97; i++)
                {
                    charset.Add(Convert.ToChar(i));
                }
                for (int i = 123; i < 127; i++)
                {
                    charset.Add(Convert.ToChar(i));
                }
            }

            return charset;
        }

        private bool CheckPassword(UserInput userInput)
        {
            foreach (char passwordChar in userInput.Password)
            {
                bool passwordCharIsFound = false;
                foreach (char character in userInput.Charset)
                {
                    if (passwordChar.Equals(character))
                    {
                        passwordCharIsFound = true;
                        break;
                    }
                }

                if (!passwordCharIsFound)
                {
                    Console.WriteLine("The selected charset does not match to the password's charset.");
                    return false;
                }
            }

            return true;
        }

        private int GetNumberOfThreads()
        {
            int numberOfThreads = 0;

            do
            {
                Console.WriteLine("Choose the number of threads [1-12]:");
                numberOfThreads = Convert.ToInt32(Console.ReadLine());

            } while (numberOfThreads < 1 || numberOfThreads > 12);

            return numberOfThreads;
        }
    }
}
