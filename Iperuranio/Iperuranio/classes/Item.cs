namespace Gioco;

public class Item 
{
  public string Name {get;set;}
  public string Description {get;set;}
  public string Tip {get;set;}
  public int? Weigth {get;set;}
  public Anagram anagram {get;set;}

  public Item(){}
  
  public Item(string name, string description, int weigth = Int32.MaxValue)
  {
    Name = name;
    Description = description;
	Weigth = weigth;
	anagram = new Anagram(Name);
	Tip = "Messaggio dopo aver risolto";
  }

  public override string ToString()
  {
		  if(anagram == null)
		  {
            return $"> {Name} - [{Description}]";
		  }
		  else if(anagram.solved)
		  {
            return $"> {Name} - [{Tip}]";
		  }
		  else
		  {
			string display = new string(anagram.Grid); 
			return $"> {display} - [{Description}]";
		  }
  }
}
