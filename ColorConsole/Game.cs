using System;

namespace ColorConsole
{
    public static class Game
    {
        public static void GuessTheNumber()
        {
            /// The methods in this class use demonstration methods in the UI class
            /// This is an example of how the UI library should be used.
            /// Put the game logic in as many classes as you need, and pass
            /// all the user I/O to the UI class
            
            string description = "\n\n~yellow~Welcome to the most incredible\n\n" +
                                 "~red~'Guess The Number Game'\n\n" +
                                 "~green~You have ever seen!\n\n";
            string intro = "This is really annoying, but students like it...";
            GameShowIntro(description, intro);
            int secretNumber = GetRandomNumber();
            int attempts = GamePlayGame(secretNumber);
            string ending = "\n\n~red~Well done! Player of the year!\n\n" +
                            "~green~You smashed it!\n\n" +
                            $"~yellow~You guessed the secret number: {secretNumber}\n\n" +
                            $"~magenta~with just {attempts} attempts!\n\n";
            GameShowEnding(ending);
        }
        private static int GetRandomNumber()
        {
            Random rng = new Random();
            return rng.Next(1, 100);
        }
        public static int GamePlayGame(int secretNumber)
        {
            /// This function has some of the game logic in it, but is mostly user I/O
            int guess = 0;
            int attempts = 0;
            while (guess != secretNumber)
            {
                attempts++;
                UI.Clear();
                string userInput = UI.InputBox("s", "int", "~magenta~Guess The Number", "~cyan~See if you can guess the number", "Type your guess for the secret number,(1 to 100)", ">_", "green", "black", 60, 1, 100);
                guess = Convert.ToInt32(userInput);
                if (guess > secretNumber)
                {
                    UI.DisplayMessage($"Sorry, your guess {guess} was too high", false, true, "magenta", "black", 3000);
                }
                else if (guess < secretNumber)
                {
                    UI.DisplayMessage($"Sorry, your guess {guess} was too low", false, true, "cyan", "black", 3000);
                }
            }
            return attempts;
        }
        public static void GameShowEnding(string description)
        {
            UI.Clear();
            int numLines = UI.DrawMultiLineBox("s", description, "yellow", "black", "white", "black", "centre", "centre");
            numLines += UI.AddLines(2);
            UI.AddLines(5, numLines);
            UI.DrawLine("d", "white", "black");
            UI.DisplayMessage("", true, false);
        }
        public static void GameShowIntro(string description, string intro)
        {
            UI.Clear();
            int numLines = UI.DrawMultiLineBox("s", description, "yellow", "black", "white", "black", "centre", "centre", 60);
            numLines += UI.AddLines(2);
            intro = intro.PadLeft(((UI.windowWidth - intro.Length) / 2) + intro.Length);
            intro = intro.PadRight(UI.windowWidth);
            numLines += UI.Teletype(intro, 20, "black", "red");
            UI.AddLines(5, numLines);
            UI.DrawLine("d", "white", "black");
            UI.DisplayMessage("", true, false);
        }
    }
}
