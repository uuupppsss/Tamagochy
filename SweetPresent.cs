using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SweetPresent : IPresent
{
    public void Open()
    {
        Console.SetCursorPosition(0, 3);
        Console.Write("Спасибо, это моя любимая конфета ! ");
        Console.SetCursorPosition(0, 0);
    }
    public void Gnaw()
    {
        Console.SetCursorPosition(0, 3);
        Console.Write("Ням, конфета съедена!              ");
        Console.SetCursorPosition(0, 0);
    }
    public void Smash()
    {
        Console.SetCursorPosition(0, 3);
        Console.Write("Ой, я случайно сломал конфету :(   ");
        Console.SetCursorPosition(0, 0);
    }
}
