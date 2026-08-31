namespace Gioco;

static class GameShell
{
  public static void getCommand(GameState gameState)
  {
    Console.WriteLine("Inserisci un comando");

    string line = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(line)) return;
    string[] arguments = line.Trim().ToLower().Split();

    // secondo argomento opzionale: evita l'eccezione su "vai" senza direzione
    string arg = arguments.Length > 1 ? arguments[1] : "";

    switch (arguments[0])
    {
      case "vai":
        if (arg == "nord" || arg == "sud" || arg == "est" || arg == "ovest")
          gameState.MoveTo(arg);
        else
          Console.WriteLine("Vai dove? nord, sud, est o ovest.");
        break;

      case "inventario":
        gameState.mainCharacter.showInventory();
        break;

      case "raccogli":
        if (arg == "") Console.WriteLine("Raccogli cosa?");
        else gameState.PickUp(arg);
        break;

      case "molla":
        gameState.putDown(arg);
        break;

      case "focalizza":
        if (arg == "") Console.WriteLine("Focalizza quale anagramma?");
        else gameState.mainCharacter.Risolvi(arg);
        break;

      // --- ciclo hub / dungeon ---------------------------------------
      case "entra":
        Hub.EnterDungeon(gameState);
        break;

      case "torna":
        Hub.ReturnToHub(gameState);
        break;

      case "forgia":
        Hub.Forge(gameState);
        break;

      case "enigma":
        Hub.OpenEnigma(gameState);
        break;

      case "parla":
        Hub.Talk(gameState);
        break;

      case "teletrasporto":
        Console.WriteLine("dove andiumo di bello?");
        string x = Console.ReadLine();
        gameState.Teletrasporto(x);
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
