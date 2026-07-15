namespace Gioco;

public class Baforb : Item 
{
  public Room CurrentRoom {get;set;} 

  public Baforb(GameState gameState)
  {
    Name = "Baforb";
    Description = "un tipico Fabbro";
	CurrentRoom = gameState.Rooms[0];
    Weigth = 100;
    anagram = null;
  }


}
public class Smeagol : Item
{
  public Room CurrentRoom {get;set;}

  public Smeagol(GameState gameState)
  {
	Name = "Smeagol";
	Description = "Hahahahhahah";
	CurrentRoom = gameState.Rooms.Last();
	Weigth = Int32.MaxValue;
	anagram = null;
  }
}
