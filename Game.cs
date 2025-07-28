namespace pig_dice_game.Game;

// Players take turns to roll a single die as many times as they wish, adding all roll results to a running total, but losing their gained score for the turn if they roll a 1.

public class Game
{
  public Game()
  {
    Console.WriteLine("Starting 🐖 game!");
    Console.WriteLine("How many players?");
    // NOTE pause our code and wait for user to type into console
    int numberOfPlayers = GetNumberOfPlayers();
    Console.WriteLine($"Number of players is {numberOfPlayers}");
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
}