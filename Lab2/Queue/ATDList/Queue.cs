using Lab2.Queue.Interfaces;

namespace Lab2.Queue.ATDList;

public class Queue<T> : IQueue<T>
{
    private Lab1.DoublyLinked.List<T> _list = new Lab1.DoublyLinked.List<T>();
    public T Dequeue()
    {
        T item = _list.Retrieve(_list.First());
        _list.Delete(_list.First());
        return item;
    }

    public bool Empty()
    {
        return _list.First() == null;
    }

    public void Enqueue(T x)
    {
        _list.Insert(x, _list.End());
    }

    public T Front()
    {
        return _list.Retrieve(_list.First());
    }

    public bool Full()
    {
        return false;
    }

    public void MakeNull()
    {
        _list.Makenull();
    }
}