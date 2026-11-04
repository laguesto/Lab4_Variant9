using System;

// Модель послуги доступу до Internet.
public class InternetService
{
    private string _tariffName;
    private decimal _monthlyPrice;
    private double _trafficLimit;
    private double _usedTraffic;
    private decimal _balance;
    private bool _isConnected;
    private bool _trafficWarningSent;

    // Подія виникає, коли використаний трафік перевищує ліміт тарифу.
    public event EventHandler<TrafficExceededEventArgs> TrafficExceeded;

    public void SelectTariff(string name, decimal price, double trafficLimit)
    {
        _tariffName = name;
        _monthlyPrice = price;
        _trafficLimit = trafficLimit;
        _usedTraffic = 0;
        _isConnected = false;
        _trafficWarningSent = false;

        Console.WriteLine($"Обрано тариф: {_tariffName} ({_trafficLimit} ГБ, {_monthlyPrice} грн/місяць).");
    }

    public void TopUp(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Сума поповнення має бути більшою за нуль.");
            return;
        }

        _balance += amount;
        Console.WriteLine($"Рахунок поповнено. Баланс: {_balance} грн.");
    }

    public void Connect()
    {
        if (string.IsNullOrEmpty(_tariffName))
        {
            Console.WriteLine("Спочатку оберіть тариф.");
            return;
        }

        if (_isConnected)
        {
            Console.WriteLine("Доступ до Internet уже активний.");
            return;
        }

        if (_balance < _monthlyPrice)
        {
            Console.WriteLine("Недостатньо коштів для підключення тарифу. Поповніть рахунок.");
            return;
        }

        _balance -= _monthlyPrice;
        _isConnected = true;
        Console.WriteLine($"Доступ до Internet активовано. З рахунку списано {_monthlyPrice} грн.");
    }

    public void UseInternet(double trafficGb)
    {
        if (!_isConnected)
        {
            Console.WriteLine("Спочатку потрібно активувати тариф.");
            return;
        }

        if (trafficGb <= 0)
        {
            Console.WriteLine("Обсяг трафіку має бути більшим за нуль.");
            return;
        }

        _usedTraffic += trafficGb;
        Console.WriteLine($"Використано {trafficGb} ГБ. Усього: {_usedTraffic} ГБ з {_trafficLimit} ГБ.");

        if (_usedTraffic > _trafficLimit && !_trafficWarningSent)
        {
            _trafficWarningSent = true;
            TrafficExceeded?.Invoke(this, new TrafficExceededEventArgs(_usedTraffic, _trafficLimit));
        }
    }

    public void ShowStatus()
    {
        Console.WriteLine("\n--- Стан Internet-послуги ---");
        Console.WriteLine($"Тариф: {(string.IsNullOrEmpty(_tariffName) ? "не обрано" : _tariffName)}");
        Console.WriteLine($"Баланс: {_balance} грн");
        Console.WriteLine($"Доступ: {(_isConnected ? "активний" : "неактивний")}");
        Console.WriteLine($"Використано трафіку: {_usedTraffic} ГБ");
        Console.WriteLine($"Ліміт трафіку: {_trafficLimit} ГБ");
    }
}
