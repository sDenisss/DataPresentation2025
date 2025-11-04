using Lab2.Stack.LinkedList;
using System;

namespace Lab2.Stack.Tests;

public class LinkedList
{
    public static void RunTest()
    {
        Console.WriteLine("=== ТЕСТИРОВАНИЕ СТЕКА НА СВЯЗНОМ СПИСКЕ ===");

        // Тест 1: Создание стека и проверка начального состояния
        Console.WriteLine("\n1. Создание стека:");
        Lab2.Stack.LinkedList.Stack<char> stack = new Lab2.Stack.LinkedList.Stack<char>();
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
        }
        Console.WriteLine($"Всего добавлено символов: {addedCount}");

        // Тест 3: Проверка метода Top (без удаления)
        Console.WriteLine("\n3. Проверка метода Top:");
        char topElement = stack.Top();
        Console.WriteLine($"Верхний элемент (Top): '{topElement}'");
        Console.WriteLine($"После Top стек пустой: {stack.Empty()}");

        // Проверяем, что Top не удаляет элемент
        char sameTopElement = stack.Top();
        Console.WriteLine($"Повторный вызов Top(): '{sameTopElement}'");

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
        Console.WriteLine($"Извлекаем: '{stack.Pop()}' (должен быть C - последний добавленный)");
        Console.WriteLine($"Извлекаем: '{stack.Pop()}' (должен быть B)");
        Console.WriteLine($"Извлекаем: '{stack.Pop()}' (должен быть A - первый добавленный)");

        // Тест 6: Проверка метода MakeNull
        Console.WriteLine("\n6. Тестирование метода MakeNull:");
        stack.Push('X');
        stack.Push('Y');
        stack.Push('Z');
        Console.WriteLine($"До MakeNull - стек пустой: {stack.Empty()}");

        stack.MakeNull();
        Console.WriteLine($"После MakeNull - стек пустой: {stack.Empty()}");

        // Тест 7: Проверка с одним элементом
        Console.WriteLine("\n7. Тестирование с одним элементом:");
        stack.Push('S');
        Console.WriteLine($"Стек с одним элементом пустой: {stack.Empty()}");
        Console.WriteLine($"Top с одним элементом: '{stack.Top()}'");
        Console.WriteLine($"Pop с одним элементом: '{stack.Pop()}'");
        Console.WriteLine($"После извлечения одного элемента - пустой: {stack.Empty()}");

        // Тест 8: Проверка "бесконечной" емкости
        Console.WriteLine("\n8. Тестирование емкости (всегда false):");
        Lab2.Stack.LinkedList.Stack<char> largeStack = new Lab2.Stack.LinkedList.Stack<char>();

        int largeCount = 0;
        // Добавляем много элементов
        for (char c = 'A'; c <= 'Z'; c++)
        {
            largeStack.Push(c);
            largeCount++;
        }
        for (char c = 'a'; c <= 'z'; c++)
        {
            largeStack.Push(c);
            largeCount++;
        }

        Console.WriteLine($"Добавлено элементов: {largeCount}");
        Console.WriteLine($"Стек полный: {largeStack.Full()} (всегда false для связного списка)");
        Console.WriteLine($"Верхний элемент: '{largeStack.Top()}' (должен быть 'z')");

        // Извлекаем несколько элементов чтобы проверить порядок
        Console.WriteLine("Первые 5 извлеченных элементов (должны быть z, y, x, w, v):");
        for (int i = 0; i < 5 && !largeStack.Empty(); i++)
        {
            Console.WriteLine($"Извлечен: '{largeStack.Pop()}'");
        }

        Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО ===");
    }
}
