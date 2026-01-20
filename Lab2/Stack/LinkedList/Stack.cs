using Lab2.Stack.Interfaces;

namespace Lab2.Stack.LinkedList;

public class Stack : IStack
{
    private Node? _head;                     // Вершина стека

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

    public char Pop()
    {
        char item = _head!.Value!;                 // Значение вершины
        _head = _head.Next;                     // Удаление вершины
        return item;
    }

    public void Push(char x)
    {
        Node node = new Node(x, _head);   // Создание узла
        _head = node;                           // Новая вершина
    }

    public char Top()
    {
        return _head!.Value!;                   // Значение вершины
    }
}