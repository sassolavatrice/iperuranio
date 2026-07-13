namespace Gioco;

public static class Interface
{
  public readonly struct CursorScope : IDisposable
  {
	private readonly int _left, _top;

	public CursorScope()
	{
		(_left, _top) = Console.GetCursorPosition();
	}

	public void Dispose() => Console.SetCursorPosition(_left, _top);
  }

//  static public bool enoughResolution()
//  {
//	if(height < 20 || width < 40)
//	{
//      return false;
//	}
//	else
//	{
//      return true;
//	}
//}
  
  static public void edgeWindow(List<string> list,int mode)
  {

	int i=0;
	int height = 0;
	switch (mode)
    {
      case 1:
			  foreach(string str in list) if(str.Length>i)i=str.Length;
    
			  Console.SetCursorPosition(Console.WindowWidth-i-8,height);
			  height++;
			  Console.Write("╔");
			  for(int j=0;j<i+2;j++) Console.Write("═");
			  Console.Write("╗\n"); 
    
			  foreach(string str in list)
			  {
				Console.SetCursorPosition(Console.WindowWidth-i-8,height);
				height++;
				Console.Write("║ "+str);
				for(int j=0;j<i-str.Length;j++) Console.Write(" ");
				Console.Write(" ║\n");
			  }
			  Console.SetCursorPosition(Console.WindowWidth-i-8,height);
			  height++;
			  Console.Write("╚");
			  for(int j=0;j<i+2;j++) Console.Write("═");
			  Console.Write("╝\n"); 
			  break;

//	case 2:
//			foreach(string str in list)
//			if(str.Length>i)i=str.Length;
//			
//			Console.Write("┌");
//			for(int j=0;j<i+2;j++)
//			Console.Write("─");
//			Console.Write("┐\n"); 
//			foreach(string str in list)
//			{
//			  Console.Write("│ "+str);
//			  for(int j=0;j<i-str.Length;j++)
//		      Console.Write(" ");
//			  Console.Write(" │\n");
//			}
//			Console.Write("└");
//			for(int j=0;j<i+2;j++)Console.Write("─");
//			Console.Write("┘\n");  
//			break;
//
	default:
			Console.WriteLine("modalità non riconosciuta");
			break;
    }
  }
}
