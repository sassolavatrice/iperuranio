namespace Gioco;

public class DialogNode
{
    public string Phrase;
    public List<DialogNode> NextNodes;

    public void AddNode(DialogNode node)
    {
        if(NextNodes==null)NextNodes=new List<DialogNode>();
        NextNodes.Add(node);
    }

}
