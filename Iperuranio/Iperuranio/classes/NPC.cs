namespace Gioco;

public class NPC : Item
{
  static public Room CurrentRoom{get;set;}
 
  public NPC(string name, string description, int weigth)
  {
    Name = name;
    Description = description;
    Weigth = weigth;
    this.anagram = new Anagram(name);
  }
 

static public void tpsmigol(List<Room> rooms)
  {
     Random smigolseed = new Random();

    int x = smigolseed.Next(rooms.Count);
    Room y = rooms[x];
    CurrentRoom = y;
  }
}