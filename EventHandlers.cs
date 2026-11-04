using System;

// Клас, у якому знаходиться обробник події.
public class EventHandlers
{
    public void OnTrafficExceeded(object sender, TrafficExceededEventArgs e)
    {
        Console.WriteLine("\nУВАГА! Перевищено ліміт інтернет-трафіку!");
        Console.WriteLine($"Використано: {e.UsedTraffic} ГБ. Ліміт: {e.TrafficLimit} ГБ.");

        if (sender is InternetService)
            Console.WriteLine("Повідомлення отримано від Internet-послуги.");
    }
}
