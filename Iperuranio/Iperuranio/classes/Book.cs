namespace Gioco
{
		public class Book
		{
				public Dictionary<Item,bool> Indovinelli {get; set;}

				public Book()
				{
						Indovinelli = new Dictionary<Item,bool>();		
				}

				public void AggiungiIndovinello(Item indovinello) 
				{
						Indovinelli.Add(indovinello,false); 
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
