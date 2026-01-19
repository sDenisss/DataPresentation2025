using Lab2.Queue.Interfaces;

namespace Lab2.Queue.ATDList;

public class Queue<T> : IQueue<T>
{
    private Lab1.DoublyLinked.List<T> _list = new();  // Используем ATD List

    public T Dequeue()
    {
        T item = _list.Retrieve(_list.First());  // Получение первого
        _list.Delete(_list.First());             // Удаление из списка
        return item;
    }

    public bool Empty()
    {
        return _list.First() == _list.End();     // Сравнение позиций
    }

    public void Enqueue(T x)
    {
        _list.Insert(x, _list.End());            // Вставка в конец
    }

    public T Front()
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