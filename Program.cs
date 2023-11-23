using System;
Tamagocha tamagocha = new Tamagocha { Name = "Бебрик" };
tamagocha.HungryChanged += Tamagocha_HungryChanged;
tamagocha.DirtyChanged += Tamagocha_DirtyChanged;
tamagocha.ThirstyChanged += Tamagocha_ThirstyChanged;
tamagocha.GoForWalk += Tamagocha_GoForWalk;

Console.SetCursorPosition(0, 1);
Console.Write("F-кормить  C-мыть  D-поить  P-подарок  I-информация  Escape-выход W-выгуливать ");
Console.SetCursorPosition(0, 0);


ConsoleKeyInfo command;
do
{
    command = Console.ReadKey();
    if (command.Key == ConsoleKey.F)
        tamagocha.Feed(); //кормить
    else if (command.Key == ConsoleKey.I)
        tamagocha.PrintInfo(); //инфо
    else if (command.Key == ConsoleKey.C)
        tamagocha.Clean(); //мыть
    else if (command.Key == ConsoleKey.D)
        tamagocha.Drink(); //поить!
    else if (command.Key == ConsoleKey.P)
        tamagocha.GivePresent(); // поадрок
    else if (command.Key == ConsoleKey.W)
        tamagocha.Walking(); //гулять
    tamagocha.ChangeHappy(); 
    tamagocha.ChangeHealth();
    tamagocha.Die();
}
while (command.Key != ConsoleKey.Escape);
tamagocha.Stop();

void Tamagocha_HungryChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 6);
    Console.Write($"{tamagocha.Name} голодает! Показатель голода растет: {tamagocha.Hungry}      ");
    Console.SetCursorPosition(0, 0); 
}
void Tamagocha_DirtyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 9);
    Console.Write($"{tamagocha.Name} испачкался! Показатель пыльности растет: {tamagocha.Dirty}  ");
    Console.SetCursorPosition(0, 0);
}
void Tamagocha_ThirstyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 12);
    Console.Write($"{tamagocha.Name} засох! Показатель жажды растет: {tamagocha.Thirsty}         ");
    Console.SetCursorPosition(0, 0);
}
void Tamagocha_GoForWalk(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 15);
    Console.Write($"{tamagocha.Name} хочет гулять! Показатель скуки растет: {tamagocha.Boredom}  ");
    Console.SetCursorPosition(0, 0);
}

