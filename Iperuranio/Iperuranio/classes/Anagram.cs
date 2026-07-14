using Spectre.Console;

namespace Gioco
{
		public class Anagram
		{
		  public string Solution {get;set;}
		  public char[] Grid {get;set;}
		  int x=0;
		  char currentchar=' ';
		  public bool solved = false;

		  public Anagram(string solution)
		  {
				Solution = solution;
				Grid = new char[solution.Length];
				char[] shuffledWord = ShuffleLetters(Solution);
			  for(int i=0;i<solution.Length;i++)
				{
					Grid[i] = shuffledWord[i];
				}
		  }

		  private char[] ShuffleLetters(string word)
		  {
				  Random r = new Random();
				char[] shuffle = new char[word.Length];
				  for(int i= 0; i<word.Length; i++)
				  {
						shuffle[i] = Solution[i];
				  }
				r.Shuffle(shuffle);
				return shuffle;
		  }

		  public void SolveAnagram()
		  {
				  bool notEqual = true;
			  while(notEqual) //mettere verde i riquadri quando è corretto
			  {	
					  notEqual = false;
				  Console.Clear();
					  for(int i=0; i<Solution.Length;++i)
					  {
						if(Grid[i] != Solution[i]) 
						{
								notEqual = true;
						}
					  }
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
				  }
				  solved = true;
				  AnsiConsole.Status()
				  .Spinner(Spinner.Known.FistBump)
				  .Start("RISOLTO",ctx =>
				  {
					Task.Delay(2000).Wait();
				  });
						Console.WriteLine("Complimenti hai risolto l'anagramma!");
						Console.WriteLine("Premi Enter per uscire");
						while(Console.ReadKey().Key != ConsoleKey.Enter)
						{
						Console.WriteLine("Enter zio, enter devi premere");
						}
						Console.Clear();
			  }
				//public override string ToString()
				//{
				//		string anagramma = "";
				//		foreach(char letter in Grid)
				//		{
				//				anagramma += letter;
				//		}
				//		anagramma += $"  [{Description}]";
				//		return anagramma;
				//}
		  }
}
