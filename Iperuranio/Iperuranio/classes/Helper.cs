namespace Gioco;

  [Serializable]
static public class Helper
{
  static public List<string> mainCommands = new List<string>{"menu","esci"};
  static public List<string> allCommands = new List<string>();
  static bool visible = true;
  static int Width = 0;
  static public void Display()
  {
	using (new Layout.CursorScope())
	{
	  if (visible) Layout.edgeWindow(Helper.allCommands, 1, out Width);
	}
  }
  static public void Switch()
  {
    visible = !visible;
	//Console.Clear();
  }
  public static void Reload(GameState gameState)
  {
	allCommands.Clear();
	foreach(string dirs in gameState.currentRoom.directions.Keys)
	{
	  allCommands.Add("vai " + dirs);
	}
	foreach(Item item in gameState.currentRoom.Items)
	{
	  if(!item.puzzle.solved)
	  {
		allCommands.Add("focalizza " + new string(item.puzzle.Grid));
	  }
	}
	  foreach(string str in allCommands) if(str.Length > Width) Width=str.Length;
	  char[] separator = new char[Width];
	  for(int i = 0; i < Width; i++)
	  {
		separator[i] = '-';
	  }
	  allCommands.Add(new string(separator));

	foreach(string command in mainCommands)
	{
	  allCommands.Add(command);
	}
  }
}
