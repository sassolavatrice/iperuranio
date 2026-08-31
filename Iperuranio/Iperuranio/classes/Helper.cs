namespace Gioco;

static public class Helper
{
	static public List<string> mainCommands = new List<string> { "inventario", "molla", "aiuto", "menu", "esci" };
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
			if (item.puzzle != null && !item.puzzle.solved)
			{
				CommandList.Add("focalizza " + new string(item.puzzle.Grid));
			}
			else if (!(item is NPC))   // gli NPC non si raccolgono
			{
				CommandList.Add("raccogli " + item.Name);
			}
		}
		// comandi che dipendono da dove ti trovi
		if (gameState.currentRoom.Items.OfType<NPC>().Any(n => n.puzzle == null || n.puzzle.solved))
			CommandList.Add("parla");
		switch (gameState.currentRoom.Name)
		{
			case Hub.Portale: CommandList.Add("entra"); break;
			case Hub.Forgia: CommandList.Add("forgia"); break;
			case Hub.SalaEnigma: CommandList.Add("enigma"); break;
		}
		if (gameState.InDungeon && gameState.currentRoom == gameState.DungeonExit)
			CommandList.Add("torna");

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
