using Lab2.Queue.Interfaces;

namespace Lab2.Queue.Array;

public class Queue<T> : IQueue<T>
{
    private const int _capacity = 52;
    private T[] _array = new T[_capacity];
    private int _first = 0;
    private int _last = -1;
    public int _count = 0;

    public T Dequeue()
    {
        if (Empty())
            throw new InvalidOperationException("Queue is empty");

        T item = _array[_first];
        _array[_first] = default!;

        if (_first == _last) // если был последний элемент
        {
            _first = 0;
            _last = -1;
        }
        else
        {
            _first = (_first + 1) % _capacity; // двигаем голову по кругу
        }
        _count--;

        return item;
    }


    public bool Empty()
    {
        return _count == 0;
    }

    public void Enqueue(T x)
    {
        if (Full()) return;

        if (Empty())
        {
            _first = _last = 0;
        }
        else
        {
            _last = (_last + 1) % _capacity; // двигаем хвост по кругу
        }
        _array[_last] = x;
        _count++;
    }

    public T Front()
    {
        if (Empty())
            throw new InvalidOperationException("Queue is empty");
        return _array[_first];
    }

    public bool Full()
    {
        return _count == _capacity;
    }

    public void MakeNull()
    {
        for (int i = 0; i < _capacity; i++)
        {
            _array[i] = default!;
        }
        _first = 0;
        _last = -1;
        _count = 0;
    }
}