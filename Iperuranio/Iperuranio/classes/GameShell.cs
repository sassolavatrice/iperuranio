namespace Gioco;

static class GameShell
{
  public static void getCommand(GameState gameState)
  {
    Console.WriteLine("Inserisci un comando");
   string[] arguments = null;
   do{ 
    arguments = Console.ReadLine().Trim().Split();
   }while (arguments == null);
    switch (arguments[0])
    {
     case "vai":
        switch (arguments[1])
        {
          case "nord":
           gameState.MoveTo("nord");
            break;
          case "sud":
            gameState.MoveTo("sud");
            break;
          case "est":
            gameState.MoveTo("est");
            break;
          case "ovest":
            gameState.MoveTo("ovest");
            break;
          default:
            Console.WriteLine("Non c'è questa direzione");
            break;
        }
        break;
     case "inventario":
		gameState.mainCharacter.InteractInventory();
        break;
     case "raccogli":
        gameState.PickUp(arguments[1]);
        break;
      case "teletrasporto":
      Console.WriteLine("dove andiumo di bello?");
      string x= Console.ReadLine();
      gameState.Teletrasporto(x);
      break;
	 case "molla":
        gameState.putDown(arguments[1]);
		break;
	 case "focalizza":
		gameState.mainCharacter.Risolvi(arguments[1]);
        break;
     case "libro":
		if(gameState.book != null)
		Console.WriteLine(gameState.book.ToString());
		break;
	 case "menu":
		Console.Clear();
		Program.endGame = true;
		GameEngine.SaveGame();
		break;
     case "aiuto":
		Helper.Switch();
        break;
     case "esci":
		Program.endGame = true;
		Program.endApp = true;
		return;
     default:
        Console.WriteLine("Comando non riconosciuto");
        break;
     }
     
    }
    
}
