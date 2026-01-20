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
            Test2_UpdateValue(map);
            Test3_NotFound(map);
            Test4_MakeNull(map);
            Test5_MultipleItems(map);
            
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
        }
        
        private static void Test2_UpdateValue(MapClass map)
        {
            Console.WriteLine("\n2. Обновление значения:");
            
            char[] key = "Иван".ToCharArray();
            char[] newValue = "Санкт-Петербург, Невский пр. 20".ToCharArray();
            
            map.Assign(key, newValue);
            
            map.Compute(key, out char[] result);
            string resultStr = new string(result).TrimEnd('\0');
            string expected = new string(newValue).TrimEnd('\0');
            
            if (resultStr != expected)
                throw new Exception($"Обновление не сработало: '{resultStr}' вместо '{expected}'");
            
            Console.WriteLine($" Обновили: {expected}");
        }
        
        private static void Test3_NotFound(MapClass map)
        {
            Console.WriteLine("\n3. Поиск несуществующего ключа:");
            
            char[] wrongKey = "Неизвестный".ToCharArray();
            bool found = map.Compute(wrongKey, out char[] result);
            
            if (found || result.Length > 0)
                throw new Exception("Несуществующий ключ не должен находиться");
            
            Console.WriteLine("Несуществующий ключ корректно не найден");
        }
        
        private static void Test4_MakeNull(MapClass map)
        {
            Console.WriteLine("\n4. Очистка словаря:");
            
            map.MakeNull();
            
            char[] key = "Иван".ToCharArray();
            bool found = map.Compute(key, out char[] _);
            
            if (found)
                throw new Exception("Данные не очистились после MakeNull");
            
            Console.WriteLine("Словарь успешно очищен");
        }
        
        private static void Test5_MultipleItems(MapClass map)
        {
            Console.WriteLine("\n5. Несколько элементов:");
            
            // Добавляем несколько элементов
            map.Assign("Анна".ToCharArray(), "Москва".ToCharArray());
            map.Assign("Борис".ToCharArray(), "СПб".ToCharArray());
            map.Assign("Сергей".ToCharArray(), "Казань".ToCharArray());
            
            // Проверяем все
            bool allFound = true;
            string[] keys = { "Анна", "Борис", "Сергей" };
            string[] values = { "Москва", "СПб", "Казань" };
            
            for (int i = 0; i < keys.Length; i++)
            {
                if (!map.Compute(keys[i].ToCharArray(), out char[] result))
                {
                    allFound = false;
                    break;
                }
                
                string resultStr = new string(result).TrimEnd('\0');
                if (resultStr != values[i])
                {
                    allFound = false;
                    break;
                }
            }
            
            if (!allFound)
                throw new Exception("Не все элементы сохранились");
            
            Console.WriteLine("Все 3 элемента добавлены и найдены");
            
            // Печатаем содержимое
            Console.WriteLine("\nСодержимое словаря:");
            map.Print();
        }
    }
}