using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gioco;

[Serializable]
public class Inventory
{
    public Stack<Item> items { get; set; }
    public int Count { get; set; }

    public Inventory()
    {
        items = new Stack<Item>();
        Count = 0;
    }

    public void Push(Item item)
    {
        items.Push(item);
        Count++;
    }
    public Item Pop()
    {
        if (Count > 0)
        {
            Count--;
            return items.Pop();
        }
        else
        {
            Console.WriteLine("L'inventario è vuoto!");
            return null;
        }
    }
    public Item Peek()
    {
        if (Count > 0)
        {
            return items.Peek();
        }
        else
        {
            Console.WriteLine("L'inventario è vuoto!");
            return null;
        }
    }
}
