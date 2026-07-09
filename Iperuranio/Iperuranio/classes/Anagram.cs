using Spectre.Console;

namespace Gioco
{
		public class Anagram : Item
		{
		  
		  public char[] Grid {get;set;}
		  int x=0;
		  char currentchar=' ';

		  public Anagram(string solution, string description)
		  {
				Description = description;
				Name = solution;
				Weigth = Int32.MaxValue;
				Grid = new char[Name.Length];
				char[] shuffledWord = ShuffleLetters();
			  for(int i=0;i<Name.Length;i++)
				{
					Grid[i] = shuffledWord[i];
				}
		  }

		  private char[] ShuffleLetters()
		  {
				  Random r = new Random();
				char[] shuffle = new char[Name.Length];
				  for(int i= 0; i<Name.Length; i++)
				  {
						shuffle[i] = Name[i];
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
				  Console.WriteLine(Description);
					  for(int i=0; i<Name.Length;++i)
					  {
						if(Grid[i] != Name[i]) 
						{
								notEqual = true;
						}
					  }
				  Console.Write("\n");
				  Console.WriteLine("x:"+x+" y:"+" char:"+currentchar+"\n");
					  for(int i=0;i<Name.Length;i++)
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
				  ConsoleKeyInfo c=Console.ReadKey(); 
				  switch(c.Key)
					{
					  case ConsoleKey.A:
					  {
						  if(x>0)
						  {x--;
								  break;}
						  if(x==0)x=Name.Length-1;
						  break;
					  }
					  case ConsoleKey.D:
					  {
						  if(x==Name.Length-1)
						  {x=0;
								break;
						  }
						  if(x<Name.Length)
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
					  Console.WriteLine("Tasto non riconosciuto");
					  break;
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
						Console.WriteLine("Premi Enter per uscire");
						while(Console.ReadKey().Key != ConsoleKey.Enter)
						{
						Console.WriteLine("Enter zio, enter devi premere");
						}
						Console.Clear();
			  }
				public override string ToString()
				{
						string anagramma = "";
						foreach(char letter in Grid)
						{
								anagramma += letter;
						}
						anagramma += $"  [{Description}]";
						return anagramma;
				}
		  }
}
