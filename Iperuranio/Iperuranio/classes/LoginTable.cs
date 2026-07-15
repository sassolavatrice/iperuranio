namespace Gioco;
using System.Text;
using System.Security.Cryptography;

public class LoginTable
{
  private Dictionary<string,string> Saves {get;set;}

  public LoginTable()
  {
   Saves = new Dictionary<string,string>();
  }

  public void AddUser(string username)
  {
	Saves.Add(username,GetUniqueId(username));
  }

  private static int GetUniqueId(string input)
  {
	int id = -1;
	foreach(string name in Saves.Keys)
	{
	  if(Saves[name] > id++){}
	  return id;
	}
  }
  public override string ToString()
  {
	StringBuilder sb = new StringBuilder();
	foreach(string key in Saves.Keys)
	{
	  sb.Append($"{key} : {Saves[key]}" +'\n');
	}
	return sb.ToString();
  }
}
