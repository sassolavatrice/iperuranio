namespace Gioco;

[Serializable]
public class GameState
{
  // Stanze dell'area in cui ti trovi adesso: l'hub oppure il dungeon corrente.
  // Tutto il codice esistente (Teletrasporto, Helper) continua a usare questa.
  public List<Room> Rooms { get; set; }

  // La base, sempre viva anche mentre sei nel dungeon.
  public List<Room> HubRooms { get; set; }

  public Room currentRoom { get; set; }
  public MainCharacter mainCharacter { get; set; }

  // --- stato del ciclo dungeon -------------------------------------
  public bool InDungeon { get; set; }

  // quante parole Smeagol ti ha divorato, e l'ultimo evento da mostrare a schermo
  public int WordsEaten { get; set; }
  public string LastEvent { get; set; }
  public Room DungeonExit { get; set; }
  public int DungeonCount { get; set; }

  // --- progressione dell'enigma finale ------------------------------
  public FinalAnagram Enigma { get; set; }
  public List<char> Letters { get; set; }

  public GameState()
  {
    Rooms = new List<Room>();
    HubRooms = new List<Room>();
    Letters = new List<char>();
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
    if (toPickUp == null)
    {
      Console.WriteLine("Non trovo questo oggetto.");
      return;
    }
    if (toPickUp is NPC)
    {
      Console.WriteLine($"{toPickUp.Name} non è un oggetto, e te lo farebbe notare.");
      return;
    }
    if (toPickUp.puzzle != null && !toPickUp.puzzle.solved)
    {
      Console.WriteLine("La parola è ancora scomposta: risolvila prima di raccoglierla.");
      return;
    }
    Console.WriteLine($"Hai raccolto {toPickUp.Name}");
    mainCharacter._inventory.Push(toPickUp);
    currentRoom.Items.Remove(toPickUp);
  }

  public void putDown(string nameItem)
  {
    Item toPutDown = mainCharacter._inventory.Peek();
    if (toPutDown != null)
    {
      mainCharacter._inventory.Pop();
      currentRoom.Items.Add(toPutDown);
      Console.WriteLine($"Hai lasciato {toPutDown.Name}");
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

  /// <summary>Riga di stato mostrata sopra ogni stanza.</summary>
  public string StatusLine()
  {
    string place = InDungeon ? $"Dungeon #{DungeonCount}" : "Base";
    string letters = Letters.Count == 0 ? "-" : string.Join("", Letters.OrderBy(c => c));
    string enigma = Enigma == null ? "-" : Enigma.Display();
    return $"[{place}]  lettere: {letters}  enigma: {enigma}";
  }

  /// <summary>
  /// La partita si vince solo risolvendo l'enigma della Sala Centrale.
  /// I singoli anagrammi dei dungeon sono il mezzo, non il fine.
  /// </summary>
  public bool CheckForWin()
  {
    if (Enigma == null || !Enigma.solved) return false;

    Console.Clear();
    Console.WriteLine($"Hai attraversato {DungeonCount} dungeon per dieci lettere.");
    Console.WriteLine("Hai risolto l'enigma dell'Iperuranio.");
    Console.WriteLine("\nPremi qualsiasi tasto per tornare alla schermata principale");
    Console.ReadKey();
    return true;
  }
}
