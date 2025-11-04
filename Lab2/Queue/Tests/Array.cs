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
        Lab2.Queue.Array.Queue<char> queue = new Lab2.Queue.Array.Queue<char>();
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
        Console.WriteLine("\n3. Извлечение символов из очереди:");
        Console.WriteLine("Извлекаем символы пока очередь не станет пустой:");
        
        int extractedCount = 0;
        while (!queue.Empty())
        {
            char ch = queue.Dequeue();
            Console.WriteLine($"Извлечен: '{ch}'");
            extractedCount++;
        }
        Console.WriteLine($"Всего извлечено символов: {extractedCount}");
        Console.WriteLine($"Очередь пустая после извлечения: {queue.Empty()}");
        
        // Тест 4: Проверка методов Front и Empty
        Console.WriteLine("\n4. Тестирование методов Front и Empty:");
        queue.Enqueue('A');
        queue.Enqueue('B');
        queue.Enqueue('C');
        
        Console.WriteLine($"Очередь пустая: {queue.Empty()}");
        Console.WriteLine($"Первый элемент (Front): '{queue.Front()}'");
        
        // Тест 5: Проверка кольцевого буфера
        Console.WriteLine("\n5. Тестирование кольцевого буфера:");
        Lab2.Queue.Array.Queue<int> smallQueue = new Lab2.Queue.Array.Queue<int>();
        
        // Заполняем частично
        for (int i = 1; i <= 3; i++)
        {
            smallQueue.Enqueue(i);
        }
        Console.WriteLine("Добавили: 1, 2, 3");
        
        // Извлекаем два элемента
        Console.WriteLine($"Извлекли: {smallQueue.Dequeue()}, {smallQueue.Dequeue()}");
        
        // Добавляем еще - должны использовать освободившееся место в начале
        smallQueue.Enqueue(4);
        smallQueue.Enqueue(5);
        Console.WriteLine("Добавили: 4, 5");
        
        Console.WriteLine("Извлекаем оставшиеся элементы:");
        while (!smallQueue.Empty())
        {
            Console.WriteLine($"Извлечен: {smallQueue.Dequeue()}");
        }
        
        // Тест 6: Проверка метода MakeNull
        Console.WriteLine("\n6. Тестирование метода MakeNull:");
        queue.Enqueue('X');
        queue.Enqueue('Y');
        queue.Enqueue('Z');
        Console.WriteLine($"До MakeNull - очередь пустая: {queue.Empty()}");
        
        queue.MakeNull();
        Console.WriteLine($"После MakeNull - очередь пустая: {queue.Empty()}");
        
        // Тест 7: Проверка на переполнение
        Console.WriteLine("\n7. Тестирование переполнения очереди:");
        Lab2.Queue.Array.Queue<char> capacityTestQueue = new Lab2.Queue.Array.Queue<char>();
        
        int capacityCount = 0;
        for (char c = 'A'; c <= 'Z'; c++)
        {
            if (!capacityTestQueue.Full())
            {
                capacityTestQueue.Enqueue(c);
                capacityCount++;
            }
        }
        
        // Пытаемся добавить больше символов
        for (char c = 'a'; c <= 'z'; c++)
        {
            if (!capacityTestQueue.Full())
            {
                capacityTestQueue.Enqueue(c);
                capacityCount++;
            }
            else
            {
                Console.WriteLine($"Очередь заполнена на символе '{c}'");
                break;
            }
        }
        
        Console.WriteLine($"Максимальная вместимость: 52");
        Console.WriteLine($"Добавлено элементов: {capacityCount}");
        Console.WriteLine($"Очередь полная: {capacityTestQueue.Full()}");
        
        Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО ===");
    }
    
    // Дополнительный тест для демонстрации работы с разными типами данных
    public static void RunAdditionalTest()
    {
        Console.WriteLine("\n=== ДОПОЛНИТЕЛЬНЫЙ ТЕСТ С ЧИСЛАМИ ===");
        
        Lab2.Queue.Array.Queue<int> intQueue = new Lab2.Queue.Array.Queue<int>();
        
        // Добавляем числа
        for (int i = 1; i <= 10; i++)
        {
            intQueue.Enqueue(i * 10);
        }
        
        Console.WriteLine("Извлекаем числа из очереди:");
        while (!intQueue.Empty())
        {
            Console.WriteLine($"Извлечено: {intQueue.Dequeue()}");
        }
        
        Console.WriteLine("=== ДОПОЛНИТЕЛЬНЫЙ ТЕСТ ЗАВЕРШЕН ===");
    }
}