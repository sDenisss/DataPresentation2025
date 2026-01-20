using System;
using Lab2.Map.Interfaces;

namespace Lab2.Map.LinkedList
{
    // Реализация Map на связном списке
    public class Map : IMap
    {
        private Node? _head;  // Голова списка

        // Сравнение двух массивов char до '\0'
        private static bool CompareCharArrays(char[] a1, char[] a2)
        {
            int len = Math.Min(a1.Length, a2.Length);  // Минимальная длина
            
            for (int i = 0; i < len; i++)
            {
                // Оба достигли конца строки
                if (a1[i] == '\0' && a2[i] == '\0')
                    return true;
                
                // Символы не совпадают
                if (a1[i] != a2[i])
                    return false;
            }
            
            return true;  // Различий нет
        }

        // Добавление/обновление пары ключ-значение
        public void Assign(char[] name, char[] address)
        {
            // Создание первого узла
            if (_head == null)
            {
                _head = new Node(name, address, null!);
                return;
            }

            // Поиск существующего ключа
            Node? node = FindViaKey(name);

            // Обновление значения
            if (node != null)
            {
                node.Data = new Addressee(name, address);
                return;
            }

            // Добавление нового узла в начало
            _head = new Node(name, address, _head);
        }

        // Получение значения по ключу
        public bool Compute(char[] name, out char[] address)
        {
            address = new char[Addressee.ADDRESS_CAPACITY];  // Массив для результата
            
            Node? node = FindViaKey(name);

            // Ключ не найден
            if (node == null)
            {
                address = Array.Empty<char>();
                return false;
            }

            // Копирование адреса в выходной массив
            char[] nodeAddress = node.Data.GetAddress();
            for (int i = 0; i < nodeAddress.Length && i < address.Length; i++)
            {
                if (nodeAddress[i] == '\0') break;  // Конец строки
                address[i] = nodeAddress[i];        // Копирование символа
            }

            return true;  // Успешное получение
        }

        // Очистка словаря
        public void MakeNull()
        {
            _head = null;  // Удаление ссылки на список
        }

        // Вывод всех элементов
        public void Print()
        {
            Node? current = _head;  // Начало списка
            
            while (current != null)
            {
                current.Data.Print();       // Печать данных узла
                current = current.Next;     // Следующий узел
            }
        }

        // Поиск узла по ключу
        private Node FindViaKey(char[] key)
        {
            Node? current = _head;  // Текущий узел
            while (current != null)
            {
                // Ключи совпадают
                if (CompareCharArrays(current.Data.GetName(), key))
                    return current;

                current = current.Next;   // Переход к следующему
            }

            return null!;  // Узел не найден
        }
    }
}