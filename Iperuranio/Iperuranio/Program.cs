namespace Gioco;

static public class Program
{
  public static bool endGame = false;
  static void Main()
  {
    GameState gameState = new GameState();

    Item gonorrea = new Item("gonorrea", "malattia venerea, tranquilla nel complesso");
    Item clamidia = new Item("clamidia", "bubboni sulla minchia, peso zio");
    Item HIV = new Item("HIV", "il più grande spettacolo dopo il big bang");
    Item tutorial = new Item("Tutorial", "seleziona la lettera, spostala e risolvi questo anagramma");
    Item blu = new Item("blu", "Nel ... dipinto di ...");
    Item bianco = new Item("bianco", "Siamo sicuri sia un colore?");
    Item petrolio = new Item("petrolio", "Sfumatura di verde, piace agli Stati Uniti");
    Item fieno = new Item("fieno", "lo mangiano i cavalli");
    Item cowboy = new Item("cowboy", "YEEEEEEHAAAW");



    gameState.book = new Book();
    gameState.book.AggiungiIndovinello(clamidia);
    gameState.book.AggiungiIndovinello(HIV);

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
   

    // creo oggetti
    NPC Baforb = new NPC("Baforb", "un tipico fabBro");
    NPC Cane = new NPC("Cane", "un cane guida, hai bisogno di \"aiuto\"?");
    NPC VanGogh = new NPC("Van Gogh", "ma non era morto???");
    NPC Unicorno = new NPC("Unicorno ", "ma quindi esistono??!");
    Tile a = new Tile('a');
    //Debris barra = new Debris('\\');

    // aggiungo oggetti nella stanza
    gameState.Rooms[0].addItem(tutorial);
    gameState.Rooms[0].addItem(Cane);
    gameState.Rooms[1].addItem(Baforb);
    //gameState.Rooms[1].addItem(barra);
    gameState.Rooms[2].addItem(clamidia);
    gameState.Rooms[2].addItem(gonorrea);
    gameState.Rooms[3].addItem(VanGogh);
    gameState.Rooms[3].addItem(blu);
    gameState.Rooms[3].addItem(bianco);
    gameState.Rooms[3].addItem(petrolio);
    gameState.Rooms[4].addItem(Unicorno);
    gameState.Rooms[4].addItem(fieno);
    gameState.Rooms[4].addItem(cowboy);




    // aggiungo personaggio e lo posiziono nella prima
    gameState.currentRoom = gameState.Rooms[0];
    gameState.mainCharacter = new MainCharacter(gameState.currentRoom);

    //DialogNode dialognode1 = new DialogNode();
    //dialognode1.Phrase = "ciao come va? \n0)bene \n1)male";
    //DialogNode dialognode2 = new DialogNode();
    //dialognode2.Phrase = "mi dispiace che stai male";
    //DialogNode dialognode3 = new DialogNode();
    //dialognode3.Phrase = "mi fa piacere che stai bene";

    //dialognode1.AddNode(dialognode3);//vanno infilati in ordine 
    //dialognode1.AddNode(dialognode2);

    ////Console.WriteLine(dialognode1.NextNodes[0].Phrase);
    ////Console.WriteLine(dialognode1.NextNodes[1].Phrase);
    ////Console.ReadKey();
    //Baforb.AddDialogNode("dialog1", dialognode1);

    while (!endGame)
    {
      Console.Clear();
      Console.WriteLine(gameState.currentRoom.Name);
      Console.WriteLine(gameState.currentRoom.Description);
      gameState.currentRoom.PrintItems();
	  Helper.Reload(gameState);
      Helper.Display();
      GameShell.getCommand(gameState);
    }
  }
  // static void Main()
  // {
  //   	Gamestate gameState = new Gamestate();

  //   	//creazione e connessione stanze
  //   	gameState.Map.Add(new Room("Iperuranio","Ti senti spossato, come appena sveglio. Non sai dove ti trovi, sembra ciano pareti ma non ne percepisci la distanza, solo una fitta luce si distingue sullo sfondo"));
  //   	gameState.Map.Add(new Room("Cameretta", "La luce si spegne all'improvviso e lo spazio ha ripreso forma. Ti guardi intorno , sembra la stanza di un bambino. Una goccia continua a cadere martellante dal soffitto.", lutto));
  //   	gameState.Map.Add(new Room("Classe", "Ti si materializza di fronte la tua classe, completamente vuota se non per una singola figura seduta in fondo alla stanza. Ti pare di sentire delle voci oltre la porta. Inoltre vedi una grande crepa sulla parete a fianco a te.", solitudine));
  //   	gameState.Map.Add(new Room("Monolocale", "Ti trovi all’interno di un monolocale, un luogo particolarmente familiare a te, in notevole disordine e confusione. La stanza è illuminata solo dalla televisione accesa e da una singola lampadina nella cucina. Ti accorgi che, in un angolo, alcuni sacchi di spazzatura sono stati impilati con cura.", riabilitazione));
  //   	gameState.Map.Add(new Room("Eclissi"," Non sai per quanto tempo hai strisciato. Capoisci di essere giunto dall'altro capo quando l'empio calore nel tuo petto diventa freddo. Ritrovi giusto le forze per alzarti, ma nell'atto senti il sospiro di qualcuno, una presenza oscura che ti mette angoscia. Il terrore ti paralizza devi farci immediatamente qualcosa!"));
  //   	gameState.Map.Add(new Room("Alba"," Non sai per quanto tempo hai strisciato. Capoisci di essere giunto dall'altro capo quando l'empio calore nel tuo petto diventa freddo. Ritrovi giusto le forze per alzarti, ma nell'atto senti il sospiro di qualcuno, una presenza oscura che ti mette angoscia. Il terrore ti paralizza devi farci immediatamente qualcosa!"));
  //   	gameState.Map.Add(new Room("Stanza Oscura", "\tComplimenti. Hai vinto!"));

  //   	//creazione e distiribuzione oggetti
  //   	Item luce = new Item();Item luce = new Item();
  //   	
  //   	//spawn del personaggio
  //   	
  //   	

  // }

}


