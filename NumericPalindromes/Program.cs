using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NumericPalindromes
{
    class Program
    {
        /// <summary>Get the file path of a file for input as specified from the console by the user and verify that the file exists.</summary>
        /// <returns>The file path as a string.</returns>
        private static string ConsoleRequestFilePath()
        {
            const string FILE_REQUEST_STRING = "Please enter the path of the file containing a list of potential numeric palindromes or enter q to quit.";
            
            Console.WriteLine(FILE_REQUEST_STRING);
            string input = Console.ReadLine();
            while (!File.Exists(input) && input.ToUpper() != "Q")
            {
                Console.WriteLine("File does not exist. Please try again.\n");
                Console.WriteLine(FILE_REQUEST_STRING);
                input = Console.ReadLine();
            }

            if (input.ToUpper() == "Q")
            {
                Environment.Exit(0);
            }

            return input;
        }

        /// <summary>Prompt the user to press enter, then exit.</summary>
        private static void ExitOnKeyPress()
        {
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            // Get the input file path from either the command arguments or the console.
            string inputPath = "";
            if (args.Length > 0 && File.Exists(args[0]))
            {
                inputPath = args[0];
            }
            else
            {
                inputPath = ConsoleRequestFilePath();
            }

            // Read the input file
            string[] numberFileLines;
            try
            {
                numberFileLines = File.ReadAllLines(inputPath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Failed to read input file \"{inputPath}\". Error message: {e.Message}");
                ExitOnKeyPress();
                return;
            }

            // Get the number from each line and add to a set to remove duplicates.
            SortedSet<int> numbers = new SortedSet<int>();
            for (int i = 0; i < numberFileLines.Length; i++)
            {
                // Using regex to extract number in case of commas, spaces, extra characters, etc.
                string numberString = Regex.Match(numberFileLines[i], @"\d+").Value;
                if (numberString == "")
                {
                    Console.WriteLine($"INFO: No number found on line {i}.");
                    continue;
                }

                // Note: invalid number values already removed by regex
                numbers.Add(int.Parse(numberString));
            }

            // Remove all numbers that do not have a palindrome descendant from the set.
            try
            {
                numbers.RemoveWhere(num => !PalindromeUtility.PalindromeDescendant(num));
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: A problem occurred while attempting to process the input numbers: {e.Message}");
                ExitOnKeyPress();
                return;
            }

            // Print sorted list of numbers to file
            string outputPath = $"{Path.GetDirectoryName(inputPath)}\\{Path.GetFileNameWithoutExtension(inputPath)}_output.txt";
            try
            {
                StreamWriter writer = new StreamWriter(outputPath);
                foreach (int number in numbers)
                {
                    writer.WriteLine(number);
                }
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Failed to write output to \"{outputPath}\". Error message: {e.Message}");
                ExitOnKeyPress();
                return;
            }

            Console.WriteLine($"\nProcess complete. Output was written to \"{outputPath}\".\n");
            ExitOnKeyPress();
        }
    }
}
