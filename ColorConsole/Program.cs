using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Using a UI static class keeps user I/O separate from the Program code.
            This is ideal in text-based games, where the UI is used as the player interface.
            The remaining code could be ported to a GUI with fewer changes required.
             */
            UI.SetConsole(80, 25, "white", "black"); //set to default size 81 x 25, white text on black bg (1 extra col added)
            bool quit = false;
            while (!quit) //present a menu of possible choices, which redraws after input errors and completing a task
            {
                string title = "This is a demo of 'UI.Menu': Choose another demo from the following";
                List<string> options = new List<string>();
                options.Add("~green~Show a range of different foreground colours");
                options.Add("~green~Show 'UI.Print' method");
                options.Add("~dgreen~Show 'Print' NOT using UI (PrintWithoutUI())");
                options.Add("~green~Show 'UI.DrawLine' method");
                options.Add("~dgreen~Show output NOT using UI (DemoWithoutUI())");
                options.Add("~green~Show 'UI.DrawBox..' methods");
                options.Add("~green~Show 'UI.MultiBox..' methods");
                options.Add("~green~Show 'UI.Grid..' methods");
                options.Add("~green~Show 'UI.InputBox' method");
                options.Add("~green~Show 'UI.DisplayMessage' method");
                options.Add("~green~Show 'UI.User Input' methods");
                options.Add("~magenta~Quit");
            
                int choice = UI.Menu("d", title, options);
                switch (choice)
                {
                    case 0:
                        UI.ColorPrintDemo();
                        break;
                    case 1:
                        PrintDemo();
                        break;
                    case 2:
                        PrintWithoutUI();
                        break;
                    case 3:
                        LineDemo();
                        break;
                    case 4:
                        DemoWithoutUI();
                        break;
                    case 5:
                        BoxDemo();
                        break;
                    case 6:
                        MultiBoxDemo();
                        break;
                    case 7:
                        GridDemo();
                        break;
                    case 8:
                        InputBoxDemo();
                        break;
                    case 9:
                        DisplayMessageDemo();
                        break;
                    case 10:
                        UserInputDemo();
                        break;
                    case 11:
                        quit = true;
                        break;
                    default:
                        break;
                }
            }
            UI.Quit(false); // exit without pressing a key
        }
        static void DemoWithoutUI()
        {
            // Demonstrate all colors and backgrounds withou using UI class
            Console.SetWindowSize(81, 33);
            Console.Clear();
            Type type = typeof(ConsoleColor);
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var name in Enum.GetNames(type))
            {
                Console.BackgroundColor = (ConsoleColor)Enum.Parse(type, name);
                Console.WriteLine(name);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            foreach (var name in Enum.GetNames(type))
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, name);
                Console.WriteLine(name);
            }
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
            Console.SetWindowSize(81, 25);
        }
        static void BoxDemo()
        {
            int numLines = 0;
            UI.Clear();
            numLines += UI.DrawBoxOutline("d", "top", "yellow", "black");
            numLines += UI.DrawBoxBody("d","This is ~blue~blue and ~green~green text in a ~yellow~yellow box!","centre","yellow", "black");
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
            UI.Clear();
            UI.DisplayMessage("This shows a message, timed for 2 secs", false, true, "cyan", "dgrey");
            UI.DisplayMessage("Same method, but requiring Enter to continue", true, false, "green","black");
        }
        static void InputBoxDemo()
        {
            UI.Clear();
            string userInput = UI.InputBox("string", "Input Box Demo", "~white~Type you opinion of this Input Box demonstration", ">_", "red","black", 60, 1, 50);
            UI.Print($"Your opinion was:\n{userInput}");
            UI.DisplayMessage("", true, false);
        }
        static void GridDemo()
        {
            UI.Clear();
            List<int> columns = new List<int> { 10, 30, 20, 20 };
            List<string> alignments = new List<string> { "left", "centre", "centre", "centre" };

            // draw top of single box and title
            UI.DrawBoxOutline("s", "top", "yellow", "black");
            UI.DrawBoxBody("s", "This is ~magenta~Dos~red~Excel!", "centre", "yellow", "black");
            // draw column headers
            List<string> textLines = new List<string> { "", "~green~Column A", "~green~Column B", "~green~Column C" };
            UI.DrawGridOutline("s", "top", columns, "yellow", "black", true);
            UI.DrawGridBody("s", "body", columns, "yellow", "black", textLines, alignments);
            // draw main columns
            UI.DrawGridOutline("s", "mid", columns, "yellow", "black");
            alignments = new List<string> { "right", "left", "centre", "right" };
            textLines = new List<string> { "~green~Row 1: ", "~white~Cell(1,1)", "~white~Cell(1,2)", "~white~Cell(1,3)" };
            UI.DrawGridBody("s", "body", columns, "yellow", "black", textLines, alignments);
            UI.DrawGridOutline("s", "mid", columns, "yellow", "black");
            textLines = new List<string> { "~green~Row 2: ", "~grey~Cell(2,1)", "~grey~Cell(2,2)", "~grey~Cell(2,3)" };
            UI.DrawGridBody("s", "body", columns, "yellow", "black", textLines, alignments);
            UI.DrawGridOutline("s", "mid", columns, "yellow", "black");
            textLines = new List<string> { "~green~Row 3: ", "~dgrey~Cell(3,1)", "~dgrey~Cell(3,2)", "~dgrey~Cell(3,3)" };
            UI.DrawGridBody("s", "body", columns, "yellow", "black", textLines, alignments);
            UI.DrawGridOutline("s", "bottom", columns, "yellow", "black");
            UI.DisplayMessage("", true, false);
        }
        static void LineDemo()
        {
            int numLines = 0;
            UI.Clear();
            numLines += UI.DrawLine("s", "red", "black", 5);
            numLines += UI.DrawLine("s", "red", "black", 10);
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
            numLines += UI.DrawLine("d", "blue", "black", 40, "centre");
            numLines += UI.DrawLine("d", "cyan", "black", 50, "centre");
            UI.AddLines(5, numLines);
            UI.DrawLine("d", "white", "black");
            UI.DisplayMessage("", true, false);
        }
        static void MultiBoxDemo()
        {
            UI.Clear();
            List<string> styles = new List<string> {"d","d","s" };
            List<int> boxSizes = new List<int> { 15, 40, 25 };
            List<string> foreColors = new List<string> { "red", "green", "blue" };
            List<string> backColors = new List<string> { "black", "black", "grey" };
            List<string> textLines = new List<string> { "~blue~blue (14)", "~red~red text (40)", "~dyellow~dark yellow (25)" };
            List<string> emptyLines = new List<string> {"","","" };
            List<string> alignments = new List<string> { "left", "centre", "right" };
            int padding = 0;
            UI.DrawMultiBoxOutline(styles, "top", boxSizes , foreColors, backColors, padding);
            UI.DrawMultiBoxBody(styles, boxSizes, foreColors, backColors, textLines, alignments, padding);
            UI.DrawMultiBoxOutline(styles, "mid", boxSizes, foreColors, backColors, padding);
            UI.DrawMultiBoxBody(styles, boxSizes, foreColors, backColors, emptyLines, alignments, padding);
            UI.DrawMultiBoxBody(styles, boxSizes, foreColors, backColors, emptyLines, alignments, padding);
            UI.DrawMultiBoxOutline(styles, "bottom", boxSizes, foreColors, backColors, padding);
            UI.DisplayMessage("", true, false);
        }
        static void PrintDemo()
        {
            UI.Clear();
            UI.Print("This is a test using default colours (4 lines of code)");
            UI.Print("This is a test using black on white (4 lines of code)", "black", "white");
            UI.DisplayMessage("", true, false);
        }
        static void PrintWithoutUI()
        {
            // 11 lines of code compared with 4 in PrintDemo
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("This is a test using default colours (11 lines of code)");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("This is a test using black on white (11 lines of code)");
            Console.ResetColor();
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
        static void UserInputDemo()
        {
            string description = "The user input methods are:\n\n" +
                                        "~green~UI.GetString ~white~to get a string typed by the user. " +
                                        "There is an option to set minimum and maximum length," +
                                        " and to convert to TitleCase\n\n" +
                                        "~cyan~UI.GetInteger ~white~to get an integer value. Minimum and max values can be specified\n\n" +
                                        "~magenta~UI. GetRealNumber ~white~ similar to UI.GetInteger, allows for real numbers to be entered\n\n" +
                                        "~blue~UI.GetBoolean ~white~requires the user to type 'y' or 'n' and returns a boolean value.\n\n"+
                                        "~green~Test each one out with Enter only, or wrong numbers, too many characters etc\n\n" +
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
