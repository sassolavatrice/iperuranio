namespace Gioco;

[Serializable]
public class MainCharacter
{
	public string _name { get; set; }
	public Inventory _inventory { get; set; }
	public Room _currentRoom { get; set; }
	public int Debris { get; set; }

	public MainCharacter(Room currentRoom)
	{
		_name = "";
		Debris = 0;
		_inventory = new Inventory();
		_currentRoom = currentRoom;
	}

	public void showInventory()
	{
		Console.Clear();
		Console.WriteLine("Inventario:");
		if (_inventory.Count == 0)
		{
			Console.WriteLine("L'inventario è vuoto!");
		}
		else
		{
			foreach (Item item in _inventory.items)
			{
				Console.WriteLine(item.ToString());
			}
		}
		Console.ReadKey();
	}

	public void Risolvi(string anagram)
	{
		bool found = false;
		foreach (Item item in _currentRoom.Items)
		{
			if (item.puzzle == null) continue;
			if (anagram == new string(item.puzzle.Grid.ToArray()))
			{
				found = true;
				if (!item.puzzle.solved) item.puzzle.SolveAnagram();
				break;
			}
		}
		if (!found) Console.WriteLine("Anagramma non trovato");
	}
}
