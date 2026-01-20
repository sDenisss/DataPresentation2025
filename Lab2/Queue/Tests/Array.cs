using Lab2.Queue.Array;
using System;

namespace Lab2.Queue.Tests;

public class Array
{
    public static void RunTest()
    {
        Console.WriteLine("=== ТЕСТИРОВАНИЕ ОЧЕРЕДИ НА МАССИВЕ ===");
        
        // Тест 1: Создание очереди и проверка начального состояния
        Console.WriteLine("\n1. Создание очереди:");
        Lab2.Queue.Array.Queue queue = new Lab2.Queue.Array.Queue();
        Console.WriteLine($"Очередь пустая: {queue.Empty()}");
        Console.WriteLine($"Очередь полная: {queue.Full()}");
        
        // Тест 2: Добавление символов из строки (согласно заданию)
        Console.WriteLine("\n2. Добавление символов из строки:");
        string testString = "HelloWorldProgramming";
        Console.WriteLine($"Исходная строка: {testString}");
        Console.WriteLine("Добавляем символы в очередь:");
        
        int addedCount = 0;
        foreach (char c in testString)
        {
            if (!queue.Full())
            {
                queue.Enqueue(c);
                Console.WriteLine($"Добавлен: '{c}'");
                addedCount++;
            }
            else
            {
                Console.WriteLine($"Очередь заполнена! Прекращаем добавление на символе '{c}'");
                break;
            }
        }
        Console.WriteLine($"Всего добавлено символов: {addedCount}");
        
        // Тест 3: Последовательное извлечение символов (согласно заданию)
        // Console.WriteLine("\n3. Извлечение символов из очереди:");
        // Console.WriteLine("Извлекаем символы пока очередь не станет пустой:");
        
        // int extractedCount = 0;
        // while (!queue.Empty())
        // {
        //     char ch = queue.Dequeue();
        //     Console.WriteLine($"Извлечен: '{ch}'");
        //     extractedCount++;
        // }
        // Console.WriteLine($"Всего извлечено символов: {extractedCount}");
        // Console.WriteLine($"Очередь пустая после извлечения: {queue.Empty()}");
        
        // Тест 4: Проверка методов Front и Empty
        Console.WriteLine("\n4. Тестирование методов Front и Empty:");
        queue.Enqueue('A');
        queue.Enqueue('B');
        queue.Enqueue('C');
        
        Console.WriteLine($"Очередь пустая: {queue.Empty()}");
        Console.WriteLine($"Первый элемент (Front): '{queue.Front()}'");

        
        // Тест 6: Проверка метода MakeNull
        Console.WriteLine("\n6. Тестирование метода MakeNull:");
        queue.Enqueue('X');
        queue.Enqueue('Y');
        queue.Enqueue('Z');
        Console.WriteLine($"До MakeNull - очередь пустая: {queue.Empty()}");
        
        queue.MakeNull();
        Console.WriteLine($"После MakeNull - очередь пустая: {queue.Empty()}");
        
        Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО ===");
    }
}