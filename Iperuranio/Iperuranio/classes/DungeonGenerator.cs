namespace Gioco;

/// <summary>
/// Genera proceduralmente un dungeon usando le classi già esistenti del gioco.
/// Produce solo le stanze: l'hub e la progressione stanno in Hub.cs.
/// </summary>
public static class DungeonGenerator
{
	// Pianta ASCII dell'ultimo dungeon generato: serve per il debug
	// e per gli screenshot della relazione. Non viene serializzata.
	public static string LastLayout { get; private set; } = "";

	private static readonly string[] AllDirections = { "nord", "est", "sud", "ovest" };

	private static string Opposite(string dir) => dir switch
	{
		"nord" => "sud",
		"sud" => "nord",
		"est" => "ovest",
		_ => "est"
	};

	// y cresce verso il basso: comodo per disegnare la pianta
	private static (int dx, int dy) Delta(string dir) => dir switch
	{
		"nord" => (0, -1),
		"sud" => (0, 1),
		"est" => (1, 0),
		_ => (-1, 0)
	};

	// ------------------------------------------------------------------
	// CONTENUTI — allunga queste liste per avere dungeon più vari
	// ------------------------------------------------------------------

	private static readonly (string Name, string Description)[] RoomPool =
	{
		("Sala delle Idee",      "Le pareti sembrano ricordare forme che non hai mai visto."),
		("Corridoio Immoto",     "Un corridoio che non porta da nessuna parte, eppure ci sei arrivato."),
		("Gabinetto dei Nomi",   "Ogni cosa qui ha due nomi, e nessuno dei due è quello giusto."),
		("Vestibolo Bianco",     "Una stanza così pulita da sembrare appena pensata."),
		("Scriptorium",          "Inchiostro secco, pagine vuote, una calligrafia che riconosci."),
		("Camera degli Echi",    "Quello che dici torna indietro in ordine sbagliato."),
		("Deposito delle Copie", "Oggetti quasi identici a oggetti che esistono davvero."),
		("Anticamera Tiepida",   "Qualcuno è appena uscito. O sta per entrare."),
		("Loggia Capovolta",     "Il pavimento è convincente, il soffitto un po' meno."),
		("Sala dei Calchi",      "Statue di cose, non di persone."),
		("Cella Ordinata",       "Tutto è al suo posto. Il posto, però, è discutibile."),
		("Atrio delle Bozze",    "Versioni preliminari di stanze migliori.")
	};

	private static readonly (string Name, string Description, string Tip)[] ItemPool =
	{
		("lanterna", "Fa luce su quello che preferiresti non vedere",  "La lanterna smette di tremare"),
		("clessidra","Misura un tempo che qui non passa",              "La sabbia finalmente scende"),
		("specchio", "Riflette con un attimo di ritardo",              "Il riflesso adesso ti somiglia"),
		("candela",  "Brucia senza consumarsi mai",                    "La fiamma si stabilizza"),
		("quaderno", "Appunti che non ricordi di aver preso",          "Le pagine tornano leggibili"),
		("bussola",  "L'ago punta sempre verso di te",                 "L'ago finalmente indica il nord"),
		("moneta",   "Ha la stessa faccia su entrambi i lati",         "La moneta acquista una croce"),
		("corda",    "Abbastanza lunga, mai abbastanza robusta",       "I nodi si sciolgono da soli"),
		("chiave",   "Apre qualcosa, prima o poi",                     "La chiave smette di essere fredda"),
		("maschera", "Ti sta comoda, ed è questo il problema",         "La maschera si stacca"),
		("pergamena","Scritta in una lingua che quasi capisci",        "Il testo diventa italiano"),
		("scodella", "Vuota, ma sporca di qualcosa",                   "La scodella si pulisce"),
		("martello", "Pesante come l'idea di usarlo",                  "Il manico smette di scottare"),
		("piuma",    "Cade più lentamente del dovuto",                 "La piuma tocca terra"),
		("sedia",    "Sembra aspettare qualcuno di preciso",           "La sedia si gira verso di te"),
		("tamburo",  "Suona da solo quando gli dai le spalle",         "Il tamburo tace")
	};

