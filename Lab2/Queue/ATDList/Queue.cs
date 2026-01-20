using Lab2.Queue.Interfaces;

namespace Lab2.Queue.ATDList;

public class Queue : IQueue
{
    private Lab1.DoublyLinked.List<char> _list = new();  // Используем ATD List

    public char Dequeue()
    {
        char item = _list.Retrieve(_list.First());  // Получение первого
        _list.Delete(_list.First());             // Удаление из списка
        return item;
    }

    public bool Empty()
    {
        return _list.First() == _list.End();     // Сравнение позиций
    }

    public void Enqueue(char x)
    {
        _list.Insert(x, _list.End());            // Вставка в конец
    }

    public char Front()
    {
        return _list.Retrieve(_list.First());    // Первый элемент
    }

    public bool Full()
    {
        return false;                            // Без ограничений
    }

    public void MakeNull()
    {
        _list.Makenull();                        // Очистка списка
    }
}