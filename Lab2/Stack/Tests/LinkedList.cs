using Lab2.Stack.LinkedList;
using System;
using System.Text;

namespace Lab2.Stack.Tests;

public class LinkedList
{
    public static void RunTest()
    {
        Console.WriteLine("=== ТЕСТИРОВАНИЕ СТЕКА НА СВЯЗНОМ СПИСКЕ ===");

        // Тест 1: Создание стека и проверка начального состояния
        Console.WriteLine("\n1. Создание стека:");
        Lab2.Stack.LinkedList.Stack stack = new Lab2.Stack.LinkedList.Stack();
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
                // Console.WriteLine($"Добавлен: '{c}'");
                addedCount++;
            }
        }
        Console.WriteLine($"Всего добавлено символов: {addedCount}");

        
        int extractedCount = 0;
        StringBuilder stringBuilder = new StringBuilder();
        while (!stack.Empty())
        {
            char ch = stack.Pop();
            // Console.WriteLine($"Извлечен: '{ch}'");
            stringBuilder.Append(ch);
            extractedCount++;
        }
        System.Console.WriteLine(stringBuilder.ToString());

        Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО ===");
    }
}
