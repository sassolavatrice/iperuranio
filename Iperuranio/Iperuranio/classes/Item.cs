namespace Gioco;

public class Item 
{
  public string Name {get;set;}
  public string Description {get;set;}
  public string Tip {get;set;}
  public int? Weigth {get;set;}
  public Anagram puzzle {get;set;}

  public Item(){}
  
  public Item(string name, string description, string tip, int weigth = Int32.MaxValue)
  {
    Name = name;
    Description = description;
	Weigth = weigth;
	Tip = tip;
	puzzle = new Anagram(Name);
  }

  public override string ToString()
  {
		  if(puzzle == null)
		  {
            return $"> {Name} - [{Description}]";
		  }
		  else if(puzzle.solved)
		  {
            return $"> {Name} - [{Tip}]";
		  }
		  else
		  {
			string display = String.Concat(puzzle.Grid); 
			return $"> {display} - [{Description}]";
		  }
  }
}
