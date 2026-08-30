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
		foreach (Item item in _currentRoom.Items)
		{
			if (anagram == new string(item.puzzle.Grid.ToArray()))
			{
				if (!item.puzzle.solved)
				{
					item.puzzle.SolveAnagram();
				}
				Console.WriteLine("Anagramma non trovato");
			}
		}
	}
}
