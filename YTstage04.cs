using System;
using System.Collections.Generic;

static Dictionary<string, ConsoleColor> conColors = new Dictionary<string, ConsoleColor>();
static string sep      = "~";
static string WHITE    = $"{sep}WHITE{sep}";
static string WHITEBG  = $"{sep}WHITEBG{sep}";
static string BLACK    = $"{sep}BLACK{sep}";
static string BLACKBG  = $"{sep}BLACKBG{sep}";
static string RED      = $"{sep}RED{sep}";
static string GREEN    = $"{sep}GREEN{sep}";

static readonly List<string> sSymbolsTop = new List<string>     { "┌", "─", "┐", "┬" };
static readonly List<string> sSymbolsBottom = new List<string>  { "└", "─", "┘", "┴" };
static readonly List<string> sSymbolsBody = new List<string>    { "│", " ", "│", "│" };
static readonly List<string> sSymbolsMid = new List<string>     { "├", "─", "┤", "┼" };

static readonly List<string> dSymbolsTop = new List<string>     { "╔", "═", "╗", "╦" };
static readonly List<string> dSymbolsBottom = new List<string>  { "╚", "═", "╝", "╩" };
static readonly List<string> dSymbolsBody = new List<string>    { "║", " ", "║", "║" };
static readonly List<string> dSymbolsMid = new List<string>     { "╠", "═", "╣", "╬" };

static void Main(string[] args)
{
    // populate the dictionary (examples only here: 32 required)
    conColors.Add("RED", ConsoleColor.Red);
	conColors.Add("GREEN", ConsoleColor.Green);
	conColors.Add("BLACK", ConsoleColor.Black);
	conColors.Add("WHITE", ConsoleColor.White);
    conColors.Add("BLACKBG", ConsoleColor.Black);
    conColors.Add("WHITEBG", ConsoleColor.White);

	//ColorPrint("~white~Hello ~red~~whitebg~Coloured~green~~blackbg~ World");  // method 1: hard coded using ~
    //ColorPrint($"{WHITE}Hello {RED}{WHITEBG}Coloured{GREEN}{BLACKBG} World"); // method 2: change character ~ in line 5 if required
    ColorPrint(GREEN + dSymbolsTop[0] + "".PadRight(78,dSymbolsTop[1][0]) + dSymbolsTop[2]);
    ColorPrint(GREEN + dSymbolsBody[0] + "".PadRight(78,dSymbolsBody[1][0]) + dSymbolsBody[2]);
    ColorPrint(GREEN + dSymbolsMid[0] + "".PadRight(78,dSymbolsMid[1][0]) + dSymbolsMid[2]);
    ColorPrint(GREEN + dSymbolsBody[0] + "".PadRight(78,dSymbolsBody[1][0]) + dSymbolsBody[2]);
    ColorPrint(GREEN + dSymbolsBottom[0] + "".PadRight(78,dSymbolsBottom[1][0]) + dSymbolsBottom[2]);
	Console.ResetColor();
	Console.Write("Any key to quit");
	Console.ReadKey();
}

static void ColorPrint(string text, bool newline = true, bool reset = true)
{
	string[] lineParts = text.Split(sep[0]); // sep[0] auto-converts string to char
    foreach (string part in lineParts)
    {
        if (conColors.ContainsKey(part.ToUpper())) // is 'RED' in the colors dictionary?
        {
            if (part.ToUpper().Contains("BG"))      Console.BackgroundColor = conColors[part.ToUpper()]; // get the ConsoleColor from dictionary
            else if (part.ToUpper() == "RESET")     Console.ResetColor();
            else                                    Console.ForegroundColor = conColors[part.ToUpper()]; // get the ConsoleColor from dictionary
        }
        else                                        Console.Write(part);    // not a colour command so print it out without newline
    }
    if (newline)                                    Console.Write("\n");    // Add newline to complete the print command
    if (reset)                                      Console.ResetColor();
}