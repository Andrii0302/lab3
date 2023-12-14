using System;
using System.Collections.Generic;
using System.Linq;
using laba3oop.DbContext.Entities;

namespace laba3oop.Repository
{
    public class GameRepository
    {
        private readonly DbContext.DbContext _dbContext;

        public GameRepository(DbContext.DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(GameEntity game)
        {
            game.Id = _dbContext.Games.Count + 1;
            _dbContext.Games.Add(game);
        }

        public List<GameEntity> ReadAll()
        {
            return _dbContext.Games;
        }

        public GameEntity ReadById(int gameId)
        {
#pragma warning disable CS8603 
            return _dbContext.Games.FirstOrDefault(g => g.Id == gameId);
#pragma warning restore CS8603 
        }

        public void Update(GameEntity game)
        {
            var existingGame = _dbContext.Games.FirstOrDefault(g => g.Id == game.Id);
            if (existingGame != null)
            {
                existingGame.GameRating = game.GameRating;
            }
        }

        public void Delete(int gameId)
        {
            var gameToDelete = _dbContext.Games.FirstOrDefault(g => g.Id == gameId);
            if (gameToDelete != null)
            {
                _dbContext.Games.Remove(gameToDelete);
            }
        }
    }
}