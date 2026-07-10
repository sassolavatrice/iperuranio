namespace Gioco;

class Debris: Item
{
 public Debris(char debris)
 {
   Name = debris.ToString();
   Description = "Un tassello scheggiato, è visible solo una piccola parte della lettera originale"; 
   Weigth = 0;
   anagram = null;
 }
//  public override string ToString()
//    {
//        return $"|{Name}|\t-\t{Description}";
//    }
}
