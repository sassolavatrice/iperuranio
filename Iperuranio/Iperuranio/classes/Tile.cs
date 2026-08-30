namespace Gioco;

[Serializable]
class Tile : Item
{
  public Tile(char letter)
  {
    Name = letter.ToString();
    Weigth = 1;
    Description = "Un tassello con suscritto una lettera, chissà a cosa serve";
    puzzle = null;
  }
  public override string ToString()
  {
    return $"[{Name}]\t-\t{Description}";
  }
}
