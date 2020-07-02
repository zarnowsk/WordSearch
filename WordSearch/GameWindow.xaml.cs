using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WordSearch
{
    /// <summary>
    /// Interaction logic for GameWinow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private List<Label> labels;
        private LetterBoard letterBoard;
        private Word word;
        private int difficulty;
        private bool displayReversedWord = false;
        private string theme;

        public GameWindow(int size, int level)
        {
            InitializeComponent();

            // Set game difficulty based on user selection
            difficulty = level;

            // Crate instance of the game board
            letterBoard = new LetterBoard(size, difficulty);

            // Divide display grid into rows and columns based on size entered by the user
            double fieldSize = 575 / size;
            
            for (int i = 0; i < size; i++)
            {
                ColumnDefinition cDef = new ColumnDefinition();
                cDef.Width = new GridLength(fieldSize);
                displayGrid.ColumnDefinitions.Add(cDef);

                RowDefinition rDef = new RowDefinition();
                rDef.Height = new GridLength(fieldSize);
                displayGrid.RowDefinitions.Add(rDef);
            }

            // Initialze game board with labels to display letters
            initCharacterBoard(size);

            // Populate game board with characters from the LetterBoard instance
            for (int i = 0; i < labels.Count; i++)
            {
                labels[i].Content = letterBoard.Content[i];
            }

            // Initialize dark theme (default)
            darkThemeClick(null, null);
        }

        public void initCharacterBoard(int size)
        {
            labels = new List<Label>();
            int counter = 0;

            // Populate the display area with labels to hold single character values
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Label lb = new Label();
                    lb.Name = string.Format("charDisplayLbl{0}", counter);
                    lb.HorizontalContentAlignment = HorizontalAlignment.Center;
                    lb.VerticalAlignment = VerticalAlignment.Stretch;
                    lb.VerticalContentAlignment = VerticalAlignment.Center;
                    Grid.SetRow(lb, i);
                    Grid.SetColumn(lb, j);
                    displayGrid.Children.Add(lb);
                    labels.Add(lb);

                    counter++;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Clear any user feedback
            clearFeedback();

            // Regenerate letters and populate the board
            letterBoard.populateBoard(difficulty);
            for (int i = 0; i < labels.Count; i++)
            {
                labels[i].Content = letterBoard.Content[i];
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Clear any user feedback
            clearFeedback();

            string inputWord = wordInputTb.Text;

            // Validate user entry
            if (inputWord.Length < 1)
            {
                MessageBox.Show("Please enter a word in the text box!", "Input error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (inputWord.Length > letterBoard.Height)
            {
                MessageBox.Show("Entered word doesn't fit on the board!", "Input error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (!Word.validateWord(inputWord)) 
            {
                MessageBox.Show("Please enusre your word doesn't contain numbers or any special characters!",
                    "Input error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                word = new Word(inputWord);
                searchBoard();
            }
        }

        private void clearFeedback()
        {
            // Clear labels of any highlighting
            for (int i = 0; i < labels.Count; i++)
            {
                labels[i].Background = Brushes.Transparent;
            }

            // Clear the matches label
            noMatchesLbl.Content = "";
        }

        private void searchBoard()
        {
            char firstChar = word.getFirstChar();
            bool matchedWord = false;

            // Loop through each letter on the board
            for (int i = 0; i < letterBoard.Content.Length; i++)
            {
                // If current letter matches user's input first letter
                if (letterBoard.Content[i] == firstChar)
                {
                    // Check if the whole word matches
                    List<int> matchedBoardDirection = confirmWordMatch(i);

                    // If word was matched, highlight the correct letters, otherwise highlight all characters red
                    if (matchedBoardDirection.Count > 0)
                    {
                        for (int j = 0; j < matchedBoardDirection.Count; j++)
                        {
                            highlightMatchedWord(i, matchedBoardDirection[j]);
                        }
                        matchedWord = true;
                    }
                }
            }

            // If word was matched, display it in the list, else inform the user
            if (matchedWord)
            {
                saveMatchedWord();
            }
            else
            {
                showNoMatch();
            }
        }

        private List<int> confirmWordMatch(int position)
        {
            List<char[]> tempList = letterBoard.getCharactersFromPosition(position);
            List<int> matchedDirections = new List<int>();

            // Loop through all letter combinations on the board
            for (int i = 0; i < tempList.Count; i++)
            {
                char[] tempArray = tempList[i];

                // Skip any board combinations shorter than inputted word
                if (tempArray.Length < word.Length)
                {
                    continue;
                }

                // Resize board words to match word length
                Array.Resize(ref tempArray, word.Length);

                if (word.compareCharsToWord(tempArray))
                {
                    matchedDirections.Add(i);
                }
            }

            return matchedDirections;
        }

        private void highlightMatchedWord(int startPosition, int direction)
        {
            int cursor = startPosition;

            // Loop through every character in found word
            for (int i = 0; i < word.Length; i++)
            {
                // Change background of current character and adjust cursor to 
                // next position based on the direction of the word (characterList order
                // from LetterBoard)
                labels[cursor].Background = Brushes.LimeGreen;
                switch (direction)
                {
                    case 0:
                        cursor++;
                        break;
                    case 1:
                        cursor--;
                        break;
                    case 2:
                        cursor -= letterBoard.Height;
                        break;
                    case 3:
                        cursor += letterBoard.Height;
                        break;
                    case 4:
                        cursor -= (letterBoard.Height - 1);
                        break;
                    case 5:
                        cursor -= (letterBoard.Height + 1);
                        break;
                    case 6:
                        cursor += (letterBoard.Height + 1);
                        break;
                    case 7:
                        cursor += (letterBoard.Height - 1);
                        break;
                }
            }
        }

        private void showNoMatch()
        {
            // Populate matches label with text
            noMatchesLbl.Content = "NO MATCHES FOUND";
        }

        private void saveMatchedWord()
        {
            // If current word not already in the matched words list, add it
            if (!matchedWordList.Items.Contains(word.getWordFromChars()))
            {
               matchedWordList.Items.Add(word.getWordFromChars());
            }

            // If displayReversedWord option is toggled on, display the matched
            // word in revese in the ListBox containing matched words
            if (displayReversedWord)
            {
                string reversedWord = word.getReversedWord();
                if (!matchedWordList.Items.Contains(reversedWord))
                {
                    matchedWordList.Items.Add(reversedWord);
                }
            }
        }

        private void brightThemeClick(object sender, RoutedEventArgs e)
        {
            // Initialize bright theme
            ResourceDictionary brightSkin = Application.LoadComponent(new Uri("BrightTheme.xaml", UriKind.Relative)) as ResourceDictionary;
            Resources.MergedDictionaries.Add(brightSkin);
            theme = "bright";

            // Recolor labels to match theme
            for (int i = 0; i < labels.Count; i++)
            {
                labels[i].Foreground = ColorPresets.BrightForeground;
            }
        }

        private void darkThemeClick(object sender, RoutedEventArgs e)
        {
            // Initialize bright theme
            ResourceDictionary darkSkin = Application.LoadComponent(new Uri("DarkTheme.xaml", UriKind.Relative)) as ResourceDictionary;
            Resources.MergedDictionaries.Add(darkSkin);
            theme = "dark";

            // Recolor labels to match theme
            for (int i = 0; i < labels.Count; i++)
            {
                labels[i].Foreground = ColorPresets.DarkForeground;
            }
        }

        private void autumnThemeClick(object sender, RoutedEventArgs e)
        {
            // Initialize autumn theme
            ResourceDictionary autumnSkin = Application.LoadComponent(new Uri("AutumnTheme.xaml", UriKind.Relative)) as ResourceDictionary;
            Resources.MergedDictionaries.Add(autumnSkin);
            theme = "autumn";

            // Recolor labels to match theme
            for (int i = 0; i < labels.Count; i++)
            {
                labels[i].Foreground = ColorPresets.AutumnForeground;
            }
        }

        private void quitGame(object sender, RoutedEventArgs e)
        {
            // Display a messaga box to confirm quitting the game
            MessageBoxResult result = MessageBox.Show("Are you sure you want to quit?", "Quit Game?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // If yes is pressed, close the window
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void clearMatchedWords(object sender, RoutedEventArgs e)
        {
            // Check if list contains items
            if (matchedWordList.Items.Count > 0)
            {
                // Display a messaga box to confirm clearing matched words
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete all matched words?",
                    "Clear Matched Words", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                // If yes is pressed, clear the list
                if (result == MessageBoxResult.Yes)
                {
                    matchedWordList.Items.Clear();
                }
            }
            else
            {
               MessageBox.Show("Matched Words list is currently empty",
                    "Clear Matched Words", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void saveMatchedWords(object sender, RoutedEventArgs e)
        {
            // Check if list contains items
            if (matchedWordList.Items.Count > 0)
            {
                for (int i = 0; i < matchedWordList.Items.Count; i++)
                {
                    Word.saveWordToFile(matchedWordList.Items[i].ToString());
                }

                MessageBox.Show("Your matched words have been saved to a file." +
                    " Please check the folder containing this application for 'matched_word.txt' file.", "Save Matched Words",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Matched Words list is currently empty",
                     "Save Matched Words", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void toggleDisplayReversedWord(object sender, RoutedEventArgs e)
        {
            // Toggle class level displayReversedWord variable
            if (reversedWordChk.IsChecked)
            {
                displayReversedWord = true;
            }
            else
            {
                displayReversedWord = false;
            }
        }

        private void displayHelpWindow(object sender, RoutedEventArgs e)
        {
            GameWindowHelp helpWindow = new GameWindowHelp(theme);
            helpWindow.Show();
        }
    }
}
