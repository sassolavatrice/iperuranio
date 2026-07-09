namespace Gioco;

public class NPC : Item
{
  public Dictionary<string,string> Dialogs;
  public Dictionary<string,DialogNode> DialogsThree;
  public NPC(string name, string Desc)
  {
    Dialogs=new Dictionary<string, string>();
    Name = name;
    Description = Desc;
    Weigth = 100;
    DialogsThree=new Dictionary<string, DialogNode>();
  }
 public override string ToString()
  {
      return $"{Name} [{Description}]";
  }

  public void AddDialogNode(string DialogName,DialogNode node)
  {
    DialogsThree.Add(DialogName,node);
  }
}
