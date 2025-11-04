using Lab2.Stack.Array;
using System;

namespace Lab2.Stack.Tests;

public class Array
{
    public static void RunTest()
    {
        Console.WriteLine("=== ТЕСТИРОВАНИЕ СТЕКА НА МАССИВЕ ===");
        
        // Тест 1: Создание стека и проверка начального состояния
        Console.WriteLine("\n1. Создание стека:");
        Lab2.Stack.Array.Stack<char> stack = new Lab2.Stack.Array.Stack<char>();
        Console.WriteLine($"Стек пустой: {stack.Empty()}");
        Console.WriteLine($"Стек полный: {stack.Full()}");
        
        // Тест 2: Добавление символов из строки (согласно заданию)
        Console.WriteLine("\n2. Добавление символов из строки:");
        string testString = "HelloWorldProgrammingDataStructures";
        Console.WriteLine($"Исходная строка: {testString}");
        Console.WriteLine("Добавляем символы в стек:");
        
        int addedCount = 0;
        foreach (char c in testString)
        {
            if (!stack.Full())
            {
                stack.Push(c);
                Console.WriteLine($"Добавлен: '{c}'");
                addedCount++;
            }
            else
            {
                Console.WriteLine($"Стек заполнен! Прекращаем добавление на символе '{c}'");
                break;
            }
        }
        Console.WriteLine($"Всего добавлено символов: {addedCount}");
        Console.WriteLine($"Стек полный: {stack.Full()}");
        
        // Тест 3: Проверка метода Top (без удаления)
        Console.WriteLine("\n3. Проверка метода Top:");
        Console.WriteLine($"Верхний элемент (Top): '{stack.Top()}'");
        Console.WriteLine($"После Top стек пустой: {stack.Empty()}");
        
        // Тест 4: Последовательное извлечение символов (согласно заданию)
        Console.WriteLine("\n4. Извлечение символов из стека:");
        Console.WriteLine("Извлекаем символы пока стек не станет пустым:");
        
        int extractedCount = 0;
        while (!stack.Empty())
        {
            char ch = stack.Pop();
            Console.WriteLine($"Извлечен: '{ch}'");
            extractedCount++;
        }
        Console.WriteLine($"Всего извлечено символов: {extractedCount}");
        Console.WriteLine($"Стек пустой после извлечения: {stack.Empty()}");
        
        // Тест 5: Проверка порядка LIFO
        Console.WriteLine("\n5. Проверка порядка LIFO (последний пришел - первый ушел):");
        stack.Push('A');
        stack.Push('B');
        stack.Push('C');
        Console.WriteLine("Добавили: A, B, C (в этом порядке)");
        Console.WriteLine($"Извлекаем: '{stack.Pop()}' (должен быть C)");
        Console.WriteLine($"Извлекаем: '{stack.Pop()}' (должен быть B)");
        Console.WriteLine($"Извлекаем: '{stack.Pop()}' (должен быть A)");
        
        // Тест 6: Проверка метода MakeNull
        Console.WriteLine("\n6. Тестирование метода MakeNull:");
        stack.Push('X');
        stack.Push('Y');
        stack.Push('Z');
        Console.WriteLine($"До MakeNull - стек пустой: {stack.Empty()}");
        
        stack.MakeNull();
        Console.WriteLine($"После MakeNull - стек пустой: {stack.Empty()}");
        
        // Тест 7: Проверка переполнения стека
        Console.WriteLine("\n7. Тестирование переполнения стека:");
        Lab2.Stack.Array.Stack<int> capacityStack = new Lab2.Stack.Array.Stack<int>();
        
        int capacityCount = 0;
        for (int i = 1; i <= 100; i++)
        {
            if (!capacityStack.Full())
            {
                capacityStack.Push(i);
                capacityCount++;
            }
            else
            {
                Console.WriteLine($"Стек заполнен на {i}-м элементе");
                break;
            }
        }
        
        Console.WriteLine($"Максимальная вместимость: 52");
        Console.WriteLine($"Добавлено элементов: {capacityCount}");
        Console.WriteLine($"Стек полный: {capacityStack.Full()}");
        
        // Тест 8: Проверка работы после переполнения
        Console.WriteLine("\n8. Работа после переполнения:");
        Lab2.Stack.Array.Stack<char> testStack = new Lab2.Stack.Array.Stack<char>();
        
        // Заполняем стек до предела
        for (char c = 'A'; c <= 'Z'; c++)
        {
            testStack.Push(c);
        }
        // Пытаемся добавить еще (не должно добавить)
        testStack.Push('+');
        testStack.Push('-');
        
        Console.WriteLine("Извлекаем элементы из заполненного стека:");
        while (!testStack.Empty())
        {
            Console.Write($"{testStack.Pop()} ");
        }
        Console.WriteLine();
        
        Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО ===");
    }
}