using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WordSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Initialize dark theme (default)
            ResourceDictionary darkSkin = Application.LoadComponent(new Uri("DarkTheme.xaml", UriKind.Relative)) as ResourceDictionary;
            Resources.MergedDictionaries.Add(darkSkin);
        }

        private void generateBoardBtn_Click(object sender, RoutedEventArgs e)
        {
            // Set game difficulty based on user input
            int difficulty = 1; //default easy difficulty
            if ((bool)masterDifficultyChBox.IsChecked)
            {
                difficulty = 9;
            }

            GameWindow gameWindow = new GameWindow(Convert.ToInt32(sizeSilder.Value), difficulty);

            this.Close();
            gameWindow.Show();
        }

        private void getSizeHelpBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindowHelp helpWindow = new MainWindowHelp();
            helpWindow.Show();
        }
    }
}
