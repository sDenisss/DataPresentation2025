using Lab2.Queue.ATDList;
using System;
using System.Text;

namespace Lab2.Queue.Tests;

public class ATDList
{
    public static void RunTest()
    {
        Console.WriteLine("=== ТЕСТИРОВАНИЕ ОЧЕРЕДИ НА АТД СПИСКЕ ===");

        // Тест 1: Создание очереди и проверка начального состояния
        Console.WriteLine("\n1. Создание очереди:");
        Lab2.Queue.Array.Queue queue = new Lab2.Queue.Array.Queue();

        // Тест 2: Добавление символов из строки (согласно заданию)
        Console.WriteLine("\n2. Добавление символов из строки:");
        string testString = "HelloWorldProgrammingDataStructuresHelloWorldProgrammingDataStructures";
        Console.WriteLine($"Исходная строка: {testString}");
        Console.WriteLine("Добавляем символы в очередь:");

        foreach (char c in testString)
        {
            if (!queue.Full())
            {
                queue.Enqueue(c);
                // stringBuilder.Append(queue.Dequeue());
                // Console.WriteLine($"Добавлен: '{c}'");
            }
        }
        // System.Console.WriteLine(stringBuilder.ToString());

        int extractedCount = 0;
        StringBuilder stringBuilder = new StringBuilder();
        while (!queue.Empty())
        {
            char ch = queue.Dequeue();
            // Console.WriteLine($"Извлечен: '{ch}'");
            stringBuilder.Append(ch);
            extractedCount++;
        }

        Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО ===");
    }
}