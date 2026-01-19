using Lab2.Stack.Interfaces;

namespace Lab2.Stack.ATDList;

public class Stack<T> : IStack<T>
{
    private Lab1.DoublyLinked.List<T> _list = new();  // ATD List как основа

    public bool Empty()
    {
        return _list.First().Posit == _list.End().Posit;  // Сравнение позиций
    }

    public bool Full()
    {
        return false;                           // Без ограничений
    }

    public void MakeNull()
    {
        _list.Makenull();                       // Очистка списка
    }

    public T Pop()
    {
        T item = _list.Retrieve(_list.First()); // Получение первого
        _list.Delete(_list.First());            // Удаление из списка
        return item;
    }

    public void Push(T x)
    {
        _list.Insert(x, _list.First());         // Вставка в начало
    }

    public T Top()
    {
        T? item = _list.Retrieve(_list.End());  // Получение последнего
        return item;
    }
}