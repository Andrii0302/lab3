using System;
using laba3oop.DbContext.Entities;
using laba3oop.Repository;
using laba3oop.Simulation.GameType;
using laba3oop.Service.Base;
using laba3oop.Service;

namespace laba3oop.Simulation
{
    public abstract class Simulation
    {
        private static DbContext.DbContext _dbContext = new DbContext.DbContext();

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Start();
        }

        private static void Start()
        {
            while (true)
            {
                Console.WriteLine("Choose an action: ");
                Console.WriteLine("1. List of players");
                Console.WriteLine("2. Create player");
                Console.WriteLine("3. Delete player");
                Console.WriteLine("4. Update player");
                Console.WriteLine("5. Played games list of the player");
                Console.WriteLine("6. List of the games ");
                Console.WriteLine("7. Update game ");
                Console.WriteLine("8. Delete game ");
                Console.WriteLine("9. Start game ");

                var startChoice = GetChoice(1, 9);
                var playerRepository = new PlayerRepository(_dbContext);
                var gameRepository = new GameRepository(_dbContext);
                var playerEntity = new PlayerEntity();
                var gameFactory = new GameFactory();

                switch (startChoice)
                {
                    case 1:
                        foreach (var player in playerRepository.ReadAll())
                        {
                            Console.WriteLine(
                                $"Player ID {player.Id}, name {player.UserName}, current rating {player.CurrentRating}, amount of played games {player.GamesCount}");
                        }
                        continue;
                    
                    case 2:
                        PlayerRepository.Create(playerEntity);
                        Console.WriteLine("Player created");
                        continue;
                    
                    case 3:
                        Console.WriteLine("Enter player's ID");
                        var answer = Console.ReadLine();

                        if (!int.TryParse(answer, out var id))
                        {
                            Console.WriteLine("Players does not exist");
                            Start();
                        }

                        var getPlayer = playerRepository.ReadById(id);

                        if (getPlayer == default)
                        {
                            Console.WriteLine("Players does not exist");
                            Start();
                        }

                        PlayerRepository.Delete(id);
                        Console.WriteLine("Player deleted");
                        continue;
                    
                    case 4:
                        Console.WriteLine("Enter player's ID");
                        answer = Console.ReadLine();

                        if (!int.TryParse(answer, out id))
                        {
                            Console.WriteLine("Players does not exist");
                            Start();
                        }

                        getPlayer = playerRepository.ReadById(id);

                        if (getPlayer == default)
                        {
                            Console.WriteLine("Players does not exist");
                            Start();
                        }

                        Console.WriteLine("Choose what to change: ");
                        Console.WriteLine("1. Players name");
                        Console.WriteLine("2. Current rating");
                        Console.WriteLine("3. Amount of games");

                        var editChoicePlayer = GetChoice(1, 3);

                        switch (editChoicePlayer)
                        {
                            case 1:
                                Console.WriteLine("Enter new players name");
                                var newName = Console.ReadLine();
                                getPlayer.UserName = newName;
                                continue;
                            case 2:
                                Console.WriteLine("Enter new players rating");
                                var newRating = int.Parse(Console.ReadLine());
                                getPlayer.CurrentRating = newRating;
                                continue;
                            case 3:
                                Console.WriteLine("Enter new amount of player's games");
                                var newGamesCount = int.Parse(Console.ReadLine());
                                getPlayer.GamesCount = newGamesCount;
                                continue;
                        }

                        playerRepository.Update(getPlayer);
                        Console.WriteLine("Player updated");
                        continue;

                    case 5:
                        Console.WriteLine("Enter player ID");
                        var playerId = int.Parse(Console.ReadLine());
                        var selectedPlayer = playerRepository.ReadById(playerId);

                        if (selectedPlayer == null)
                        {
                            Console.WriteLine("Player does not exist");
                            continue;
                        }

                        Console.WriteLine($"Game list of the player {selectedPlayer.UserName}:");

                        foreach (var game in gameRepository.ReadAll())
                        {
                            if (game.PlayerId == playerId)
                            {
                                Console.WriteLine(
                                    $"Game ID {game.Id}, Game rating {game.GameRating}, game type {game.GameType}, account type {game.AccountType}, Win: {game.IsWin}");
                            }
                        }
                        continue;

                    case 6:
                        Console.WriteLine("All games list:");

                        foreach (var gameEntity in gameRepository.ReadAll())
                        {
                            Console.WriteLine(
                                $"Game ID  {gameEntity.Id}, Game rating {gameEntity.GameRating}, game type {gameEntity.GameType}, account type {gameEntity.AccountType}, Win: {gameEntity.IsWin}");
                        }
                        continue;

                    case 7:
                        Console.WriteLine("Enter game ID");
                        var gameId = int.Parse(Console.ReadLine());
                        var selectedGame = gameRepository.ReadById(gameId);

                        if (selectedGame == null)
                        {
                            Console.WriteLine("This game does not exist");
                            continue;
                        }

                        Console.WriteLine("Choose what to change: ");
                        Console.WriteLine("1. Game rating");
                        Console.WriteLine("2. Player ID ");

                        var editChoiceGame = GetChoice(1, 2);

                        switch (editChoiceGame)
                        {
                            case 1:
                                Console.WriteLine("Enter new game rating");
                                var newRating = int.Parse(Console.ReadLine());
                                selectedGame.GameRating = newRating;
                                break;
                            case 2:
                                Console.WriteLine("Enter new player ID");
                                var newPlayerId = int.Parse(Console.ReadLine());
                                selectedGame.PlayerId = newPlayerId;
                                break;
                        }

                        gameRepository.Update(selectedGame);
                        Console.WriteLine("Game updated");
                        continue;

                    case 8:
                        Console.WriteLine("Enter game ID");
                        gameId = int.Parse(Console.ReadLine());
                        gameRepository.Delete(gameId);
                        Console.WriteLine("Game deletes");
                        continue;

                    case 9:
                        Console.WriteLine("Enter ID of the first player");
                        var player1Id = int.Parse(Console.ReadLine());
                        var player1 = playerRepository.ReadById(player1Id);

                        Console.WriteLine("Enter ID of the second player");
                        var player2Id = int.Parse(Console.ReadLine());
                        var player2 = playerRepository.ReadById(player2Id);

                        Console.WriteLine("Choose account type:");
                        Console.WriteLine("1. Default account");
                        Console.WriteLine("2. Account with reduced penalty ");
                        Console.WriteLine("3. Account with win bonus");
                        var accountTypeChoice = GetChoice(1, 3);

                        Console.WriteLine("Choose game type:");
                        Console.WriteLine("1. Standard game");
                        Console.WriteLine("2. Training game");
                        Console.WriteLine("3. One player game");
                        var gameTypeChoice = GetChoice(1, 3);

                        var player1Account = CreatePlayer(gameFactory, accountTypeChoice, player1.UserName,
                            player1.CurrentRating);
                        var player2Account = CreatePlayer(gameFactory, accountTypeChoice, player2.UserName,
                            player2.CurrentRating);

                        Console.WriteLine("\nSimulation of the game ");

                        for (var i = 0; i < 1; i++)
                        {
                            var gameRating = new Random().Next(1, 255);
                            var game = CreateGame(gameTypeChoice, gameRating);

                            player1Account.WinGame(player2Account, game.GetGameRating());
                            player2Account.LoseGame(player1Account, game.GetGameRating());

                            player1.CurrentRating = player1Account.CurrentRating;
                            player1.GamesCount = player1Account.GamesCount;
                            playerRepository.Update(player1);

                            player2.CurrentRating = player2Account.CurrentRating;
                            player2.GamesCount = player2Account.GamesCount;
                            playerRepository.Update(player2);

                            var gameEntity = new GameEntity
                            {
                                GameRating = gameRating, PlayerId = player1Id, GameType = game.GetGameType(),
                                AccountType = player1Account.GetAccountType()
                            };
                            gameRepository.Create(gameEntity);
                        }

                        Console.WriteLine("\nStats of players:");
                        Console.WriteLine();
                        player1Account.GetStats();
                        Console.WriteLine();
                        player2Account.GetStats();

                        Console.WriteLine("Game created");
                        continue;
                }
                break;
            }
        }
        private static int GetChoice(int minValue, int maxValue)
        {
            int choice;
            while (true)
            {
                Console.Write($"Enter the number from {minValue} to {maxValue}: ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= minValue && choice <= maxValue)
                {
                    break;
                }

                Console.WriteLine("Wrong input");
            }

            return choice;
        }

        static GameAccount CreatePlayer(GameFactory factory, int accountTypeChoice, string userName,
            int initialRating)
        {
            switch (accountTypeChoice)
            {
                case 1:
                    return GameFactory.CreateStandardGameAccount(userName, initialRating);
                case 2:
                    return GameFactory.CreateReducedLossGameAccount(userName, initialRating);
                case 3:
                    return GameFactory.CreateWinningStreakGameAccount(userName, initialRating);
                default:
                    throw new ArgumentException("Wrong type of player");
            }
        }

        private static Game CreateGame(int gameTypeChoice, int rating)
        {
            switch (gameTypeChoice)
            {
                case 1:
                    return new StandardGame(rating);
                case 2:
                    return new TrainingGame();
                case 3:
                    return new SoloGame(rating);
                default:
                    throw new ArgumentException("Wrong type of game");
            }
        }
    }
}
