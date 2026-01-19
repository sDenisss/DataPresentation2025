using Lab2.Queue.Interfaces;

namespace Lab2.Queue.CircularyLinkedList;

public class Queue<T> : IQueue<T>
{
    private Node<T>? _tail;                       // Хвост кольцевого списка

    public T Dequeue()
    {
        Node<T> first = _tail!.Next!;            // Голова очереди
        T item = first.Value!;                    // Значение головы
        
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

    public void Enqueue(T x)
    {
        if (_tail == null)                       // Первый элемент
        {
            _tail = new Node<T>(x, null);
            _tail.Next = _tail;                  // Замыкание на себя
        }
        else                                     // Добавление в конец
        {
            Node<T>? temp = new Node<T>(x, _tail!.Next);
            _tail.Next = temp;                   // Обновление связей
            _tail = temp;                        // Новый хвост
        }
    }

    public T Front()
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