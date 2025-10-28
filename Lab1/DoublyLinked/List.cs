namespace Lab1.DoublyLinked;

public class List<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;
    // private readonly Position<T> _end = new Position<T>(null);

    /// <summary>
    /// Возвращает позицию после последнего элемента списка
    /// </summary>
    /// <returns>Позиция конца списка</returns>
    public Position<T> End()
    {
        return new Position<T>(null);
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

        // 1. Вставка в конец
        if (position.Posit == End().Posit)
        {
            if (IsEmpty())
            {
                // Случай: пустой список
                _head = _tail = newNode;
            }
            else
            {
                // Случай: непустой список - добавляем после tail
                newNode.Previous = _tail;
                _tail!.Next = newNode;
                _tail = newNode;
            }
        }
        // 2. Вставка в начало или середину
        else if (CheckPosition(position))
        {
            Node<T> currentNode = position.Posit!;

            // Случай: вставка перед первым элементом
            if (currentNode == _head)
            {
                newNode.Next = _head;
                _head!.Previous = newNode;
                _head = newNode;
            }
            // Случай: вставка в середину
            else
            {
                Node<T> previousNode = currentNode.Previous!;

                newNode.Previous = previousNode;
                newNode.Next = currentNode;

                previousNode.Next = newNode;
                currentNode.Previous = newNode;
            }
        }
        else
        {
            Console.WriteLine("Неверная позиция для вставки");
            return;
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
        return End();  // элемент не найден
    }

    /// <summary>
    /// Возвращает элемент в указанной позиции
    /// </summary>
    /// <param name="position">Позиция элемента</param>
    /// <returns>Элемент в указанной позиции</returns>
    /// <exception cref="Exception">Если позиция неверная или равна End()</exception>
    public T Retrieve(Position<T> position)
    {
        if (!CheckPosition(position))
        {
            throw new Exception("Неверная позиция для получения элемента");
        }

        return position.Posit!.Value!;
    }

    /// <summary>
    /// Удаляет элемент в указанной позиции
    /// </summary>
    /// <param name="position">Позиция элемента для удаления</param>
    /// <exception cref="Exception">Если позиция неверная</exception>
    public void Delete(Position<T> position)
    {
        if (!CheckPosition(position)) 
        {
            Console.WriteLine("Неверная позиция для удаления");
            return;
        }
            
        Node<T> nodeToDelete = position.Posit!;
        
        // Случай: один элемент в списке (head == tail)
        if (_head == _tail)
        {
            _head = _tail = null;
            return;
        }
        
        // Случай: удаляем первый элемент
        if (nodeToDelete == _head)
        {
            _head = _head!.Next;
            _head!.Previous = null;
            return;
        }
        
        // Случай: удаляем последний элемент  
        if (nodeToDelete == _tail)
        {
            _tail = _tail!.Previous;
            _tail!.Next = null;
            return;
        }
        
        // Случай: удаляем из середины
        nodeToDelete.Previous!.Next = nodeToDelete.Next;
        nodeToDelete.Next!.Previous = nodeToDelete.Previous;
    }

    /// <summary>
    /// Возвращает позицию следующего элемента после указанной позиции
    /// </summary>
    /// <param name="position">Текущая позиция</param>
    /// <returns>Следующая позиция или End() если текущая последняя</returns>
    public Position<T> Next(Position<T> position)
    {
        if (!CheckPosition(position))
            throw new ArgumentException("Invalid position");
        
        // Если следующий элемент null - возвращаем End()
        return position.Posit!.Next == null ? End() : new Position<T>(position.Posit.Next);
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
        return IsEmpty() ? End() : new Position<T>(_head);
    }

    /// <summary>
    /// Выводит все элементы списка в консоль
    /// </summary>
    public void PrintList()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Список пуст");
            return;
        }

        Node<T>? current = _head;
        while (current != null)
        {
            Console.WriteLine(current.Value?.ToString() ?? "null");
            current = current.Next;
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Возвращает индекс элемента в списке (0-based)
    /// </summary>
    /// <param name="item">Элемент для поиска</param>
    /// <returns>Индекс элемента или -1 если не найден</returns>
    public int IndexOf(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        Node<T>? current = _head;
        int index = 0;

        while (current != null)
        {
            if (current.Value!.Equals(item))
                return index;

            current = current.Next;
            index++;
        }

        return -1; // элемент не найден
    }

    /// <summary>
    /// Возвращает позицию предыдущего элемента перед указанной позицией
    /// </summary>
    /// <param name="position">Текущая позиция</param>
    /// <returns>Предыдущая позиция</returns>
    /// <exception cref="Exception">Если позиция неверная или это первый элемент</exception>
    public Position<T> Previous(Position<T> position)
    {
        if (!CheckPosition(position))
        {
            Console.WriteLine("Неверная позиция");
            return End();
        }
        
        if (position.Posit == _head)
        {
            Console.WriteLine("Первый элемент не имеет предыдущего");
            return End();
        }
        
        return new Position<T>(position.Posit!.Previous);
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

    /// <summary>
    /// Проверяет, существует ли позиция в списке
    /// </summary>
    private bool CheckPosition(Position<T> position)
    {
        if (position.Posit == null || position.Posit == End().Posit)
            return false;

        Node<T> current = _head!;
        while (current != null)
        {
            if (current == position.Posit)
                return true;
            current = current.Next!;
        }
        return false;
    }
}