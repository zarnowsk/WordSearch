using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Linq;

namespace WordSearch
{
    class LetterBoard:Board
    {
        private int height;
        private int width;
        private char[] content;

        private int[] topBounds;
        private int[] bottomBounds;
        private int[] leftBounds;
        private int[] rightBounds;

        public int Height { get { return height; } }
        public int Widht { get { return width; } }
        public char[] Content { get { return content; } }

        public LetterBoard(int size, int level)
        {
            height = size;
            width = size;

            populateBoard(level);
            getBounds();

        }

        public void printContent() {

            // Compose fie name
            string fileName = "boardContent.txt";

            // Compose path to file
            var path = System.AppContext.BaseDirectory;
            var filePath = path + fileName;

            // Create a text writer
            TextWriter writer = new StreamWriter(filePath, true);

            int counter = 0;
            string saveString = "";

            // Write to file
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    saveString += (content[counter] + " ");
                    counter++;
                }
                writer.WriteLine(saveString);
                saveString = "";
            }
            writer.Close();
        }

        public void populateBoard(int difficulty) {

            Level level = null;

            if (difficulty == 9)
            {
                level = new MasterLevel(height * width);
            }
            else
            {
                level = new Level(height * width);
            }
            

            // Save to array
            content = level.getCharset().ToArray();
        }

        public void getBounds()
        {
            // Get positions of top row fields
            topBounds = new int[width];
            for (int i = 0; i < width; i ++)
            {
                topBounds[i] = i;
            }

            // Get positions of bottom row fields
            bottomBounds = new int[width];
            for (int i = ((width - 1) * width), c = 0; i < (width * width); i++, c++)
            {
                bottomBounds[c] = i;
            }

            // Get positions of left most column fields
            leftBounds = new int[height];
            for (int i = 0, c = 0; c < height; i += height, c++)
            {
                leftBounds[c] = i;
            }

            // Get positions of right most column fields
            rightBounds = new int[height];
            for (int i = (height - 1), c = 0; c < height; i += height, c++)
            {
                rightBounds[c] = i;
            }
        }

        public List<int> getCharPositions(char character)
        {
            //Get all board positions where character argument is found
            List<int> positions = new List<int>();
            for (int i = 0; i < content.Length; i++)
                if (content[i] == character)
                    positions.Add(i);

            return positions;
        }

        public List<char[]> getCharactersFromPosition(int position)
        {
            List<char[]> characterList = new List<char[]>();

            // Get characters to the right and left of the positon
            getCharactersFromDirection(rightBounds, "right", position, characterList);
            getCharactersFromDirection(leftBounds, "left", position, characterList);

            // Get characters upward and downward of the positon
            getCharactersFromDirection(topBounds, "up", position, characterList);
            getCharactersFromDirection(bottomBounds, "down", position, characterList);

            // Get characters in diagonal directions from the position
            getCharactersFromDiagonalDirection(rightBounds, topBounds, "upright", position, characterList);
            getCharactersFromDiagonalDirection(leftBounds, topBounds, "upleft", position, characterList);
            getCharactersFromDiagonalDirection(rightBounds, bottomBounds, "downright", position, characterList);
            getCharactersFromDiagonalDirection(leftBounds, bottomBounds, "downleft", position, characterList);

            return characterList;
        }
        
        private void getCharactersFromDirection(int[] bounds, string direction, int position, List<char[]> words)
        {
            
            int currentPosition = position;
            List<char> tempList = new List<char>();

            // If position is not on the edge of the board
            if (!bounds.Contains(position))
            {
                // Loop until reaching the edge of the board
                while (!bounds.Contains(currentPosition))
                {
                    // Add character at current position, increment current position based on direction
                    tempList.Add(content[currentPosition]);

                    switch (direction)
                    {
                        case "right":
                            currentPosition++;
                            break;
                        case "left":
                            currentPosition--;
                            break;
                        case "up":
                            currentPosition -= height;
                            break;
                        case "down":
                            currentPosition += height;
                            break;
                    }
                    
                }
                // Add character from reached edge
                tempList.Add(content[currentPosition]);
            }
            else
            {
                // If position argument already on the edge of the board, add the edge character
                tempList.Add(content[currentPosition]);
            }
            // Add characters to returned list
            char[] tempArray = tempList.ToArray();
            words.Add(tempArray);
        }

        private void getCharactersFromDiagonalDirection(int[] bounds1, int[] bounds2, string direction, int position, List<char[]> words)
        {
            int currentPosition = position;
            List<char> tempList = new List<char>();

            // If position is not on the edge of the board
            if ((!bounds1.Contains(position)) && (!bounds2.Contains(position)))
            {
                // Loop until reaching the edge of the board
                while ((!bounds1.Contains(currentPosition)) && (!bounds2.Contains(currentPosition)))
                {
                    // Add character at current position, increment current position based on direction
                    tempList.Add(content[currentPosition]);

                    switch (direction)
                    {
                        case "upright":
                            currentPosition -= (height - 1);
                            break;
                        case "upleft":
                            currentPosition -= (height + 1);
                            break;
                        case "downright":
                            currentPosition += (height + 1);
                            break;
                        case "downleft":
                            currentPosition += (height - 1);
                            break;
                    }
                }
                // Add character from reached edge
                tempList.Add(content[currentPosition]);
            }
            else
            {
                // If position argument already on the edge of the board, add the edge character
                tempList.Add(content[currentPosition]);
            }
            // Add characters to returned list
            char[] tempArray = tempList.ToArray();
            words.Add(tempArray);
        }
    }
}
