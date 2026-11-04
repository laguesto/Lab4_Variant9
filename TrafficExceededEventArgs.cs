using System;

// Аргументи події перевищення трафіку.
public class TrafficExceededEventArgs : EventArgs
{
    public double UsedTraffic { get; }
    public double TrafficLimit { get; }

    public TrafficExceededEventArgs(double usedTraffic, double trafficLimit)
    {
        UsedTraffic = usedTraffic;
        TrafficLimit = trafficLimit;
    }
}
