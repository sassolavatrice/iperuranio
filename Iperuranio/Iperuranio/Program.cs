namespace Gioco;

static public class Program
{
  public static bool endGame = false;
  static void Main()
  {
	GameEngine gE = new GameEngine();
   while (!endGame)
    {
      Console.Clear();
      Console.WriteLine(GameEngine.gameState.currentRoom.ToString());
      GameEngine.gameState.currentRoom.PrintItems();
	  Helper.Reload(GameEngine.gameState);
      Helper.Display();
	  if(endGame = Anagram.CheckForWin()) break;
      GameShell.getCommand(GameEngine.gameState);
    }
  }
}

