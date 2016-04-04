using System;
using AIMLbot;
using System.Threading;

namespace ConsoleBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot myBot = new Bot();
            //Load settings for the bot
            myBot.loadSettings();
            //Create a new instance of the user interacting with the bot
            User myUser = new User("consoleUser", myBot);

            //False to allow the AIML from files
            //myBot.isAcceptingUserInput = false;

            //Loads AIML from files
            myBot.loadAIMLFromFiles();

            //Accept user input to manipulate the AI
            myBot.isAcceptingUserInput = true;

            Console.WriteLine("-------------------------Kieran's Chatbot!-------------------------");
            //While the bot is accepting user input
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n\nYou: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                string output;
                
                //If the user enters 'quit', the loop breaks out and exits the program.
                //Otherwise continue with AI manipulation
                if (input.ToLower() == "quit")
                {
                    break;
                }
                else
                {
                    Request r = new Request(input, myUser, myBot);
                    Result res = myBot.Chat(r);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bot is writing... ");

                    output = res.Output;
                    Thread.Sleep(output.Length * 125);

                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    ClearCurrentConsoleLine();
                    Console.Write("Bot: "); Console.ResetColor(); Console.Write(res.Output);
                }
            }
        }

        //Reference: http://stackoverflow.com/questions/8946808/can-console-clear-be-used-to-only-clear-a-line-instead-of-whole-console
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
