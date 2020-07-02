using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Text;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WordSearch
{
    public class ColorPresets
    {
        private static BrushConverter converter = new System.Windows.Media.BrushConverter();

        // DARK THEME COLOR PALLETTE
        public static Brush DarkForeground { get { return (Brush)converter.ConvertFromString("#FF66FCF1"); } }
        public static Brush DarkButtonBackground { get { return (Brush)converter.ConvertFromString("#FF0B0C10"); } }
        public static Brush DarkTextBackground { get { return (Brush)converter.ConvertFromString("#FF2C2C2C"); } }
        public static Brush DarkBorder { get { return (Brush)converter.ConvertFromString("#FF45A29E"); } }
        public static Brush DarkHover { get { return (Brush)converter.ConvertFromString("#3E3F40"); } }
        public static Brush DarkBoard { get { return (Brush)converter.ConvertFromString("#FF1F2833"); } }

        // BRIGHT THEME COLOR PALLETTE
        public static Brush BrightForeground { get { return (Brush)converter.ConvertFromString("#026670"); } }
        public static Brush BrightButtonBackground { get { return (Brush)converter.ConvertFromString("#FEF9C7"); } }
        public static Brush BrightTextBackground { get { return (Brush)converter.ConvertFromString("#FEF9C7"); } }
        public static Brush BrightBorder { get { return (Brush)converter.ConvertFromString("#FCE181"); } }
        public static Brush BrightHover { get { return (Brush)converter.ConvertFromString("#E8CF74"); } }
        public static Brush BrightBoard { get { return (Brush)converter.ConvertFromString("#EDEAE5"); } }

        // AUTUMN THEME COLOR PALLETTE
        public static Brush AutumnForeground { get { return (Brush)converter.ConvertFromString("#DE4055"); } }
        public static Brush AutumnButtonBackground { get { return (Brush)converter.ConvertFromString("#97AABD"); } }
        public static Brush AutumnTextBackground { get { return (Brush)converter.ConvertFromString("#97AABD"); } }
        public static Brush AutumnBorder { get { return (Brush)converter.ConvertFromString("#314455"); } }
        public static Brush AutumnHover { get { return (Brush)converter.ConvertFromString("#7C8C9C"); } }
        public static Brush AutumnBoard { get { return (Brush)converter.ConvertFromString("#644E5B"); } }
    }
}
