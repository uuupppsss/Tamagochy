using System;
Tamagocha tamagocha = new Tamagocha { Name = "Бебрик" };
tamagocha.HungryChanged += Tamagocha_HungryChanged;
tamagocha.DirtyChanged += Tamagocha_DirtyChanged;
tamagocha.ThirstyChanged += Tamagocha_ThirstyChanged;

Console.SetCursorPosition(0, 0);
Console.Write("F-кормить  C-мыть  D-поить  P-подарок  I-информация  Escape-выход ");
Console.SetCursorPosition(0, 0);


ConsoleKeyInfo command;
do
{
    command = Console.ReadKey();
    if (command.Key == ConsoleKey.F)
        tamagocha.Feed();
    else if (command.Key == ConsoleKey.I)
        tamagocha.PrintInfo();
    else if (command.Key == ConsoleKey.C)
        tamagocha.Clean();
    else if (command.Key == ConsoleKey.D)
        tamagocha.Drink();
    else if(command.Key == ConsoleKey.P)
        tamagocha.GivePresent();
    tamagocha.ChangeHappy();
    tamagocha.ChangeHealth();
    tamagocha.Die();
}
while (command.Key != ConsoleKey.Escape);
tamagocha.Stop();

void Tamagocha_HungryChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 6);
    Console.Write($"{tamagocha.Name} голодает! Показатель голода растет: {tamagocha.Hungry}");
    Console.SetCursorPosition(0, 0); 
}
void Tamagocha_DirtyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 9);
    Console.Write($"{tamagocha.Name} испачкался! Показатель пыльности растет: {tamagocha.Dirty}");
    Console.SetCursorPosition(0, 0);
}
void Tamagocha_ThirstyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 12);
    Console.Write($"{tamagocha.Name} засох! Показатель жажды растет: {tamagocha.Dirty}");
    Console.SetCursorPosition(0, 0);
}

class Tamagocha
{
    public string Name { get; set; }
    public int Health { get; set; } = 100;
    public int Happyness { get; set; } = 100;
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
    public bool IsDead { get; set; } = false;

    public event EventHandler HungryChanged;
    public event EventHandler DirtyChanged;
    public event EventHandler ThirstyChanged;

    public Tamagocha()
    {
        Thread thread = new Thread(LifeCircle);
        thread.Start();
    }
    Random random = new Random();

    private int hungry = 0;

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
        WriteMessageToConsole($"{Name} внезапно начинает спать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!  ",15);
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void JumpMinute()
    {
        WriteMessageToConsole($"{Name} внезапно начинает прыгать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!",15);
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    public void PrintInfo()
    {
        WriteMessageToConsole($"{Name}: Health:{Health} Hungry:{Hungry} Dirty:{Dirty} Thirsty:{Thirsty} IsDead:{IsDead} Happyness:{Happyness}",18);
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

    internal void Feed()
    {
        WriteMessageToConsole($"{Name} внезапно начинает ЖРАТЬ как угорелый. Это продолжается целую минуту. Показатели голода повышены!     ",21);

        Hungry -= random.Next(5, 10);
    }
    internal void Clean()
    {
        WriteMessageToConsole($"{Name} внезапно начинает мыть попу как угорелый. Это продолжается целую минуту. Показатели чистоты повышены!",21);

        Dirty -= random.Next(5, 10);
    }
    internal void Drink()
    {
        WriteMessageToConsole($"{Name} внезапно начинает пить как угорелый. Это продолжается целую минуту. Показатели жажды повышены!       ",21);

        Thirsty -= random.Next(5, 10);
    }

    internal void ChangeHappy()
    {

        if (dirty >= 1000 || thirsty >= 1000 || hungry >= 1000)
        {
            Happyness -= random.Next(20, 50);
            WriteMessageToConsole($"{Name} внезапно начинает плакать как угорелый. Это продолжается целую минуту. Показатели грусти повышены!", 24);
        }
        else if (dirty <= 1000 && thirsty <= 1000 && hungry <= 1000)
        {
            Happyness += random.Next(20, 50);
            if (Happyness > 100) Happyness = 100;
            WriteMessageToConsole($"{Name} счастливый малыш                                                                                      ", 24);
        }
        
    }

    internal void ChangeHealth()
    {

        if (thirsty >= 1000 || hungry >= 1000)
        {
            Health -= random.Next(20, 50);
            WriteMessageToConsole($"{Name} внезапно начинает болеть как угорелый. Это продолжается целую минуту. Показатели болезни повышены!", 27);
        }
        else if (thirsty <= 1000 && hungry <= 1000)
        {
            Health += random.Next(20, 50);
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
        Random rndP = new Random();
        int random_present = rndP.Next(0, 2);
        switch (random_present)
        {
            case 0: present = new ToyPresent(); break;
            case 1: present = new SweetPresent(); break;
            default: present = new ClothesPresent(); break;
        }

        Random rndA = new Random();
        int action = rndA.Next(0, 3);
        switch (action)
        {
            case 0:present.Open();break;
            case 1:present.Smash();break;
            case 2:present.Gnaw();break;
        }
    }
}
public interface IPresent
{
    public void Open();
    public void Gnaw();
    public void Smash();
}