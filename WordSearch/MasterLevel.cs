using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    class MasterLevel:Level
    {

        public MasterLevel(int size): base(size)
        {
            
        }

        public override List<char> getCharset()
        {
            Random rand = new Random();

            // Add upper case characters created at random to list
            for (int i = 0; i < (base.CharsetSize); i++)
            {
                int temp = rand.Next(65, 91);
                Charset.Add((char)temp);
            }

            return Charset;
        }
    }
}
