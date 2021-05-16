using System;
using System.Collections.Generic;

static Dictionary<string, ConsoleColor> conColors = new Dictionary<string, ConsoleColor>();
static void Main(string[] args)
{
	conColors.Add("RED", ConsoleColor.Red);
	conColors.Add("GREEN", ConsoleColor.Green);
	conColors.Add("BLACK", ConsoleColor.Black);
	conColors.Add("WHITE", ConsoleColor.White);
	ColorPrint("Hello World", "green", "black");
	Console.ResetColor();
	Console.Write("Any key to quit");
	Console.ReadKey();
}
static void ColorPrint(string text, string foreColor = "WHITE", string backColor = "BLACK")
{
	Console.ForegroundColor = conColors[foreColor.ToUpper()];
	Console.BackgroundColor = conColors[backColor.ToUpper()];
	Console.WriteLine(text);
}
