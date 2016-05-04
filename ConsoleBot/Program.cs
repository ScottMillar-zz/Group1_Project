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
            //Set the console size and height

            //Read the dictionary into the program
            string dictionary = File.ReadAllText("Dictionary.txt");

            //Change the title of the console window
            Console.Title = "The Mentalist";

            //Initializing classes
            Bot myBot = new Bot();

            //Declare new class
            Spelling spelling = new Spelling(dictionary);

            //Load settings for the bot
            myBot.loadSettings();

            //Create a new instance of the user interacting with the bot
            User myUser = new User("consoleUser", myBot);

            //Declare new random object
            Random rand = new Random();

            //Random hello = new Random();

            //string[] helloResponses = new string[] { "Hello", "Hello there", "Hi there", "Hi" };

            //Loads AIML from files
            myBot.loadAIMLFromFiles();

            //Accept user input to manipulate the AI
            myBot.isAcceptingUserInput = true;

            //Asthetic apperance for the console
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();

           // Console.ReadLine();
            //Used to pretend that the mentalist is reading the message before showing 'The Mentalist is writing...'
           // Thread.Sleep(helloResponses.Length * rand.Next(150, 300));
           // Console.WriteLine("The Mentalist is writing... ");


            //Thread.Sleep(helloResponses.Length * rand.Next(80, 150));

           // Console.SetCursorPosition(0, Console.CursorTop - 1);
            //clearCurrentConsoleLine();
            //Console.Write("Mentalist: "); Console.Write(helloResponses[hello.Next(3)]);

            //While the bot is accepting user input

            while (true)
            {
                Console.Write("\n\nYou: ");

                string userInput = Console.ReadLine();
                string input = "";
                //string input = userInput;

               //checking the spelling of the userInput
               foreach (string item in userInput.Split(' '))
               {
                  input += " " + spelling.Correct(item);
               }
               //Console.WriteLine(input);
               string output;
                
                //If the user enters 'quit', the loop breaks out and exits the program.
                //Otherwise continue with AI manipulation
                if (input.ToLower() == " quit" || input.ToLower() == " exit")
                {
                    break;
                }
                else
                {
                    Request r = new Request(removePunctuation(input), myUser, myBot);
                    Result res = myBot.Chat(r);

                    //Used to pretend that the mentalist is reading the message before showing 'The Mentalist is writing...'
                    Thread.Sleep(userInput.Length * rand.Next(150, 300));
                    Console.WriteLine("The Mentalist is writing... ");

                    output = res.Output;
                    Thread.Sleep(output.Length * rand.Next(80, 150));

                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    clearCurrentConsoleLine();
                    Console.Write("Mentalist: "); Console.Write(res.Output);
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
