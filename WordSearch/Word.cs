using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.IO;

namespace WordSearch
{
    class Word
    {
        private int length;
        private char[] characters;

        public int Length { get { return length; } }
        public char[] Characters { get { return characters; } }

        public Word(string newWord) {

            length = newWord.Length;
            characters = newWord.ToUpper().ToCharArray();
        }

        public string getReversedWord()
        {
            char[] temp = characters;
            Array.Reverse(temp);

            return new string(temp);
        }

        public string getWordFromChars()
        {
            return new string(characters);
        }

        public string getWordFromChars(char[] chars)
        {
            return new string(chars);
        }

        public char getFirstChar()
        {
            return characters[0];
        }

        public static bool validateWord(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z]+$");
        }

        public bool compareCharsToWord(char[] compareMe)
        {
            bool match = true;

            // Loop through word characters comparing to compareMe characters
            for (int i = 0; i < length; i++)
            {
                if (characters[i] != compareMe[i])
                {
                    match = false;
                    break;
                }
            }

            return match;
        }

        public static void saveWordToFile(string word)
        {
            // Hardcoded file path
            var filePath = System.AppContext.BaseDirectory + "matched_words.txt";

            // Save line to file
            TextWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine(word);
            writer.Close();
        }
    }
}
