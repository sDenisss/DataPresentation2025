using Lab3.Interfce;

namespace Lab3.OpenHash;

public class Dictionary : IDictionary
{
    private const int Capacity = 69;           // Размер хеш-таблицы
    private Node[] _array = new Node[Capacity]; // Массив цепочек

    public void Delete(char[] x)
    {
        if (!Member(x)) return;                // Элемента нет

        int hash = Hash(x);                    // Получение хеша
        Node current = _array[hash];
        
        // Удаление из головы списка
        if (current != null && IsEquals(current.Value, x))
        {
            _array[hash] = current.Next!;      // Новая голова
            return;
        }
        
        // Удаление из середины/конца списка
        Node previous = null!;
        while (current != null)
        {
            if (IsEquals(current.Value, x))
            {
                if (previous != null)
                {
                    previous.Next = current.Next; // Пересвязывание
                }
                return;
            }
            previous = current;
            current = current.Next!;
        }
    }

    public void Insert(char[] x)
    {
        if (Member(x)) return;                 // Дубликат
        
        int hash = Hash(x);                    // Получение хеша
        Node head = _array[hash];
        _array[hash] = new Node(x, head);      // Вставка в голову
    }

    public void Makenull()
    {
        for (int i = 0; i < Capacity; i++)
        {
            _array[i] = null!;                 // Очистка всех цепочек
        }
    }

    public bool Member(char[] x)
    {
        int hash = Hash(x);                    // Получение хеша
        Node current = _array[hash];

        while (current != null)                // Обход цепочки
        {
            if (IsEquals(current.Value, x)) return true; // Найден
            current = current.Next!;
        }

        return false;                          // Не найден
    }

    public void Print()
    {
        for (int i = 0; i < Capacity; i++)     // Обход всех ячеек
        {
            Node current = _array[i];
            if (current != null)               // Непустая цепочка
            {
                Console.Write($"{i}: ");
                while (current != null)        // Обход цепочки
                {
                    Console.Write($"{new string(current.Value)}");
                    if (current.Next != null)
                        Console.Write(" -> ");
                    else
                        Console.Write(" -> null");
                    current = current.Next!;
                }
                Console.WriteLine();
            }
        }
    }

    // Хеш-функция (сумма ASCII кодов)
    private int Hash(char[] name)
    {
        int sum = 0;

        for (int i = 0; i < name.Length && name[i] != '\0'; i++)
            sum += (int)name[i];

        return sum % Capacity;
    }
    
    // Сравнение массивов поэлементно
    private bool IsEquals(char[] name1, char[] name2)
    {
        if (name1.Length != name2.Length) return false;
        if (name1 == null || name2 == null) return name1 == name2;

        for (int i = 0; i < name1.Length; i++)
            if (name1[i] != name2[i]) return false;

        return true;
    }
}