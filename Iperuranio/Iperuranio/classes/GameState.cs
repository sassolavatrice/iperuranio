namespace Gioco;

[Serializable]
public class GameState
{
  public List<Room> Rooms { get; set; }
  public Room currentRoom { get; set; }
  public MainCharacter mainCharacter { get; set; }

  public GameState()
  {
    Rooms = new List<Room>();
  }

  public void MoveTo(string direction)
  {
    if (currentRoom.directions.ContainsKey(direction))
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
    Item toPickUp = currentRoom.Items.Find(x => x.Name == nameItem);
    if (toPickUp != null)
    {
      Console.WriteLine($"Hai raccolto {toPickUp.Name}");
      mainCharacter._inventory.Push(toPickUp);
      currentRoom.Items.Remove(toPickUp);
    }
  }
  public void putDown(string nameItem)
  {
    Item toPutDown = mainCharacter._inventory.Peek();
    if (toPutDown != null)
    {
      mainCharacter._inventory.Pop();
      currentRoom.Items.Add(toPutDown);
    }
  }

  public void Teletrasporto(string Destinazione)
  {
    Room stanzadiarrivo = Rooms.Find(y => y.Name == Destinazione);
    if (stanzadiarrivo != null)
    {
      currentRoom = stanzadiarrivo;
      mainCharacter._currentRoom = currentRoom;
    }
    else
    {
      Console.WriteLine("non esiste");
    }
  }
  public bool CheckForWin()
  {
    Anagram.anagramCount = 0;
    int solvedAnagramCount = 0;
    foreach (Room r in Rooms)
    {
      foreach (Item i in r.Items)
      {
        Anagram.anagramCount++;
        if (i.puzzle.solved) solvedAnagramCount++;
      }
    }
    if (Anagram.anagramCount == solvedAnagramCount)
    {
      Console.Clear();
      Console.WriteLine($"totale Anagrammi: {Anagram.anagramCount} anagrammi Risolti: {solvedAnagramCount}");
      Console.WriteLine("Hai risolto tutti gli anagrammi!");
      Console.WriteLine("Premi qualsiasi tasto per tornare alla schermata principale");
      Console.ReadKey();
      return true;
    }
    else { return false; }
  }
}

