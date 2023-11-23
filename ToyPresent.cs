using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 public class ToyPresent: IPresent
    {
        public void Open()
        {
            Console.SetCursorPosition(0, 3);
            Console.Write("Спасибо, я давно хотел эту игрушку ! ");
            Console.SetCursorPosition(0, 0);
        }
        public void Gnaw()
        {
            Console.SetCursorPosition(0, 3);
            Console.Write("Ням, игрушка съедена!                ");
        Console.SetCursorPosition(0, 0);
        }
        public void Smash()
        {
            Console.SetCursorPosition(0, 3);
            Console.Write("Ой, я случайно сломал игрушку :(     ");
            Console.SetCursorPosition(0, 0);
        }
    }
