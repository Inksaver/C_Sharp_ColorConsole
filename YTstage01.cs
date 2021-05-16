using System;

static void Main()
{
    //Console.SetWindowSize(81, 25); //Not used in VSCode output
    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.Black;
    Console.Clear();
    Console.Write("Hello");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write(" Coloured");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write(" World\n");
    Console.ResetColor();
    Console.Write("Press any key to continue");
    Console.ReadKey();
}
