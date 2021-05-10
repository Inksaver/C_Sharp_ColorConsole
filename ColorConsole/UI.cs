using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace ColorConsole
{
    #region Custom exception classes
    //Creating custom Exception Classes by inheriting Exception class
    class ColorValueException : Exception
    {
        public ColorValueException(string message) : base(message) { }
        public ColorValueException(string message, Exception innerException) : base(message, innerException) { }
    }
    class MatchingTagsException : Exception
    {
        public MatchingTagsException(string message) : base(message) { }
        public MatchingTagsException(string message, Exception innerException) : base(message, innerException) { }
    }
    class AlignException : Exception
    {
        public AlignException(string message) : base(message) { }
        public AlignException(string message, Exception innerException) : base(message, innerException) { }
    }
    class BoxPartException : Exception
    {
        public BoxPartException(string message) : base(message) { }
        public BoxPartException(string message, Exception innerException) : base(message, innerException) { }
    }
    #endregion
    public static class UI
    {
        #region colour constants
        public static Dictionary<string, string> colors = new Dictionary<string, string>();
        public static Dictionary<string, ConsoleColor> conColors = new Dictionary<string, ConsoleColor>();
        public static string sep       = "~"; // change this value to any other char that you will NOT be using in any strings supplied to ColorPrint()
        public  static string BLACK     = sep + "BLACK" + sep;
        public  static string GREY      = sep + "GREY" + sep;
        public  static string GRAY      = sep + "GREY" + sep;
        public  static string DGREY     = sep + "DGREY" + sep;
        public  static string DGRAY     = sep + "DGREY" + sep;
        public  static string WHITE     = sep + "WHITE" + sep;
        public  static string BLUE      = sep + "BLUE" + sep;
        public  static string GREEN     = sep + "GREEN" + sep;
        public  static string CYAN      = sep + "CYAN" + sep;
        public  static string RED       = sep + "RED" + sep;
        public  static string MAGENTA   = sep + "MAGENTA" + sep;
        public  static string YELLOW    = sep + "YELLOW" + sep;
        public  static string DBLUE     = sep + "DBLUE" + sep;
        public  static string DGREEN    = sep + "DGREEN" + sep;
        public  static string DCYAN     = sep + "DCYAN" + sep;
        public  static string DRED      = sep + "DRED" + sep;
        public  static string DMAGENTA  = sep + "DMAGENTA" + sep;
        public  static string DYELLOW   = sep + "DYELLOW" + sep;
        public  static string BLACKBG   = sep + "BLACKBG" + sep;
        public  static string GREYBG    = sep + "GREYBG" + sep;
        public  static string GRAYBG    = sep + "GREYBG" + sep;
        public  static string DGREYBG   = sep + "DGREYBG" + sep;
        public  static string DGRAYBG   = sep + "GREYBG" + sep;
        public  static string WHITEBG   = sep + "WHITEBG" + sep;
        public  static string BLUEBG    = sep + "BLUEBG" + sep;
        public  static string GREENBG   = sep + "GREENBG" + sep;
        public  static string CYANBG    = sep + "CYANBG" + sep;
        public  static string REDBG     = sep + "REDBG" + sep;
        public  static string MAGENTABG = sep + "MAGENTABG" + sep;
        public  static string YELLOWBG  = sep + "YELLOBGW" + sep;
        public  static string DBLUEBG   = sep + "DBLUEBG" + sep;
        public  static string DGREENBG  = sep + "DGREENBG" + sep;
        public  static string DCYANBG   = sep + "DCYANBG" + sep;
        public  static string DREDBG    = sep + "DREDBG" + sep;
        public  static string DMAGENTABG= sep + "DMAGENTABG" + sep;
        public  static string DYELLOWBG = sep + "DYELLOWBG" + sep;
        public  static string RESET     = sep + "RESET" + sep;
        #endregion
        #region public variables
        public static int windowWidth = Console.WindowWidth - 1; // set to 80 + 1 to allow for 80 cols to display without overflow to next line
        public static int windowHeight = Console.WindowHeight;
        public static string back = "BLACKBG";      // default background colour
        public static string fore = "WHITE";        // default foreground colour

        /* Character set used in old DOS programs to draw lines and boxes
          ┌───────┬───────┐   ╔═════╦═════╗ 
          │       │       │   ║     ║     ║
          ├───────┼───────┤   ╠═════╬═════╣ 
          │       │       │   ║     ║     ║
          └───────┴───────┘   ╚═════╩═════╝
         */
         static List<string> sSymbolsTop = new List<string>     { "┌", "─", "┐", "┬" };
         static List<string> sSymbolsBottom = new List<string>  { "└", "─", "┘", "┴" };
         static List<string> sSymbolsBody = new List<string>    { "│", " ", "│", "│" };
         static List<string> sSymbolsMid = new List<string>     { "├", "─", "┤", "┼" };

         static List<string> dSymbolsTop = new List<string>     { "╔", "═", "╗", "╦" };
         static List<string> dSymbolsBottom = new List<string>  { "╚", "═", "╝", "╩" };
         static List<string> dSymbolsBody = new List<string>    { "║", " ", "║", "║" };
         static List<string> dSymbolsMid = new List<string>     { "╠", "═", "╣", "╬" };

        private static Dictionary<string, List<string>> dictSingle = new Dictionary<string, List<string>>
        {
            { "top",    sSymbolsTop },
            { "body",   sSymbolsBody },
            { "mid",    sSymbolsMid },
            { "bottom", sSymbolsBottom }
        };
        private static Dictionary<string, List<string>> dictDouble = new Dictionary<string, List<string>>
        {
            { "top",    dSymbolsTop },
            { "body",   dSymbolsBody },
            { "mid",    dSymbolsMid },
            { "bottom", dSymbolsBottom }
        };
        private static List<string> colorList = new List<string> { "BLACK", "RED", "GREEN", "YELLOW", "BLUE", "MAGENTA", "CYAN", "WHITE" };
        private static List<string> colorCheck = new List<string> { BLACK, GREY, GRAY, RED, GREEN, YELLOW, BLUE, MAGENTA, CYAN, WHITE,
                                                                     DGREY, DGRAY, DRED, DGREEN, DYELLOW, DBLUE, DMAGENTA, DCYAN };
        private static List<string> colorCheckBg = new List<string> {  BLACKBG, GREYBG, GRAYBG, REDBG, GREENBG, YELLOWBG, BLUEBG, MAGENTABG, CYANBG,
                                                                        WHITEBG, DGREYBG, DGRAYBG, DREDBG, DGREENBG, DYELLOWBG, DBLUEBG, DMAGENTABG, DCYANBG };
        #endregion
        #region static constructor
        static UI() 
        {
            /// static class "constructor" runs only once on first use ///
            InitialseColourPrint();
        }
        #endregion
        #region Core functions
        private static void InitialseColourPrint(bool reset = false)
        {
            /// initialises "constants" (variables in CAPS) lists and dictionaries ///
            if (reset)
            {
                BLACK = sep + "BLACK" + sep;
                GREY = sep + "GREY" + sep;
                GRAY = sep + "GREY" + sep;
                DGREY = sep + "DGREY" + sep;
                DGRAY = sep + "DGREY" + sep;
                WHITE = sep + "WHITE" + sep;
                BLUE = sep + "BLUE" + sep;
                GREEN = sep + "GREEN" + sep;
                CYAN = sep + "CYAN" + sep;
                RED = sep + "RED" + sep;
                MAGENTA = sep + "MAGENTA" + sep;
                YELLOW = sep + "YELLOW" + sep;
                DBLUE = sep + "DBLUE" + sep;
                DGREEN = sep + "DGREEN" + sep;
                DCYAN = sep + "DCYAN" + sep;
                DRED = sep + "DRED" + sep;
                DMAGENTA = sep + "DMAGENTA" + sep;
                DYELLOW = sep + "DYELLOW" + sep;
                BLACKBG = sep + "BLACKBG" + sep;
                GREYBG = sep + "GREYBG" + sep;
                GRAYBG = sep + "GREYBG" + sep;
                DGREYBG = sep + "DGREYBG" + sep;
                DGRAYBG = sep + "GREYBG" + sep;
                WHITEBG = sep + "WHITEBG" + sep;
                BLUEBG = sep + "BLUEBG" + sep;
                GREENBG = sep + "GREENBG" + sep;
                CYANBG = sep + "CYANBG" + sep;
                REDBG = sep + "REDBG" + sep;
                MAGENTABG = sep + "MAGENTABG" + sep;
                YELLOWBG = sep + "YELLOBGW" + sep;
                DBLUEBG = sep + "DBLUEBG" + sep;
                DGREENBG = sep + "DGREENBG" + sep;
                DCYANBG = sep + "DCYANBG" + sep;
                DREDBG = sep + "DREDBG" + sep;
                DMAGENTABG = sep + "DMAGENTABG" + sep;
                DYELLOWBG = sep + "DYELLOWBG" + sep;
                RESET = sep + "RESET" + sep;
            }

            colors.Clear();
            colors.Add("BLACK", BLACK);
            colors.Add("GREY", GREY);
            colors.Add("GRAY", GRAY);
            colors.Add("DGREY", DGREY);
            colors.Add("DGRAY", DGRAY);
            colors.Add("WHITE", WHITE);
            colors.Add("BLUE", BLUE);
            colors.Add("GREEN", GREEN);
            colors.Add("CYAN", CYAN);
            colors.Add("RED", RED);
            colors.Add("MAGENTA", MAGENTA);
            colors.Add("YELLOW", YELLOW);
            colors.Add("DBLUE", DBLUE);
            colors.Add("DGREEN", DGREEN);
            colors.Add("DCYAN", DCYAN);
            colors.Add("DRED", DRED);
            colors.Add("DMAGENTA", DMAGENTA);
            colors.Add("DYELLOW", DYELLOW);
            colors.Add("WHITEBG", WHITEBG);
            colors.Add("BLACKBG", BLACKBG);
            colors.Add("GREYBG", GREYBG);
            colors.Add("GRAYBG", GRAYBG);
            colors.Add("DGREYBG", DGREYBG);
            colors.Add("DGRAYBG", DGRAYBG);
            colors.Add("BLUEBG", BLUEBG);
            colors.Add("GREENBG", GREENBG);
            colors.Add("CYANBG", CYANBG);
            colors.Add("REDBG", REDBG);
            colors.Add("MAGENTABG", MAGENTABG);
            colors.Add("YELLOWBG", YELLOWBG);
            colors.Add("DBLUEBG", DBLUEBG);
            colors.Add("DGREENBG", DGREENBG);
            colors.Add("DCYANBG", DCYANBG);
            colors.Add("DREDBG", DREDBG);
            colors.Add("DMAGENTABG", DMAGENTABG);
            colors.Add("DYELLOWBG", DYELLOWBG);
            colors.Add("RESET", RESET);

            conColors.Clear();
            conColors.Add("BLACK", ConsoleColor.Black);
            conColors.Add("GREY", ConsoleColor.Gray);
            conColors.Add("GRAY", ConsoleColor.Gray);
            conColors.Add("DGREY", ConsoleColor.DarkGray);
            conColors.Add("DGRAY", ConsoleColor.DarkGray);
            conColors.Add("WHITE", ConsoleColor.White);
            conColors.Add("BLUE", ConsoleColor.Blue);
            conColors.Add("GREEN", ConsoleColor.Green);
            conColors.Add("CYAN", ConsoleColor.Cyan);
            conColors.Add("RED", ConsoleColor.Red);
            conColors.Add("MAGENTA", ConsoleColor.Magenta);
            conColors.Add("YELLOW", ConsoleColor.Yellow);
            conColors.Add("DBLUE", ConsoleColor.DarkBlue);
            conColors.Add("DGREEN", ConsoleColor.DarkGreen);
            conColors.Add("DCYAN", ConsoleColor.DarkCyan);
            conColors.Add("DRED", ConsoleColor.DarkRed);
            conColors.Add("DMAGENTA", ConsoleColor.DarkMagenta);
            conColors.Add("DYELLOW", ConsoleColor.DarkYellow);
            conColors.Add("BLACKBG", ConsoleColor.Black);
            conColors.Add("GREYBG", ConsoleColor.Gray);
            conColors.Add("GRAYBG", ConsoleColor.Gray);
            conColors.Add("DGREYBG", ConsoleColor.DarkGray);
            conColors.Add("DGRAYBG", ConsoleColor.DarkGray);
            conColors.Add("WHITEBG", ConsoleColor.White);
            conColors.Add("DBLUEBG", ConsoleColor.DarkBlue);
            conColors.Add("DGREENBG", ConsoleColor.DarkGreen);
            conColors.Add("DCYANBG", ConsoleColor.DarkCyan);
            conColors.Add("DREDBG", ConsoleColor.DarkRed);
            conColors.Add("DMAGENTABG", ConsoleColor.DarkMagenta);
            conColors.Add("DYELLOWBG", ConsoleColor.DarkYellow);
        }
        public static void AddLines(int numLines = 25, string foreColor = "WHITE", string backColor = "BLACKBG") //default 25 lines if not given
        {
            /// Adds numLines blank lines in specified or default colour ///
            ValidateColors(ref foreColor, ref backColor, true);
            string blank = "".PadRight(windowWidth); //string of spaces across entire width of Console
            if (numLines > 0)
            {
                for (int i = 0; i < numLines; i++)
                {
                    ColorPrint($"{foreColor}{backColor}{blank}");
                }
            }
        }
        public static void AddLines(int leaveLines, int currentLines, string foreColor = "WHITE", string backColor = "BLACKBG") 
        {
            /// Adds blank lines until windowHeight - leaveLines in specified or default colour ///
            ValidateColors(ref foreColor, ref backColor, true);
            string blank = "".PadRight(windowWidth); //string of spaces across entire width of Console
            int numLines = windowHeight - currentLines - leaveLines;
            if (numLines > 0)
            {
                for (int i = 0; i < numLines; i++)
                {
                    ColorPrint($"{foreColor}{backColor}{blank}");
                }
            }
        }
        public static void ChangeSeparator(string value)
        {
            /// Allows use of different character to separate colour tags. Default is ~ ///
            sep = value.Substring(0, 1);    // if user supplies more than 1 character, use the first only. Stored as string
            InitialseColourPrint(true);     //re-initialise to make use of new character
        }
        public static void Clear()
        {
            /// Does what it says on the tin. (Allows calls from other classes not involved with UI) ///
            Console.Clear();
        }
        public static void DisplayMessage(string message, bool useInput, bool useTimer, string foreColor = "WHITE", string backColor = "BLACKBG") // string version
        {
            /// show user message or default, either wait for 2 secs, or ask user to press enter ///
            ValidateColors(ref foreColor, ref backColor, true);
            if (useTimer)
            {
                ColorPrint($"{foreColor}{backColor}{message}");
                Thread.Sleep(2000);
            }
            if (useInput)
            {
                if (message == string.Empty) message = "Press Enter to continue";
                else message = $"{foreColor}{backColor}{message}";
                Input(message, "...", foreColor, backColor);
            }
        }
        public static int DrawLine(string style, string foreColor, string backColor, int width = 0, string align = "left")
        {
            ///  Draw a single or double line across the console with fore and back colours set  ///
            style = style.Trim().ToLower();
            if (style != "s" && style != "d") style = "s";
            ValidateColors(ref foreColor, ref backColor, true);
            ValidateAlignment(ref align);
            // width has to be 1 less than windowWidth or spills over to next line: Odd behaviour of C# Console?
            if (width > windowWidth || width <= 0)         width = windowWidth;
            List<string> s = SelectCharacterList("mid", style);
            string output = string.Empty;
            if (width == windowWidth)
            {
                output = PadString("", width, s[1], "left");                // -2 to allow for start/end margin chars
                output = $"{foreColor}{backColor}{output}";                     //eg "~WHITE~~BLACKBG~════════════════════"
            }
            else
            {
                output = PadString("", width, s[1], "left");
                output = PadString(output, windowWidth, " ", align);
                output = $"{foreColor}{backColor}{output}";           //eg "~WHITE~~BLACKBG~════════════════════"
            }
            ColorPrint(output);

            return 1; // single line used
        }
        public static int DrawBoxOutline(string style, string part, string foreColor, string backColor, string align = "left", int width = 0, bool midMargin = false)
        {
            /// Draw the top, mid or bottom of a box to width ///
            style = style.Trim().ToLower();
            if (style != "s" && style != "d") style = "s";
            ValidateColors(ref foreColor, ref backColor, true);                  // true = colours validated to const format: ~WHITE~
            ValidateAlignment(ref align);
            ValidateBoxPart(ref part);
            List<string> s = SelectCharacterList(part, style);
            if (width > windowWidth || width <= 0) width = windowWidth;
            string lSide = s[0];                                                // start with left corner
            if (midMargin) lSide = SelectCharacterList("mid", style)[0];        // "├"
            string rSide = s[2];                                                // end with right corner
            if (midMargin) rSide = SelectCharacterList("mid", style)[2];        // "┤"
            string output = PadString("", width - 2, s[1], align);              // -2 to allow for start/end margin chars
            output = $"{foreColor}{backColor}{lSide}{output}{rSide}";
            ColorPrint(output);

            return 1; // single line used
        }
        public static int DrawBoxBody(string style,  string text, string align, string foreColor, string backColor, string textColor = "WHITE", string textBackColor = "BLACKBG", int width = 0)
        {
            /// print out single line mid section of a box with or without text ///
            style = style.Trim().ToLower();
            if (style != "s" && style != "d") style = "s";
            ValidateColors(ref foreColor, ref backColor, true); // true = colours validated to const format: ~WHITE~
            ValidateColors(ref textColor, ref textBackColor, true); // true = colours validated to const format: ~WHITE~
            ValidateAlignment(ref align);
            List<string> s = SelectCharacterList("body", style);
            if (width > windowWidth || width <= 0) width = windowWidth;
            string lSide = s[0];
            string rSide = s[2];
            string output = PadString(text, width - 2, " ", align);              // -2 to allow for start/end margin chars
            // foreColor border on backColor -> colour formatted text -> foreColor border on backColor
            ColorPrint($"{foreColor}{backColor}{lSide}{textColor}{textBackColor}{output}{foreColor}{backColor}{rSide}");

            return 1; // single line used
        }
        public static int DrawMultiBoxBody(List<string> styles, List<int> sizes, List<string> foreColors, List<string> backColors, List<string> textLines, List<string> alignments, int padding = 0)
        {
            /// print out single line mid section of multiple boxes with or without text ///
            if(styles.Count != sizes.Count && styles.Count != foreColors.Count && styles.Count != backColors.Count && styles.Count != alignments.Count)
            {
                throw new MatchingTagsException("All supplied parameter lists must have the same number of items");
            }
            for (int i = 0; i < styles.Count; i++)
            {
                if (styles[i] != "s" && styles[i] != "d") styles[i] = "s";
            }
            if (foreColors.Count != backColors.Count) throw new ColorValueException($"DrawMultiBoxOutline: Number of foreColours must equal number of backColours");
            for (int i = 0; i < foreColors.Count; i++) //validate supplied colours
            {
                string foreColor = foreColors[i];
                string backColor = backColors[i];
                ValidateColors(ref foreColor, ref backColor, true);
                foreColors[i] = foreColor;
                backColors[i] = backColor;
            }
            for (int i = 0; i < alignments.Count; i++)
            {
                string align = alignments[i];
                ValidateAlignment(ref align);
                alignments[i] = align;
            }
            List<string> s = sSymbolsBody; //default
            List<string> outputs = new List<string>();
            // calculate each box size from sizes: {15, 40, 25} 
            int numBoxes = sizes.Count;
            int width = windowWidth;
            int boxLength = 0;
            string output = string.Empty;
            for (int i = 0; i < sizes.Count; i++)
            {
                // size examples {15, 40, 25} = 80 cols
                s = SelectCharacterList("body", styles[i]);
                if (i < sizes.Count - 1) boxLength = sizes[i] - padding - 2;     // -2 as is box edge, and characters will be added both sides
                else boxLength = sizes[i] - 2;
                string lSide = s[0];                                                // start with left side char
                string rSide = s[3];                                                // end with right side char
                output = PadString(textLines[i], boxLength, " ", alignments[i]);
                output = $"{foreColors[i]}{backColors[i]}{lSide}{output}{foreColors[i]}{backColors[i]}{rSide}";
                outputs.Add(output);                                  // create new list item with completed string
            }

            output = "";
            foreach (string line in outputs)                         // concatenate all box outlines in outputs
            {
                output += line;
            }
            ColorPrint(output);

            return 1; // single line used
        }
        public static int DrawGridBody(string style, string part, List<int> columns, string foreColor, string backColor, List<string> textLines, List<string> alignments)
        {
            /// Draw the top, mid or bottom of a grid to width ///
            ValidateColors(ref foreColor, ref backColor, true);             // true = colours validated to const format: ~WHITE~
            List<string> s = SelectCharacterList(part, style);              // "┌", "─", "┐", "┬"
            List<string> outputs = new List<string>();
            string output = string.Empty;
            for (int col = 0; col < columns.Count; col++)
            {
                int colWidth = columns[col];                                // eg {10, 20, 20, 30 } = 80 cols
                string lSide = s[0];                                        // "┌"
                string rSide = s[2];                                        // "┐"
                if (col == 0)
                {
                    lSide = s[0];                                           // "┌"
                    rSide = s[3];                                           // "┬"
                }
                else if (col == columns.Count - 1)
                {
                    lSide = s[1];                                           // "─"
                    rSide = s[2];                                           // "┐"
                }
                else
                {
                    lSide = s[1];                                           // "─"
                    rSide = s[3];                                           // "┬"
                }
                output = PadString(textLines[col], colWidth - 2, " ", alignments[col]);          // -2 to allow for start/end margin chars
                output = $"{foreColor}{backColor}{lSide}{output}{foreColor}{backColor}{rSide}";
                outputs.Add(output);                                        // create new list item with completed string
            }
            output = string.Empty;
            foreach (string line in outputs) // concatenate all box outlines in outputs
            {
                output += line;
            }
            ColorPrint(output);

            return 1; // single line used
        }
        public static int DrawGridOutline(string style, string part, List<int> columns, string foreColor, string backColor, bool midMargin = false)
        {
            /// Draw the top, mid or bottom of a grid to width ///
            ValidateColors(ref foreColor, ref backColor, true);             // true = colours validated to const format: ~WHITE~
            List<string> s = SelectCharacterList(part, style);              // "┌", "─", "┐", "┬"
            List<string> outputs = new List<string>();
            string output = string.Empty;
            for (int col = 0; col < columns.Count; col++)
            {
                int colWidth = columns[col];                                // eg {10, 20, 20, 30 } = 80 cols
                string lSide = s[0];                                        // "┌"
                
                string rSide = s[2];                                        // "┐"
                if (col == 0)
                {
                    lSide = s[0];                                           // "┌"
                    if (midMargin) lSide = SelectCharacterList("mid", style)[0]; // "├"
                    rSide = s[3];                                           // "┬"
                }
                else if (col == columns.Count - 1)
                {
                    lSide = s[1];                                           // "─"
                    rSide = s[2];                                           // "┐"
                    if (midMargin) rSide = SelectCharacterList("mid", style)[2]; // "├"
                }
                else
                {
                    lSide = s[1];                                           // "─"
                    rSide = s[3];                                           // "┬"
                }
                output = PadString("", colWidth - 2, s[1], "left");         // -2 to allow for start/end margin chars
                output = $"{foreColor}{backColor}{lSide}{output}{rSide}";
                outputs.Add(output);                                        // create new list item with completed string
            }
            output = string.Empty;
            foreach (string line in outputs) // concatenate all box outlines in outputs
            {
                output += line;
            }
            ColorPrint(output);

            return 1; // single line used
        }
        public static int DrawMultiBoxOutline(List<string> styles, string part, List<int> sizes, List<string> foreColors, List<string> backColors, int padding = 0)
        {
            /// Draw the top/bottoms of sizes.Count boxes, width in absolute values ///
            if(styles.Count != sizes.Count && styles.Count != foreColors.Count && styles.Count != backColors.Count)
            {
                throw new MatchingTagsException("All supplied parameter lists must have the same number of items");
            }
            for (int i = 0; i < styles.Count; i++)
            {
                if (styles[i] != "s" && styles[i] != "d") styles[i] = "s";
            }
            for (int i = 0; i <foreColors.Count; i++)
            {
                string foreColor = foreColors[i];
                string backColor = backColors[i];
                ValidateColors(ref foreColor, ref backColor, true);
                foreColors[i] = foreColor;
                backColors[i] = backColor;
            }
            List<string> s = sSymbolsTop;
            List<string> outputs = new List<string>();
            int numBoxes = sizes.Count;
            int width = windowWidth;
            int boxLength = 0;
            string output = string.Empty;
            for (int i = 0; i < sizes.Count; i++)
            {
                // size examples {15, 40, 25} = 80 cols
                s = SelectCharacterList(part, styles[i]);
                if (i < sizes.Count - 1)    boxLength = sizes[i] - padding - 2;     // -2 as is box edge, and characters will be added both sides
                else                        boxLength = sizes[i] - 2;
                string lSide = s[0];                                                // start with left corner
                string rSide = s[2];                                                // end with right corner
                output = PadString("", boxLength, s[1], "left");
                output = $"{foreColors[i]}{backColors[i]}{lSide}{output}{foreColors[i]}{backColors[i]}{rSide}";
                outputs.Add(output);        // create new list item with completed string
            }

            output = string.Empty;
            foreach(string line in outputs) // concatenate all box outlines in outputs
            {
                output += line;
            }
            ColorPrint(output);

            return 1; //single line
        }
        public static int DrawMultiLineBox(string style, string text, string foreColor, string backColor, string textColor = "WHITE", string textBackColor = "BLACKBG", string align = "left", int width = 0)
        {
            ///  Draw a single box containing many lines of text ///
            /// Convert the supplied string 'text' into a list and pass to same function with over-ride ///
            List<string> textLines = SplitClean(text, '\n');
            return DrawMultiLineBox(style, textLines, foreColor, backColor, textColor, textBackColor, align, width);
        }
        public static int DrawMultiLineBox(string style, List<string> textLines, string foreColor, string backColor, string textColor = "WHITE", string textBackColor = "BLACKBG", string align = "left", int width = 0)
        {
            ///  Draw a single box containing many lines of text ///
            int numLines = 0;
            ValidateColors(ref foreColor, ref backColor, true); // true = colours validated to const format: ~WHITE~
            if (width > windowWidth || width <= 0) width = windowWidth;
            DrawBoxOutline(style, "top", foreColor, backColor, align, width);
            numLines += 1;
            textLines = GetFormattedLines(textLines, width);
            foreach (string line in textLines)
            {
                DrawBoxBody(style, line, align, foreColor, backColor, textColor, textBackColor, width);
                numLines += 1;
            }
            DrawBoxOutline(style, "bottom", foreColor, backColor, align, width);
            numLines += 1;

            return numLines;
        }
        private static void ColorPrint(string value, bool newline = true)
        {
            /*  This uses the default char ~ to separate colour strings
                change the line:  static string sep = "~"; as required
                other possibles are ` (backtick) ¬ (?) any character you will not use
                example value = "~RED~This is a line of red text"
            */
            List<string> lineParts = SplitClean(value, sep[0]); //{'RED', 'This is a line of red text'} 
            foreach (string part in lineParts)
            {
                if (colors.ContainsKey(part)) // is 'RED' in the colors dictionary?
                {
                    if (part.Contains("BG")) // background colour eg 'BLACKBG'
                    {
                        Console.BackgroundColor = conColors[part]; // get the ConsoleColor from dictionary
                    }
                    else if (part == "RESET") // reset command
                    {
                        Console.ResetColor();
                    }
                    else // foreground colour
                    {
                        Console.ForegroundColor = conColors[part]; // get the ConsoleColor from dictionary
                    }
                }
                else // not a colour command so print it out without newline
                {
                    Console.Write(part);
                }
            }
            Console.ResetColor();                // in case it was not added already
            if (newline) Console.Write("\n");    // Add newline to complete the print command
            Console.ResetColor();                // in case it was not added already
        }
        public static Tuple<bool, string, string> GetBoolean(string prompt, string promptChar, string textColor, string backColor)
        {
            /// gets a boolean (yes/no) type entries from the user ///
            Tuple<bool, string, string> tuple = ProcessInput(prompt, promptChar, textColor, backColor, 1, 3, "bool"); //correct user input returned
            return new Tuple<bool, string, string>(tuple.Item1, tuple.Item2, tuple.Item3);
        }
        public static Tuple<bool, string, string> GetRealNumber(string prompt, string promptChar, string textColor, string backColor, double min, double max)
        {
            /// gets a float / double from the user ///
            Tuple<bool, string, string> tuple = ProcessInput(prompt, promptChar, textColor, backColor, min, max, "real"); //correct user input returned
            return new Tuple<bool, string, string>(tuple.Item1, tuple.Item2, tuple.Item3);
        }
        public static Tuple<bool, string, string> GetInteger(string prompt, string promptChar, string foreColor, string backColor, double min = 0, double max = 65536)
        {
            /// Public Method: gets an integer from the user ///
            Tuple<bool, string, string> tuple = ProcessInput(prompt, promptChar, foreColor, backColor, min, max, "int"); //correct user input returned
            return new Tuple<bool, string, string>(tuple.Item1, tuple.Item2, tuple.Item3);
        }
        public static Tuple<bool, string, string> GetString(string prompt, string ending, string foreColor, string backColor, bool withTitle, int min, int max)
        {
            /// Public Method: gets a string from the user ///
            Tuple<bool, string, string> tuple = ProcessInput(prompt, ending, foreColor, backColor, min, max, "string"); //correct user input returned
            bool isValid = tuple.Item1;
            string userInput = tuple.Item2;
            if (withTitle && isValid)
            {
                TextInfo textInfo = new CultureInfo("en-UK", false).TextInfo;
                userInput = textInfo.ToTitleCase(userInput);
            }
            return new Tuple<bool, string, string>(isValid, userInput, tuple.Item3); // valid, userInput, errorMessage or ""
        }
        private static int FormatColorTags(ref string text)
        {
            /// Checks supplied string has matching tags to define colour strings ///
            // check if even numbers of tags
            if ((text.Count(x => x == sep[0]) % 2 == 1)) throw new MatchingTagsException($"The supplied text {text} does not have matching colour separators: {sep}");
            text = text.Replace(sep, "¶" + sep + "¶");
            List<string> data = SplitClean(text, '¶'); //"~red~some text~green~more text" -> "¶~¶red¶~¶some text¶~¶green¶~¶more text"
                                                       // {"~", "red", "~", "some text", "~","green", "~", "more text"}
            int colourTagSpaces = 0;
            int startTag = -1;
            int endTag = -1;
            int colorData = -1;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] == sep) // found ~
                {
                    if (startTag == -1)
                    {
                        startTag = i;
                        colorData = i + 1;
                        endTag = i + 2;
                    }
                    else if (i == endTag) //marked already, next data is normal text
                    {
                        colorData = -1;
                    }
                    else // i not -1 and not endTag
                    {
                        startTag = i;
                        colorData = i + 1;
                        endTag = i + 2;
                    }
                }
                else
                {
                    if (i == colorData)
                    {
                        // convert "~red~" to "~RED~"
                        string textColour = data[i].ToUpper();
                        if (colors.ContainsKey(textColour))
                        {
                            data[i] = colors[textColour];
                            colourTagSpaces = colourTagSpaces + data[i].Length;
                        }
                        colorData = -1;
                    }
                }
            }
            // re-combine textLines
            string output = string.Empty;
            foreach (string line in data)
            {
                if (line != sep) output += line;
            }
            text = output; //byref
            return colourTagSpaces;
        }
        public static List<string> GetFormattedLines(string text, int maxLength = 0, bool noBorder = false)
        {
            /// format single text string into  fixed length lines ///

            /*example input string: (could contain \n characters, or continuous line)
	        "This is the first line\n\nThis is the third line (follows blank due to \\n\\n).\nThis is the fourth line which is very long and therefore exceeds the standard console
            or terminal width of eighty characters" */

            List<string> textLines = SplitClean(text, '\n');
            /* textLines = {"This is the first line", "", "This is the third line (follows blank due to \\n\\n)",
               "This is the fourth line which is very long and therefore exceeds the standard console or terminal width of eighty characters"} */
            return GetFormattedLines(textLines, maxLength, noBorder);
        }
        public static List<string> GetFormattedLines(List<string> textLines, int maxLength = 0, bool noBorder = false)
        {
            ///takes a list of lines , checks length of each and adds additional lines if required, returns list///
            /* textLines = {
                "This is the first line",
                "This is the second line",
                "This is the third line which is very long and therefore exceeds the standard console or terminal width of eighty characters"
               } */
            // check length of each line max = shared.term_width - 2
            if (maxLength > windowWidth || maxLength <= 0) maxLength = windowWidth;
            if (!noBorder) maxLength -= 2; //default    
            List<string> newLines = new List<string>();
            for (int index = 0; index < textLines.Count; index++)   // 3 lines in this example as contains  2 * \n
            {
                string line = textLines[index].Trim();              // remove leading/trailing spaces
                int colorTagSpaces = 0;
                // now check the length of each line and cut into shorter sections if required
                if (line.Length == 0) newLines.Add(line); // add empty
                else
                {
                    while (line.Length > maxLength + colorTagSpaces)                 // line length > 77 in 80 col terminal
                    {
                        int end = line.Substring(0, maxLength).LastIndexOf(" ");
                        string test = line.Substring(0, end);
                        colorTagSpaces = FormatColorTags(ref test);
                        end = maxLength + colorTagSpaces;
                        //if > 0 tags re-try with longer line
                        if (colorTagSpaces > 0 && line.Length > end)
                        {
                            end = line.Substring(0, maxLength + colorTagSpaces).LastIndexOf(" ");      // break line from last space within the first 77 chars
                            newLines.Add(line.Substring(0, end));       // add this reduced line to list
                            line = line.Substring(end).Trim();          // trim the remainder and re-check it
                        }
                        else //either no colour tags or line length < maxLength + colourtags
                        {
                            //newLines.Add(line);       // add this reduced line to list
                            
                        }
                    }
                    if (line.Length > 0) newLines.Add(line);        // partial line left over from the while loop ( <maxLength) or short line
                }
            }
            /* newLines = {
               "This is the first line",
               "This is the second line",
               "This is the third line which is very long and therefore exceeds the standard console",
               "or terminal width of eighty characters"
              } */
            return newLines;
        }
        public static string Input(string prompt, string ending, string foreColor, string backColor)
        {
            /// Get keyboard input from user (requires Enter ) ///
            ValidateColors(ref foreColor, ref backColor, true); // true = colours validated to const format: ~WHITE~
            ColorPrint($"{foreColor}{backColor}{prompt + ending}", false); //do not use newline
            return Console.ReadLine();
        }
        public static string InputBox(string returnType, string title, string boxMessage, string inputPrompt, string foreColor = "WHITE", 
                                       string backColor = "BLACKBG", int width = 0, int minReturnLen = 1, int maxReturnLen = 20)
        {
            /// Draw an inputBox with title, message, input area ///
            /// Example "bool", "File Exists Warning", "Are you sure you want to over-write?", "Confirm over-write (y/n)_" ///
            ValidateColors(ref foreColor, ref backColor, true); // true = colours validated to const format: ~WHITE~
            if (width > windowWidth || width <= 0) width = windowWidth;
            bool isValid = false;
            string userInput = "";
            while (!isValid)
            {
                Console.Clear();
                int numLines = 0;
                List<string> lines = GetFormattedLines(boxMessage); // returns alist of lines max length of any line Console.Windowwidth - 3
                DrawBoxOutline("d", "top", foreColor, backColor); // draw top of box double line, yellow
                DrawBoxBody("d", $"{title}", "centre", foreColor, backColor); // draw title
                DrawBoxOutline("d", "mid", foreColor, backColor); // draw single line
                numLines = numLines + 3;
                foreach (string line in lines)
                {
                    DrawBoxBody("d",  $"{line}", "centre", foreColor, backColor); // draw each line of text
                    numLines = numLines + 1;
                }
                DrawBoxBody("d", "", "centre", foreColor, backColor); // draw empty line
                DrawBoxOutline("d", "bottom", foreColor, backColor); // draw bottom of box double line, yellow
                numLines = numLines + 2;
                AddLines(19 - numLines);
                DrawLine("d", WHITE, BLACKBG);
                Tuple<bool, string, string> retValue = new Tuple<bool, string, string>(false, "", "");
                if (returnType == "str" || returnType == "string")
                {
                    //valid, userInput, message = getString(inputPrompt, "", "", false, minLength, maxLength);
                    retValue = GetString(inputPrompt, "", WHITE, BLACKBG, false, minReturnLen, maxReturnLen);
                }
                else if (returnType == "bool" || returnType == "boolean")
                {
                    //valid, userInput, message = getBoolean(inputPrompt, "", "")
                    retValue = GetBoolean(inputPrompt, "_", "", "");
                }
                isValid = retValue.Item1;
                userInput = retValue.Item2;
                string message = retValue.Item3;
                if (!isValid)
                {
                    ColorPrint($"{RED}{message}{MAGENTA} retry in 2 secs...");
                    Thread.Sleep(2000);
                }
            }
            return userInput; // string eg filename typed in, || bool
        }
        public static int Menu(string style,  string title, List<string> textLines, string foreColor = "WHITE",
                                string backColor = "BLACKBG", string align = "left", int width = 0)
        {
            /// displays a menu using the text in 'title', and a list of menu items (string) ///
            /// This menu will re-draw until user enters correct data ///
            ValidateColors(ref foreColor, ref backColor, true);                  // true = colours validated to const format: ~WHITE~
            if (width > windowWidth || width <= 0) width = windowWidth;
            int userInput = 0;
            bool isValid = false;
            string message = "";
            for (int i = 0; i < textLines.Count; i++)
            {
                if ( i < 9)     textLines[i] = $"     {i + 1}) {textLines[i]}";
                else            textLines[i] = $"    {i + 1}) {textLines[i]}";
            }
            while (!isValid)
            {
                Console.Clear();
                DrawBoxOutline("d", "top", foreColor, backColor);           // draw top of box double line
                DrawBoxBody("d", "", "centre", foreColor, backColor);       // draw empty line
                DrawBoxBody("d", $"{title}", "left", foreColor, backColor); // draw title
                DrawBoxBody("d", "", "centre", foreColor, backColor);       // draw empty line
                for (int i = 0; i < textLines.Count; i++)
                {
                    DrawBoxBody("d", $"{textLines[i]}", "left", foreColor, backColor); // draw menu options
                }
                DrawBoxBody("d", "", "centre", foreColor, backColor);       // draw empty line
                DrawBoxOutline("d", "bottom", foreColor, backColor);        // draw top of box double line, yellow
                // get multiple return variables using Tuple
                var tuple = GetInteger($"Type the number of your choice (1 to {textLines.Count})", ">", WHITE, BLACKBG, 1, textLines.Count);
                isValid = tuple.Item1;
                int.TryParse(tuple.Item2, out userInput);
                message = tuple.Item3;
                if (!isValid)
                {
                    ColorPrint($"{RED}{message}{MAGENTA} retry in 2 secs...");
                    Thread.Sleep(2000);
                }
            }
            return userInput - 1;
        }
        private static string PadString(string text, int width, string padChar, string align = "left")
        {
            /// width should be width of the part to be padded, so excludes left/right margin characters ///
            string output = text;
            int colourTagSpaces = 0;
            if (text.Contains(sep[0])) // colour tag(s) present
            {
                colourTagSpaces = FormatColorTags(ref output); // returns additional spaces required and fomats output by ref
            }
            if (align == "centre" || align == "center")
            {
                output = output.PadLeft(((width - output.Length + colourTagSpaces) / 2) + output.Length, padChar[0]); //eg output 10 chars long, pad to half of width - 2 and allow for chars used by colour tag
                output = output.PadRight(width + colourTagSpaces, padChar[0]);
            }
            else if (align == "right")
            {
                output = output.PadLeft(width + colourTagSpaces, padChar[0]); // right align = pad left
            }
            else //left align = PadRight
            {
                output = output.PadRight(width + colourTagSpaces, padChar[0]);  //eg output 10 chars long, pad to width - 2 and allow for chars used by colour tag
            }

            return output;
        }
        public static void Print(string text, string foreColor = "WHITE", string backColor = "BLACKBG")
        {
            ValidateColors(ref foreColor, ref backColor);
            Console.ForegroundColor = conColors[foreColor];
            Console.BackgroundColor = conColors[backColor];
            Console.WriteLine(text);
            Console.ResetColor();
        }
        private static Tuple<bool, string, string> ProcessInput(string prompt, string promptChar, string textColor, string backColor, double min, double max, string dataType)
        {
            string message = "";
            bool valid = false;
            if (dataType.ToLower().Substring(0, 3) == "str") dataType = "string";
            if (dataType.ToLower().Substring(0, 3) == "int") dataType = "int";
            string userInput = Input(prompt, promptChar, textColor, backColor);

            if (dataType == "string")
            {
                if (userInput.Length == 0 && min > 0 )  message = "Just pressing the Enter key doesn't work...";
                else if (userInput.Length > max)        message = $"Try entering between {min} and {max} characters...";
                else                                    valid = true;
            }
            else //integer, float, bool
            {         
                if (dataType == "bool")
                {
                    if (userInput.Substring(0, 1).ToLower() == "y")
                    {
                        userInput = "true";
                        valid = true;
                    }
                    else if (userInput.Substring(0, 1).ToLower() == "n")
                    {
                        userInput = "false";
                        valid = true;
                    }
                    else                                                    message = "Only anything starting with y or n is accepted...";
                }
                else
                {
                    if (dataType == "int")
                    {
                        if (int.TryParse(userInput, out int conversion))
                        {
                            if (conversion >= min && conversion <= max)     valid = true;
                            else                                            message = $"Try a number from {min} to {max}...";
                        }
                    }
                    else if (dataType == "real")
                    {
                        if (double.TryParse(userInput, out double conversion))
                        {
                            if (conversion >= min && conversion <= max)     valid = true;
                            else                                            message = $"Try a number from {min} to {max}...";
                        }
                    }
                    if (!valid && message == "")
                    {
                        if (userInput == string.Empty) message = $"Try entering a number: 'Enter' does not work...";
                        else
                        {
                            if(dataType == "int")   message = $"Try entering a whole number: {userInput} cannot be converted...";
                            else                    message = $"Try entering a decimal number: {userInput} cannot be converted...";
                        }
                    }
                }
            }
            return new Tuple<bool, string, string>(valid, userInput, message); //tuple.Item1 string can be converted to bool, int or float
        }
        public static void Quit(bool withConfirm = true)
        {
            if (withConfirm)
            {
                Console.Write("Enter to quit");
                Console.ReadKey();
            }
        }
        public static void ResetSeparator()
        {
            sep = "~";
            InitialseColourPrint(true);
        }
        private static List<string> SelectCharacterList(string part, string style)
        {
            /// select correct character list depending on style and part ///
            List<string> s = sSymbolsTop;
            switch (part)
            {
                case "top":                                 // 0 1 2 3 <- index
                    s = sSymbolsTop;                        // ┌ ─ ┐ ┬
                    if (style == "d") s = dSymbolsTop;      // ╔ ═ ╗ ╦
                    break;
                case "mid":
                    s = sSymbolsMid;                        // ├ ─ ┤ ┼
                    if (style == "d") s = dSymbolsMid;      // ╠ ═ ╣ ╬
                    break;
                case "bottom":
                    s = sSymbolsBottom;                     // └ ─ ┘ ┴ 
                    if (style == "d") s = dSymbolsBottom;   // ╚ ═ ╝ ╩
                    break;
                case "body":
                    s = sSymbolsBody;                     // └ ─ ┘ ┴ 
                    if (style == "d") s = dSymbolsBody;   // ╚ ═ ╝ ╩
                    break;
            }
            return s;
        }
        public static void SetConsole(int cols, int rows, string foreColor = "WHITE", string backColor = "BLACKBG")
        {
            /// Setup size and colour scheme of the console ///
            ValidateColors(ref foreColor, ref backColor);
            windowWidth = cols;
            windowHeight = rows;
            Console.SetWindowSize(windowWidth + 1, windowHeight); // eg 80 cols requested, add 1 to prevent overflow to next line
            Console.BufferHeight = windowHeight;
            Console.ForegroundColor = conColors[foreColor];
            Console.BackgroundColor = conColors[backColor];
            Console.Clear();
        }
        public static List<string> SplitClean(string stringToSplit, char sepChar)
        {
            /// .Split often gives empty array elements. This returns a list of non-empty strings ///
            List<string> retValue = new List<string>();
            string[] temp = stringToSplit.Split(sepChar);
            foreach (string part in temp)
            {
                if (part.Length > 0 || sepChar == '\n') // allow empty elements if \n\n supplied to create blank lines
                {
                    retValue.Add(part);
                }
            }

            return retValue;
        }
        private static void ValidateAlignment(ref string align)
        {
            /// check and correct align strings, else error ///
            align = align.Trim().ToLower();
            List<string> check = new List<string> { "left", "centre", "center", "right" };
            if (!check.Contains(align)) throw new AlignException("Align parameter must be: 'left', 'centre', 'center', 'right'");
        }
        private static void ValidateBoxPart(ref string part)
        {
            /// check and correct box part strings, else error ///
            part = part.Trim().ToLower();
            List<string> check = new List<string> { "top", "mid", "bottom", "body" };
            if (!check.Contains(part)) throw new BoxPartException("Part parameter must be: 'top', 'mid', 'bottom', 'body'");
        }
        private static void ValidateColors(ref string foreColor, ref string backColor, bool toConstant = false)
        {
            /// Fore and back colours passed as pointers, so changing them here does not require a return  ///
            bool isTagged = false;
            string modifiedForeColor = foreColor.ToUpper();
            string modifiedBackColor = backColor.ToUpper();
            if (modifiedBackColor.StartsWith(sep) && modifiedBackColor.EndsWith(sep))
            {
                isTagged = true;
                if (!modifiedBackColor.EndsWith("BG" + sep)) modifiedBackColor += $"BG{sep}";
            }
            else
            {
                if (!modifiedBackColor.EndsWith("BG")) modifiedBackColor += "BG";
            }
            if (!colors.ContainsKey(modifiedForeColor) && !colors.ContainsValue(modifiedForeColor)) throw new ColorValueException($"foreground colour must match name in any case: 'white' or 'WhiTE' not {foreColor}");
            if (!colors.ContainsKey(modifiedBackColor) && !colors.ContainsValue(modifiedBackColor)) throw new ColorValueException($"background colour must match name in any case: 'black' or 'bLACk' not {backColor}");
            if (backColor == foreColor) throw new Exception("You are advised NOT to try the same colour for text and background!");
            if (toConstant) // change the colour string to a value recognised by ColorPrint eg ~RED~
            {
                if (!isTagged)
                {
                    // add colour tags (default ~ to ends of colour string)
                    foreColor = sep + modifiedForeColor + sep; //got through checks so re-assign original parameters by reference
                    backColor = sep + modifiedBackColor + sep;
                }
            }
            else
            {
                foreColor = modifiedForeColor; //got through checks so re-assign original parameters by reference
                backColor = modifiedBackColor;
            }
        }
        
        #endregion
        #region demo code ***Can be deleted****
        public static void ColorPrintDemo()
        {
            Console.Clear();
            ColorPrint($"{WHITE}This line is white on black.");
            ColorPrint($"{GREY}This line is grey on black.");
            ColorPrint($"{DGREY}This line is dark grey on black.");
            ColorPrint($"{BLUE}This line is blue on black.");
            ColorPrint($"{GREEN}This line is green on black.");
            ColorPrint($"{CYAN}This line is cyan on black.");
            ColorPrint($"{RED}This line is red on black.");
            ColorPrint($"{MAGENTA}This line is magenta on black.");
            ColorPrint($"{YELLOW}This line is yellow on black.");
            ColorPrint($"{DBLUE}This line is dark blue on black.");
            ColorPrint($"{DGREEN}This line is dark green on black");
            ColorPrint($"{DCYAN}This line is dark cyan on black.");
            ColorPrint($"{DRED}This line is dark red on black.");
            ColorPrint($"{DMAGENTA}This line is dark magenta on black.");
            ColorPrint($"{DYELLOW}This line is dark yellow on black.");
            ColorPrint($"{BLACK}{WHITEBG}This line is black on white.");
            ColorPrint($"{WHITE}This line is white, and now {RED}red on black.");
            DisplayMessage("Press Enter to continue...", true, false);
        }
        public static string GetInputDemo(string demoType, string description)
        {
            /// Most UI operations should be done within this UI class  ///
            /// Try to keep all I/O operations out of other classes     ///
            /// This will make transfer of code to a GUI much easier    ///
            bool valid = false;
            string userInput = "";
            while (!valid)
            {
                Clear();
                string message = string.Empty;
                Tuple<bool, string, string> getData = new Tuple<bool, string, string> (false,"","");
                int numLines = DrawMultiLineBox("s", description, "yellow", "black", "white", "black", "left");
                AddLines(5, numLines); // pad Console to leave 5 empty lines
                DrawLine("d", "white", "black"); // now leaves 4 empty lines
                switch(demoType)
                {
                    case "string":
                        getData = GetString("UI.GetString: Type your name (1-10 chars)",
                                            ">_", "green", "black", true, 1, 10);
                        break;
                    case "int":
                        getData = GetInteger("UI.GetInteger: Type your age (integer only)",
                                             ">_", "cyan", "black", 1, 100);
                        break;

                    case "real":
                        getData = GetRealNumber("UI.GetRealNumber: Type your height in metres (0.5 to 2.0)",
                                                ">_", "magenta", "black", 0.5, 2);
                        break;

                    case "bool":
                        getData = GetBoolean("UI.GetBoolean: Is this library useful (y/n)?",
                                             ">_", "blue", "black");
                        break;
                }
                
                valid = getData.Item1;
                userInput = getData.Item2;
                message = getData.Item3;
                if (message != string.Empty) DisplayMessage(message, false, true, "red", "black");
            }
            return userInput;
        }
        #endregion
    }
}
