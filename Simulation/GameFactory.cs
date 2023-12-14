using laba3oop.Simulation.GameAccounts;

namespace laba3oop.Simulation
{
    public class GameFactory
    {
        public static GameAccount CreateStandardGameAccount(string userName, int initialRating)
        {
            return new StandardGameAccount(userName, initialRating);
        }

        public static GameAccount CreateReducedLossGameAccount(string userName, int initialRating)
        {
            return new ReducedLossGameAccount(userName, initialRating);
        }

        public static GameAccount CreateWinningStreakGameAccount(string userName, int initialRating)
        {
            return new WinningStreakGameAccount(userName, initialRating);
        }
    }
}
