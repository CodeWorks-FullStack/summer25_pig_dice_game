namespace pig_dice_game.Models;

public class Player
{
  public string Name { get; } // readonly
  public int Score { get; set; } // changeable

  public Player(string name)
  {
    Name = name;
    Score = 0;
  }
}