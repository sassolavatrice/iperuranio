using Spectre.Console;

namespace Gioco
{
  [Serializable]
		public class Anagram
		{
		  static public int anagramCount {get;set;}
		  public string Solution {get;set;}
		  public char[] Grid {get;set;}
		  int x=0;
		  char currentchar=' ';
		  public bool solved = false;

		  public Anagram(string solution)
		  {
			anagramCount++;
				Solution = solution;
				Grid = new char[Solution.Length];
				ShuffleLetters(Solution);
		  }

		  private void ShuffleLetters(string word)
		  {
			Random r = new Random();
			char[] letters = word.ToCharArray();
			r.Shuffle(letters);
			  for(int i=0;i<Solution.Length;i++)
				{
					Grid[i] = letters[i];
				}
		  }
		  
		  public void SolveAnagram()
		  {
			  while(!solved) 
			  {	
				solved = true;
				  Console.Clear();
				  Console.Write("\n");
				  Console.WriteLine("x:"+x+" y:"+" char:"+currentchar+"\n");
					  for(int i=0;i<Solution.Length;i++)
					  { 
						if(x==i)
						{
						 
						  AnsiConsole.Markup($"[blue][[{Grid[i]}]][/]");
						}
						else
						{
						  
						  Console.Write($"[{Grid[i]}]");

						}
						
					  }
				  ConsoleKeyInfo c=Console.ReadKey(true);
				  switch(c.Key)
					{
					  case ConsoleKey.A:
					  {
						  if(x>0)
						  {x--;
								  break;}
						  if(x==0)x=Solution.Length-1;
						  break;
					  }
					  case ConsoleKey.D:
					  {
						  if(x==Solution.Length-1)
						  {x=0;
								break;
						  }
						  if(x<Solution.Length)
						  {x++;
								  break;}
						  break;
						  
					  }
					  case ConsoleKey.Enter:
					  {
						  if(currentchar==' ')
						  {
							  currentchar = Grid[x];
							  Grid[x]=' ';
						  }
						  else
						  {
							  if( Grid[x]==' ')
							  {
								  Grid[x]= currentchar;
								  currentchar=' ';
							  }
							  else
							  {
								  char tem= Grid[x];
								  Grid[x]=currentchar;
								  currentchar=tem;
							  }
						  }
						  break;
					  }
					  case ConsoleKey.S:
					  {
						ShuffleLetters(Solution);
						break;
					  }
					  case ConsoleKey.Escape:
					  {
						Console.WriteLine();
						return;
					  }
					  default:
					  {
					  break;
					  }
					}
					  for(int i=0; i<Solution.Length;++i)
					  {
						if(Grid[i] != Solution[i]) 
						{
								solved = false;
						}
					  }
				  }
				  AnsiConsole.Status()
				  .Spinner(Spinner.Known.FistBump)
				  .Start("RISOLTO",ctx =>
				  {
					Task.Delay(2000).Wait();
				  });
						Console.WriteLine("Complimenti hai risolto l'anagramma!");
						Console.WriteLine("\nPremi qualsiasi tasto per uscire dalla schermata");
						Console.ReadKey();
						Console.Clear();
			  }
  }
}
