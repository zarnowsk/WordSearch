using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    class Level
    {
        List<char> charset;
        int charsetSize;

        public List<char> Charset { get { return charset; } }
        public int CharsetSize { get { return charsetSize; } }

        char[] vowels = { 'A', 'E', 'I', 'O', 'U' };
        char[] consonants = { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L',
            'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z' };

        public Level(int size)
        {
            charset = new List<char>();
            charsetSize = size;
        }

        public virtual List<char> getCharset()
        {
            Random rand = new Random();

            // Get a third and two thirds of the size
            int third = charsetSize / 3;
            int twoThirds = charsetSize - third;

            // Fill a third of the list with random vowels
            for (int i = 0; i < third; i++)
            {
                int temp = rand.Next(0, vowels.Length);
                charset.Add(vowels[temp]);
            }

            // Fill two thirds of the list with random consonants
            for (int i = 0; i < twoThirds; i++)
            {
                int temp = rand.Next(0, consonants.Length);
                charset.Add(consonants[temp]);
            }

            // Shuffle the list
            shuffle(charset);

            return charset;
        }

        private void shuffle(List<char> list)
        {
            Random rand = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                char value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
