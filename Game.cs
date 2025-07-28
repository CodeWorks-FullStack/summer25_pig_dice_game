using pig_dice_game.Models;

namespace pig_dice_game.Game;

// Players take turns to roll a single die as many times as they wish, adding all roll results to a running total, but losing their gained score for the turn if they roll a 1.

public class Game
{
  public List<Player> Players = []; // defaults to an empty list
  public int RunningTotal = 0;

  public Game()
  {
    Console.WriteLine("Starting 🐖 game!");
    Console.WriteLine("How many players?");
    // NOTE pause our code and wait for user to type into console
    int numberOfPlayers = GetNumberOfPlayers();
    Console.WriteLine($"Number of players is {numberOfPlayers}");

    for (int i = 0; i < numberOfPlayers; i++)
    {
      Console.WriteLine($"Enter a name for player {i + 1}");
      string playerName = GetPlayerName();
      Player player = new Player(playerName);
      Players.Add(player);
      Console.WriteLine($"Player {i + 1} is {player.Name}");
    }

    for (int i = 0; i <= Players.Count; i++)
    {
      if (i == Players.Count)
      {
        i = 0;
      }

      // NOTE PRO TIP: ctrl+c in terminal will kill application
      RunningTotal = 0;
      Player player = Players[i];
      Console.Clear();
      Console.WriteLine($"Turn for {player.Name}");
      RollDice(player);
    }
  }

  public int GetNumberOfPlayers()
  {
    string? consoleInput = Console.ReadLine();

    if (consoleInput == null)
    {
      Console.WriteLine("You must enter a valid number of players");
      return GetNumberOfPlayers();
    }

    int numberOfPlayers;

    bool success = int.TryParse(consoleInput, out numberOfPlayers);

    if (!success)
    {
      Console.WriteLine($"{consoleInput} is not a number");
      return GetNumberOfPlayers();
    }

    return numberOfPlayers;
  }

  public string GetPlayerName()
  {
    string? consoleInput = Console.ReadLine();

    if (string.IsNullOrEmpty(consoleInput))
    {
      Console.WriteLine("You must enter a name");
      return GetPlayerName();
    }

    return consoleInput;
  }

  public void RollDice(Player currentPlayer)
  {
    int randomRoll = new Random().Next(1, 7); // number between 0-7
    Console.WriteLine($"You rolled a {randomRoll}.");

    if (randomRoll == 1)
    {
      Console.Beep();
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("TOO BAD!");
      Thread.Sleep(1000); // wait for 1000 milliseconds and then continue
      Console.ResetColor();
      return;
    }

    RunningTotal += randomRoll;
    Console.WriteLine($"Your running total is {RunningTotal}. Your current Score is {currentPlayer.Score}. If you stop now, your score will be {currentPlayer.Score + RunningTotal}");
    Console.WriteLine("Would you like to roll again? y/n");

    char consoleInput = Console.ReadKey().KeyChar;
    Console.WriteLine();

    if (consoleInput == 'n')
    {
      currentPlayer.Score += RunningTotal;
      return;
    }

    RollDice(currentPlayer);
  }
}