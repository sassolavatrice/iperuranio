using Spectre.Console;

namespace Gioco;

/// <summary>
/// La base fissa del gioco e le regole del ciclo:
/// hub → portale → dungeon → varco → forgia → enigma.
/// </summary>
public static class Hub
{
	public const string Risveglio = "Risveglio";
	public const string Atrio = "Atrio";
	public const string SalaEnigma = "Sala dell'Enigma";
	public const string Forgia = "Forgia";
	public const string Portale = "Portale";

	private const string EnigmaSolution = "iperuranio";

	private const string EnigmaRiddle =
		"Sulla parete non c'è nulla. Solo dieci caselle vuote,\n" +
		"e sotto, inciso:\n\n" +
		"  \"Non è un luogo, eppure ci sei.\n" +
		"   Non ha porte, eppure sei entrato.\n" +
		"   Qui le cose sono quello che sono\n" +
		"   da prima che qualcuno le pensasse.\"";

	private const string LoreCustode =
		"Il Custode ti guarda come si guarda qualcuno che è in ritardo.\n\n" +
		"«Non sei morto, se è questo che ti preoccupa. Sei solo finito nel posto\n" +
		"dove le parole stanno prima di essere dette. Qui ogni cosa è un anagramma\n" +
		"di se stessa, e nessuna sta ferma.\n\n" +
		"Nella sala qui accanto c'è l'unico enigma che conta. È vuoto: dieci caselle\n" +
		"e nessuna lettera. Le lettere non te le do io, devi estrarle.\n\n" +
		"Attraversa il Portale. Ogni volta troverai un posto diverso, e ogni volta\n" +
		"dovrai risolvere ogni singolo anagramma prima che il varco ti lasci uscire.\n" +
		"Ma potrai riportare indietro una parola sola. Una. Scegli quale con calma:\n" +
		"le altre le perdi.\n\n" +
		"Poi va' da Baforb, alla Forgia. Lui spacca le parole e ti tiene le lettere\n" +
		"che servono. Il resto diventa scarto.»";

	// ------------------------------------------------------------------
	// NUOVA PARTITA
	// ------------------------------------------------------------------
	public static GameState NewGame()
	{
		GameState gameState = new GameState();

		Room risveglio = new Room(Risveglio,
			"Un pavimento freddo e la sensazione precisa di non esserci arrivato camminando.");
		Room atrio = new Room(Atrio,
			"Una sala alta, con quattro aperture e nessuna finestra. Qualcuno ti aspettava.");
		Room sala = new Room(SalaEnigma,
			"Una parete liscia con dieci caselle vuote. Scrivi \"enigma\" per avvicinarti.");
		Room forgia = new Room(Forgia,
			"Calore, incudine, e un fabbro che non alza lo sguardo. Scrivi \"forgia\" per lavorare una parola.");
		Room portale = new Room(Portale,
			"Un arco di pietra che non inquadra la stanza dietro di sé. Scrivi \"entra\" per attraversarlo.");

		risveglio.addExit("nord", atrio);
		atrio.addExit("sud", risveglio);
		atrio.addExit("nord", sala);
		sala.addExit("sud", atrio);
		atrio.addExit("est", forgia);
		forgia.addExit("ovest", atrio);
		atrio.addExit("ovest", portale);
		portale.addExit("est", atrio);

		risveglio.addItem(new Item("tutorial",
			"Come tutorial, dovrai focalizzare quel mucchio di lettere.",
			"Complimenti! Hai risolto il tutorial. A nord c'è l'Atrio."));

		atrio.addItem(new NPC("custode", "Scrivi \"parla\" per ascoltarlo"));
		forgia.addItem(new NPC("baforb", "un tipico fabBro"));

		gameState.HubRooms = new List<Room> { risveglio, atrio, sala, forgia, portale };
		gameState.Rooms = gameState.HubRooms;
		gameState.currentRoom = risveglio;
		gameState.InDungeon = false;
		gameState.Letters = new List<char>();
		gameState.Enigma = new FinalAnagram(EnigmaSolution, EnigmaRiddle);
		gameState.mainCharacter = new MainCharacter(gameState.currentRoom);

		return gameState;
	}

