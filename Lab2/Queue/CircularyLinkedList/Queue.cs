using Lab2.Queue.Interfaces;

namespace Lab2.Queue.CircularyLinkedList;

public class Queue<T> : IQueue<T>
{
    private Node<T>? _tail;
    public T Dequeue()
    {
        Node<T> first = _tail!.Next!; 
        T item = first.Value!;
        if (_tail == first)
        {
            _tail = null;
        }
        else
        {
            _tail.Next = first.Next;
        }
        return item;
    }

    public bool Empty()
    {
        return _tail == null;
    }

    public void Enqueue(T x)
    {
        if (Empty())
        {
            _tail = new Node<T>(x, null);
            _tail.Next = _tail;
        }
        else
        {
            Node<T>? temp = new Node<T>(x, _tail!.Next);
            _tail.Next = temp;
            _tail = temp;
        }
    }

    public T Front()
    {
        return _tail!.Next!.Value!;
    }

    public bool Full()
    {
        return false;
    }

    public void MakeNull()
    {
        _tail = null;
    }
}