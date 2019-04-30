using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TechnicalTest
{
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.        
        /// </summary>
        /// <param name="args">The arguments, expected in form DictionaryFileLocation FirstWord SecondWord OutputFileLocation</param>
        public static void Main(string[] args)
        {
            try
            {
                if (args.Length == 4)
                {
                    string fileLocation = args[0];
                    string firstWord = args[1];
                    string secondWord = args[2];
                    string outputFileLocation = args[3];

                    List<string> dictionary;
                    if (File.Exists(fileLocation))
                    {
                        dictionary = File.ReadAllLines(fileLocation).ToList();
                    }
                    else
                    {
                        Console.WriteLine($"Invalid File Location : {fileLocation}");
                        Console.ReadLine();
                        return;
                    }

                    string inputValidationResult = InputArgumentValidation(dictionary, firstWord, secondWord);
                    if (!string.IsNullOrEmpty(inputValidationResult))
                    {
                        Console.WriteLine(inputValidationResult);
                        Console.ReadLine();
                        return;
                    }

                    Console.WriteLine("Initial input validation successful. Beginning dictionary graph creation.");
                    DictionaryGraph graph = new DictionaryGraph(dictionary, firstWord.Length, new BreadthFirstSearch());

                    Console.WriteLine("Dictionary graph creation successful, beginning Breadth First Search to find shortest path.");

                    string searchResult = graph.GetShortestPath(firstWord, secondWord);

                    File.WriteAllLines(outputFileLocation, searchResult.Split(',').ToArray());

                    Console.WriteLine($"Results written to {outputFileLocation}");

                }
                else
                {
                    Console.WriteLine($"Invalid Argument Count.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Main.Program Error {ex.InnerException}");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Performs rudimentary validation on the input arguments. 
        /// </summary>
        private static string InputArgumentValidation(List<string> dictionary, string firstWord, string secondWord)
        {
            string result = string.Empty;

            if (dictionary.Count == 0)
            {
                result = "Dictionary File is empty.";
            }
            else if (firstWord.Length != secondWord.Length)
            {
                result = $"{firstWord} and {secondWord} are of differing lengths.";
            }

            return result;
        }
    }
}
