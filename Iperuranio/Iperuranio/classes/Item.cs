namespace Gioco;

public class Item 
{
  public string Name {get;set;}
  public string Description {get;set;}
  public int? Weigth {get;set;}
  public Anagram anagram {get;set;}

  public Item(){}
  
  public Item(string name, string description, int weigth = Int32.MaxValue)
  {
    Name = name;
    Description = description;
	Weigth = weigth;
	anagram = new Anagram(Name);
  }

  public override string ToString()
  {
		  if(anagram != null)
		  {
    string display = new string(anagram.Grid); 
      return $"> {display} - [{Description}]";
		  }
		  else
		  {
      return $"> {Name} - [{Description}]";
		  }
  }
}
