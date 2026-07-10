namespace Gioco;

class MainCharacter
{
  public string Name {get;set;}
  public Stack<Item> Inventory {get;set;}
  public Room _currentRoom {get;set;}

  public MainCharacter(Room currentRoom)
  {
		Name = "";
		Stack<Item> Inventory = new Stack<Item>();
		_currentRoom = currentRoom;
  }

  public void InteractInventory()
  {
		int current;
		ConsoleKeyInfo MyKey;
    do
    {
			current = 0;
			MyKey = Console.ReadKey();
			switch (MyKey.Key)
			{
					case ConsoleKey.W:
							if(current-1 > 0)
								current--;
							break;
					case ConsoleKey.S:
							if(current+1 < Inventory.Count)
									current++;
							break;
			}
			var currentItem = Inventory.ElementAt(current);
      foreach(Item item in Inventory)
      {
		if(item == currentItem)
        Console.WriteLine("> " + item.ToString());
	  }
    } while(MyKey.Key != ConsoleKey.Escape);
  }

  public void showInventory()
  {
    Console.WriteLine("Inventario:");
    if(Inventory.Count == 0)
    {
      Console.WriteLine("L'inventario è vuoto!");
    }
    else
    {
      foreach(Item item in Inventory)
      {
        Console.WriteLine(item.ToString());
	  }
	}
  }

  public void Risolvi(string anagramma)
  {
		foreach(Item item in _currentRoom.Items)
		{
				if( (item.anagram != null) && (new string(item.anagram.Grid).Equals(anagramma)))
				{
						item.anagram.SolveAnagram();
				}
		}
  }
}
