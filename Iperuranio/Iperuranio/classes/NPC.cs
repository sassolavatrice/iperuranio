namespace Gioco;

public class NPC : Item
{
  static public Room CurrentRoom{get;set;}
 
  public NPC(string name, string description, int weigth = Int32.MaxValue)
  {
    Name = name;
    Description = description;
    Weigth = weigth;
    puzzle = new Anagram(name);
  }
 

  public void RandomTP(List<Room> rooms)
  {
     Random smigolseed = new Random();
    int x = smigolseed.Next(rooms.Count);
    Room y = rooms[x];
    CurrentRoom = y;
  }
}
