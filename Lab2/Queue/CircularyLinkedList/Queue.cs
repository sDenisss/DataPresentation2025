using Lab2.Queue.Interfaces;

namespace Lab2.Queue.CircularyLinkedList;

public class Queue : IQueue
{
    private Node? _tail;                       // Хвост кольцевого списка

    public char Dequeue()
    {
        Node first = _tail!.Next!;            // Голова очереди
        char item = first.Value!;                    // Значение головы
        
        if (_tail == first)                      // Единственный элемент
        {
            _tail = null;
        }
        else
        {
            _tail.Next = first.Next;             // Удаление головы
        }
        return item;
    }

    public bool Empty()
    {
        return _tail == null;                    // Проверка на пустоту
    }

    public void Enqueue(char x)
    {
        if (_tail == null)                       // Первый элемент
        {
            _tail = new Node(x, null!);
            _tail.Next = _tail;                  // Замыкание на себя
        }
        else                                     // Добавление в конец
        {
            Node? temp = new Node(x, _tail!.Next);
            _tail.Next = temp;                   // Обновление связей
            _tail = temp;                        // Новый хвост
        }
    }

    public char Front()
    {
        return _tail!.Next!.Value!;              // Значение головы
    }

    public bool Full()
    {
        return false;                            // Без ограничений
    }

    public void MakeNull()
    {
        _tail = null;                            // Очистка очереди
    }
}