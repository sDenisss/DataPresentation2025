using Lab2.Queue.ATDList;
using System;

namespace Lab2.Queue.Tests;

public class ATDList
{
    public static void RunTest()
    {
        Console.WriteLine("=== ТЕСТИРОВАНИЕ ОЧЕРЕДИ НА АТД СПИСКЕ ===");

        // Тест 1: Создание очереди и проверка начального состояния
        Console.WriteLine("\n1. Создание очереди:");
        Lab2.Queue.Array.Queue<char> queue = new Lab2.Queue.Array.Queue<char>();
        Console.WriteLine($"Очередь пустая: {queue.Empty()}");
        Console.WriteLine($"Очередь полная: {queue.Full()}");

        // Тест 2: Добавление символов из строки (согласно заданию)
        Console.WriteLine("\n2. Добавление символов из строки:");
        string testString = "HelloWorld";
        Console.WriteLine($"Исходная строка: {testString}");
        Console.WriteLine("Добавляем символы в очередь:");

        foreach (char c in testString)
        {
            if (!queue.Full())
            {
                queue.Enqueue(c);
                Console.WriteLine($"Добавлен: '{c}'");
            }
        }

        // Тест 3: Последовательное извлечение символов (согласно заданию)
        Console.WriteLine("\n3. Извлечение символов из очереди:");
        Console.WriteLine("Извлекаем символы пока очередь не станет пустой:");

        while (!queue.Empty())
        {
            char ch = queue.Dequeue();
            Console.WriteLine($"Извлечен: '{ch}'");
        }
        Console.WriteLine($"Очередь пустая после извлечения: {queue.Empty()}");

        // Тест 4: Проверка методов Front
        Console.WriteLine("\n4. Тестирование метода Front:");
        queue.Enqueue('A');
        queue.Enqueue('B');
        queue.Enqueue('C');

        Console.WriteLine($"Первый элемент (Front): '{queue.Front()}'");
        Console.WriteLine($"После Front очередь пустая: {queue.Empty()}");

        // Проверяем, что Front не удаляет элемент
        Console.WriteLine($"Повторный вызов Front(): '{queue.Front()}'");

        // Тест 5: Проверка порядка FIFO
        Console.WriteLine("\n5. Проверка порядка FIFO:");
        Console.WriteLine($"Dequeue: '{queue.Dequeue()}' (должен быть A)");
        Console.WriteLine($"Dequeue: '{queue.Dequeue()}' (должен быть B)");
        Console.WriteLine($"Dequeue: '{queue.Dequeue()}' (должен быть C)");
        Console.WriteLine($"Очередь пустая после извлечения всех: {queue.Empty()}");

        // Тест 6: Проверка метода MakeNull
        Console.WriteLine("\n6. Тестирование метода MakeNull:");
        queue.Enqueue('X');
        queue.Enqueue('Y');
        queue.Enqueue('Z');
        Console.WriteLine($"До MakeNull - очередь пустая: {queue.Empty()}");

        queue.MakeNull();
        Console.WriteLine($"После MakeNull - очередь пустая: {queue.Empty()}");

        // Тест 7: Проверка с одним элементом
        Console.WriteLine("\n7. Тестирование с одним элементом:");
        queue.Enqueue('S');
        Console.WriteLine($"Очередь с одним элементом пустая: {queue.Empty()}");
        Console.WriteLine($"Front с одним элементом: '{queue.Front()}'");
        Console.WriteLine($"Dequeue с одним элементом: '{queue.Dequeue()}'");
        Console.WriteLine($"После извлечения одного элемента - пустая: {queue.Empty()}");

        // Тест 8: Проверка "бесконечной" емкости
        Console.WriteLine("\n8. Тестирование емкости (всегда false):");
        Lab2.Queue.Array.Queue<char> largeQueue = new Lab2.Queue.Array.Queue<char>();

        for (char c = 'A'; c <= 'Z'; c++)
        {
            largeQueue.Enqueue(c);
        }

        Console.WriteLine($"Добавлено 26 элементов, очередь полная: {largeQueue.Full()}");
        Console.WriteLine($"Очередь пустая: {largeQueue.Empty()}");

        Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО ===");
    }
}