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
            UI.GameShowIntro(description, intro);
            int secretNumber = GetRandomNumber();
            int attempts = UI.GamePlayGame(secretNumber);
            string ending = "\n\n~red~Well done! Player of the year!\n\n" +
                            "~green~You smashed it!\n\n" +
                            $"~yellow~You guessed the secret number: {secretNumber}\n\n" +
                            $"~magenta~with just {attempts} attempts!\n\n";
            UI.GameShowEnding(ending);
        }
        private static int GetRandomNumber()
        {
            Random rng = new Random();
            return rng.Next(1, 100);
        }
    }
}
