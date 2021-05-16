using System;
using System.Collections.Generic;

namespace ColorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Using a UI static class keeps user I/O separate from the Program code.
            /// This is ideal in text-based games, where the UI is used as the player interface.
            /// The remaining code could be ported to a GUI with fewer changes required.
       
            UI.SetConsole(80, 25, "white", "black"); //set to default size 81 x 25, white text on black bg (1 extra col added)
            bool quit = false;
            while (!quit) //present a menu of possible choices, which redraws after input errors and completing a task
            {
                string title = "This is a demo of 'UI.Menu': Choose another demo from the following";
                List<string> options = new List<string>();
                // Every menu choice has a colour string: ~colour~
                options.Add("~green~Show a mix of different colours");
                options.Add("~dgreen~Show colour mix NOT using UI (DemoWithoutUI())");
                options.Add("~green~Show 'UI.Print' method");
                options.Add("~dgreen~Show 'Print' NOT using UI (PrintWithoutUI())");
                options.Add("~green~Show 'UI.DrawLine' method");
                options.Add("~green~Show 'UI.DrawBox..' methods");
                options.Add("~green~Show 'UI.MultiBox..' methods");
                options.Add("~green~Show 'UI.Grid..' methods");
                options.Add("~green~Show 'UI.InputBox' method");
                options.Add("~green~Show 'UI.DisplayMessage' method");
                options.Add("~green~Show 'UI.User Input' methods");
                options.Add("~red~Play the awesome 'Guess The Number' Game!");
                options.Add("~magenta~Quit");
                /// Menu(string style,  string title, string promptChar, List<string> textLines, string foreColor = "WHITE", ///
                ///      string backColor = "BLACKBG", string align = "left", int width = 0)                                 ///
                int choice = UI.Menu("d", title, ">_", options);
                if (choice == 0)        UI.ColorPrintDemo();
                else if (choice == 1)   DemoWithoutUI();
                else if (choice == 2)   PrintDemo();
                else if (choice == 3)   PrintWithoutUI();
                else if (choice == 4)   LineDemo();
                else if (choice == 5)   BoxDemo();
                else if (choice == 6)   MultiBoxDemo();
                else if (choice == 7)   GridDemo();
                else if (choice == 8)   InputBoxDemo();
                else if (choice == 9)   DisplayMessageDemo();
                else if (choice == 10)  UserInputDemo();
                else if (choice == 11)  Game.GuessTheNumber();
                else if (choice == 12)  quit = true;
            }
            UI.Quit(false); // false = exit without pressing a key
        }
        static void DemoWithoutUI()
        {
            /// All user I/O handled here, not using the UI class
            /// Demonstrate all colors and backgrounds without using UI class
            
            Console.SetWindowSize(81, 33);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("ConsoleColor.White".PadRight(80,' '));
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("ConsoleColor.Gray".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("ConsoleColor.DarkGray".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("ConsoleColor.Blue".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("ConsoleColor.Green".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("ConsoleColor.Cyan".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("ConsoleColor.Red".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine("ConsoleColor.Magenta".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("ConsoleColor.Yellow".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("ConsoleColor.DarkBlue".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("ConsoleColor.DarkGreen".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("ConsoleColorDark.Cyan".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ConsoleColor.DarkRed".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("ConsoleColor.DarkMagenta".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("ConsoleColor.DarkYellow".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("ConsoleColor.Black".PadRight(80, ' '));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("ConsoleColor.White".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("ConsoleColor.Gray".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("ConsoleColor.DarkGray".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("ConsoleColor.Blue".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ConsoleColor.Green".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("ConsoleColor.Cyan".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ConsoleColor.Red".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("ConsoleColor.Magenta".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("ConsoleColor.DarkBlue".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("ConsoleColor.DarkGreen".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("ConsoleColor.DarkCyan".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ConsoleColor.DarkRed".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("ConsoleColor.DarkMagenta".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("ConsoleColor.DarkYellow".PadRight(80, ' '));
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("ConsoleColor.Black".PadRight(80, ' '));
            Console.ResetColor();
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
            Console.SetWindowSize(81, 25);
        }
        static void BoxDemo()
        {
            /// All user i/o handled by UI class
            /// DrawBoxOutline(string style, string part, string foreColor, string backColor,
            ///                string boxAlign = "left", int width = 0, bool midMargin = false)
            /// DrawBoxBody(string style, string text, string boxAlign, string foreColor, string backColor,
            ///             string textColor = "WHITE", string textBackColor = "BLACKBG",
            ///             string textAlign = "left", int width = 0)
            
            UI.Clear();
            int numLines = UI.DrawBoxOutline("d", "top", "yellow", "black");
            numLines += UI.DrawBoxBody("d","This is ~blue~blue and ~green~green text in a ~yellow~yellow box!",
                                       "centre","yellow", "black", "white", "black", "centre");
            numLines += UI.DrawBoxOutline("d", "mid", "yellow", "black");
            numLines += UI.DrawBoxBody("d", "", "centre", "yellow", "black");
            numLines += UI.DrawBoxBody("d", "", "centre", "yellow", "black");
            numLines += UI.DrawBoxBody("d", "", "centre", "yellow", "black");
            numLines += UI.DrawBoxBody("d", "", "centre", "yellow", "black");
            numLines += UI.DrawBoxBody("d", "", "centre", "yellow", "black");
            numLines += UI.DrawBoxOutline("d", "bottom", "yellow", "black");
            UI.AddLines(5, numLines);
            UI.DrawLine("D", "WHITE", "black");
            UI.DisplayMessage("", true, false);
        }
        static void DisplayMessageDemo()
        {
            /// All user I/O handled by UI class
            /// DisplayMessage(string message, bool useInput, bool useTimer,
            ///                string foreColor = "WHITE", string backColor = "BLACKBG", int delay = 2000)
            /// +1 overload  DisplayMessage(List<string> messages ...
            UI.Clear();
            UI.DisplayMessage("This shows a message, timed for 2 secs", false, true, "cyan", "dgrey");
            UI.DisplayMessage("Same method, but requiring Enter to continue", true, false, "green","black");
        }
        static void InputBoxDemo()
        {
            /// All user I/O handled by UI class
            /// InputBox(string style, string returnType, string title, string boxMessage,
            ///          string inputPrompt, string promptEnd, string foreColor = "WHITE", 
            ///          string backColor = "BLACKBG", int width = 0, int minReturnLen = 1,
            ///          int maxReturnLen = 20, bool withTitle = false)
            UI.Clear();
            string userInput = UI.InputBox("s", "string", "Input Box Demo",
                                            "~white~What is your opinion of this Input Box demonstration",
                                            "Type your comment", ">_", "red","black", 60, 1, 50);
            UI.Print($"Your opinion was:\n{userInput}");
            UI.DisplayMessage("", true, false);
        }
        static void GridDemo()
        {
            /// All user I/O handled by UI class
            /// DrawBoxOutline(string style, string part, string foreColor, string backColor,
            ///                string boxAlign = "left", int width = 0, bool midMargin = false)
            /// DrawBoxBody(string style, string text, string boxAlign, string foreColor, string backColor,
            ///             string textColor = "WHITE", string textBackColor = "BLACKBG",
            ///             string textAlign = "left", int width = 0)
            /// DrawGridOutline(string style, string part, List<int> columns, string foreColor, string backColor, bool midMargin = false)
            /// DrawGridBody(string style, string part, List<int> columns, string boxColor, string boxBackColor,
            ///              string textColor, string textBackColor, List< string > textLines, List<string> alignments)
            
            UI.Clear();
            List<int> columns = new List<int> { 10, 30, 20, 20 };
            List<string> alignments = new List<string> { "left", "centre", "centre", "centre" };  
            UI.DrawBoxOutline("s", "top", "yellow", "black"); // draw top of single box and title
            UI.DrawBoxBody("s", "This is ~magenta~Dos~red~Excel!", "centre", "yellow", "black", "white", "black","centre");
            // draw column headers
            List<string> textLines = new List<string> { "", "~green~Column A", "~green~Column B", "~green~Column C" };
            UI.DrawGridOutline("s", "top", columns, "yellow", "black", true);
            UI.DrawGridBody("s", "body", columns, "yellow", "black", "green", "black", textLines, alignments);
            // draw main columns
            UI.DrawGridOutline("s", "mid", columns, "yellow", "black");
            alignments = new List<string> { "right", "left", "centre", "right" };
            textLines = new List<string> { "~green~Row 1: ", "~white~Cell(1,1)", "~white~Cell(1,2)", "~white~Cell(1,3)" };
            UI.DrawGridBody("s", "body", columns, "yellow", "black", "green", "black", textLines, alignments);
            UI.DrawGridOutline("s", "mid", columns, "yellow", "black");
            textLines = new List<string> { "~green~Row 2: ", "~grey~Cell(2,1)", "~grey~Cell(2,2)", "~grey~Cell(2,3)" };
            UI.DrawGridBody("s", "body", columns, "yellow", "black", "green", "black", textLines, alignments);
            UI.DrawGridOutline("s", "mid", columns, "yellow", "black");
            textLines = new List<string> { "~green~Row 3: ", "~dgrey~Cell(3,1)", "~dgrey~Cell(3,2)", "~dgrey~Cell(3,3)" };
            UI.DrawGridBody("s", "body", columns, "yellow", "black", "green", "black", textLines, alignments);
            UI.DrawGridOutline("s", "mid", columns, "yellow", "black");
            textLines = new List<string> { "~green~Row 4: ", "~dgrey~align=\"left=\"", "~dgrey~align=\"centre\"", "~dgrey~align=\"right\"" };
            UI.DrawGridBody("s", "body", columns, "yellow", "black", "green", "black", textLines, alignments);
            UI.DrawGridOutline("s", "bottom", columns, "yellow", "black");
            UI.AddLines(5, 13);
            UI.DrawLine("d", "white", "black");
            UI.DisplayMessage("", true, false);
        }
        static void LineDemo()
        {
            /// All user I/O handled by UI class
            /// DrawLine(string style, string foreColor, string backColor, int width = 0, string align = "left")
            int numLines = 0;
            UI.Clear();
            numLines += UI.DrawLine("s", "red", "blackbg", 5);
            numLines += UI.DrawLine("s", "red", "blackbg", 10);
            numLines += UI.DrawLine("s", "red", "black", 20);
            numLines += UI.DrawLine("s", "red", "black", 40);
            numLines += UI.DrawLine("s", "red", "black", 60);
            numLines += UI.DrawLine("s", "red", "black");
            numLines += UI.DrawLine("s", "red", "black", 60, "right");
            numLines += UI.DrawLine("s", "red", "black", 40, "right");
            numLines += UI.DrawLine("s", "red", "black", 20, "right");
            numLines += UI.DrawLine("s", "red", "black", 10, "right");
            numLines += UI.DrawLine("s", "red", "black", 5, "right");
            numLines += UI.DrawLine("d", "white", "black");
            numLines += UI.DrawLine("d", "grey", "black");
            numLines += UI.DrawLine("d", "dGrey", "black");
            numLines += UI.DrawLine("d", "green", "black");
            numLines += UI.DrawLine("d", "black", "green");
            numLines += UI.DrawLine("d", "red", "white");
            numLines += UI.DrawLine("d", "blue", "black", 40, "centre");
            numLines += UI.DrawLine("d", "cyan", "black", 50, "centre");
            UI.AddLines(5, numLines);
            UI.DrawLine("d", "white", "black");
            UI.DisplayMessage("", true, false);
        }
        static void MultiBoxDemo()
        {
            /// All user I/O handled by UI class
            /// DrawMultiBoxOutline(List<string> styles, string part, List<int> sizes, List<string> foreColors,
            ///                     List<string> backColors, int padding = 0)
            /// DrawMultiBoxBody(List<string> styles, List<int> sizes, List<string> foreColors, List<string> backColors,
            ///                  List<string> textLines, List<string> alignments, int padding = 0)
            
            UI.Clear();
            List<string> styles     = new List<string>  { "d","s","d" };
            List<int>    boxSizes   = new List<int>     { 15, 40, 25 };
            List<string> foreColors = new List<string>  { "red", "green", "blue" };
            List<string> backColors = new List<string>  { "black", "black", "grey" };
            List<string> textLines  = new List<string>  { "~blue~blue 14 chars", "~red~red text 40 chars", "~dyellow~dark yellow 25 chars" };
            List<string> emptyLines = new List<string>  { "", "", "" };
            List<string> alignments = new List<string>  { "left", "centre", "right" };
            int padding = 0;
            UI.DrawMultiBoxOutline(styles, "top", boxSizes , foreColors, backColors, padding);
            UI.DrawMultiBoxBody   (styles, boxSizes, foreColors, backColors, textLines, alignments, padding);
            UI.DrawMultiBoxOutline(styles, "mid", boxSizes, foreColors, backColors, padding);
            UI.DrawMultiBoxBody   (styles, boxSizes, foreColors, backColors, emptyLines, alignments, padding);
            UI.DrawMultiBoxBody   (styles, boxSizes, foreColors, backColors, emptyLines, alignments, padding);
            textLines = new List<string> { "~yellow~align=\"left\"", "~yellow~align=\"centre\"", "~red~align=\"right\"" };
            UI.DrawMultiBoxBody   (styles, boxSizes, foreColors, backColors, textLines, alignments, padding);
            UI.DrawMultiBoxBody   (styles, boxSizes, foreColors, backColors, emptyLines, alignments, padding);
            UI.DrawMultiBoxBody   (styles, boxSizes, foreColors, backColors, emptyLines, alignments, padding);
            UI.DrawMultiBoxOutline(styles, "bottom", boxSizes, foreColors, backColors, padding);
            UI.AddLines(5, 9);
            UI.DrawLine("d", "white", "black");
            UI.DisplayMessage("", true, false);
        }
        static void PrintDemo()
        {
            /// All user I/O handled by UI class
            /// ColorPrint(string value, bool newline = true, bool reset = true)
            
            UI.Clear();
            UI.ColorPrint("1. UI.Clear();");
            UI.ColorPrint("2. UI.Print(~yellow~~blackbg~\"This demo uses 10 lines of code\");");
            UI.ColorPrint("3. UI.Print(~green~\"This line is green on black\");");
            UI.ColorPrint("4. UI.AddLines(5, 7);");
            UI.ColorPrint("5. UI.DrawLine(\"d\", \"white\", \"black\");");
            UI.ColorPrint("6. UI.DisplayMessage(\"\", true, false);");
            UI.AddLines(5, 7);
            UI.DrawLine("d", "white", "black");
            UI.DisplayMessage("", true, false);
        }
        static void PrintWithoutUI()
        {
            /// 22 lines of code compared with 10 in PrintDemo. All user I/O handled here
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("1. Console.ForegroundColor = ConsoleColor.Yellow;");
            Console.WriteLine("2. Console.BackgroundColor = ConsoleColor.Black;");
            Console.WriteLine("3. Console.Clear();");
            Console.ResetColor();
            Console.WriteLine("4. Console.ResetColor();");
            Console.WriteLine("5. Console.ForegroundColor = ConsoleColor.Green;");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("6. Console.WriteLine(\"6.This line is green on black\");");
            Console.ResetColor();
            Console.WriteLine("7. Console.ResetColor();");
            Console.WriteLine("8. for (int i = 0; i < 5; i++)");
            Console.WriteLine("9. { Console.WriteLine(); }");
            Console.WriteLine("10. Console.WriteLine(\"\".PadRight(80, '═'));");
            Console.WriteLine("11. Console.Write(\"Press Enter to continue...\"");
            Console.WriteLine("12. Console.ReadLine();");
            for (int i = 0; i < 5; i++) { Console.WriteLine(); }    // = UI.AddLines(5, 7);
            Console.WriteLine("".PadRight(80, '═'));                // = UI.DrawLine("d", "white", "black");
            Console.Write("Press Enter to continue...");            // = UI.DisplayMessage("", true, false);
            Console.ReadLine();
        }
        static void UserInputDemo()
        {
            /// All user I/O handled by UI class
            /// UI.GetInputDemo specifically written for this procedure. This is how the library was designed
            /// to be used. Do the logic in Program.cs or similar, pass all the UI stuff to UI.cs
            
            string description = "The user input methods are:\n\n" +
                                "~green~UI.GetString ~white~to get a string typed by the user. " +
                                "There is an option to set minimum and maximum length," +
                                " and to convert to TitleCase.\n\n" +
                                "~cyan~UI.GetInteger ~white~to get an integer value. Minimum and max values can be specified.\n\n" +
                                "~magenta~UI. GetRealNumber ~white~similar to UI.GetInteger, allows for real numbers to be entered.\n\n" +
                                "~blue~UI.GetBoolean ~white~requires the user to type 'y' or 'n' and returns a boolean value.\n\n"+
                                "~green~Test ~white~each one out with ~blue~Enter ~white~only, or wrong numbers, too many characters etc.\n\n" +
                                "~red~Try and break it!";
            string retValue = UI.GetInputDemo("string", description);
            UI.DisplayMessage($"You entered {retValue}",false, true);
            retValue = UI.GetInputDemo("int", description);
            UI.DisplayMessage($"You entered {retValue}", false, true);
            retValue = UI.GetInputDemo("real", description);
            UI.DisplayMessage($"You entered {retValue}", false, true);
            retValue = UI.GetInputDemo("bool", description);
            UI.DisplayMessage($"You entered {retValue}. Enter to continue", true, false);
        }
    }
}
