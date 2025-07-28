
// allows other modules in our application to use Game class by `using` this namespace (similar to export)
using pig_dice_game.Models;

namespace pig_dice_game.Game;

// Players take turns to roll a single die as many times as they wish, adding all roll results to a running total, but losing their gained score for the turn if they roll a 1.

public class Game
{
  public List<Player> Players = []; // defaults to an empty list
  public int RunningTotal = 0;
  public int WinningScore { get; } = 30; // readonly

  public Game() // constructor
  {
    Console.WriteLine("Starting 🐖 game!");
    Console.WriteLine("How many players?");
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
      // NOTE restarts for loop
      // NOTE PRO TIP: ctrl+c in terminal will kill application
      if (i == Players.Count)
      {
        i = 0;
      }

      RunningTotal = 0;
      Player player = Players[i];
      Console.Clear();
      Console.WriteLine($"Turn for {player.Name}");
      RollDice(player);

      if (player.Score >= WinningScore)
      {
        break;
      }
    }

    Player? winningPlayer = Players.Find(player => player.Score >= WinningScore);

    if (winningPlayer == null)
    {
      throw new Exception("Jeremy your code is bad");
    }

    Console.ForegroundColor = ConsoleColor.Green;

    Console.WriteLine($"{winningPlayer.Name.ToUpper()} WINS!");
    Console.ResetColor();
  }

  public int GetNumberOfPlayers()
  {
    // NOTE pauses our code and wait for user to type into console
    string? consoleInput = Console.ReadLine();

    if (consoleInput == null)
    {
      Console.WriteLine("You must enter a valid number of players");
      return GetNumberOfPlayers(); // recursion
    }

    int numberOfPlayers;

    // NOTE returns true if can successfully parse a string into an interger, and assigns numberOfPlayers variable to said integer. If it cannot parse the string into an integer, returns false
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
    int randomRoll = new Random().Next(1, 7); // random number between 0-7
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

    if (currentPlayer.Score + RunningTotal >= WinningScore)
    {
      currentPlayer.Score += RunningTotal;
      return;
    }

    Console.WriteLine($"Your running total is {RunningTotal}. Your current Score is {currentPlayer.Score}. If you stop now, your score will be {currentPlayer.Score + RunningTotal}");
    Console.WriteLine("Would you like to roll again? y/n");

    // NOTE reads the first key pressed on the keyboard and converts it into a `char`
    char consoleInput = Console.ReadKey().KeyChar;

    // NOTE adds empty line
    Console.WriteLine();

    if (consoleInput == 'n')
    {
      currentPlayer.Score += RunningTotal;
      return;
    }

    RollDice(currentPlayer);
  }
}