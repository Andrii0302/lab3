namespace laba3oop.DbContext.Entities
{
    public class PlayerEntity
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public int CurrentRating { get; set; }
        public int GamesCount { get; set; }
        
    }
}