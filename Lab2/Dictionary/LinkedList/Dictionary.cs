using Lab2.Dictionary.Interfaces;

namespace Lab2.Dictionary.LinkedList;

public class Dictionary<TKey, TValue> : Interfaces.IDictionary<TKey, TValue>
{
    private Node<TKey, TValue>? _head;
    public void Assign(TKey key, TValue value)
    {
        // Поиск узла с заданным ключом
        Node<TKey, TValue>? current = _head;
        while (current != null)
        {
            if (Equals(current.Key, key))
            {
                // Ключ найден - обновляем значение
                current.Value = value;
                return;
            }
            current = current.Next;
        }

        // Ключ не найден - добавляем новый узел в начало списка
        Node<TKey, TValue> newNode = new Node<TKey, TValue>(key, value);
        newNode.Next = _head;
        _head = newNode;
    }
    

    public bool Compute(TKey key, out TValue value)
    {
        // Поиск узла с заданным ключом
        Node<TKey, TValue>? current = _head;
        while (current != null)
        {
            if (Equals(current.Key, key))
            {
                // Ключ найден - обновляем значение
                value = current.Value!;
                return true;
            }
            current = current.Next;
        }

        value = default!;
        return false;
    }

    public void MakeNull()
    {
        _head = null;
    }

    public void Print()
    {
        Node<TKey, TValue>? current = _head;
        while (current != null)
        {
            Console.WriteLine($"Ключ: {current.Key}, Значение: {current.Value}");
            current = current.Next;
        }
    }
}