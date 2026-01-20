using System.Transactions;
using Lab3.Interfce;

namespace Lab3.CloseHash;

/// <summary>
/// Реализация словаря на основе закрытого хеширования (линейное пробирование).
/// </summary>
public class Dictionary : IDictionary
{
    private const int Capacity = 69;               // Фиксированный размер хеш-таблицы
    private const int MaxSizeName = 10;           // Максимальная длина имени (символов)
    private char[][] _array = new char[Capacity][]; // Основная таблица: массив строк (массивов char)

    /// <summary>
    /// Удаляет элемент из словаря.
    /// </summary>
    public void Delete(char[] x)
    {
        int index = FindIndex(x);                 // Ищем индекс элемента
        
        if (index != -1)                          // Если элемент найден
            _array[index][0] = '\0';              // Помечаем как удалённый (первый символ = '\0')
    }

    /// <summary>
    /// Вставляет новый элемент в словарь.
    /// </summary>
    public void Insert(char[] x)
    {
        if (FindIndex(x) != -1)                   // Проверяем, нет ли уже такого элемента
            return;                               // Элемент уже существует — вставка не требуется
        
        int index = FindIndex(x, forInsert: true); // Ищем свободную ячейку для вставки
        
        if (index == -1)                          // Если свободной ячейки не нашлось
        {
            Console.WriteLine("Ошибка: словарь переполнен!");
            return;
        }
        
        _array[index] = CopyName(x);              // Копируем имя в найденную ячейку
    }

    /// <summary>
    /// Очищает весь словарь (удаляет все элементы).
    /// </summary>
    public void Makenull()
    {
        for (int i = 0; i < _array.Length; i++)   // Проходим по всем ячейкам таблицы
        {
            _array[i] = null!;                    // Очищаем каждую ячейку
        }
    }

    /// <summary>
    /// Проверяет, содержится ли элемент в словаре.
    /// </summary>
    public bool Member(char[] x)
    {
        return FindIndex(x) != -1;                // Ищем элемент; если найден — true, иначе false
    }

    /// <summary>
    /// Выводит все элементы словаря на экран.
    /// </summary>
    public void Print()
    {
        Console.WriteLine("\nPrint:\n");
        
        for (int i = 0; i < Capacity; i++)        // Проходим по всем ячейкам таблицы
        {
            if (_array[i] == null || _array[i][0] == '\0') // Пропускаем пустые и удалённые ячейки
                continue;
            
            Console.WriteLine($"{i} - {new string(_array[i])}"); // Выводим индекс и значение
        }
        
        Console.WriteLine();                      // Пустая строка после вывода
    }

    // === Вспомогательные (приватные) методы ===

    /// <summary>
    /// Хеш-функция: сумма ASCII-кодов символов строки по модулю Capacity.
    /// </summary>
    private int Hash(char[] name)
    {
        int sum = 0;                              // Аккумулятор суммы кодов
        
        for (int i = 0; i < name.Length && name[i] != '\0'; i++) // Проходим до конца строки или массива
            sum += name[i];                       // Суммируем ASCII-коды символов
        
        return sum % Capacity;                    // Возвращаем остаток от деления на размер таблицы
    }

    /// <summary>
    /// Вычисляет следующий индекс при линейном пробировании.
    /// </summary>
    private int HashNext(int hash, int i)
    {
        return (hash + i) % Capacity;             // Сдвигаем на i позиций по модулю Capacity
    }

    /// <summary>
    /// Сравнивает два массива символов (до MaxSizeName или до конца строки).
    /// </summary>
    private bool IsEquals(char[] name1, char[] name2)
    {
        if (name1 == null && name2 == null)       // Оба null — равны
            return true;
        
        if (name1 == null || name2 == null)       // Один null, другой нет — не равны
            return false;
        
        for (int i = 0; i < MaxSizeName; i++)     // Проверяем до максимальной длины имени
        {
            char c1 = (i < name1.Length) ? name1[i] : '\0'; // Берем символ или '\0', если вышли за границу
            char c2 = (i < name2.Length) ? name2[i] : '\0';
            
            if (c1 != c2)                         // Символы не совпали — строки разные
                return false;
            
            if (c1 == '\0' || c2 == '\0')         // Достигнут конец одной из строк
                break;                            // Прерываем сравнение
        }
        
        return true;                              // Все проверенные символы совпали
    }

    /// <summary>
    /// Ищет индекс элемента в таблице.
    /// </summary>
    /// <param name="x">Искомый элемент (массив символов).</param>
    /// <param name="forInsert">Если true — ищет свободную ячейку для вставки.</param>
    /// <returns>Индекс найденного элемента или -1, если не найден.</returns>
    private int FindIndex(char[] x, bool forInsert = false)
    {
        int hash = Hash(x);                       // Вычисляем начальный хеш
        int startHash = hash;                     // Запоминаем начальную позицию
        
        for (int i = 0; i < Capacity; i++)        // Проходим не более Capacity раз
        {
            int index = HashNext(hash, i);        // Вычисляем текущий индекс (с учётом пробирования)
            
            if (forInsert)                        // Если ищем место для вставки
            {
                if (_array[index] == null || IsDeleted(index)) // Ячейка пуста или помечена как удалённая
                    return index;                 // Возвращаем индекс для вставки
            }
            
            // Если ищем элемент (не для вставки)
            if (_array[index] != null && !IsDeleted(index) && IsEquals(_array[index], x))
                return index;                     // Элемент найден — возвращаем его индекс
            
            if (!forInsert && _array[index] == null) // Если не для вставки и наткнулись на пустую ячейку
                return -1;                        // Элемент точно не существует — возвращаем -1
            
            if ((hash + i + 1) % Capacity == startHash) // Проверяем, не прошли ли полный круг
                break;                            // Прошли все ячейки — выходим из цикла
        }
        
        return -1;                                // Элемент не найден или таблица переполнена
    }

    /// <summary>
    /// Копирует имя (массив char) в новый массив фиксированной длины MaxSizeName.
    /// </summary>
    private char[] CopyName(char[] name)
    {
        char[] copy = new char[MaxSizeName];      // Создаём новый массив фиксированной длины
        int length = Math.Min(name.Length, MaxSizeName); // Определяем длину копирования (не больше MaxSizeName)
        Array.Copy(name, copy, length);           // Копируем символы из исходного массива
        return copy;                              // Возвращаем копию
    }

    /// <summary>
    /// Проверяет, помечена ли ячейка как удалённая (первый символ = '\0').
    /// </summary>
    private bool IsDeleted(int index)
    {
        return _array[index] != null && _array[index][0] == '\0'; // Не null и первый символ — нулевой
    }
}