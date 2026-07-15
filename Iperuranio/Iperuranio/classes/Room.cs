namespace Gioco;

public class Room
{
  private string? _name;
  public string Name
  {
    get
    {
      if (_name is not null)
      {
        return _name;
      }
      else
      {
       throw new NullReferenceException();
      }
    }
    set
    {
      _name = value;
    }
  }
  private string? _description;
  public string Description
  {
    get
    {
      if (_description is not null)
      {
        return _description;
      }
      else
      {
       throw new NullReferenceException();
      }
    }
    set
    {
      _description = value;
    }
  }
  
  public List<Item> Items = new List<Item>();
  public Dictionary<string,Room> directions = new Dictionary<string,Room>();
  public Room(string name, string description)
  {
    Name = name;
    Description = description;
  }

  public void addExit(string direction, Room nextRoom)
  {
    directions.Add(direction,nextRoom);
  }

  public Room getDirection(string direction)
  {
    return directions[direction];
  }

  public void addItem(Item toAdd)
  {
    Items.Add(toAdd);
  }

  public void removeItem(string toRemove)
  {
    int indexToRemove = Items.FindIndex(s => s.Name.Equals(toRemove));
    if (indexToRemove > 0)
      Items.RemoveAt(indexToRemove);
  }

  public void PrintItems(){
		  Console.WriteLine("Oggetti nella stanza:");
    foreach(Item item in Items)
    {
      Console.WriteLine(item.ToString());
    }
	Console.WriteLine();
  }
    

  
}




