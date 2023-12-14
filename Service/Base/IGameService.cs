using System.Collections.Generic;
using laba3oop.DbContext.Entities;

namespace laba3oop.Service.Base
{
    public interface IGameService
    {
        void CreateGame(int gameRating);
        List<GameEntity> GetAllGames();
        GameEntity GetGameById(int gameId);
        void UpdateGame(GameEntity game);
        void DeleteGame(int gameId);
    }
}