	// ------------------------------------------------------------------
	// PORTALE
	// ------------------------------------------------------------------
	public static void EnterDungeon(GameState gameState)
	{
		if (gameState.InDungeon)
		{
			Console.WriteLine("Sei già dall'altra parte.");
			return;
		}
		if (gameState.currentRoom.Name != Portale)
		{
			Console.WriteLine("Non c'è nessun portale qui. L'arco è a ovest dell'Atrio.");
			return;
		}
		if (gameState.mainCharacter._inventory.Count > 0)
		{
			Console.WriteLine("Il portale non accetta chi porta parole con sé.");
			Console.WriteLine("Passa dalla Forgia e svuota l'inventario prima di attraversare.");
			Console.ReadKey();
			return;
		}

		// dungeon un po' più grande a ogni viaggio
		int size = Math.Min(6 + gameState.DungeonCount, 12);

		Room start, exit;
		List<Room> dungeon = DungeonGenerator.GenerateRooms(out start, out exit, roomCount: size);

		gameState.Rooms = dungeon;
		gameState.currentRoom = start;
		gameState.DungeonExit = exit;
		gameState.InDungeon = true;
		gameState.DungeonCount++;
		Log.Info($"ingresso dungeon #{gameState.DungeonCount} ({size} stanze)");

		Console.Clear();
		AnsiConsole.MarkupLine($"[grey]Attraversi l'arco. Viaggio numero {gameState.DungeonCount}.[/]");
		Console.WriteLine("Il posto è nuovo, e non lo rivedrai mai più.");
		Console.WriteLine("\nPremi un tasto.");
		Console.ReadKey();
	}

	// ------------------------------------------------------------------
	// VARCO DI USCITA
	// ------------------------------------------------------------------
	public static bool AllSolved(List<Room> rooms)
	{
		foreach (Room r in rooms)
			foreach (Item i in r.Items)
				if (i.puzzle != null && !i.puzzle.solved) return false;
		return true;
	}

	public static void ReturnToHub(GameState gameState)
	{
		if (!gameState.InDungeon)
		{
			Console.WriteLine("Sei già alla base.");
			return;
		}
		if (gameState.currentRoom != gameState.DungeonExit)
		{
			Console.WriteLine("Da qui non si torna indietro. Il varco è altrove.");
			return;
		}
		if (!AllSolved(gameState.Rooms))
		{
			int left = gameState.Rooms.Sum(r => r.Items.Count(i => i.puzzle != null && !i.puzzle.solved));
			Console.WriteLine($"Il varco resta chiuso: ci sono ancora {left} anagrammi irrisolti.");
			Console.ReadKey();
			return;
		}
		if (gameState.mainCharacter._inventory.Count > 1)
		{
			Console.WriteLine("Puoi riportare indietro una parola sola.");
			Console.WriteLine("Usa \"molla\" finché non ne resta una.");
			Console.ReadKey();
			return;
		}

		gameState.Rooms = gameState.HubRooms;
		gameState.currentRoom = gameState.HubRooms.First(r => r.Name == Portale);
		gameState.mainCharacter._currentRoom = gameState.currentRoom;
		gameState.DungeonExit = null;
		gameState.InDungeon = false;
		Log.Info($"ritorno alla base dal dungeon #{gameState.DungeonCount}, " +
				 $"parole perse finora: {gameState.WordsEaten}");

		Console.Clear();
		Console.WriteLine("Riemergi dall'arco. Dietro di te non c'è più niente.");
		Console.WriteLine("\nPremi un tasto.");
		Console.ReadKey();
	}

	// ------------------------------------------------------------------
	// FORGIA
	// ------------------------------------------------------------------
	public static void Forge(GameState gameState)
	{
		if (gameState.currentRoom.Name != Forgia)
		{
			Console.WriteLine("Qui non c'è nessuna incudine.");
			return;
		}

		Item word = gameState.mainCharacter._inventory.Peek();
		if (word == null)
		{
			Console.WriteLine("Baforb guarda le tue mani vuote e torna a battere il ferro.");
			Console.ReadKey();
			return;
		}

		gameState.mainCharacter._inventory.Pop();

		var kept = new List<char>();
		int scrap = 0;

		foreach (char c in word.Name.ToLowerInvariant())
		{
			if (!char.IsLetter(c)) continue;

			if (gameState.Enigma.StillNeeded(c, gameState.Letters) > 0)
			{
				gameState.Letters.Add(c);
				kept.Add(c);
			}
			else
			{
				scrap++;
			}
		}

		gameState.mainCharacter.Debris += scrap;
		Log.Info($"forgia: '{word.Name}' -> tenute [{string.Join("", kept)}], scarti {scrap}");

		Console.Clear();
		AnsiConsole.MarkupLine($"[yellow]Baforb spacca [/][white]{word.Name}[/][yellow] sull'incudine.[/]\n");
		Console.WriteLine(kept.Count > 0
			? "Lettere recuperate: " + string.Join(" ", kept)
			: "Niente da recuperare: l'enigma non ha bisogno di queste lettere.");
		if (scrap > 0) Console.WriteLine($"Scarti: {scrap} (totale accumulato: {gameState.mainCharacter.Debris})");
		Console.WriteLine("\nLettere in tuo possesso: " +
			(gameState.Letters.Count == 0 ? "nessuna" : string.Join(" ", gameState.Letters.OrderBy(c => c))));
		Console.WriteLine("\nPremi un tasto.");
		Console.ReadKey();
	}

