namespace Gioco;

static public class Program
{
  public static bool endGame = false;
  public static bool endApp = false;
  static void Main()
  {
	LoginTable.Init();
	while(!endApp)
	{
	  GameEngine.gameState = Serializator.Menu();
	  while (!endGame)
	  {
		Console.Clear();
		Console.WriteLine(GameEngine.gameState.currentRoom.Name);
		Console.WriteLine(GameEngine.gameState.currentRoom.Description);
		GameEngine.gameState.currentRoom.PrintItems();
		Helper.Reload(GameEngine.gameState);
		Helper.Display();
		GameShell.getCommand(GameEngine.gameState);
		if(GameEngine.gameState.CheckForWin()) break;
		GameEngine.SaveGame();
	  }
	}
  }
}

