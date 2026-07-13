namespace Gioco;

static public class Helper
{
  static public List<string> allCommands = new List<string>{"vai","inventario","raccogli"};
  static public List<string> availableCommands = new List<string>();
  static bool visible = true;
  static public void Display()
  {
	using (new Interface.CursorScope())
	{
	  if (visible) Interface.edgeWindow(Helper.allCommands, 1);
	}
  }
  static public void Switch()
  {
    visible = !visible;
	//Console.Clear();
  }
}
