namespace Gioco;

  public class Map
{
		internal enum Direction
  {
    nord,
    est,
    sud,
    ovest
  };
		internal List<Room> Rooms {get;set;}
		internal Dictionary<Direction,Room> Joints {get;set;}
}

	//	public string MapRepresentation()
	//	{
	//			
	//	}

