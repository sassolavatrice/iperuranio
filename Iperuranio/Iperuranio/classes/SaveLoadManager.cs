using System.Text.Json;

namespace Gioco
{
    static class SaveLoadManager
    {
	  const string LTPath = @"LoginTable.txt";
      const string SavePath= @"save/.";
	  
        public static void SaveGame(GameState gameState)
        {
            File.WriteAllText(SavePath + GameEngine.sessionID.ToString(),JsonSerializer.Serialize(gameState));
        }

        public static GameState LoadGame()
        {
            if(!File.Exists(SavePath + GameEngine.sessionID.ToString()))
			{
			  return GameEngine.GenerateNewGame();
			}
            return JsonSerializer.Deserialize<GameState>(File.ReadAllText(SavePath + GameEngine.sessionID.ToString()));
        }

        public static Dictionary<string,int> LoadLoginTable()
        {
            if(!File.Exists(LTPath))
			{
			  return new Dictionary<string, int>();
			}
            return JsonSerializer.Deserialize<Dictionary<string,int>>(File.ReadAllText("LoginTable.txt"));
        }
	}
}
