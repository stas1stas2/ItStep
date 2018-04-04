using System;
using System.Text;

namespace TitleCapitalizationTool
{
    internal class Program
    {
        internal static void Main()
        {
            string[] articles = { "a", "an", "the" };
            string[] conjunctions = { "and", "but", "for", "not", "so", "yet" };
            string[] prepositions = { "at", "by", "in", "of", "on", "or", "out", "to", "up" };
            string[][] allWords = { articles, conjunctions, prepositions };
            string str = Console.ReadLine();
            str = str.Trim();
            str = RemoweDoubleSpaceFromString(str);
            str = SymbolsSpacing(str);

            str = ChangeFirstLiteral(str, allWords);
            Console.WriteLine(str);
        }

        private static string RemoweDoubleSpaceFromString(string inputString)
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
        private static string SymbolsSpacing(string inputString)
        {
            var str = new StringBuilder();
            str.Append(inputString);
            int strLength = str.Length;

            for (int i = 1; i < strLength; i++)
            {
                if ((str[i] == '.' || str[i] == ',' || str[i] == ':' || str[i] == ';' || str[i] == '!' || str[i] == '?') && str[i - 1] == ' ')
                {
                    if (i + 1 < strLength)
                    {
                        if (str[i + 1] == ' ')
                        {
                            str.Remove(i - 1, 1);
                            strLength--;
                        }
                        else
                        {
                            char c = str[i];
                            str[i] = ' ';
                            str[i - 1] = c;
                        }
                    }
                    else
                    {
                        str.Remove(i - 1, 1);
                        strLength--;
                    }
                }
 
                if (str[i] == '-')
                {
                    if (str[i - 1] != ' ')
                    {
                        str.Insert(i, ' ');
                        strLength++;
                        i++;
                    }
                    if (i + 1 < strLength)
                        if (str[i + 1] != ' ')
                        {
                            str.Insert(i + 1, ' ');
                            strLength++;
                            i++;
                        }
                }
            }

            return str.ToString();
        }
        private static string ChangeFirstLiteral(string inputString, string[][] keyWords)
        {
            var str = new StringBuilder();
            str.Append(inputString);
            int strLength = str.Length;
            bool IsIncludedInMass = false;
            int firstSymb = -1;
            StringBuilder word = new StringBuilder();

            for (int i = 0, wordLength = 0; i < strLength; i++, wordLength++)
            {
                if (str[i] == ' ' || i + 1 == strLength)
                {
                    string stringWord = word.ToString();
                    foreach (string keyword in keyWords[0])
                    {
                        if (stringWord.Equals(keyword))
                        {
                            IsIncludedInMass = true;
                        }
                    }
                    foreach (string keyword in keyWords[1])
                    {
                        if (stringWord.Equals(keyword))
                        {
                            IsIncludedInMass = true;
                        }
                    }
                    foreach (string keyword in keyWords[2])
                    {
                        if (stringWord.Equals(keyword))
                        {
                            IsIncludedInMass = true;
                        }
                    }
                    if (IsIncludedInMass == true)
                    {
                        IsIncludedInMass = false;
                        continue;
                    }
                    else
                    {
                        if (str[firstSymb] >= 97 && str[firstSymb] <= 122)
                            str[firstSymb] = Char.ToUpper(str[firstSymb]);
                    }
                    firstSymb = -1;
                    wordLength = -1;
                    word.Clear();
                    continue;
                }
                if (firstSymb == -1)
                    firstSymb = i;
                word.Insert(wordLength, str[i]);
            }

            return str.ToString();
        }
    }
}