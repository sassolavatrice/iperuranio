using System.Text.Json;

namespace Gioco
{
    static class SaveLoadManager
    {
        public static void SaveGame(GameState savefile,string name)
        {
            string filename="SaveFiles\\"+name+".txt";
            File.WriteAllText(filename,JsonSerializer.Serialize(savefile));
            LogFileManager.Write("saved game file ");
        }

        public static GameState LoadGame(string name)
        {
            string filename="SaveFiles\\"+name+".txt";
            LogFileManager.Write("Load game");
            //if(!File.Exists(filename))return GenerateNewGame();
            return JsonSerializer.Deserialize<GameState>(File.ReadAllText(filename));
        }

        public static Dictionary<string,string> LoadLoginTable()
        {
            LogFileManager.Write("load login table");
            if(!File.Exists("LoginTable.txt")) return new Dictionary<string, string>();
            return JsonSerializer.Deserialize<Dictionary<string,string>>(File.ReadAllText("LoginTable.txt"));
        }
	}
}
