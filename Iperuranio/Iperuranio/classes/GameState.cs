namespace Gioco;

public class GameState
{
  public List<Room> Rooms = new List<Room>();
  public Room? currentRoom {get;set;}
  public MainCharacter mainCharacter {get;set;}
  public Book book {get;set;}

  public void MoveTo(string direction)
  {
    if(currentRoom.directions.ContainsKey(direction))
    {
      currentRoom = currentRoom.getDirection(direction);
	  mainCharacter._currentRoom = currentRoom;
    }
    else
    {
      Console.WriteLine("Non c'è questa direzione");
    }
  }

  public void PickUp(string nameItem)
  { 
    Item toPickUp = currentRoom.Items.Find(x => x.Name== nameItem);
    if(toPickUp != null)
    {
    currentRoom.Items.Remove(toPickUp);
    mainCharacter.Inventory.Push(toPickUp);
    }
  }
  public void putDown(string nameItem)
  { 
    Item toPutDown = mainCharacter.Inventory.Peek();
    if(toPutDown != null)
    {
    mainCharacter.Inventory.Pop();
    currentRoom.Items.Add(toPutDown);
    }
  }
}
