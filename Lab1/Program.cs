using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Lab1
{
    class Program
    {

        static void Main(string[] args)
        {
            Program prog = new Program();
            IList<string> words = null;
            Console.WriteLine("Hello World! This is Patrick Czermak's first C# Console Application :) ");

            while (true)
            {
                Console.WriteLine("\n\nMAIN MENU OPTIONS\n" +
                    "------------------------------------------------------------------------------------------------------------------------\n" +
                    "1 - Import Words from File\n" +
                    "2 - Bubble Sort words\n" +
                    "3 - LINQ/Lambda sort words\n" +
                    "4 - Coundt the Distinct Words\n" +
                    "5 - Take the first 10 words\n" +
                    "6 - Get the number of words that start with 'j' and display the count\n" +
                    "7 - Get and display the words that end with 'd' and display the count\n" +
                    "8 - Get and display the words that are greater than 4 characters long, and display the count\n" +
                    "9 - Get and display the words that are less than 3 characters long and start with the letter 'a', and display the count\n" +
                    "x - Exit\n" +
                    "Make a selection: ");

                string userInput = Console.ReadLine(); //variable to hold user input that's read from console
                Console.Clear();
                if (userInput == "1")
                {
                    Console.WriteLine("Reading Words\nReading Words complete");
                    words = prog.ImportFromFile();
                }
                else if (userInput == "2")
                {
                    prog.BubbleSort(words);
                }
                else if (userInput == "3")
                {
                    prog.LINQExpressionSort(words);
                }
                else if (userInput == "4")
                {
                    prog.DistinctWords(words);
                }
                else if (userInput == "5")
                {
                    prog.FirstTenWords(words);
                }
                else if (userInput == "6")
                {
                    prog.WordsStartingWithJ(words);
                }
                else if (userInput == "7")
                {
                    prog.WordsEndingWithD(words);
                }
                else if (userInput == "8")
                {
                    prog.WordsGreaterThanFourChars(words);
                }
                else if (userInput == "9")
                {
                    prog.WordsLessThanThreeCharsStartingWithA(words);
                }
                else if (userInput == "x")
                {
                    Environment.Exit(0);
                }
            }


        }//end main

        //1- import words from file - DONE
        private IList<string> ImportFromFile()
        {
            //System.IO.StreamReader file = new System.IO.StreamReader("Words.txt");
            string[] lines = System.IO.File.ReadAllLines("Words.txt");
            IList<string> words = new List<string>(lines);
            int totalWords = words.Count();
            System.Console.WriteLine("Number of words found: {0}", totalWords);
            return words;
        }


        // 2- bubble sort words - DONE
        private IList<string> BubbleSort(IList<string> words)
        {
            IList<string> tempWords = new List<string>(words);
            String temp;

            if (tempWords != null)
            {
                var wordsTemp = words.Count - 1;
                var time = new System.Diagnostics.Stopwatch();
                time.Start();

                for (int i = 0; i < wordsTemp; i++)
                {
                    for (int j = 0; j < wordsTemp - 1; j++)
                    {
                        if (tempWords[j].CompareTo(tempWords[j + 1]) > 0)
                        {
                            temp = tempWords[j];
                            tempWords[j] = tempWords[j + 1];
                            tempWords[j + 1] = temp;
                        }
                    }

                }
                time.Stop();
                Console.WriteLine($"Bubble Sort Time Elapsed: {time.ElapsedMilliseconds} ms");
                return tempWords;
            }
            else
            {
                Console.WriteLine("cannot perform bubble sort on empty list");
            }
            return null;
        }

        // 3- LINQ/Lambda sort words - DONE
        private IList<string> LINQExpressionSort(IList<string> words)
        {
            var time = new System.Diagnostics.Stopwatch();
            time.Start();

            IList<string> tempWords = new List<string>();
            tempWords = words.OrderBy(x => x).ToList();

            time.Stop();
            Console.WriteLine($"Lambda Sort Time Elapsed: {time.ElapsedMilliseconds} ms");
            return tempWords;
        }

        // 4- count distinct words - DONE
        private IList<string> DistinctWords(IList<string> words)
        {
            int dc = words.Distinct().Count();
            Console.WriteLine("The number of distinct words is: {0}", dc);
            return null;
        }

        // 5- take first 10 words - DONE
        private IList<string> FirstTenWords(IList<string> words)
        {
            var firstTen = words.Take(10).ToList();

            foreach (var fT in firstTen)
            {
                Console.WriteLine("{0}", fT);
            }
            return null;
        }

        // 6- number of words starting with 'j' and display count
        private IList<string> WordsStartingWithJ(IList<string> words)
        {
            int counter = 0;
            var startingWithJ = from wrd in words
                                where wrd.StartsWith("j")
                                select wrd;

            foreach (var swJ in startingWithJ)
            {
                Console.WriteLine("{0}", swJ);
                counter++;
            }
            Console.WriteLine("Number of words that start with 'j': {0}", counter);
            return null;
        }

        // 7- display words ending with 'd' and display count - DONE
        private IList<string> WordsEndingWithD(IList<string> words)
        {
            int counter = 0;

            var endingWithD = from wrd in words
                              where wrd.EndsWith("d")
                              select wrd;

            foreach (var ewD in endingWithD)
            {
                Console.WriteLine("{0}", ewD);
                counter++;
            }
            Console.WriteLine("Number of words that end with 'd': {0}", counter);
            return null;
        }

        // 8- display words greater than 4 characters long and display count - DONE
        private IList<string> WordsGreaterThanFourChars(IList<string> words)
        {
            int counter = 0;

            var greaterThan4Chars = words.Where(x => x.Length >= 4);

            foreach (var gt4C in greaterThan4Chars)
            {
                Console.WriteLine("{0}", gt4C);
                counter++;
            }
            Console.WriteLine("Number of words greater than 4 characters: {0}", counter);
            return null;
        }

        // 9- display words less than 3 characters long and start with 'a' and display count - DONE
        private IList<string> WordsLessThanThreeCharsStartingWithA(IList<string> words)
        {
            int counter = 0;

            var lessThan3CharsStartWithA = from wrd in words
                                           where wrd.StartsWith("a") && wrd.Length < 3
                                           select wrd;

            foreach (var lt3cswA in lessThan3CharsStartWithA)
            {
                Console.WriteLine("{0}", lt3cswA);
                counter++;
            }
            Console.WriteLine("Number of words less than 3 characters and starting with 'a': {0}", counter);
            return null;
        }

    } //end class
}//end namespace
