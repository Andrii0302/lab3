using System.Collections.Generic;
using laba3oop.Repository.Base;
using laba3oop.DbContext.Entities;
using laba3oop.Service.Base;

namespace laba3oop.Service
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public void CreatePlayer(string userName, int initialRating)
        {
            var player = new PlayerEntity { UserName = userName, CurrentRating = initialRating, GamesCount = 0 };
            _playerRepository.Create(player);
        }

        public List<PlayerEntity> GetAllPlayers()
        {
            return _playerRepository.ReadAll();
        }

        public PlayerEntity GetPlayerById(int playerId)
        {
            return _playerRepository.ReadById(playerId);
        }

        public void UpdatePlayer(PlayerEntity player)
        {
            _playerRepository.Update(player);
        }

        public void DeletePlayer(int playerId)
        {
            _playerRepository.Delete(playerId);
        }
    }
}