using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AIMLbot;
using System.Threading;


namespace ConsoleBot
{
    public class Program
    {

        static void Main(string[] args)
        {
            string dictionary = File.ReadAllText("Dictionary.txt");

            //Initializing classes
            Bot myBot = new Bot();
            Spelling spelling = new Spelling(dictionary);

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

            Console.WriteLine("-------------------------Mentalist Machine!-------------------------");
            //While the bot is accepting user input
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n\nYou: ");
                Console.ResetColor();

                string userInput = Console.ReadLine();                
                string input = "";

                //checking the spelling of the userInput
                foreach (string item in userInput.Split(' '))
                {
                    input += " " + spelling.Correct(item);
                }
                string output;
                
                //If the user enters 'quit', the loop breaks out and exits the program.
                //Otherwise continue with AI manipulation
                if (input.ToLower() == " quit")
                {
                    break;
                }
                else
                {
                    Request r = new Request(removePunctuation(input), myUser, myBot);
                    Result res = myBot.Chat(r);

                    Console.ForegroundColor = ConsoleColor.Red;
                    //Thread.Sleep(500);
                    Console.WriteLine("The Mentalist is writing... ");

                    output = res.Output;
                    //Thread.Sleep(output.Length * 125);

                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    clearCurrentConsoleLine();
                    Console.Write("Mentalist: "); Console.ResetColor(); Console.Write(res.Output);
                }
            }
        }

        //Reference: http://stackoverflow.com/questions/8946808/can-console-clear-be-used-to-only-clear-a-line-instead-of-whole-console
        public static void clearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        //Reference: https://msdn.microsoft.com/en-us/library/system.text.regularexpressions.regex(v=vs.110).aspx
        public static string removePunctuation(string input)
        {
            input = Regex.Replace(input, @"[^\w\s]", string.Empty);
            return input;
        }
    }
}
