using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    interface Board
    {
        void printContent();
        void populateBoard(int difficulty);
        void getBounds();
    }
}
