using System.IO;
using System.Text;
using Spectre.Console;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gioco;

[Serializable]
public class Fantoccio
{
	public Fantoccio() { }
	public Fantoccio(string name, GameState gameState)
	{
		Name = name;
		Salvataggio = gameState;
	}
	public string Name { get; set; }
	public GameState Salvataggio { get; set; }

	public override string ToString()
	{
		return $"Ce sta un certo {Name}";
	}
}

[Serializable]
public static class LoginTable
{
	static public List<Fantoccio> Saves { get; set; }

	static public void Init()
	{
		BinaryFormatter serializer = new BinaryFormatter();
		Saves = new List<Fantoccio>();

		if (!File.Exists(GameEngine.savePath))
		{
			Saves = new List<Fantoccio>();
			File.Create(GameEngine.savePath);
		}
		else
		{
			using (var stream = File.Open(GameEngine.savePath, FileMode.OpenOrCreate))
			{
				if (stream.Length != 0) Saves = (List<Fantoccio>)serializer.Deserialize(stream);
			}
		}
	}
}

public static class Serializator
{
	static string input = String.Empty;
	static GameState gameState { get; set; }


	static public GameState Menu()
	{
		bool endLogin = false;
		int option;
		while (!endLogin)
		{
			do
			{
				Console.WriteLine("Selezionare tra le scelte: \n1 - aggiungere utente \n2 - leggere lista utenti \n3 - cancella tutto \n4 - rasuma il game \n5 - salva ed esci ");
				input = Console.ReadKey().KeyChar.ToString();
			} while (!int.TryParse(input, out option));

			Console.Clear();
			switch (option)
			{
				case 1:
					{
						gameState = Hub.NewGame();
						Console.WriteLine("inserire username: ");
						string username = Console.ReadLine();
						Console.Clear();
						LoginTable.Saves.Add(new Fantoccio(username, gameState));
						Console.WriteLine($"Salvataggio di {username} creato, premere un tasto per continuare");
						Console.ReadKey();
						Console.Clear();
						break;
					}
				case 2:
					{
						if (LoginTable.Saves.Any())
						{
							foreach (Fantoccio f in LoginTable.Saves)
							{
								Console.WriteLine($"Ce sta {f.Name}");
							}
						}
						else
						{
							Console.WriteLine("No saved games, amigo");
						}
						Console.ReadKey();
						Console.Clear();
						break;
					}
				case 3:
					{
						LoginTable.Saves.Clear();
						Console.WriteLine("Tutti i salvataggi sono stati cancellati.");
						Console.ReadKey();
						Console.Clear();
						break;
					}
				case 4:
					{
						Console.WriteLine("Che salvataggio vuoi resumere?");
						string resumeSave = String.Empty;
						while (resumeSave == String.Empty)
						{
							resumeSave = Console.ReadLine();
						}
						Console.Clear();
						Fantoccio saveSlot = new Fantoccio();
						foreach (Fantoccio f in LoginTable.Saves)
						{
							if (f.Name == resumeSave)
							{
								saveSlot = f;
								using (var stream = File.Open("Saves.dat", FileMode.Open))
								{
									var serializer = new BinaryFormatter();
									serializer.Deserialize(stream);
								}
							}
						}

						if (saveSlot.Salvataggio == null)
						{
							Console.WriteLine("Nessun salvataggio trovato");
							Console.ReadKey();
							Console.Clear();
							break;
						}
						else
						{
							Console.WriteLine();
							AnsiConsole.Status()
							.Spinner(Spinner.Known.Arc)
							.Start($"recuperando le lettere di {saveSlot.Name}...", ctx =>
								{
									// Simulate some work
									Thread.Sleep(2000);
								});
							endLogin = true;
							Program.endGame = false;
						}
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
					Console.WriteLine(" - Non esiste");
					Console.WriteLine("Premere un tasto per continuare");
					Console.ReadKey();
					break;
			}
		}
		return null;
	}
}
