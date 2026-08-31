using Spectre.Console;

namespace Gioco;

/// <summary>
/// L'enigma della Sala Centrale: un anagramma che nasce senza lettere.
/// Le caselle si riempiono solo con le lettere estratte alla forgia
/// dalle parole riportate dai dungeon.
/// </summary>
[Serializable]
public class FinalAnagram
{
	public string Solution { get; set; }
	public string Riddle { get; set; }
	public char[] Grid { get; set; }
	public bool solved = false;

	public FinalAnagram(string solution, string riddle)
	{
		Solution = solution.ToLowerInvariant();
		Riddle = riddle;
		Grid = new char[Solution.Length];
		for (int i = 0; i < Grid.Length; i++) Grid[i] = ' ';
	}

	/// <summary>Quante volte la lettera serve ancora, contando pool e caselle già piene.</summary>
	public int StillNeeded(char c, List<char> pool)
	{
		c = char.ToLowerInvariant(c);
		int required = Solution.Count(x => x == c);
		int placed = Grid.Count(x => x == c);
		int inPool = pool.Count(x => x == c);
		return required - placed - inPool;
	}

	public string Display()
	{
		return string.Concat(Grid.Select(c => c == ' ' ? "[_]" : $"[{c}]"));
	}

	/// <summary>
	/// Schermata interattiva. Frecce per muoversi, una lettera per inserirla,
	/// BACKSPACE per toglierla, ESC per uscire.
	/// Il pool viene modificato direttamente: le lettere si consumano.
	/// </summary>
	public void Interact(List<char> pool)
	{
		int x = 0;

		while (true)
		{
			Console.Clear();
			AnsiConsole.MarkupLine("[yellow]LA SALA DELL'ENIGMA[/]\n");
			Console.WriteLine(Riddle);
			Console.WriteLine();

			for (int i = 0; i < Grid.Length; i++)
			{
				char shown = Grid[i] == ' ' ? '_' : Grid[i];
				if (i == x) AnsiConsole.Markup($"[blue][[{shown}]][/]");
				else Console.Write($"[{shown}]");
			}

			Console.WriteLine("\n");
			Console.WriteLine(pool.Count == 0
				? "Lettere disponibili: nessuna. Devi tornare a scavare."
				: "Lettere disponibili: " + string.Join(" ", pool.OrderBy(c => c)));
			Console.WriteLine("\n← → per muoverti · una lettera per inserirla · BACKSPACE per toglierla · ESC per uscire");

			ConsoleKeyInfo key = Console.ReadKey(true);

			if (key.Key == ConsoleKey.Escape) return;

			if (key.Key == ConsoleKey.LeftArrow)
			{
				x = (x - 1 + Grid.Length) % Grid.Length;
				continue;
			}
			if (key.Key == ConsoleKey.RightArrow)
			{
				x = (x + 1) % Grid.Length;
				continue;
			}
			if (key.Key == ConsoleKey.Backspace)
			{
				if (Grid[x] != ' ')
				{
					pool.Add(Grid[x]);   // la lettera torna disponibile
					Grid[x] = ' ';
				}
				continue;
			}

			char typed = char.ToLowerInvariant(key.KeyChar);
			if (!char.IsLetter(typed)) continue;

			int idx = pool.IndexOf(typed);
			if (idx < 0) continue;               // non possiedi quella lettera

			if (Grid[x] != ' ') pool.Add(Grid[x]);  // rimetti nel pool quella che sostituisci
			Grid[x] = typed;
			pool.RemoveAt(idx);
			x = (x + 1) % Grid.Length;

			if (new string(Grid) == Solution)
			{
				solved = true;
				Log.Info($"enigma risolto: {Solution}");
				Console.Clear();
				AnsiConsole.Status()
					.Spinner(Spinner.Known.Star)
					.Start("L'ENIGMA CEDE", ctx => Task.Delay(2000).Wait());
				AnsiConsole.MarkupLine($"[green]{Solution.ToUpperInvariant()}[/]\n");
				Console.WriteLine("Le pareti smettono di fingere di essere pareti.");
				Console.WriteLine("\nPremi un tasto.");
				Console.ReadKey();
				return;
			}
		}
	}
}
