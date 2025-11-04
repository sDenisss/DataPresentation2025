using Lab2.Stack.Interfaces;

namespace Lab2.Stack.LinkedList;

public class Stack<T> : IStack<T>
{
    private Node<T>? _head;
    public bool Empty()
    {
        return _head == null;
    }

    public bool Full()
    {
        return false;
    }

    public void MakeNull()
    {
        _head = null;
    }

    public T Pop()
    {
        if (Empty()) throw new Exception("Empty!");

        T item = _head!.Value!;
        _head = _head.Next;
        return item;
    }

    public void Push(T x)
    {
        Node<T> node = new Node<T>(x, _head);
        _head = node;
    }

    public T Top()
    {
        if (Empty()) throw new Exception("Empty");
        
        return _head!.Value!;
    }
}