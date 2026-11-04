using System;

// Делегат для порівняння двох масивів.
public delegate void ArrayComparisonDelegate(int[] first, int[] second);

public class ArrayComparer
{
    public static void CompareValues(int[] first, int[] second)
    {
        int count = Math.Min(first.Length, second.Length);

        Console.WriteLine("\nПорівняння елементів масивів:");

        for (int i = 0; i < count; i++)
        {
            if (first[i] > second[i])
                Console.WriteLine($"[{i}] {first[i]} > {second[i]}");
            else if (first[i] < second[i])
                Console.WriteLine($"[{i}] {first[i]} < {second[i]}");
            else
                Console.WriteLine($"[{i}] {first[i]} = {second[i]}");
        }

        if (first.Length != second.Length)
            Console.WriteLine("Масиви мають різну довжину. Порівняно спільну кількість елементів.");
    }
}
