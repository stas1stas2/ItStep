using System;
using System.Text;

namespace TitleCapitalizationTool
{
    internal class Program
    {
        internal static void Main()
        {
            while (true)
            {
                string[] articles = { "a", "an", "the" };
                string[] conjunctions = { "and", "but", "for", "not", "so", "yet" };
                string[] prepositions = { "at", "by", "in", "of", "on", "or", "out", "to", "up" };
                string[][] allWords = { articles, conjunctions, prepositions };
                string currentString = Console.ReadLine();
                currentString = currentString.Trim();
                currentString = RemoveDoubleSpace(currentString);
                currentString = NormalizeSpacing(currentString);
                currentString = ChangeFirstLetter(currentString, allWords);
                Console.WriteLine(currentString);
            }
        }

        private static string RemoveDoubleSpace(string inputString)
        {
            var currentString = new StringBuilder();
            currentString.Append(inputString);
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
            var returnValue = currentString.ToString();
            return returnValue;
        }

        private static string NormalizeSpacing(string inputString)
        {
            var currentString = new StringBuilder();
            currentString.Append(inputString);
            int lengthOfCurrentString = currentString.Length;

            for (int i = 1; i < lengthOfCurrentString; i++)
            {
                if ((currentString[i] == '.' || currentString[i] == ',' || currentString[i] == ':' || currentString[i] == ';' || currentString[i] == '!' || currentString[i] == '?') && currentString[i - 1] == ' ')
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
            return currentString.ToString();
        }

        private static string ChangeFirstLetter(string inputString, string[][] symbolsWithLowRegister)
        {
            var currentString = new StringBuilder();
            currentString.Append(inputString);
            int lengthOfCurrentString = currentString.Length;
            bool isSymbolWithLowRegister = false;
            int firstSymbolOfCurrentString = -1;
            StringBuilder word = new StringBuilder();

            for (int i = 0, wordLength = 0; i <= lengthOfCurrentString; i++, wordLength++)
            {
                if (i == lengthOfCurrentString || currentString[i] == ' ')
                {
                    string stringWord = word.ToString();
                    for (int index = 0; index < 3; index++)
                    {
                        foreach (string keyword in symbolsWithLowRegister[index])
                        {
                            if (stringWord.Equals(keyword))
                            {
                                isSymbolWithLowRegister = true;
                            }
                        }
                    }
                    if (isSymbolWithLowRegister == true)
                    {
                        var cycleStr = word.ToString();
                        if (firstSymbolOfCurrentString == 0 || i == lengthOfCurrentString)
                        {
                            currentString[firstSymbolOfCurrentString] = Char.ToUpper(word[0]);
                            for (int index = 1; index < cycleStr.Length; index++)
                            {
                                currentString[firstSymbolOfCurrentString + index] = Char.ToLower(word[index]);
                            }

                        }
                        else
                        {
                            for (int index = 0; index < cycleStr.Length; index++)
                            {
                                currentString[firstSymbolOfCurrentString + index] = Char.ToLower(word[index]);
                            }
                        }
                        isSymbolWithLowRegister = false;
                    }
                    else
                    {
                        currentString[firstSymbolOfCurrentString] = Char.ToUpper(word[0]);
                        var cycleStr = word.ToString();

                        for (int index = 1; index < cycleStr.Length; index++)
                        {
                            currentString [firstSymbolOfCurrentString + index] = Char.ToLower(word[index]);
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
            return currentString.ToString();
        }
    }
}

