namespace Gioco;

static public class Program
{
  public static bool endGame = false;
  public static bool endApp = false;
  static void Main()
  {
	Log.Init();
	LoginTable.Init();
	while(!endApp)
	{
	  GameEngine.gameState = Serializator.Menu();
	endGame = false;
	  while (!endGame)
	  {
		Console.Clear();
		Console.WriteLine(GameEngine.gameState.StatusLine());
		Console.WriteLine();
		Console.WriteLine(GameEngine.gameState.currentRoom.Name);
		Console.WriteLine(GameEngine.gameState.currentRoom.Description);
		GameEngine.gameState.currentRoom.PrintItems();
		if (!string.IsNullOrEmpty(GameEngine.gameState.LastEvent))
		{
		  Console.WriteLine(GameEngine.gameState.LastEvent);
		  Console.WriteLine();
		}
		Helper.Reload(GameEngine.gameState);
		Helper.Display();
		GameShell.getCommand(GameEngine.gameState);
		Hub.SmeagolTurn(GameEngine.gameState);
		if(GameEngine.gameState.CheckForWin()) break;
		GameEngine.SaveGame();
	  }
	}
  }
}

