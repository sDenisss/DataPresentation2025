using Lab2.Dictionary.LinkedList;
using System;

namespace Lab2.Dictionary.Tests;

public class LinkedList
{
    public static void RunTest()
    {
        Console.WriteLine("=== ТЕСТИРОВАНИЕ СЛОВАРЯ ===");
        
        // Создаем словарь
        Lab2.Dictionary.LinkedList.Dictionary<string, string> dictionary = new Lab2.Dictionary.LinkedList.Dictionary<string, string>();
        
        // Тест 1: Добавляем три значения с различными ключами
        Console.WriteLine("\n1. Добавляем три элемента:");
        dictionary.Assign("Савин Денис", "Санкт-Петербург, пер. Вяземский 5-7");
        dictionary.Assign("Анна Ковалева", "Москва, ул. Пушкина 15");
        dictionary.Assign("Андрей Миронов", "Санкт-Петербург, пр. Кронверкский 49");
        
        // Выводим отображение на экран
        Console.WriteLine("Содержимое словаря:");
        dictionary.Print();
        
        // Тест 2: Задаем новое значение по существующему ключу
        Console.WriteLine("\n2. Обновляем значение для 'Анна Ковалева':");
        dictionary.Assign("Анна Ковалева", "Москва, ул. Ленина 20");
        
        // Вновь выводим отображение на экран
        Console.WriteLine("Содержимое после обновления:");
        dictionary.Print();
        
        // Тест 3: Запрашиваем значение по существующему ключу
        Console.WriteLine("\n3. Поиск существующего ключа:");
        string address = "";
        bool found = dictionary.Compute("Савин Денис", out address);
        Console.WriteLine($"Ключ 'Савин Денис': найден = {found}, адрес = {address}");
        
        // Тест 4: Запрашиваем значение по несуществующему ключу
        Console.WriteLine("\n4. Поиск несуществующего ключа:");
        found = dictionary.Compute("Неизвестный", out address);
        Console.WriteLine($"Ключ 'Неизвестный': найден = {found}, адрес = {address}");
        
        // Тест 5: Очищаем словарь
        Console.WriteLine("\n5. Очищаем словарь:");
        dictionary.MakeNull();
        Console.WriteLine("Содержимое после очистки:");
        dictionary.Print();
        
        Console.WriteLine("=== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО ===");
    }
}