namespace Gioco
{
   public class GameEngine
    { 
        public static GameState gameState {get;set;}
		public static int sessionID = 0;

		public GameEngine()
		{
		  LoginTable.Saves = SaveLoadManager.LoadLoginTable();
		  while(Authentication());
		  if(sessionID != 0)
		  { 
		    gameState = SaveLoadManager.LoadGame();
		  } else {Console.WriteLine("è stato un piacere.");}
		}

        public static bool Authentication()
        {
			 Console.WriteLine("Benvenuto nel login (digitare aiuto per vedere comandi disponibili) ");
			 string[] arguments = null;
			  arguments = Console.ReadLine().Trim().Split();
            switch (arguments[0].ToLower())
            {            
			  case "aiuto":
                Console.WriteLine("\"add\" <nickname> to create save slot\n"+
												"\"utenti\" per vedere gli slot di salvataggio\n" +
											   "\"resume\" <nickname> to resume\n"+
                                               "\"esci\" to leave");
				return true;
			  case "utenti":
				Console.WriteLine(LoginTable.Display());
				return true;
			  case "add":
				LoginTable.AddUser(arguments[1]);
				return true;
			  case "resume":
				GameEngine.sessionID = LoginTable.GetUserId(arguments[1]);
				return false;
			  case "esci":
				return false;
			  default:
				Console.WriteLine("comando non riconosciuto");
				return true;
            }
		}
		public static void QuitGame()
		{
		  SaveLoadManager.SaveGame(GameEngine.gameState);
		  Program.endGame = true;
		}

		public static GameState GenerateNewGame()
		{
		  GameState gameState = new GameState();
    Item blu = new Item("blu", "Nel ... dipinto di ...","");
    Item bianco = new Item("bianco", "Siamo sicuri sia un colore?","");
    Item petrolio = new Item("petrolio", "Sfumatura di verde, piace agli Stati Uniti","");
    Item fieno = new Item("fieno", "lo mangiano i cavalli","");
    Item cowboy = new Item("cowboy", "YEEEEEEHAAAW","");

    gameState.book = new Book();

    gameState.Rooms.Add(new Room("???????????", "Dove sono?"));
    gameState.Rooms.Add(new Room("Gattabuia", "Sei circondato da mattonelle e sbare e metallo"));
    gameState.Rooms.Add(new Room("Gattahiara", "Sei circondato da mattoni e sbarre di metallo"));
    gameState.Rooms.Add(new Room("Tavolozza", "È tutto così....colorato"));//blu nel ... dipinto di ... bianco siamo sicuri sia un colore,petrolio sfumatura di verde, piace agli stati uniti
    gameState.Rooms.Add(new Room("Stalla", "Cavalli, ma non solo"));//fieno lo mangiano i cavalli, ferro quello di cavallo porta fortuna, cowboy yeehaw

    gameState.Rooms[0].addExit("nord", gameState.Rooms[1]);
    gameState.Rooms[1].addExit("sud", gameState.Rooms[0]);
    gameState.Rooms[1].addExit("nord", gameState.Rooms[2]);
    gameState.Rooms[2].addExit("sud", gameState.Rooms[1]);
    gameState.Rooms[2].addExit("est",gameState.Rooms[3]);
    gameState.Rooms[3].addExit("ovest",gameState.Rooms[2]);
    gameState.Rooms[3].addExit("sud",gameState.Rooms[4]);
    gameState.Rooms[4].addExit("ovest",gameState.Rooms[3]);
   

    // creo NPC
    NPC baforb = new NPC("Baforb", "un tipico fabBro");
	NPC smeagol = new NPC("Smeagol", "mo te ciulo le lettere");

    // aggiungo oggetti nella stanza
    gameState.Rooms[0].addItem(new Item("tutorial", "Risolvi il tutorial, prego.","Complimenti! Hai risolto il tutorial"));
	gameState.Rooms.Last().addItem(baforb);
	gameState.Rooms.Last().addItem(smeagol);
    gameState.Rooms[3].addItem(bianco);
    gameState.Rooms[3].addItem(petrolio);
    gameState.Rooms[4].addItem(fieno);
    gameState.Rooms[4].addItem(cowboy);




    // aggiungo personaggio e lo posiziono nella prima
    gameState.currentRoom = gameState.Rooms[0];
    gameState.mainCharacter = new MainCharacter(gameState.currentRoom);
	return gameState;
        }
  }
}
