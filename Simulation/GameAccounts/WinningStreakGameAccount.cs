namespace laba3oop.Simulation.GameAccounts
{
    public class WinningStreakGameAccount : GameAccount
    {
        private int _consecutiveWins;

        public WinningStreakGameAccount(string userName, int initialRating) : base(userName, initialRating)
        {
            _consecutiveWins = 0;
        }

        protected override int CalculateRatingForWin(int gameRating)
        {
            _consecutiveWins++;
            var bonus = 0;
            if (_consecutiveWins > 1)
            {
                bonus = _consecutiveWins * 50;
            }
            return gameRating + bonus;
        }

        protected override int CalculateRatingForLoss(int gameRating)
        {
            _consecutiveWins = 0;
            return gameRating;
        }

        public override string GetAccountType()
        {
            return "Bonus for win streak";
        }
    }
}