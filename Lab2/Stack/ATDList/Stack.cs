using Lab2.Stack.Interfaces;

namespace Lab2.Stack.ATDList;

public class Stack : IStack
{
    private Lab1.DoublyLinked.List<char> _list = new();  // ATD List как основа

    public bool Empty()
    {
        return _list.First() == _list.End();  // Сравнение позиций
    }

    public bool Full()
    {
        return false;                           // Без ограничений
    }

    public void MakeNull()
    {
        _list.Makenull();                       // Очистка списка
    }

    public char Pop()
    {
        char item = _list.Retrieve(_list.First()); // Получение первого
        _list.Delete(_list.First());            // Удаление из списка
        return item;
    }

    public void Push(char x)
    {
        _list.Insert(x, _list.First());         // Вставка в начало
    }

    public char Top()
    {
        char item = _list.Retrieve(_list.End());  // Получение последнего
        return item;
    }
}