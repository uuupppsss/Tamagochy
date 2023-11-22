using System;
Tamagocha tamagocha = new Tamagocha { Name = "Бебрик" };
tamagocha.HungryChanged += Tamagocha_HungryChanged;
tamagocha.DirtyChanged += Tamagocha_DirtyChanged;
tamagocha.ThirstyChanged += Tamagocha_ThirstyChanged;

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
}
while (command.Key != ConsoleKey.Escape);
tamagocha.Stop();

void Tamagocha_HungryChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 0);
    Console.Write($"{tamagocha.Name} голодает! Показатель голода растет: {tamagocha.Hungry}");
    Console.SetCursorPosition(0, 15); 
}
void Tamagocha_DirtyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 5);
    Console.Write($"{tamagocha.Name} испачкался! Показатель пыльности растет: {tamagocha.Dirty}");
    Console.SetCursorPosition(0, 15);
}
void Tamagocha_ThirstyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 10);
    Console.Write($"{tamagocha.Name} засох! Показатель жажды растет: {tamagocha.Dirty}");
    Console.SetCursorPosition(0, 15);
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
        WriteMessageToConsole($"{Name} внезапно начинает спать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!",15);
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
        WriteMessageToConsole($"{Name}: Health:{Health} Hungry:{Hungry} Dirty:{Dirty} Thirsty:{Thirsty} IsDead:{IsDead} Happyness:{Happyness}",20);
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
        WriteMessageToConsole($"{Name} внезапно начинает ЖРАТЬ как угорелый. Это продолжается целую минуту. Показатели голода повышены!",25);

        Hungry -= random.Next(5, 10);
    }
    internal void Clean()
    {
        WriteMessageToConsole($"{Name} внезапно начинает мыть попу как угорелый. Это продолжается целую минуту. Показатели чистоты повышены!",25);

        Dirty -= random.Next(5, 10);
    }
    internal void Drink()
    {
        WriteMessageToConsole($"{Name} внезапно начинает пить как угорелый. Это продолжается целую минуту. Показатели жажды повышены!",25);

        Thirsty -= random.Next(5, 10);
    }

    internal void LowHappy()
    {
        WriteMessageToConsole($"{Name} внезапно начинает плакать как угорелый. Это продолжается целую минуту. Показатели грусти повышены!",25);
        if (dirty >=500||thirsty>=500||hungry>=500)
            Happyness -= random.Next(5, 10);
        if (dirty <= 500 || thirsty <= 500 || hungry <= 500)
        {
            while(Happyness<=100)
                Happyness += random.Next(5, 10);
        }
    }

    internal void LowHealth()
    {
        WriteMessageToConsole($"{Name} внезапно начинает болеть как угорелый. Это продолжается целую минуту. Показатели грусти повышены!",25);
        if (thirsty >= 500 || hungry >= 500)
            Health -= random.Next(5, 10);
        if (thirsty <= 500 || hungry <= 500)
        {
            while (Happyness <= 100)
                Health += random.Next(5, 10);
        }
    }
}