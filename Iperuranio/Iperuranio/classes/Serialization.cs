using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gioco;

[Serializable]
public class Fantoccio
{
  public Fantoccio() {}
  public Fantoccio(string name, GameState gameState)
  {
	Name = name;
	Salvataggio = gameState;
  }
  public string Name {get; set;}
  public GameState Salvataggio {get; set;}

  public override string ToString()
  {
	return $"Ce sta un certo {Name}";
  }
}

  [Serializable]
public static class LoginTable
{
  static public List<Fantoccio> Saves {get; set;}

  static public void Init()
  {
	BinaryFormatter serializer = new BinaryFormatter();
	Saves = new List<Fantoccio>();

	if(!File.Exists(GameEngine.savePath))
	{
	  Saves = new List<Fantoccio>();
	  File.Create(GameEngine.savePath);
	}
	else
	{
	  using (var stream = File.Open(GameEngine.savePath, FileMode.OpenOrCreate))
	  {
		if(stream.Length != 0) Saves = (List<Fantoccio>)serializer.Deserialize(stream);
	  }
	}
  }
}

public static class Serializator
{
  static string input = String.Empty;
  static GameState gameState {get;set;}

  
  static public GameState Menu()
  {
	bool endLogin = false;
	int option;
	while(!endLogin)
  {
	do
	{
	  Console.WriteLine("Selezionare tra le scelte: \n1 - aggiungere utente \n2 - leggere lista utenti \n3 - cancella tutto \n4 - rasuma il game \n5- salva ed esci ");
	  input = Console.ReadLine();
	} while (!int.TryParse(input, out option));

	switch(option)
	{
	  case 1:
		{
		  gameState = GameEngine.GenerateNewGame();
		  Console.WriteLine("inserire username: ");
		  string username = Console.ReadLine();
		  LoginTable.Saves.Add(new Fantoccio(username,gameState));
		  break;
		}
	  case 2:
		{
		  if(LoginTable.Saves.Any())
		  {
			foreach(Fantoccio f in LoginTable.Saves)
			{
			  Console.WriteLine($"Ce sta {f.Name}");
			}
		  }
		  else
		  {
			Console.WriteLine("No saved games, amigo");
		  }
		  break;
		}
	  case 3:
		{
		  LoginTable.Saves.Clear();
		  break;
		}
	  case 4:
		{
		  Console.WriteLine("Che salvataggio vuoi resumere?");
		  string resumeSave = Console.ReadLine();
		  Fantoccio saveSlot = new Fantoccio();
		foreach(Fantoccio f in LoginTable.Saves)
		{
		  if (f.Name == resumeSave)
		  {
			saveSlot = f;
		  }
		  else
		  {
			Console.WriteLine("Nessun salvataggio con questo nome");
			break;
		  }
		}
		  using (var stream = File.Open("Dipendenti.dat", FileMode.Open))
		  {
			  var serializer = new BinaryFormatter();
			  serializer.Serialize(stream, LoginTable.Saves);
		  }

		endLogin = true;
		Program.endGame = false;
		return saveSlot.Salvataggio;
		}
	  case 5:
		{
		 // GameEngine.SaveGame();
		 // using (var stream = File.Open("Dipendenti.dat", FileMode.OpenOrCreate))
		 // {
		 //     var serializer = new BinaryFormatter();
		 //     serializer.Serialize(stream, LoginTable.Saves);
		 // }
		  Environment.Exit(0);
		  break;
		}
	  default:
		Console.WriteLine("Not an option");
		  break;
	}
  } 
	return null;
}}
