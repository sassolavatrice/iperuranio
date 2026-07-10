namespace Gioco;

public class NPC : Item
{
  public NPC(string name, string description, int weigth = 100)
  {
    Name = name;
    Description = description;
    Weigth = weigth;
    anagram = new Anagram(Name);
  }

  //public void AddDialogNode(string DialogName, DialogNode node)
  //{
  //  DialogsTree.Add(DialogName,node);
  //}
}
