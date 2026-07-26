namespace Gioco
{
		public class Book
		{
				public Dictionary<Item,bool> Indovinelli {get; set;}

				public void AggiungiIndovinello(Item indovinello) 
				{
						Indovinelli.Add(indovinello,false); 
				}

				public override string ToString()
				{
						string Page = "Neee";
						return Page;
				}
		}
}
