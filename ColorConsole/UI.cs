using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        /* Character set used in old DOS programs to draw lines and boxes
          ┌───────┬───────┐   ╔═════╦═════╗ 
          │       │       │   ║     ║     ║
          ├───────┼───────┤   ╠═════╬═════╣ 
          │       │       │   ║     ║     ║
          └───────┴───────┘   ╚═════╩═════╝
         */
         static readonly List<string> sSymbolsTop = new List<string>     { "┌", "─", "┐", "┬" };
         static readonly List<string> sSymbolsBottom = new List<string>  { "└", "─", "┘", "┴" };
         static readonly List<string> sSymbolsBody = new List<string>    { "│", " ", "│", "│" };
         static readonly List<string> sSymbolsMid = new List<string>     { "├", "─", "┤", "┼" };

         static readonly List<string> dSymbolsTop = new List<string>     { "╔", "═", "╗", "╦" };
         static readonly List<string> dSymbolsBottom = new List<string>  { "╚", "═", "╝", "╩" };
         static readonly List<string> dSymbolsBody = new List<string>    { "║", " ", "║", "║" };
         static readonly List<string> dSymbolsMid = new List<string>     { "╠", "═", "╣", "╬" };

        #endregion
        #region static constructor
        static UI() 
        {
            /// static class "constructor" runs only once on first use ///
            Initialise();
        }
        #endregion
        #region Core functions
        public static int AddLines(int numLines = 25, string foreColor = "WHITE", string backColor = "BLACKBG") //default 25 lines if not given
        {
            /// Adds numLines blank lines in specified or default colour ///
            ValidateColors(ref foreColor, ref backColor);
            string blank = "".PadRight(windowWidth); //string of spaces across entire width of Console
            if (numLines > 0)
            {
                for (int i = 0; i < numLines; i++)
                {
                    ColorPrint($"{foreColor}{backColor}{blank}");
                }
            }
            return numLines;
        }
        public static int AddLines(int leaveLines, int currentLines, string foreColor = "WHITE", string backColor = "BLACKBG") 
        {
            /// overload 1: Adds blank lines until windowHeight - leaveLines in specified or default colour ///
            ValidateColors(ref foreColor, ref backColor);
            string blank = "".PadRight(windowWidth); //string of spaces across entire width of Console
            int numLines = windowHeight - currentLines - leaveLines;
            if (numLines > 0)
            {
                for (int i = 0; i < numLines; i++)
                {
                    ColorPrint($"{foreColor}{backColor}{blank}");
                }
            }
            return numLines;
        }
        public static void ChangeSeparator(string value)
        {
            /// Allows use of different character to separate colour tags. Default is ~ ///
            sep = value.Substring(0, 1);    // if user supplies more than 1 character, use the first only. Stored as string
            Initialise(true);               //re-initialise to make use of new character
        }
        public static void Clear()
        {
            /// Clears the Console (Allows calls from other classes not involved with UI) ///
            Console.Clear();
        }
        public static int ColorPrint(string value, bool newline = true, bool reset = true)
        {
            ///  This uses the default char ~ to separate colour strings                ///
            ///  change the line:  static string sep = "~"; as required                 ///
            ///  other possibles are ` (backtick) ¬ (?) any character you will not use  ///

            //  example value = "~RED~This is a line of red text"                      
            int numLines = 0;
            // SplitClean returns a List<string> and removes empty items
            List<string> lineParts = SplitClean(value, sep[0]); // = {'RED', 'This is a line of red text'} 
            foreach (string part in lineParts)
            {
                if (colors.ContainsKey(part.ToUpper())) // is 'RED' in the colors dictionary?
                {
                    if (part.ToUpper().Contains("BG")) // background colour eg 'BLACKBG'
                    {
                        Console.BackgroundColor = conColors[part.ToUpper()]; // get the ConsoleColor from dictionary
                    }
                    else if (part.ToUpper() == "RESET") // reset command
                    {
                        Console.ResetColor();
                    }
                    else // foreground colour
                    {
                        Console.ForegroundColor = conColors[part.ToUpper()]; // get the ConsoleColor from dictionary
                    }
                }
                else // not a colour command so print it out without newline
                {
                    if (part.Contains("\n")) numLines++;
                    Console.Write(part);
                }
            }
            if (newline)
            {
                Console.Write("\n");    // Add newline to complete the print command
                numLines++;
            }
            if (reset)  Console.ResetColor();
            return numLines;
        }
        public static int DisplayMessage(string message, bool useInput, bool useTimer, string foreColor = "WHITE", string backColor = "BLACKBG", int delay = 2000) // string version
        {
            /// show user message or default, either wait for 2 secs, or ask user to press enter ///
            int numLines = message.Count(f => (f == '\n'));
            ValidateColors(ref foreColor, ref backColor);
            if (useTimer) //pause for delay mS
            {
                if (message != string.Empty)
                {
                    ColorPrint($"{foreColor}{backColor}{message}");
                    Thread.Sleep(delay);  
                }
            }
            if (useInput) // wait for Enter key
            {
                if (message == string.Empty) message = "Press Enter to continue";
                else message = $"{foreColor}{backColor}{message}";
                Input(message, "...", foreColor, backColor);
            }
            numLines += 1;
            return numLines;
        }
        public static int DisplayMessage(List<string> messages, bool useInput, bool useTimer, string foreColor = "WHITE", string backColor = "BLACKBG", int delay = 2000) //list vesion
        {
            /// show user message or default, either wait for 2 secs, or ask user to press enter ///
            ValidateColors(ref foreColor, ref backColor);
            int numLines = 0;
            foreach (string message in messages)
            {
                ColorPrint($"{foreColor}{backColor}{message}");
                numLines++;
            }
            if (useTimer) Thread.Sleep(delay); //pause for delay mS
            if (useInput)
            {
                Input("Press Enter to continue", "...", WHITE, BLACK);
                numLines++;
            }
            return numLines;
        } 
        public static int DrawBoxBody(string style, string text, string boxAlign, string foreColor, string backColor,
                                      string textColor = "WHITE", string textBackColor = "BLACKBG", string textAlign = "left", int width = 0)
        {
            /// print out single line body section of a box with text and/or spaces: "║  text  ║"
            style = FixStyle(style);
            ValidateColors(ref foreColor, ref backColor);
            ValidateColors(ref textColor, ref textBackColor);
            ValidateAlignment(ref boxAlign);                            // check alignments are in permitted list
            ValidateAlignment(ref textAlign);
            List<string> s = SelectCharacterList("body", style);        // { "║", " ", "║", "║" };
            if (width > windowWidth || width <= 0) width = windowWidth;
            string output = PadString(text, width - 2, " ", textAlign);  // "  text  " -2 to allow for start/end margin chars
            string[] sides = { s[0], s[2] };
            sides = PadBoxSides(sides, output, boxAlign);
            // foreColor border on backColor -> colour formatted text -> foreColor border on backColor
            ColorPrint($"{foreColor}{backColor}{sides[0]}{textColor}{textBackColor}{output}{foreColor}{backColor}{sides[1]}");

            return 1; // single line used
        }
        public static int DrawBoxOutline(string style, string part, string foreColor, string backColor, string boxAlign = "left", int width = 0, bool midMargin = false)
        {
            /// Draw the top, mid or bottom of a box to width ///
            style = FixStyle(style);
            ValidateColors(ref foreColor, ref backColor);
            ValidateAlignment(ref boxAlign);
            ValidateBoxPart(ref part);
            string[] sides = { "", "" };
            List<string> s = SelectCharacterList(part, style);
            if (width > windowWidth || width <= 0) width = windowWidth;
            sides[0] = s[0];                                            // start with left corner
            sides[1] = s[2];                                            // end with right corner
            if (midMargin)
            {
                sides[0] = SelectCharacterList("mid", style)[0];        // "├"
                sides[1] = SelectCharacterList("mid", style)[2];        // "┤"
            }
            string output = PadString("", width - 2, s[1], boxAlign);   // -2 to allow for start/end margin chars
            sides = PadBoxSides(sides, output, boxAlign);
            // line padded, now do same for spaces around the line
            output = $"{foreColor}{backColor}{sides[0]}{output}{foreColor}{backColor}{sides[1]}";
            ColorPrint(output);

            return 1; // single line used
        }
        public static int DrawLine(string style, string foreColor, string backColor, int width = 0, string align = "left")
        {
            ///  Draw a single or double line across the console with fore and back colours set  ///
            style = FixStyle(style);
            ValidateColors(ref foreColor, ref backColor);
            ValidateAlignment(ref align);
            // width has to be 1 less than windowWidth or spills over to next line: Odd behaviour of C# Console?
            if (width > windowWidth || width <= 0) width = windowWidth;
            List<string> s = SelectCharacterList("mid", style);
            string output;
            if (width == windowWidth)
            {
                output = PadString("", width, s[1], "left");          // -2 to allow for start/end margin chars
                output = $"{foreColor}{backColor}{output}";           //eg "~WHITE~~BLACKBG~════════════════════"
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
        public static int DrawMultiBoxBody(List<string> styles, List<int> sizes, List<string> foreColors, List<string> backColors, List<string> textLines, List<string> alignments, int padding = 0)
        {
            /// print out single line mid section of multiple boxes with or without text
            /// ║  box 0  ║ ║   box 1   ║ │  box 2  │
            if (styles.Count != sizes.Count && styles.Count != foreColors.Count && styles.Count != backColors.Count && styles.Count != alignments.Count)
            {
                throw new MatchingTagsException("All supplied parameter lists must have the same number of items");
            }
            for (int i = 0; i < styles.Count; i++)
            {
                styles[i] = FixStyle(styles[i]);
            }
            if (foreColors.Count != backColors.Count) throw new ColorValueException($"DrawMultiBoxOutline: Number of foreColours must equal number of backColours");
            for (int i = 0; i < foreColors.Count; i++) //validate supplied colours
            {
                string foreColor = foreColors[i];
                string backColor = backColors[i];
                ValidateColors(ref foreColor, ref backColor);
                foreColors[i] = foreColor;
                backColors[i] = backColor;
            }
            for (int i = 0; i < alignments.Count; i++)
            {
                string align = alignments[i];
                ValidateAlignment(ref align);
                alignments[i] = align;
            }
            List<string> s;
            List<string> outputs = new List<string>();
            int boxLength;
            string output;
            for (int i = 0; i < sizes.Count; i++)
            {
                // size examples {15, 40, 25} = 80 cols
                s = SelectCharacterList("body", styles[i]);
                if (i < sizes.Count - 1) boxLength = sizes[i] - padding - 2;    // -2 as is box edge, and characters will be added both sides
                else boxLength = sizes[i] - 2;
                string lSide = s[0];                                            // start with left side char
                string rSide = s[3];                                            // end with right side char
                //check length of string
                output = textLines[i];
                int colorTagSpaces = FormatColorTags(ref output, colorTagList: out List<string> colorTagList);
                if (output.Length > sizes[i] - 2 + colorTagSpaces) output = output.Substring(0, sizes[i] - 2 + colorTagSpaces);
                output = PadString(output, boxLength, " ", alignments[i]);
                output = $"{foreColors[i]}{backColors[i]}{lSide}{output}{foreColors[i]}{backColors[i]}{rSide}";
                outputs.Add(output);                                            // create new list item with completed string
            }

            output = "";
            foreach (string line in outputs)                                    // concatenate all box outlines in outputs
            {
                output += line;
            }
            ColorPrint(output);

            return 1; // single line used
        }
        public static int DrawMultiBoxOutline(List<string> styles, string part, List<int> sizes, List<string> foreColors, List<string> backColors, int padding = 0)
        {
            /// Draw the top/bottoms of sizes.Count boxes, width in absolute values
            /// ╔══════════╗ ┌────────┐ ╔════════════════════╗ 
            if (styles.Count != sizes.Count && styles.Count != foreColors.Count && styles.Count != backColors.Count)
            {
                throw new MatchingTagsException("All supplied parameter lists must have the same number of items");
            }
            for (int i = 0; i < styles.Count; i++)
            {
                styles[i] = FixStyle(styles[i]);
            }
            for (int i = 0; i < foreColors.Count; i++)
            {
                string foreColor = foreColors[i];
                string backColor = backColors[i];
                ValidateColors(ref foreColor, ref backColor);
                foreColors[i] = foreColor;
                backColors[i] = backColor;
            }
            List<string> s;
            List<string> outputs = new List<string>();
            //int numBoxes;
            //int width;
            int boxLength;
            string output;
            for (int i = 0; i < sizes.Count; i++)
            {
                // size examples {15, 40, 25} = 80 cols
                s = SelectCharacterList(part, styles[i]);
                if (i < sizes.Count - 1) boxLength = sizes[i] - padding - 2;     // -2 as is box edge, and characters will be added both sides
                else boxLength = sizes[i] - 2;
                string lSide = s[0];                                                // start with left corner
                string rSide = s[2];                                                // end with right corner
                output = PadString("", boxLength, s[1], "left");
                output = $"{foreColors[i]}{backColors[i]}{lSide}{output}{foreColors[i]}{backColors[i]}{rSide}";
                outputs.Add(output);        // create new list item with completed string
            }

            output = string.Empty;
            foreach (string line in outputs) // concatenate all box outlines in outputs
            {
                output += line;
            }
            ColorPrint(output);

            return 1; //single line
        }
        public static int DrawGridBody( string style, string part, List<int> columns, string boxColor, string boxBackColor,
                                        string textColor, string textBackColor, List<string> textLines, List<string> alignments)
        {
            /// Draw the body of a grid to width
            /// ║  box 0  ║    box 1   ║         box 2        ║
            style = FixStyle(style);
            ValidateColors(ref boxColor, ref boxBackColor);
            ValidateColors(ref textColor, ref textBackColor);
            List<string> s = SelectCharacterList(part, style);              //  "│", " ", "│", "│"
            List<string> outputs = new List<string>();
            string output;
            for (int col = 0; col < columns.Count; col++)
            {
                int colWidth = columns[col];                                // eg {10, 20, 20, 30 } = 80 cols
                string lSide;
                string rSide;
                if (col == 0)
                {
                    lSide = s[0];                                           // "│"
                    rSide = s[3];                                           // "│"
                }
                else if (col == columns.Count - 1)
                {
                    lSide = s[1];                                           // "│"
                    rSide = s[2];                                           // "│"
                }
                else
                {
                    lSide = s[1];                                           // " "
                    rSide = s[3];                                           // "│"
                }
                string text = "";
                if (col < textLines.Count) text = textLines[col];
                output = PadString(text, colWidth - 2, " ", alignments[col]);          // -2 to allow for start/end margin chars
                output = $"{boxColor}{boxBackColor}{lSide}{textColor}{textBackColor}{output}{boxColor}{boxBackColor}{rSide}";
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
            /// Draw the top, mid or bottom of a grid to width 
            /// ┌───────────┬───────────┬───────────┬───────────┬──────────┐
            style = FixStyle(style);
            ValidateColors(ref foreColor, ref backColor);
            List<string> s = SelectCharacterList(part, style);              // "┌", "─", "┐", "┬"
            List<string> outputs = new List<string>();
            string output;
            for (int col = 0; col < columns.Count; col++)
            {
                int colWidth = columns[col];                                // eg {10, 20, 20, 30 } = 80 cols
                string lSide;                                               // "┌"
                string rSide;                                               // "┐"
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
            foreach (string line in outputs)                                // concatenate all box outlines in outputs
            {
                output += line;
            }
            ColorPrint(output);

            return 1; // single line used
        }
        public static int DrawMultiLineBox(string style, string text, string foreColor, string backColor, string textColor = "WHITE", string textBackColor = "BLACKBG", string boxAlign = "left", string textAlign = "left", int width = 0)
        {
            /// Draw a single box containing many lines of text
            /// Convert the supplied string 'text' into a list and pass to same function with over-ride
            List<string> textLines = SplitClean(text, '\n');
            return DrawMultiLineBox(style, textLines, foreColor, backColor, textColor, textBackColor, boxAlign, textAlign, width);
        }
        public static int DrawMultiLineBox(string style, List<string> textLines, string foreColor, string backColor, string textColor = "WHITE", string textBackColor = "BLACKBG", string boxAlign = "left", string textAlign = "left", int width = 0)
        {
            ///  Draw a single box containing many lines of text, split at whole words
            int numLines = 0;
            style = FixStyle(style);
            ValidateColors(ref foreColor, ref backColor);
            if (width > windowWidth || width <= 0) width = windowWidth;
            numLines += DrawBoxOutline(style, "top", foreColor, backColor, boxAlign, width);
            textLines = GetFormattedLines(textLines, width);
            foreach (string line in textLines)
            {
                numLines += DrawBoxBody(style, line, boxAlign, foreColor, backColor, textColor, textBackColor, textAlign, width);
            }
            numLines += DrawBoxOutline(style, "bottom", foreColor, backColor, boxAlign, width);

            return numLines;
        }
        private static string FixStyle(string style)
        {
            /// format given style to either "s" or "d"
            style = style.Trim().ToLower();
            if (style != "s" && style != "d") style = "s";
            return style;
        }
        private static int FormatColorTags(ref string text, out List<string> colorTagList)
        {
            /// Checks supplied string has matching tags to define colour strings
            /// If string is split on ~ alone, no record of whether this was a colour tag
            colorTagList = new List<string>();
            // use Linq to check if even numbers of tags
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
                            colourTagSpaces += data[i].Length;
                            colorTagList.Add(sep + data[i] + sep);
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
        public static bool GetBoolean(int row, string prompt, string promptChar, string textColor, string backColor)
        {
            /// gets a boolean (yes/no) type entry from the user
            string userInput = ProcessInput(row, prompt, promptChar, textColor, backColor, 1, 3, "bool");
            return Convert.ToBoolean(userInput);
        }
        public static double GetRealNumber(int row, string prompt, string promptChar, string textColor, string backColor, double min, double max)
        {
            /// gets a float / double from the user
            string userInput = ProcessInput(row, prompt, promptChar, textColor, backColor, min, max, "real");
            return Convert.ToDouble(userInput);
        }
        public static int GetInteger(int row, string prompt, string promptChar, string foreColor, string backColor, double min = 0, double max = 65536)
        {
            /// Public Method: gets an integer from the user ///
            string userInput = ProcessInput(row, prompt, promptChar, foreColor, backColor, min, max, "int");
            return Convert.ToInt32(userInput);
        }
        public static string GetString(int row, string prompt, string ending, string foreColor, string backColor, bool withTitle, int min, int max)
        {
            /// Public Method: gets a string from the user ///
            string userInput = ProcessInput(row, prompt, ending, foreColor, backColor, min, max, "string");
            if (withTitle)
            {
                TextInfo textInfo = new CultureInfo("en-UK", false).TextInfo;
                userInput = textInfo.ToTitleCase(userInput);
            }
            return userInput;
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
            ///takes a list of lines , checks length of each and adds additional lines if required, returns list
            // check length of each line. max = windowWidth
            if (maxLength > windowWidth || maxLength <= 0) maxLength = windowWidth;
            if (!noBorder) maxLength -= 2; //default  80-2 = 78 chars  
            List<string> newLines = new List<string>();
            for (int index = 0; index < textLines.Count; index++) 
            {
                string line = textLines[index].Trim();                  // remove leading/trailing spaces
                int colorTagSpaces = 0;
                // now check the length of each line and cut into shorter sections if required
                if (line.Length == 0) newLines.Add(line);               // add empty
                else
                {
                    while (line.Length >= maxLength + colorTagSpaces)   // line length > 77 in 80 col terminal
                    {
                        string text = GetMaxLengthString(line, maxLength, out colorTagSpaces);
                        newLines.Add(text);
                        line = line.Substring(text.Length).Trim();
                    }
                    if (line.Length > 0) newLines.Add(line);            // partial line left over from the while loop ( <maxLength) or short line
                }
            }
            return newLines;
        }
        private static string GetMaxLengthString(string text, int maxLength, out int colorTagSpaces)
        {
            int colorTagSpacesCount = FormatColorTags(ref text, out List<string> colorTagList);
	        if (text.Length > maxLength + colorTagSpacesCount)
            {
                text = text.Substring(0, maxLength + colorTagSpacesCount);
                int ending = text.LastIndexOf(" ");
                text = text.Substring(0, ending);
	        }
            colorTagSpaces = colorTagSpacesCount;
            return text;
        }
        private static void Initialise(bool reset = false)
        {
            /// initialises "constants" (variables in CAPS) lists and dictionaries
            /// runs when this class is first referenced, and if user changes the colour tagging character
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
            conColors.Add("BLUEBG", ConsoleColor.Blue);
            conColors.Add("GREENBG", ConsoleColor.Green);
            conColors.Add("CYANBG", ConsoleColor.Cyan);
            conColors.Add("REDBG", ConsoleColor.Red);
            conColors.Add("MAGENTABG", ConsoleColor.Magenta);
            conColors.Add("YELLOWBG", ConsoleColor.Yellow);
            conColors.Add("DBLUEBG", ConsoleColor.DarkBlue);
            conColors.Add("DGREENBG", ConsoleColor.DarkGreen);
            conColors.Add("DCYANBG", ConsoleColor.DarkCyan);
            conColors.Add("DREDBG", ConsoleColor.DarkRed);
            conColors.Add("DMAGENTABG", ConsoleColor.DarkMagenta);
            conColors.Add("DYELLOWBG", ConsoleColor.DarkYellow);

            conColors.Add($"{sep}BLACK{sep}", ConsoleColor.Black);
            conColors.Add($"{sep}GREY{sep}", ConsoleColor.Gray);
            conColors.Add($"{sep}GRAY{sep}", ConsoleColor.Gray);
            conColors.Add($"{sep}DGREY{sep}", ConsoleColor.DarkGray);
            conColors.Add($"{sep}DGRAY{sep}", ConsoleColor.DarkGray);
            conColors.Add($"{sep}WHITE{sep}", ConsoleColor.White);
            conColors.Add($"{sep}BLUE{sep}", ConsoleColor.Blue);
            conColors.Add($"{sep}GREEN{sep}", ConsoleColor.Green);
            conColors.Add($"{sep}CYAN{sep}", ConsoleColor.Cyan);
            conColors.Add($"{sep}RED{sep}", ConsoleColor.Red);
            conColors.Add($"{sep}MAGENTA{sep}", ConsoleColor.Magenta);
            conColors.Add($"{sep}YELLOW{sep}", ConsoleColor.Yellow);
            conColors.Add($"{sep}DBLUE{sep}", ConsoleColor.DarkBlue);
            conColors.Add($"{sep}DGREEN{sep}", ConsoleColor.DarkGreen);
            conColors.Add($"{sep}DCYAN{sep}", ConsoleColor.DarkCyan);
            conColors.Add($"{sep}DRED{sep}", ConsoleColor.DarkRed);
            conColors.Add($"{sep}DMAGENTA{sep}", ConsoleColor.DarkMagenta);
            conColors.Add($"{sep}DYELLOW{sep}", ConsoleColor.DarkYellow);

            conColors.Add($"{sep}BLACKBG{sep}", ConsoleColor.Black);
            conColors.Add($"{sep}GREYBG{sep}", ConsoleColor.Gray);
            conColors.Add($"{sep}GRAYBG{sep}", ConsoleColor.Gray);
            conColors.Add($"{sep}DGREYBG{sep}", ConsoleColor.DarkGray);
            conColors.Add($"{sep}DGRAYBG{sep}", ConsoleColor.DarkGray);
            conColors.Add($"{sep}WHITEBG{sep}", ConsoleColor.White);
            conColors.Add($"{sep}BLUEBG{sep}", ConsoleColor.Blue);
            conColors.Add($"{sep}GREENBG{sep}", ConsoleColor.Green);
            conColors.Add($"{sep}CYANBG{sep}", ConsoleColor.Cyan);
            conColors.Add($"{sep}REDBG{sep}", ConsoleColor.Red);
            conColors.Add($"{sep}MAGENTABG{sep}", ConsoleColor.Magenta);
            conColors.Add($"{sep}YELLOWBG{sep}", ConsoleColor.Yellow);
            conColors.Add($"{sep}DBLUEBG{sep}", ConsoleColor.DarkBlue);
            conColors.Add($"{sep}DGREENBG{sep}", ConsoleColor.DarkGreen);
            conColors.Add($"{sep}DCYANBG{sep}", ConsoleColor.DarkCyan);
            conColors.Add($"{sep}DREDBG{sep}", ConsoleColor.DarkRed);
            conColors.Add($"{sep}DMAGENTABG{sep}", ConsoleColor.DarkMagenta);
            conColors.Add($"{sep}DYELLOWBG{sep}", ConsoleColor.DarkYellow);
        }
        public static string Input(string prompt, string ending, string foreColor, string backColor)
        {
            /// Get keyboard input from user (requires Enter )
            /// check if prompt contains embedded colours

            FormatColorTags(ref prompt, out List<string> embeddedColors); //extract any colour info from prompt eg ~red~
            foreach(string color in embeddedColors)
            {
                if (color.Contains("BG")) backColor = color;
                else foreColor = color;
            }
            ValidateColors(ref foreColor, ref backColor);
            ColorPrint($"{foreColor}{backColor}{prompt + ending}", false); //do not use newline
            return Console.ReadLine();
        }
        public static string InputBox(string style, string returnType, string title, string boxMessage, string inputPrompt, string promptEnd, string foreColor = "WHITE", 
                                       string backColor = "BLACKBG", int width = 0, int minReturnLen = 1, int maxReturnLen = 20, bool withTitle = false)
        {
            /// Draw an inputBox with title, message, input area
            /// Example "bool", "File Exists Warning", "Are you sure you want to over-write?", "Confirm over-write (y/n)_"
            style = FixStyle(style);
            ValidateColors(ref foreColor, ref backColor);
            if (width > windowWidth || width <= 0) width = windowWidth;
            Clear();
            string userInput = "";
            List<string> lines = GetFormattedLines(boxMessage); // returns alist of lines max length of any line Console.Windowwidth - 3
            int numLines = DrawBoxOutline(style, "top", foreColor, backColor, "centre", width); // draw top of box double line, yellow
            numLines += DrawBoxBody(style, $"{title}", "centre", foreColor, backColor, foreColor, backColor, "centre", width); // draw title
            numLines += DrawBoxOutline(style, "mid", foreColor, backColor, "centre", width); // draw single line
            foreach (string line in lines)
            {
                numLines += DrawBoxBody(style,  $"{line}", "centre", foreColor, backColor, foreColor, backColor, "left", width); // draw each line of text
            }
            numLines += DrawBoxBody(style, "", "centre", foreColor, backColor, foreColor, backColor, "centre", width); // draw empty line
            numLines += DrawBoxOutline(style, "bottom", foreColor, backColor, "centre", width); // draw bottom of box double line, yellow
            numLines += AddLines(5, numLines);
            numLines += DrawLine("d", WHITE, BLACKBG);

            if (returnType == "str" || returnType == "string")
            {
                userInput = GetString(numLines, inputPrompt, promptEnd, foreColor, backColor, withTitle, minReturnLen, maxReturnLen);
            }
            else if (returnType == "int")
            {
                userInput = GetInteger(numLines, inputPrompt, promptEnd, foreColor, backColor, minReturnLen, maxReturnLen).ToString();
            }
            else if (returnType == "real" || returnType == "float" || returnType == "double")
            {
                userInput = GetRealNumber(numLines, inputPrompt, promptEnd, foreColor, backColor, minReturnLen, maxReturnLen).ToString();
            }
            else if (returnType == "bool" || returnType == "boolean")
            {
                userInput = GetBoolean(numLines, inputPrompt, promptEnd, "", "").ToString();
            }
            return userInput; // string eg filename typed in, || bool
        }
        public static int Menu(string style,  string title, string promptChar, List<string> textLines, string foreColor = "WHITE",
                                string backColor = "BLACKBG", string align = "left", int width = 0)
        {
            /// displays a menu using the text in 'title', and a list of menu items (string)
            /// This menu will re-draw until user enters correct data
            style = FixStyle(style);
            ValidateColors(ref foreColor, ref backColor);
            if (width > windowWidth || width <= 0) width = windowWidth;            
            for (int i = 0; i < textLines.Count; i++)
            {
                if ( i < 9)     textLines[i] = $"     {i + 1}) {textLines[i]}";
                else            textLines[i] = $"    {i + 1}) {textLines[i]}";
            }
            Clear();
            int numLines = 0;
            numLines += DrawBoxOutline(style, "top", foreColor, backColor);           // draw top of box double line
            numLines += DrawBoxBody(style, "", "centre", foreColor, backColor);       // draw empty line
            numLines += DrawBoxBody(style, $"{title}", "left", foreColor, backColor); // draw title
            numLines += DrawBoxBody(style, "", "centre", foreColor, backColor);       // draw empty line
            for (int i = 0; i < textLines.Count; i++)
            {
                numLines += DrawBoxBody(style, $"{textLines[i]}", "left", foreColor, backColor); // draw menu options
            }
            numLines += DrawBoxBody(style, "", "centre", foreColor, backColor);       // draw empty line
            numLines += DrawBoxOutline(style, "bottom", foreColor, backColor);        // draw top of box double line, yellow
            // get multiple return variables using Tuple
            numLines += AddLines(5, numLines);
            numLines += DrawLine("d", WHITE, BLACKBG);
            int userInput = GetInteger(numLines, $"Type the number of your choice (1 to {textLines.Count})", promptChar, WHITE, BLACKBG, 1, textLines.Count);
            return userInput - 1;
        }
        private static string[] PadBoxSides(string[] sides, string text, string boxAlign)
        {
            /// Takes exising sides: ║║, text: some~red~coloured text, and pads to width of console using alignment
            if (boxAlign == "left") //pad rSide 
            {
                int length = windowWidth - text.Length - 2; // 80 - 78 - 2 = 0
                if (length > 0)    sides[1] = PadString(sides[1], length, " ", "right");    // "║    "
            }
            else if (boxAlign == "right") //pad lSide 
            {
                int length = windowWidth - text.Length - 2; // 80 - 78 - 1 = 1
                if (length > 0) sides[0] = PadString(sides[0], windowWidth - text.Length - 1, " ", "left");     // "    ║"
            }
            else // centre
            {
                int colorTagSpaces = FormatColorTags(ref text, out List<string> embeddedColors); //extract any colour info from body eg ~red~);
                int length = windowWidth - text.Length - 2 + colorTagSpaces; // 80 - 78 - 2 = 0
                if (length >= 2)
                {
                    sides[0] = PadString(sides[0], length / 2, " ", "right");
                    length = windowWidth - text.Length - sides[0].Length + colorTagSpaces;
                    if (length > 0)    sides[1] = PadString(sides[1], length, " ", "left");
                }
            }
            return sides;
        }
        private static string PadString(string text, int width, string padChar, string align = "left")
        {
            /// width should be width of the part to be padded, so excludes left/right margin characters
            string output = text;
            int colourTagSpaces = 0;
            if (text.Contains(sep[0])) // colour tag(s) present
            {
                colourTagSpaces = FormatColorTags(ref output, out List<string> embeddedColors); //extract any colour info from output eg ~red~ returns additional spaces required and fomats output by ref
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
        private static string ProcessInput(int row, string prompt, string promptChar, string textColor, string backColor, double min, double max, string dataType)
        {
            bool valid = false;
            if (dataType.ToLower().Substring(0, 3) == "str") dataType = "string";
            if (dataType.ToLower().Substring(0, 3) == "int") dataType = "int";
            string userInput = "";
            while (!valid)
            {
                string message = "";
                userInput = Input(prompt, promptChar, textColor, backColor);
                if (dataType == "string")
                {
                    if (userInput.Length == 0 && min > 0) message = "Just pressing the Enter key doesn't work...";
                    else if (userInput.Length > max) message = $"Try entering between {min} and {max} characters...";
                    else if (double.TryParse(userInput, out double conversion)) message = "You just entered a number";
                    else valid = true;
                }
                else //integer, float, bool
                {
                    if (userInput.Length == 0)
                    {
                        message = "Just pressing the Enter key doesn't work...";
                    }
                    else
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
                            else message = "Only anything starting with y or n is accepted...";
                        }
                        else
                        {
                            if (dataType == "int")
                            {
                                if (int.TryParse(userInput, out int conversion))
                                {
                                    if (conversion >= min && conversion <= max) valid = true;
                                    else message = $"Try a number from {min} to {max}...";
                                }
                            }
                            else if (dataType == "real")
                            {
                                if (double.TryParse(userInput, out double conversion))
                                {
                                    if (conversion >= min && conversion <= max) valid = true;
                                    else message = $"Try a number from {min} to {max}...";
                                }
                            }
                            if (!valid && message == "")
                            {
                                if (dataType == "int") message = $"Try entering a whole number: {userInput} cannot be converted...";
                                else message = $"Try entering a decimal number: {userInput} cannot be converted...";
                            }
                        }
                    }
                }
                if (!valid)
                {
                    ColorPrint($"{RED}{message}{MAGENTA} retry in 2 secs...");
                    Thread.Sleep(2000);
                    for (int i = row; i < row + 4; i++)
                    {
                        Console.SetCursorPosition(0, i);
                        Console.Write("".PadRight(windowWidth));
                    }
                    Console.SetCursorPosition(0, row);
                }
            }
            return  userInput; //string can be converted to bool, int or float
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
            Initialise(true);
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
                    s = sSymbolsBody;                       // └ ─ ┘ ┴ 
                    if (style == "d") s = dSymbolsBody;     // ╚ ═ ╝ ╩
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
        private static void SetConsoleColors(string foreColor, string backColor)
        {
            ValidateColors(ref foreColor, ref backColor);
            Console.ForegroundColor = conColors[foreColor];
            Console.BackgroundColor = conColors[backColor];
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
        public static int Teletype(string text, int delay, string foreColor = "WHITE", string backColor = "BLACKBG")
        {
            /// Visual effect of slow character printout
            SetConsoleColors(foreColor, backColor);
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                Thread.Sleep(delay);
            }
            Console.ResetColor();
            return 1;
        }
        private static void ValidateAlignment(ref string align)
        {
            /// check and correct align strings, else error
            align = align.Trim().ToLower();
            List<string> check = new List<string> { "left", "centre", "center", "right" };
            if (!check.Contains(align)) throw new AlignException("Align parameter must be: 'left', 'centre', 'center', 'right'");
        }
        private static void ValidateBoxPart(ref string part)
        {
            /// check and correct box part strings, else error
            part = part.Trim().ToLower();
            List<string> check = new List<string> { "top", "mid", "bottom", "body" };
            if (!check.Contains(part)) throw new BoxPartException("Part parameter must be: 'top', 'mid', 'bottom', 'body'");
        }
        private static void ValidateColors(ref string foreColor, ref string backColor)
        {
            /// Fore and back colours passed as pointers, so changing them here does not require a return
            string modifiedForeColor = foreColor.ToUpper(); //"red" -> "RED" "~red~" -> "~RED~"
            string modifiedBackColor = backColor.ToUpper(); //"red" -> "RED" "~redbg~" -> "~REDBG~"
            modifiedForeColor = modifiedForeColor.Replace(sep,""); //remove separators
            modifiedBackColor = modifiedBackColor.Replace(sep, ""); //remove separators
            if (!colors.ContainsKey(modifiedForeColor)) throw new ColorValueException($"foreground colour must match name in any format: 'white' or 'WhiTE' not {foreColor}");
            if (!colors.ContainsKey(modifiedBackColor)) throw new ColorValueException($"background colour must match name in any format: 'black' or 'bLACk' not {backColor}");
            modifiedForeColor = $"{sep}{modifiedForeColor}{sep}";
            if(modifiedBackColor.EndsWith("BG")) modifiedBackColor = $"{sep}{modifiedBackColor}{sep}";
            else modifiedBackColor = $"{sep}{modifiedBackColor}BG{sep}";
            foreColor = modifiedForeColor; // "~WHITE~" got through checks so re-assign original parameters by reference
            backColor = modifiedBackColor; // "~BLACKBG~"
        }
        #endregion
    }
}