	// ------------------------------------------------------------------
	// ENIGMA FINALE
	// ------------------------------------------------------------------
	public static void OpenEnigma(GameState gameState)
	{
		if (gameState.currentRoom.Name != SalaEnigma)
		{
			Console.WriteLine("L'enigma è nella sala a nord dell'Atrio.");
			return;
		}
		gameState.Enigma.Interact(gameState.Letters);
	}

	// ------------------------------------------------------------------
	// DIALOGO
	// ------------------------------------------------------------------
	public static void Talk(GameState gameState)
	{
		var presenti = gameState.currentRoom.Items.OfType<NPC>().ToList();
		if (presenti.Count == 0)
		{
			Console.WriteLine("Non c'è nessuno con cui parlare.");
			return;
		}

		// si può parlare solo con chi ha già un nome:
		// finché l'anagramma è scomposto, l'NPC non è ancora nessuno
		NPC npc = presenti.FirstOrDefault(n => n.puzzle == null || n.puzzle.solved);
		if (npc == null)
		{
			NPC muto = presenti[0];
			Console.Clear();
			Console.WriteLine("La figura davanti a te apre la bocca, ma le lettere del suo nome");
			Console.WriteLine("sono ancora fuori posto: non esce nessun suono.");
			Console.WriteLine($"\nFocalizza {new string(muto.puzzle.Grid)} per dargli un nome.");
			Console.WriteLine("\nPremi un tasto.");
			Console.ReadKey();
			return;
		}

		Console.Clear();
		if (npc.Name == "custode") Console.WriteLine(LoreCustode);
		else if (npc.Name == "baforb") Console.WriteLine("«Portami una parola e te la rompo. Scrivi \"forgia\".»");
		else Console.WriteLine($"{npc.Name} non ha molto da dire.");
	}

	// ------------------------------------------------------------------
	// SMEAGOL
	// ------------------------------------------------------------------

	/// <summary>
	/// Turno di Smeagol: si aggira per il dungeon e divora le parole
	/// già risolte che trova incustodite. Va chiamato dopo ogni comando
	/// del giocatore, solo mentre si è dentro un dungeon.
	/// </summary>
	public static void SmeagolTurn(GameState gameState)
	{
		gameState.LastEvent = "";
		if (!gameState.InDungeon || gameState.Rooms == null) return;

		// lo si ritrova scandendo le stanze: nessuno stato da tenere allineato
		Room tana = null;
		NPC smeagol = null;
		foreach (Room r in gameState.Rooms)
		{
			NPC found = r.Items.OfType<NPC>().FirstOrDefault(n => n.Name == "Smeagol");
			if (found != null) { tana = r; smeagol = found; break; }
		}
		if (smeagol == null) return;

		Random rng = new Random();
		bool insieme = tana == gameState.currentRoom;

		// --- mangia solo se non lo stai guardando -------------------------
		if (!insieme)
		{
			Item preda = tana.Items.FirstOrDefault(i => !(i is NPC)
													 && i.puzzle != null
													 && i.puzzle.solved);
			if (preda != null)
			{
				tana.Items.Remove(preda);
				gameState.WordsEaten++;
				Log.Debug($"smeagol ha divorato '{preda.Name}' in '{tana.Name}'");
				gameState.LastEvent = $"Da qualche parte qualcosa mastica. Una parola in meno nel mondo. ({gameState.WordsEaten})";
				return;   // resta a digerire: ti dà un turno per raggiungerlo
			}
		}

		// --- altrimenti si sposta ----------------------------------------
		var uscite = tana.directions.Values.Where(r => r != null).ToList();
		if (uscite.Count == 0) return;

		Room destinazione = uscite[rng.Next(uscite.Count)];
		tana.Items.Remove(smeagol);
		destinazione.addItem(smeagol);

		if (insieme)
		{
			string dove = Verso(tana, destinazione);
			gameState.LastEvent = $"Smeagol ti sfila accanto e sguscia via verso {dove}.";
		}
		else if (destinazione == gameState.currentRoom)
		{
			gameState.LastEvent = "Qualcosa è appena entrato nella stanza con te.";
		}
		else if (gameState.currentRoom.directions.Values.Contains(destinazione))
		{
			string dove = Verso(gameState.currentRoom, destinazione);
			gameState.LastEvent = $"Senti trascinare qualcosa a {dove}.";
		}
	}

	/// <summary>Nome della direzione che da 'da' porta a 'a', se esiste.</summary>
	private static string Verso(Room da, Room a)
	{
		foreach (var coppia in da.directions)
			if (coppia.Value == a) return coppia.Key;
		return "chissà dove";
	}
}
