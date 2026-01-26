using System;
using Lab2.Map.Interfaces;
using Lab2.Map.LinkedList;
using MapClass = Lab2.Map.LinkedList.Map;

namespace Lab2.Map.Tests
{
    public class LinkedList
    {
        public static void RunTest()
        {
            Console.WriteLine("=== КОРОТКИЕ ТЕСТЫ Map<TKey, TValue> ===");
            
            // Создаем Map с любыми типами (все равно работаем с char[])
            MapClass map = new MapClass();
            
            Test1_BasicAddSearch(map);
            
            Console.WriteLine("\nВсе тесты пройдены!");
        }
        
        private static void Test1_BasicAddSearch(MapClass map)
        {
            Console.WriteLine("\n1. Базовое добавление и поиск:");
            
            char[] key = "Иван".ToCharArray();
            char[] value = "Москва, ул. Ленина 10".ToCharArray();
            
            map.Assign(key, value);
            
            bool found = map.Compute(key, out char[] result);
            
            if (!found)
                throw new Exception("Не найден добавленный ключ");
                
            string resultStr = new string(result).TrimEnd('\0');
            string expected = new string(value).TrimEnd('\0');
            
            if (resultStr != expected)
                throw new Exception($"Ожидалось: '{expected}', получено: '{resultStr}'");
            
            Console.WriteLine($"Добавили: {new string(key).TrimEnd('\0')} -> {expected}");

            char[] newAd = "London".ToCharArray();
            map.Assign(key, newAd);
            map.Compute(key, out char[] result2);
            string resultStr2 = new string(result2).TrimEnd('\0');

            System.Console.WriteLine($"Найден для {new string(key)} адрес: {resultStr}");

            System.Console.WriteLine($"Updated {new string(key)} adress: {resultStr2}");
        }
        
    }
}