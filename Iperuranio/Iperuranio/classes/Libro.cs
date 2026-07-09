namespace Gioco
{
		public class Libro : Item
		{
				public Dictionary<Anagram,bool> Indovinelli {get; set;}

				public Libro()
				{
						Indovinelli = new Dictionary<Anagram,bool>();		
				}

				public void AggiungiIndovinello(Anagram anagramma) 
				{
						Indovinelli.Add(anagramma,false); 
				}

				public override string ToString()
				{
						string Page = "";
						foreach(var kvp in Indovinelli)
						{
								if(kvp.Value == true)
								{
										Page += String.Format("Risolto {0} - {1} \n",new string(kvp.Key.ToString()),kvp.Key.Description);
								}
								else
								{
										Page += String.Format("Da Risolvere {0} - {1} \n",new string(kvp.Key.ToString()),kvp.Key.Description);
								}
						}
						return Page;
				}
		}
}
