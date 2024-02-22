using System;

public class Builder 
{
    public static string checkPassword = "ABCD";
    public static string name;
    public static int counter = 0;
    public static int modified = 0;
    public static string password;
}

public class Publisher
{
    public delegate void EventHandler();
    public event EventHandler Ev;
    
    public void RaiseEvent()
    {
        Ev?.Invoke();
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
            Builder.modified += 1;
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
        Console.WriteLine("Please enter the password:\n");
        Builder.password = Console.ReadLine();
        while (Builder.password != Builder.checkPassword)
        {
            // need to count attempts
            Console.WriteLine("Not correct! Enter a valid one!\n");
            Builder.password = Console.ReadLine();
        }
        publisher.Ev += subscriber.OnEventRaised;
        Console.WriteLine("Press Enter to raise the event...");

        while (true)
        {
            if (Builder.modified > 2)
            {
                Console.WriteLine("reached more than 2 users, STOPPED");
                Environment.Exit(0);
            }
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                publisher.RaiseEvent();
            }
            //Console.WriteLine("reached the end of while loop")
        }
    }
}
