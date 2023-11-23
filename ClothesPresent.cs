using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ClothesPresent : IPresent
{
    public void Open()
    {
        Console.SetCursorPosition(0, 3);
        Console.Write("Спасибо, я давно хотел эту одежду ! ");
        Console.SetCursorPosition(0, 0);
    }
    public void Gnaw()
    {
        Console.SetCursorPosition(0, 3);
        Console.Write("Ням, одежда съедена!                ");
        Console.SetCursorPosition(0, 0);
    }
    public void Smash()
    {
        Console.SetCursorPosition(0, 3);
        Console.Write("Ой, я случайно порвал одежду :(      ");
        Console.SetCursorPosition(0, 0);
    }
}
