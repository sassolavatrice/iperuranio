namespace Gioco;

class Tile : Item
{
  char _letter = ' ';
  public Tile(char letter)
  {
    Name = letter.ToString();
    Weigth = 1;
    Description = "Un tassello con suscritto una lettera, chissà a cosa serve"; 
  }

  //static public void spawnTiles()
  //{
  //  return 0;	
  //}

  public override string ToString()
  {
      return $"[{Name}]\t-\t{Description}";
  }
}
