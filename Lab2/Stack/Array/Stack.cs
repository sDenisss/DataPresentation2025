using Lab2.Stack.Interfaces;

namespace Lab2.Stack.Array;

public class Stack : IStack
{
    private const int _capacity = 52;        // Максимальный размер
    private char[] _array = new char[_capacity];  // Хранилище элементов
    private int _last = -1;                  // Индекс вершины (-1 = пусто)

    public bool Empty()
    {
        return _last == -1;                  // Проверка на пустоту
    }

    public bool Full()
    {
        return _last == _capacity - 1;       // Проверка на заполненность
    }

    public void MakeNull()
    {
        _last = -1;                          // Сброс указателя
    }

    public char Pop()
    {
        char item = _array[_last--];            // Получение и уменьшение указателя
        return item;
    }

    public void Push(char x)
    {
        _array[++_last] = x;                 // Увеличение указателя и запись
    }

    public char Top()
    {
        return _array[_last];                // Элемент на вершине
    }
}