namespace Gioco;

class Debris: Item
{
 public Debris(char debris)
 {
   Name = debris.ToString();
   Weigth = 0;
   Description = "Un tassello scheggiato, è visible solo una piccola parte della lettera originale"; 
 }
  public override string ToString()
    {
        return $"|{Name}|\t-\t{Description}";
    }
}
