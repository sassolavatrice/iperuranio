namespace Gioco;
using System.Text;
using System.Security.Cryptography;

public static class LoginTable
{
  public static Dictionary<string,int> Saves {get;set;}

  public static void AddUser(string username)
  {
	if(!Saves.ContainsKey(username))
	{
	  Saves.Add(username,SetUniqueId());
	}else{Console.WriteLine("Utente esiste già!");}
  }

  public static int SetUniqueId()
  {
	int id = -1;
	foreach(string name in Saves.Keys)
	{
	  id++;
	}
	  return id;
  }
  public static int GetUserId(string username)
  {
	return Saves[username];
  }

  public static string Display()
  {
	StringBuilder sb = new StringBuilder();
	foreach(string key in Saves.Keys)
	{
	  sb.Append($"{key} : {Saves[key]}" +'\n');
	}
	return sb.ToString();
  }
}
