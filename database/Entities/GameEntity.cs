namespace laba3oop.DbContext.Entities
{
    public class GameEntity
    {
        public int Id { get; set; }
        public int GameRating { get; set; }
        public int PlayerId { get; set; }
        public string? GameType { get; set; }
        public string? AccountType { get; set; }
        public bool IsWin { get; set; }
    }
}