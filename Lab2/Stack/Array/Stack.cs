using Lab2.Stack.Interfaces;

namespace Lab2.Stack.Array;

public class Stack<T> : IStack<T>
{
    // Внутренний массив для хранения элементов стека
    private const int _capacity = 52; // Вместимость стека (фиксированный размер)
    private T[] _array = new T[_capacity]; // Массив элементов стека
    private int _last = -1; // Индекс верхнего элемента стека (-1 означает пустой стек)

    /// <summary>
    /// Проверяет, является ли стек пустым
    /// </summary>
    /// <returns>true - если стек пустой, false - если содержит элементы</returns>
    public bool Empty()
    {
        return _last == -1 ? true : false; // Стек пуст, когда _last равен -1
    }

    /// <summary>
    /// Проверяет, является ли стек полным
    /// </summary>
    /// <returns>true - если стек полный, false - если есть свободное место</returns>
    public bool Full()
    {
        return _last == _capacity - 1 ? true : false; // Стек полон, когда _last достиг максимального индекса
    }

    /// <summary>
    /// Очищает стек, удаляя все элементы
    /// </summary>
    public void MakeNull()
    {
        if (Empty()) return; // Если стек уже пуст, ничего не делаем

        // Очищаем массив, устанавливая всем элементам значение по умолчанию
        for (int i = 0; i <= _last; i++)
        {
            _array[i] = default!;
        }
        _last = -1; // Сбрасываем указатель на пустое состояние
    }

    /// <summary>
    /// Извлекает верхний элемент из стека
    /// </summary>
    /// <returns>Извлеченный элемент</returns>
    public T Pop()
    {
        if (Empty())
            throw new InvalidOperationException("Stack is empty"); // Защита от пустого стека

        T item = _array[_last]; // Сохраняем элемент для возврата
        _array[_last] = default!; // Очищаем ячейку в массиве
        _last--; // Уменьшаем указатель вершины
        return item; // Возвращаем сохраненный элемент
    }

    /// <summary>
    /// Добавляет элемент на вершину стека
    /// </summary>
    /// <param name="x">Элемент для добавления</param>
    public void Push(T x)
    {
        if (Full()) return; // Если стек полон, не добавляем
        _array[++_last] = x; // Увеличиваем _last и добавляем элемент
    }

    /// <summary>
    /// Возвращает верхний элемент стека без его удаления
    /// </summary>
    /// <returns>Верхний элемент стека</returns>
    public T Top()
    {
        if (Empty()) throw new InvalidOperationException("Stack is empty");
        
        return _array[_last]; // Возвращаем элемент с вершины
    }
}