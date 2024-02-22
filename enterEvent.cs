using System;

public class Builder
{
    public static string name;
    public static int counter = 0;
}

public class Publisher
{
    public delegate void EventHandler(); // this is a delegate
    public event EventHandler Ev; // this is an event
    
    public void RaiseEvent()
    {
        Ev?.Invoke(); //if there is a subscriber
    }
}

public class Subscriber
{
    public static string previousName = Builder.name;
    public void OnEventRaised()
    {
        Builder.counter += 1;
        if (Builder.counter <= 5)
            Console.WriteLine($"Welcome {Builder.name}!!\n");
        else
        {
            Console.WriteLine("Stop! You reached the limits!!\n");
            Builder.counter = 0;
            Console.WriteLine("Free trial is Over, Another name:\n");
            Builder.name = Console.ReadLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Publisher publisher = new Publisher();
        Subscriber subscriber = new Subscriber();
        Console.WriteLine("Please enter your name:\n");
        Builder.name = Console.ReadLine();
        publisher.Ev += subscriber.OnEventRaised;
        Console.WriteLine("Press Enter to raise the event...");

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                publisher.RaiseEvent();
            }
        }
    }
}