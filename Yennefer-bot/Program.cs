using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yennefer_bot
{
    internal static class Program
    {
        private static CancellationTokenSource TokenSource = new CancellationTokenSource();
        private static CancellationToken Token = TokenSource.Token;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            MultiTask().Wait();
        }

        //Method to Run Forever Until Cancelled
        private static void ExecuteStrategy()
        {
            while (!Token.IsCancellationRequested)
            {
                //Kill Key Value
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.X)
                    {
                        TokenSource.Cancel();
                    }
                }
              
            }
        }

        //Create New Task to run. 
        static async Task MultiTask()
        {
            var testThread = new Task(ExecuteStrategy);
            testThread.Start();
            await testThread;
         
        }
    }
}
