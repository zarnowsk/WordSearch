using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for MainWindowHelp.xaml
    /// </summary>
    public partial class MainWindowHelp : Window
    {
        public MainWindowHelp()
        {
            InitializeComponent();

            // Initialize dark theme (default)
            ResourceDictionary darkSkin = Application.LoadComponent(new Uri("DarkTheme.xaml", UriKind.Relative)) as ResourceDictionary;
            Resources.MergedDictionaries.Add(darkSkin);

            sizeTitleLbl.Content = "BOARD SIZE SLIDER";
            sizeTextLbl.Content = "Use the slider to select the size of the board. The board will be \npopulated with selected amount of letters in a square grid.";

            difficultyTitleLbl.Content = "MASTER DIFFICULTY CHECKBOX";
            difficultyTextLbl.Content = "The game can be played in two difficulty levels:\nRegular difficulty - letter distribution with 1/3 vowels and 2/3\n" +
                "                               consonants.\nMaster difficulty - random letter distribution.";

            generateTitleLbl.Content = "GENERATE BUTTON";
            generateTextLbl.Content = "Once you are happy with your board size and difficulty selections,\n click the Generate button to initialize your board.";
        }
    }
}
