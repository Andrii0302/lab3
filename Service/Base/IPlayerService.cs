using System.Collections.Generic;
using laba3oop.DbContext.Entities;

namespace laba3oop.Service.Base
{
    public interface IPlayerService
    {
        void CreatePlayer(string userName, int initialRating);
        List<PlayerEntity> GetAllPlayers();
        PlayerEntity GetPlayerById(int playerId);
        void UpdatePlayer(PlayerEntity player);
        void DeletePlayer(int playerId);
    }
}