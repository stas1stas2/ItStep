using System;
using System.Text;

namespace TitleCapitalizationTool
{
    internal class Program
    {
        private static char[] charactersLineSeparation = { '.', ',', ':', ';', '!', '?' };
        internal static void Main()
        {
            string[] articles = { "a", "an", "the" };
            string[] conjunctions = { "and", "but", "for", "not", "so", "yet" };
            string[] prepositions = { "at", "by", "in", "of", "on", "or", "out", "to", "up" };
            string[][] allWords = { articles, conjunctions, prepositions };
            string askToEnter = "Enter title to capitalize: ";
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(askToEnter);

                Console.ForegroundColor = ConsoleColor.Red;
                string currentString;
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    currentString = Console.ReadLine();

                    if (currentString == "")
                    {
                        Console.SetCursorPosition(Console.CursorLeft + (askToEnter).Length, Console.CursorTop - 1);
                        continue;
                    }
                    break;
                }

                currentString = currentString.Trim();
                currentString = RemoveDoubleSpace(currentString);
                currentString = NormalizeSpacing(currentString);
                currentString = ChangeFirstLetter(currentString, allWords);

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Capitalized title: ");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(currentString);

                Console.Write("\n");
            }
        }

        private static string RemoveDoubleSpace(string inputString)
        {
            var currentString = new StringBuilder();
            currentString.Append(inputString);

            if (inputString.Length != 0)
            {
                int stringLength = currentString.Length;

                for (int i = 1; i < stringLength; i++)
                {
                    if (currentString[i] == ' ' && currentString[i - 1] == ' ')
                    {
                        currentString.Remove(i, 1);

                        i--;
                    }
                    stringLength = currentString.Length;
                }
            }
            return currentString.ToString();
        }

        private static string NormalizeSpacing(string inputString)
        {
            var currentString = new StringBuilder();
            currentString.Append(inputString);

            if (inputString.Length != 0)
            {
                int lengthOfCurrentString = currentString.Length;

                for (int i = 1; i < lengthOfCurrentString; i++)
                {
                    foreach (char characterLineSeparation in charactersLineSeparation)
                    {
                        if (currentString[i] == characterLineSeparation && currentString[i - 1] == ' ')
                        {
                            if (i + 1 < lengthOfCurrentString)
                            {
                                if (currentString[i + 1] == ' ')
                                {
                                    currentString.Remove(i - 1, 1);
                                    lengthOfCurrentString--;
                                }
                                else
                                {
                                    char swapChararcter = currentString[i];
                                    currentString[i] = ' ';
                                    currentString[i - 1] = swapChararcter;
                                }
                            }
                            else
                            {
                                currentString.Remove(i - 1, 1);
                                lengthOfCurrentString--;
                            }
                        }
                    }
                    if (i < lengthOfCurrentString && currentString[i] == '-')
                    {
                        if (currentString[i - 1] != ' ')
                        {
                            currentString.Insert(i, ' ');
                            lengthOfCurrentString++;
                            i++;
                        }
                        if (i + 1 < lengthOfCurrentString && currentString[i + 1] != ' ')
                        {
                            currentString.Insert(i + 1, ' ');
                            lengthOfCurrentString++;
                            i++;

                        }
                    }
                }
            }
            return currentString.ToString();
        }

        private static string ChangeFirstLetter(string inputString, string[][] symbolsWithLowRegister)
        {
            var currentString = new StringBuilder();
            currentString.Append(inputString);
            if (inputString.Length != 0)
            {
                int lengthOfCurrentString = currentString.Length;
                bool isSymbolWithLowRegister = false;
                int firstSymbolOfCurrentString = -1;
                StringBuilder word = new StringBuilder();

                for (int i = 0, wordLength = 0; i <= lengthOfCurrentString; i++, wordLength++)
                {
                    if (i == lengthOfCurrentString || currentString[i] == ' ')
                    {
                        string stringWord = word.ToString();
                        foreach (string[] massOfSymbolsWithLowRegister in symbolsWithLowRegister)
                        {
                            foreach (string keyword in massOfSymbolsWithLowRegister)
                            {
                                if (stringWord.Equals(keyword))
                                {
                                    isSymbolWithLowRegister = true;
                                }
                            }
                        }
                        if (isSymbolWithLowRegister == true)
                        {
                            int wordsLength = stringWord.Length;

                            for (int index = 0; index < wordsLength; index++)
                            {
                                currentString[firstSymbolOfCurrentString + index] = char.ToLower(word[index]);
                            }
                            if (firstSymbolOfCurrentString == 0 || i == lengthOfCurrentString)
                            {
                                currentString[firstSymbolOfCurrentString] = char.ToUpper(word[0]);
                            }

                            isSymbolWithLowRegister = false;
                        }
                        else
                        {
                            currentString[firstSymbolOfCurrentString] = char.ToUpper(word[0]);
                            var cycleStr = word.ToString();

                            for (int index = 1; index < cycleStr.Length; index++)
                            {
                                currentString[firstSymbolOfCurrentString + index] = char.ToLower(word[index]);
                            }
                        }
                        firstSymbolOfCurrentString = -1;
                        wordLength = -1;
                        word.Clear();
                        continue;
                    }
                    if (firstSymbolOfCurrentString == -1)
                    {
                        firstSymbolOfCurrentString = i;
                    }
                    word.Insert(wordLength, currentString[i]);
                }
            }
            return currentString.ToString();
        }
    }
}