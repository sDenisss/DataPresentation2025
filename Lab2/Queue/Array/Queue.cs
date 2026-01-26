using Lab2.Queue.Interfaces;

namespace Lab2.Queue.Array;

public class Queue : IQueue
{
    private const int _capacity = 52;           // Фиксированная емкость
    private char[] _array = new char[_capacity];     // Основной массив
    private int _first = 0;                    // Индекс начала очереди
    private int _last = _capacity - 1;         // Индекс конца очереди

    public char Dequeue()
    {
        char item = _array[_first];               // Получение элемента
        _first = Next(_first);                 // Сдвиг начала
        return item;
    }

    public bool Empty()
    {
        return _first == Next(_last);          // Очередь пуста
    }

    public void Enqueue(char x)
    {
        _last = Next(_last);      // Циклическое увеличение
        _array[_last] = x;                     // Запись элемента
    }

    public char Front()
    {
        return _array[_first];                 // Элемент в начале
    }

    public bool Full()
    {
        return Next(Next(_last)) == _first;    // Очередь заполнена
    }

    public void MakeNull()
    {
        _last = _capacity - 1;                 // Сброс указателей
        _first = 0;
    }

    private int Next(int pos)
    {
        return (pos + 1) % _capacity;         // Следующая позиция
    }
}