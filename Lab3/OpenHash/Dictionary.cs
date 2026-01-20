using Lab3.Interfce;

namespace Lab3.OpenHash;

/// <summary>
/// Реализация словаря на основе открытого хеширования (метод цепочек).
/// </summary>
public class Dictionary : IDictionary
{
    private const int Capacity = 69;           // Фиксированный размер хеш-таблицы
    private const int MaxSizeName = 10;       // Максимальная длина имени (символов)
    private Node?[] _array = new Node?[Capacity]; // Основная таблица: массив цепочек (списков)

    /// <summary>
    /// Удаляет элемент из словаря.
    /// </summary>
    public void Delete(char[] x)
    {
        int hash = Hash(x);                   // Вычисляем хеш элемента
        Node? current = _array[hash];         // Начинаем с головы цепочки
        Node? previous = null;                // Предыдущий узел (для перевязки)
        
        while (current != null)               // Проходим по цепочке
        {
            if (IsEquals(current.Value, x))   // Если нашли нужный элемент
            {
                if (previous == null)         // Удаляем из головы цепочки
                    _array[hash] = current.Next;  // Новая голова — следующий узел
                else                          // Удаляем из середины или конца
                    previous.Next = current.Next; // Пропускаем удаляемый узел
                return;                       // Выход после удаления
            }
            previous = current;               // Сохраняем текущий как предыдущий
            current = current.Next;           // Переходим к следующему узлу
        }
        // Элемент не найден — ничего не делаем
    }

    /// <summary>
    /// Вставляет новый элемент в словарь.
    /// </summary>
    public void Insert(char[] x)
    {
        if (FindNode(x) != null)              // Проверяем, нет ли уже такого элемента
            return;                           // Элемент уже существует — вставка не требуется
        
        int hash = Hash(x);                   // Вычисляем хеш элемента
        _array[hash] = new Node(x, _array[hash]); // Вставляем новую голову цепочки
    }

    /// <summary>
    /// Очищает весь словарь (удаляет все элементы).
    /// </summary>
    public void Makenull()
    {
        for (int i = 0; i < Capacity; i++)    // Проходим по всем ячейкам таблицы
        {
            _array[i] = null;                 // Очищаем каждую цепочку
        }
    }

    /// <summary>
    /// Проверяет, содержится ли элемент в словаре.
    /// </summary>
    public bool Member(char[] x)
    {
        return FindNode(x) != null;           // Ищем элемент; если найден — true, иначе false
    }

    /// <summary>
    /// Выводит все элементы словаря на экран.
    /// </summary>
    public void Print()
    {
        for (int i = 0; i < Capacity; i++)    // Проходим по всем ячейкам таблицы
        {
            Node? current = _array[i];        // Берём голову текущей цепочки
            if (current != null)              // Если цепочка не пуста
            {
                Console.Write($"{i}: ");      // Выводим индекс ячейки
                while (current != null)       // Проходим по всей цепочке
                {
                    Console.Write($"{new string(current.Value)}"); // Выводим значение узла
                    if (current.Next != null) // Если есть следующий узел
                        Console.Write(" -> ");  // Разделитель между узлами
                    else                      // Если это последний узел
                        Console.Write(" -> null"); // Конец цепочки
                    current = current.Next;   // Переходим к следующему узлу
                }
                Console.WriteLine();          // Новая строка для следующей цепочки
            }
        }
    }

    /// <summary>
    /// Ищет узел с заданным значением в словаре.
    /// </summary>
    private Node? FindNode(char[] x)
    {
        int hash = Hash(x);                   // Вычисляем хеш элемента
        Node? current = _array[hash];         // Начинаем с головы соответствующей цепочки

        while (current != null)               // Проходим по цепочке
        {
            if (IsEquals(current.Value, x))   // Сравниваем значения
                return current;               // Найден — возвращаем узел
            current = current.Next;           // Переходим к следующему узлу
        }
        
        return null;                          // Не найден — возвращаем null
    }

    // === Вспомогательные (приватные) методы ===

    /// <summary>
    /// Хеш-функция: сумма ASCII-кодов символов строки по модулю Capacity.
    /// </summary>
    private int Hash(char[] name)
    {
        int sum = 0;                          // Аккумулятор суммы кодов
        
        for (int i = 0; i < name.Length && name[i] != '\0'; i++) // Проходим до конца строки или массива
            sum += name[i];                   // Суммируем ASCII-коды символов
        
        return sum % Capacity;                // Возвращаем остаток от деления на размер таблицы
    }
    
    /// <summary>
    /// Сравнивает два массива символов (до MaxSizeName или до конца строки).
    /// </summary>
    private bool IsEquals(char[] name1, char[] name2)
    {
        if (name1 == null && name2 == null)   // Оба null — равны
            return true;
        
        if (name1 == null || name2 == null)   // Один null, другой нет — не равны
            return false;
        
        for (int i = 0; i < MaxSizeName; i++) // Проверяем до максимальной длины имени
        {
            char c1 = (i < name1.Length) ? name1[i] : '\0'; // Берем символ или '\0', если вышли за границу
            char c2 = (i < name2.Length) ? name2[i] : '\0';
            
            if (c1 != c2)                     // Символы не совпали — строки разные
                return false;
            
            if (c1 == '\0' || c2 == '\0')     // Достигнут конец одной из строк
                break;                        // Прерываем сравнение
        }
        
        return true;                          // Все проверенные символы совпали
    }
}