using System.Text.Json;

namespace Gioco
{
    static class SaveLoadManager
    {
	  const string LTPath = @"LoginTable.txt";
      const string SavePath= @"save/.";
	  
        public static void SaveGame(GameState gameState,int id)
        {
            File.WriteAllText(SavePath + id.ToString(),JsonSerializer.Serialize(gameState));
        }

        public static GameState LoadGame(int id)
        {
            if(!File.Exists(SavePath + id.ToString()))
			{
			  return GameEngine.GenerateNewGame();
			}
            return JsonSerializer.Deserialize<GameState>(File.ReadAllText(SavePath + id.ToString()));
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
