namespace Gioco;

public class NPC : Item
{
  public Dictionary<string,string> Dialogs;
  public Dictionary<string,DialogNode> DialogsThree;
  public NPC(string name)
  {
    Dialogs=new Dictionary<string, string>();
    Name = name;
    Description = "un tipico fabBro";
    Weigth = 100;
    DialogsThree=new Dictionary<string, DialogNode>();
  }
 public override string ToString()
  {
      return $"{Name}\t-\t{Description}";
  }

  public void AddDialogNode(string DialogName,DialogNode node)
  {
    DialogsThree.Add(DialogName,node);
  }
}
