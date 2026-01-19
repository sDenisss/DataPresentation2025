using System.Transactions;
using Lab3.Interfce;

namespace Lab3.CloseHash;
public class Dictionary : IDictionary
{
    private const int Capacity = 69;           // Размер хеш-таблицы
    private const int MaxSizeName = 10;       // Максимальный размер имени
    private char[][] _array = new char[Capacity][];  // Основной массив

    public void Delete(char[] x)
    {
        int hash = Hash(x);                    // Получение хеша
        
        for (int i = 0; i < Capacity; i++)     // Линейное пробирование
        {
            int index = (hash + i) % Capacity; // Вычисление индекса
            
            if (_array[index] == null)         // Пустая ячейка
                return;
                
            if (!IsDeleted(index) && IsEquals(_array[index], x))
            {
                _array[index][0] = '\0';       // Маркировка как удаленная
                return;
            }
        }
    }

    public void Insert(char[] x)
    {
        int hash = Hash(x);                    // Получение хеша
        
        for (int i = 0; i < Capacity; i++)     // Линейное пробирование
        {
            int index = (hash + i) % Capacity; // Вычисление индекса
            
            if (_array[index] == null || IsDeleted(index))
            {
                _array[index] = CopyName(x);   // Вставка в свободную ячейку
                return;
            }
            
            if (IsEquals(_array[index], x))    // Элемент уже существует
                return;
        }
        
        Console.WriteLine("Ошибка: словарь переполнен!");
    }

    public void Makenull()
    {
        for (int i = 0; i < _array.Length; i++)
        {
            _array[i] = null!;                 // Очистка всего массива
        }
    }

    public bool Member(char[] x)
    {
        int hash = Hash(x);                    // Получение хеша
        
        for (int i = 0; i < Capacity; i++)     // Линейное пробирование
        {
            int index = (hash + i) % Capacity; // Вычисление индекса
            
            if (_array[index] == null)         // Элемента не было
                return false;
                
            if (!IsDeleted(index) && IsEquals(_array[index], x))
                return true;                   // Элемент найден
        }
        
        return false;                          // Элемент не найден
    }

    public void Print()
    {
        Console.WriteLine("\nPrint:\n");
        for (int i = 0; i < Capacity; i++)     // Обход всей таблицы
        {
            if (_array[i] == null || _array[i][0] == '\0')
                continue;                      // Пропуск пустых/удаленных

            Console.WriteLine($"{i} - {new string(_array[i])}");
        }
        Console.WriteLine();
    }

    // Хеш-функция (сумма ASCII кодов)
    private int Hash(char[] name)
    {
        int sum = 0;

        for (int i = 0; i < name.Length && name[i] != '\0'; i++)
            sum += name[i];

        return sum % Capacity;
    }

    private int HashNext(int hash)
    {
        return (hash + 1) % Capacity;          // Следующая позиция
    }

    // Сравнение двух массивов символов
    private bool IsEquals(char[] name1, char[] name2)
    {
        if (name1 == null && name2 == null) return true;
        if (name1 == null || name2 == null) return false;
        
        for (int i = 0; i < MaxSizeName; i++)
        {
            char c1 = (i < name1.Length) ? name1[i] : '\0';
            char c2 = (i < name2.Length) ? name2[i] : '\0';
            
            if (c1 != c2) return false;       // Различие найдено
            if (c1 == '\0' || c2 == '\0') break; // Конец строки
        }
        
        return true;
    }

    // Копирование имени в новый массив
    private char[] CopyName(char[] name)
    {
        char[] copy = new char[MaxSizeName];
        int length = Math.Min(name.Length, MaxSizeName);
        Array.Copy(name, copy, length);
        return copy;
    }

    // Проверка маркировки как удаленная
    private bool IsDeleted(int index)
    {
        return _array[index] != null && _array[index][0] == '\0';
    }
}