	// ------------------------------------------------------------------
	// GENERAZIONE
	// ------------------------------------------------------------------

	/// <summary>
	/// Costruisce un dungeon casuale e restituisce le stanze.
	/// </summary>
	/// <param name="start">Stanza in cui compare il giocatore.</param>
	/// <param name="exit">Varco di uscita: la stanza più lontana dalla partenza.</param>
	/// <param name="roomCount">Numero di stanze (minimo 2).</param>
	/// <param name="itemsPerRoom">Anagrammi per stanza.</param>
	/// <param name="seed">Se valorizzato, la generazione è riproducibile.</param>
	public static List<Room> GenerateRooms(out Room start, out Room exit,
										   int roomCount = 8, int itemsPerRoom = 1, int? seed = null)
	{
		const int W = 7, H = 7;
		if (roomCount < 2) roomCount = 2;
		if (roomCount > W * H) roomCount = W * H;

		// il seed viene loggato: è l'unico modo di ricostruire un dungeon
		// dopo che il giocatore lo ha attraversato
		int usedSeed = seed ?? Environment.TickCount;
		Random rng = new Random(usedSeed);
		Log.Info($"generazione dungeon: seed={usedSeed} stanze={roomCount} oggetti/stanza={itemsPerRoom}");

		Room[,] grid = new Room[W, H];
		List<Room> rooms = new List<Room>();
		Dictionary<Room, (int x, int y)> coords = new Dictionary<Room, (int x, int y)>();

		var namePool = RoomPool.OrderBy(_ => rng.Next()).ToList();
		int nameIndex = 0;

		Room MakeRoom()
		{
			var flavour = namePool[nameIndex % namePool.Count];
			// i nomi devono restare unici: Teletrasporto cerca le stanze per nome
			string name = nameIndex < namePool.Count
				? flavour.Name
				: $"{flavour.Name} {nameIndex / namePool.Count + 1}";
			nameIndex++;
			return new Room(name, flavour.Description);
		}

		// --- random walk: scava partendo dal centro ----------------------
		// Il percorso è contiguo per costruzione, quindi ogni stanza resta
		// raggiungibile: niente pezzi di mappa isolati.
		// out non può essere usato dentro una lambda: si lavora su locali
		// e si assegnano start/exit solo alla fine del metodo.
		int cx = W / 2, cy = H / 2;
		Room startRoom = MakeRoom();
		grid[cx, cy] = startRoom;
		rooms.Add(startRoom);
		coords[startRoom] = (cx, cy);

		int guard = 0;
		while (rooms.Count < roomCount && guard++ < 100000)
		{
			string dir = AllDirections[rng.Next(AllDirections.Length)];
			var (dx, dy) = Delta(dir);
			int nx = cx + dx, ny = cy + dy;

			if (nx < 0 || ny < 0 || nx >= W || ny >= H) continue;

			if (grid[nx, ny] == null)
			{
				Room fresh = MakeRoom();
				grid[nx, ny] = fresh;
				rooms.Add(fresh);
				coords[fresh] = (nx, ny);
			}
			// ci si sposta comunque: ripassare su celle già scavate
			// crea anelli e scorciatoie invece di un corridoio unico
			cx = nx; cy = ny;
		}

		// --- collega le stanze adiacenti ---------------------------------
		foreach (Room room in rooms)
		{
			var (x, y) = coords[room];
			foreach (string dir in AllDirections)
			{
				var (dx, dy) = Delta(dir);
				int nx = x + dx, ny = y + dy;
				if (nx < 0 || ny < 0 || nx >= W || ny >= H) continue;

				Room neighbour = grid[nx, ny];
				if (neighbour == null) continue;

				// addExit usa Dictionary.Add, che esplode sulle chiavi duplicate
				if (!room.directions.ContainsKey(dir))
					room.addExit(dir, neighbour);
				if (!neighbour.directions.ContainsKey(Opposite(dir)))
					neighbour.addExit(Opposite(dir), room);
			}
		}

		// --- il varco è la stanza più lontana dalla partenza --------------
		Dictionary<Room, int> distance = BreadthFirst(startRoom);
		Room exitRoom = rooms.OrderByDescending(r => distance.TryGetValue(r, out int d) ? d : 0).First();
		exitRoom.Name = "Varco";
		exitRoom.Description = "Un arco identico a quello da cui sei entrato.\nScrivi \"torna\" per attraversarlo — se hai finito qui.";

		// --- popola le stanze con gli anagrammi ---------------------------
		// Le parole lunghe finiscono lontano dalla partenza.
		var byDifficulty = ItemPool.OrderBy(_ => rng.Next())
								   .OrderBy(w => w.Name.Length)
								   .ToList();
		var targets = rooms.Where(r => r != startRoom)
						   .OrderBy(r => distance.TryGetValue(r, out int d) ? d : 0)
						   .ToList();

		int wordIndex = 0;
		foreach (Room room in targets)
		{
			for (int i = 0; i < itemsPerRoom && wordIndex < byDifficulty.Count; i++)
			{
				var w = byDifficulty[wordIndex++];
				room.addItem(new Item(w.Name, w.Description, w.Tip));
			}
		}

		// un dungeon senza anagrammi si aprirebbe subito: mai lasciarlo vuoto
		if (!rooms.Any(r => r.Items.Count > 0))
		{
			var w = byDifficulty[0];
			exitRoom.addItem(new Item(w.Name, w.Description, w.Tip));
		}

		// Smeagol si annida lontano dalla partenza, ma mai sul varco:
		// deve incrociarti mentre lavori, non aspettarti all'uscita.
		var tane = rooms.Where(r => r != startRoom && r != exitRoom).ToList();
		if (tane.Count > 0)
			tane[rng.Next(tane.Count)].addItem(new NPC("Smeagol", "mo te ciulo le lettere"));

		LastLayout = RenderLayout(grid, W, H, startRoom, exitRoom);

		Log.Debug($"dungeon pronto: {rooms.Count} stanze, varco='{exitRoom.Name}'\n{LastLayout}");

		start = startRoom;
		exit = exitRoom;
		return rooms;
	}

