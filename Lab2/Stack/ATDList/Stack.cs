// using Lab1.DoublyLinked;
using Lab2.Stack.Interfaces;

namespace Lab2.Stack.ATDList;

public class Stack<T> : IStack<T>
{
    private Lab1.DoublyLinked.List<T> _list = new Lab1.DoublyLinked.List<T>();
    public bool Empty()
    {
        return _list.First().Posit == _list.End().Posit;
    }

    public bool Full()
    {
        return false;
    }

    public void MakeNull()
    {
        _list.Makenull();
    }

    public T Pop()
    {
        if (Empty()) 
            throw new InvalidOperationException("Stack is empty");
            
        T item = _list.Retrieve(_list.First());
        _list.Delete(_list.First());
        return item;
    }

    public void Push(T x)
    {
        _list.Insert(x, _list.First());
    }

    public T Top()
    {
        if (Empty())
            throw new InvalidOperationException("Stack is empty");
            
        T? item = _list.Retrieve(_list.End());
        return item;
    }
}