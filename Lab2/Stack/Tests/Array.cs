using Lab2.Stack.Array;
using System;
using System.Text;

namespace Lab2.Stack.Tests;

public class Array
{
    public static void RunTest()
    {
        Console.WriteLine("=== ТЕСТИРОВАНИЕ СТЕКА НА МАССИВЕ ===");
        
        // Тест 1: Создание стека и проверка начального состояния
        Console.WriteLine("\n1. Создание стека:");
        Lab2.Stack.Array.Stack stack = new Lab2.Stack.Array.Stack();
        // Console.WriteLine($"Стек пустой: {stack.Empty()}");
        // Console.WriteLine($"Стек полный: {stack.Full()}");
        
        // Тест 2: Добавление символов из строки (согласно заданию)
        Console.WriteLine("\n2. Добавление символов из строки:");
        string testString = "HelloWorldProgrammingDataStructuresHelloWorldProgrammingDataStructures";
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
        StringBuilder stringBuilder = new StringBuilder();
        while (!stack.Empty())
        {
            char ch = stack.Pop();
            // Console.WriteLine($"Извлечен: '{ch}'");
            stringBuilder.Append(ch);
            extractedCount++;
        }

        System.Console.WriteLine(stringBuilder.ToString());


        Console.WriteLine($"Всего извлечено символов: {extractedCount}");
        Console.WriteLine($"Стек пустой после извлечения: {stack.Empty()}");
        
    
        Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО ===");
    }
}