class Tamagocha
{
    public string Name { get; set; }
    public int Health { get; set; } = 100;
    public int Happyness { get; set; } = 100;
    private int hungry = 0;
    public int Hungry
    {
        get => hungry;
        set {
            hungry = value;
            HungryChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    private int dirty = 0;
    public int Dirty
    {
        get => dirty;
        set
        {
            dirty = value;
            DirtyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    private int thirsty = 0;
    public int Thirsty
    {
        get => thirsty;
        set
        {
            thirsty = value;
            ThirstyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    private int boredom = 0;
    public int Boredom
    {
        get => boredom;
        set
        {
            boredom = value;
            GoForWalk?.Invoke(this, EventArgs.Empty);
        }
    }
    public bool IsDead { get; set; } = false;

    public event EventHandler HungryChanged;
    public event EventHandler DirtyChanged;
    public event EventHandler ThirstyChanged;
    public event EventHandler GoForWalk;

    public Tamagocha()
    {
        Thread thread = new Thread(LifeCircle);
        thread.Start();
    }
    Random random = new Random();

    private void LifeCircle(object? obj)
    {
        while (!IsDead)
        {
            Thread.Sleep(500);
            int rnd = random.Next(0, 2);
            switch(rnd)
            {
                case 0: JumpMinute(); break;
                case 1: FallSleep(); break;
                case 2: break;
                case 3: break;
                case 4: break;
                case 5: break;
                default: break;
            }

        }
    }

    private void FallSleep()
    {
        WriteMessageToConsole($"{Name} внезапно начинает спать как угорелый. Это продолжается целую минуту. Показатели голода, жажды, чистоты и скуки повышены!  ",18);
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(0, 5);
        Dirty += random.Next(0, 5);
        Boredom += random.Next(5, 10);
    }

    private void JumpMinute()
    {
        WriteMessageToConsole($"{Name} внезапно начинает прыгать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!",18);
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
        Boredom -= random.Next(0, 5);
    }

    public void PrintInfo()
    {
        string alive;
        if (IsDead) alive = "да";
        else alive = "нет";
        WriteMessageToConsole($"{Name}: Здоровье:{Health} Голод:{Hungry} Грязь:{Dirty} Жажда:{Thirsty} Жив:{alive} Счастье:{Happyness} Скука:{Boredom}",2);
    }


    private void WriteMessageToConsole(string message, int i)
    {
        Console.SetCursorPosition(0, i);
        Console.Write(message);
        Console.SetCursorPosition(0, 0); 
    }
    public void Stop()
    {
        IsDead = true;
    }

    internal void Feed() //кормить
    {
        WriteMessageToConsole($"{Name} внезапно начинает ЖРАТЬ как угорелый. Это продолжается целую минуту. Показатели голода повышены!     ",21);

        Hungry -= random.Next(5, 10);
    }

    internal void Clean() //мыть
    {
        WriteMessageToConsole($"{Name} внезапно начинает мыть попу как угорелый. Это продолжается целую минуту. Показатели чистоты повышены!",21);

        Dirty -= random.Next(5, 10);
    }

    internal void Drink() //поить!
    {
        WriteMessageToConsole($"{Name} внезапно начинает пить как угорелый. Это продолжается целую минуту. Показатели жажды повышены!       ",21);

        Thirsty -= random.Next(10, 40);
    }

    internal void Walking() //гулять
    {
        WriteMessageToConsole($"{Name} внезапно бежит гулять как угорелый. Это продолжается целую минуту. Показатели скуки повышены!       ", 21);

        if (Boredom > 0) Boredom -= random.Next(5, 10);
        else Boredom = 0;

    }

    internal void ChangeHappy()
    {

        if (dirty >= 1000 || boredom >= 1000 )
        {
            Happyness -= random.Next(0, 20);
            WriteMessageToConsole($"{Name} внезапно начинает плакать как угорелый. Это продолжается целую минуту. Показатели грусти повышены!", 24);
        }
        else if (dirty <= 1000 && boredom <= 1000 )
        {
            Happyness += random.Next(0, 20);
            if (Happyness > 100) Happyness = 100;
            WriteMessageToConsole($"{Name} счастливый малыш                                                                                  ", 24);
        }
        
    }

    internal void ChangeHealth()
    {

        if (thirsty >= 1000 || hungry >= 1000)
        {
            Health -= random.Next(0, 20);
            WriteMessageToConsole($"{Name} внезапно начинает болеть как угорелый. Это продолжается целую минуту. Показатели болезни повышены!", 27);
        }
        else if (thirsty <= 1000 && hungry <= 1000)
        {
            Health += random.Next(0, 20);
            if (Health>100) Health = 100;
            WriteMessageToConsole($"{Name} здоров как бык!                                                                                    ", 27);
        }
    }
    internal void Die()
    {
        if (Health < 0) 
        {
            IsDead = true;
            WriteMessageToConsole($"{Name} внезапно заболел   и умер :(", 30);
        }
        else if (Happyness < 0)
        {
            IsDead = true;
            WriteMessageToConsole($"{Name} внезапно загрустил и умер :(", 30);
        }

    }
    internal void GivePresent()
    {
        IPresent present;
        int random_present = random.Next(0, 2);
        switch (random_present)
        {
            case 0: present = new ToyPresent(); break;
            case 1: present = new SweetPresent(); break;
            default: present = new ClothesPresent(); break;
        }

        int action = random.Next(0, 3);
        switch (action)
        {
            case 0:present.Open();break;
            case 1:present.Smash();break;
            case 2:present.Gnaw();break;
        }
        Happyness += random.Next(0, 50);
        if(Happyness>100) Happyness = 100;
    }
}
public interface IPresent
{
    public void Open();
    public void Gnaw();
    public void Smash();
}