using Lab2.Stack.Interfaces;

namespace Lab2.Stack.LinkedList;

public class Stack<T> : IStack<T>
{
    private Node<T>? _head;                     // Вершина стека

    public bool Empty()
    {
        return _head == null;                   // Проверка на пустоту
    }

    public bool Full()
    {
        return false;                           // Без ограничений
    }

    public void MakeNull()
    {
        _head = null;                           // Очистка стека
    }

    public T Pop()
    {
        T item = _head!.Value!;                 // Значение вершины
        _head = _head.Next;                     // Удаление вершины
        return item;
    }

    public void Push(T x)
    {
        Node<T> node = new Node<T>(x, _head);   // Создание узла
        _head = node;                           // Новая вершина
    }

    public T Top()
    {
        return _head!.Value!;                   // Значение вершины
    }
}