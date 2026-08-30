namespace Gioco;

static public class Helper
{
	static public List<string> mainCommands = new List<string> { "inventario", "aiuto", "menu", "esci" };
	static public List<string> CommandList = new List<string>();
	static bool visible = true;
	static int Width = 0;
	static public void Display()
	{
		using (new Layout.CursorScope())
		{
			if (visible) Layout.edgeWindow(Helper.CommandList, 1, out Width);
		}
	}
	static public void Switch()
	{
		visible = !visible;
		//Console.Clear();
	}
	public static void Reload(GameState gameState)
	{
		CommandList.Clear();
		foreach (string dirs in gameState.currentRoom.directions.Keys)
		{
			CommandList.Add("vai " + dirs);
		}
		foreach (Item item in gameState.currentRoom.Items)
		{
			if (item.puzzle == null)
			{
				CommandList.Add("raccogli " + item.Name);
			}
			else if (item.puzzle.solved)
			{
				CommandList.Add("raccogli " + item.Name);
			}
			else
			{
				CommandList.Add("focalizza " + new string(item.puzzle.Grid));
			}
		}
		foreach (string str in CommandList) if (str.Length > Width) Width = str.Length;
		char[] separator = new char[Width];
		for (int i = 0; i < Width; i++)
		{
			separator[i] = '-';
		}
		CommandList.Add(new string(separator));

		foreach (string command in mainCommands)
		{
			CommandList.Add(command);
		}
	}
}
