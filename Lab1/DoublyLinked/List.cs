namespace Lab1.DoublyLinked;

public class List<T> where T : IEquatable<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;
    private readonly Position<T> _end = new Position<T>(null);

    /// <summary>
    /// Возвращает позицию после последнего элемента списка
    /// </summary>
    /// <returns>Позиция конца списка</returns>
    public Position<T> End()
    {
        return _end;
    }

    /// <summary>
    /// Вставляет элемент в указанную позицию списка
    /// </summary>
    /// <param name="item">Элемент для вставки</param>
    /// <param name="position">Позиция для вставки (перед этим элементом)</param>
    /// <exception cref="Exception">Если нет свободного места или неверная позиция</exception>
    public void Insert(T item, Position<T> position)
    {
        Node<T> newNode = new Node<T> { Value = item };
        if (position.Posit == End().Posit)
        {
            if (_tail == null)
            {
                _tail = newNode;
                _head = newNode;
            }
            else
            {
                newNode.Previous = _tail;
                _tail.Next = newNode;
                _tail = newNode;
            }
        }
        else if (position.Posit == First().Posit)
        {
            if (_head == null)
            {
                _tail = newNode;
                _head = newNode;
            }
            else
            {
                newNode.Next = _head;
                _head.Previous = newNode;
                _head = newNode;
            }
        }
    }

    /// <summary>
    /// Находит позицию первого вхождения элемента в списке
    /// </summary>
    /// <param name="item">Элемент для поиска</param>
    /// <returns>Позиция элемента или End() если не найден</returns>
    public Position<T> Locate(T item)
    {
        Node<T>? current = _head;
        while (current != null)
        {
            if (current.Value!.Equals(item))
                return new Position<T>(current);
            current = current.Next;
        }
        return _end;  // элемент не найден
    }

    /// <summary>
    /// Возвращает элемент в указанной позиции
    /// </summary>
    /// <param name="position">Позиция элемента</param>
    /// <returns>Элемент в указанной позиции</returns>
    /// <exception cref="Exception">Если позиция неверная или равна End()</exception>
    public T Retrieve(Position<T> position)
    {
        if (position.Posit == _end.Posit)
            throw new Exception("Неверная позиция!");

        return position.Posit.Value;
    }

    /// <summary>
    /// Удаляет элемент в указанной позиции
    /// </summary>
    /// <param name="position">Позиция элемента для удаления</param>
    /// <exception cref="Exception">Если позиция неверная</exception>
    public void Delete(Position<T> position)
    {
        if (position.Posit == null) throw new Exception("Данная позиция отсутствует в списке");

        // Удаляем первый элемент
        if (position.Posit == _head)
        {
            _head = _head!.Next;
            if (_head != null)
                _head.Previous = null;
            else
                _tail = null;  // если список стал пустым
            return;
        }

        // Удаляем последний элемент
        if (position.Posit == _tail)
        {
            _tail = _tail!.Previous;
            if (_tail != null)
                _tail.Next = null;
            else
                _head = null;  // если список стал пустым
            return;
        }

        // Удаляем из середины
        position.Posit.Previous!.Next = position.Posit.Next;
        position.Posit.Next!.Previous = position.Posit.Previous;
    }

    /// <summary>
    /// Возвращает позицию следующего элемента после указанной позиции
    /// </summary>
    /// <param name="position">Текущая позиция</param>
    /// <returns>Следующая позиция или End() если текущая последняя</returns>
    public Position<T> Next(Position<T> position)
    {
        if (position.Posit == End().Posit)
        {
            return new Position<T>(null);  // после End() нет следующего
        }

        if (position.Posit == null)
        {
            throw new Exception("Позиция нулевая");
        }

        return position.Posit == _tail ? _end : new Position<T>(position.Posit.Next);
    }

    /// <summary>
    /// Очищает список, делая его пустым
    /// </summary>
    public void Makenull()
    {
        if (IsEmpty())
        {
            return;
        }

        _head = _tail = null;
    }

    /// <summary>
    /// Возвращает позицию первого элемента списка
    /// </summary>
    /// <returns>Позиция первого элемента или End() если список пуст</returns>
    public Position<T> First()
    {
        return IsEmpty() ? _end : new Position<T>(_head);
    }

    /// <summary>
    /// Выводит все элементы списка в консоль
    /// </summary>
    public void PrintList()
    {
        if (_head == null)
        {
            Console.WriteLine("Список пуст");
            return;
        }

        Node<T>? current = _head;
        while (current!= null)
        {
            if (current == null)
            {
                throw new Exception($"Invalid position index: {current}");
            }

            Console.WriteLine(current.Value?.ToString() ?? "null");

            current = current.Next;
        }

        Console.WriteLine();
    }


    /// <summary>
    /// Возвращает позицию предыдущего элемента перед указанной позицией
    /// </summary>
    /// <param name="position">Текущая позиция</param>
    /// <returns>Предыдущая позиция</returns>
    /// <exception cref="Exception">Если позиция неверная или это первый элемент</exception>
    public Position<T> Previous(Position<T> position)
    {
        // Node<T>? prev;
        if (position.Posit == _head || position.Posit == null)
            throw new Exception("Позиции нет в списке");

        return new Position<T>(position.Posit.Previous);
    }

    //=== ВСПОМОГАТЕЛЬНЫЕ ПРИВАТНЫЕ МЕТОДЫ ===

    /// <summary>
    /// Проверяет, пуст ли список
    /// </summary>
    /// <returns>true если список пуст, иначе false</returns>
    private bool IsEmpty()
    {
        return _head == null;
    }

    // /// <summary>
    // /// Проверяет, существует ли позиция в списке
    // /// </summary>
    // /// <param name="index">Индекс для проверки</param>
    // /// <returns>true если позиция существует, иначе false</returns>
    // private bool CheckPosition(int index)
    // {
    //     if (index == _end.Posit) return false;  // End() не считается валидной позицией для операций
    //     int current = _start.Posit;
    //     while (current != -1)
    //     {
    //         if (current == index) return true;
    //         current = Nodes[current].Next;
    //     }
    //     return false;
    // }
}