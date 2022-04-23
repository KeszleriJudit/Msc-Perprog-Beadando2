using BruteForcePasswordCracker.Logic;
using BruteForcePasswordCracker.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BruteForcePasswordCracker
{
    class Program
    {

        static void Main(string[] args)
        {
            UserInputReader inputReader = new UserInputReader();
            UserInput userInput = inputReader.GetValidatedUserInput();

            TaskManager taskManager = new TaskManager(userInput.Charset);
            CancellationTokenSource cts = new CancellationTokenSource();

            Stopwatch timer = new Stopwatch();
            timer.Start();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < userInput.NumberOfThreads; i++)
            {
                Generator generator = new Generator(userInput);
                tasks.Add(new Task(() => generator.GenerateAndSearchPassword(taskManager, cts), cts.Token, TaskCreationOptions.LongRunning));
            }

            for (int i = 0; i < userInput.NumberOfThreads; i++)
            {
                tasks[i].Start();
            }

            for (int i = 0; i < userInput.NumberOfThreads; i++)
            {
                tasks[i].Wait();
            }
            timer.Stop();
            Console.WriteLine(timer.Elapsed);
            Console.ReadLine();
        }
    }
}