	// ------------------------------------------------------------------
	// UTILITÀ
	// ------------------------------------------------------------------

	private static Dictionary<Room, int> BreadthFirst(Room origin)
	{
		var distance = new Dictionary<Room, int> { [origin] = 0 };
		var queue = new Queue<Room>();
		queue.Enqueue(origin);

		while (queue.Count > 0)
		{
			Room current = queue.Dequeue();
			foreach (Room next in current.directions.Values)
			{
				if (distance.ContainsKey(next)) continue;
				distance[next] = distance[current] + 1;
				queue.Enqueue(next);
			}
		}
		return distance;
	}

	/// <summary>Pianta ASCII: [S] partenza, [V] varco, [·] stanza normale.</summary>
	private static string RenderLayout(Room[,] grid, int w, int h, Room start, Room exit)
	{
		var sb = new System.Text.StringBuilder();
		for (int y = 0; y < h; y++)
		{
			var line = new System.Text.StringBuilder();
			bool anything = false;
			for (int x = 0; x < w; x++)
			{
				Room r = grid[x, y];
				if (r == null) { line.Append("    "); continue; }
				anything = true;

				char c = r == start ? 'S' : r == exit ? 'V' : '·';
				line.Append('[').Append(c).Append(']');
				line.Append(r.directions.ContainsKey("est") ? "-" : " ");
			}
			if (anything) sb.AppendLine(line.ToString().TrimEnd());
		}
		return sb.Length == 0 ? "(vuoto)" : sb.ToString();
	}
}
