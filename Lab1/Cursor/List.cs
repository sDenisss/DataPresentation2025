namespace Lab1.Cursor;

public class List<T> where T : IEquatable<T>
{
    // Статический массив всех узлов - общий для всех экземпляров списка
    private static readonly Node<T>[] Nodes;
    // Максимальный размер списка
    private const int Size = 52;
    // Позиция первого элемента списка (-1 если список пуст)
    private Position _start = new Position(-1);
    // Позиция первого свободного узла в массиве
    private Position _space = new Position(0);
    // Специальная позиция "конец списка" (после последнего элемента)
    private Position _end = new Position(-1);

    // Статический конструктор - инициализирует массив узлов один раз при загрузке класса
    static List()
    {
        Nodes = new Node<T>[Size];

        // Инициализация цепочки свободных узлов
        for (int i = 0; i < Size - 1; i++)
        {
            Nodes[i] = new Node<T>
            {
                Next = i + 1  // каждый узел указывает на следующий
            };
        }

        // Последний узел указывает на -1 (конец цепочки)
        Nodes[Size - 1] = new Node<T>
        {
            Next = -1
        };
    }
    
    /// <summary>
    /// Возвращает позицию после последнего элемента списка
    /// </summary>
    /// <returns>Позиция конца списка</returns>
    public Position End()
    {
        return _end;
    }
    
    /// <summary>
    /// Вставляет элемент в указанную позицию списка
    /// </summary>
    /// <param name="item">Элемент для вставки</param>
    /// <param name="position">Позиция для вставки (перед этим элементом)</param>
    /// <exception cref="Exception">Если нет свободного места или неверная позиция</exception>
    public void Insert(T item, Position position)
    {
        // Вставка в конец списка
        if (position.Posit == End().Posit)
        {
            if (_space.Posit == -1)
                throw new Exception("Нет свободного места в списке!");

            int freeIndex = _space.Posit;

            // ЗАПОМНИ следующую свободную ячейку ПРЕЖДЕ чем менять Nodes[freeIndex]
            int nextFree = Nodes[freeIndex].Next;

            // Теперь заполняем ячейку
            Nodes[freeIndex].Value = item;
            Nodes[freeIndex].Next = -1;  // новый элемент становится концом списка

            // Если список пустой
            if (_start.Posit == -1)
            {
                _start.Posit = freeIndex;  // новый элемент становится началом
            }
            else
            {
                // Находим последний элемент и обновляем его ссылку
                int lastIndex = FindLastIndex();
                Nodes[lastIndex].Next = freeIndex;
            }

            // ОБНОВЛЯЕМ _space на ЗАПОМНЕННУЮ следующую свободную
            _space.Posit = nextFree;
        }
        else if (CheckPosition(position.Posit))
        {
            int freeIndex = _space.Posit;
            int nextFree = Nodes[freeIndex].Next;

            // 🔥 Ключевое исправление: находим предыдущий элемент
            int previousIndex = GetPrevious(position.Posit);

            // Заполняем новую ячейку данными
            Nodes[freeIndex].Value = item;
            
            // Новая ячейка указывает на целевую позицию
            Nodes[freeIndex].Next = position.Posit;

            // Если вставляем в начало
            if (previousIndex == -1)
            {
                _start.Posit = freeIndex;
            }
            else
            {
                // Предыдущий элемент теперь указывает на новую ячейку
                Nodes[previousIndex].Next = freeIndex;
            }

            _space.Posit = nextFree;

            // Если список был пустой
            if (_start.Posit == -1)
            {
                _start.Posit = freeIndex;
            }
        }
        else
        {
            throw new Exception("Неверная позиция для вставки!");
        }
        // Вставка в начало списка
        // else if (position.Posit == First().Posit)
        // {
        //     if (_space.Posit == -1)
        //         throw new Exception("Нет свободного места в списке!");

        //     int freeIndex = _space.Posit;
        //     int nextFree = Nodes[freeIndex].Next;

        //     // Копируем текущий первый элемент в свободную ячейку
        //     Nodes[freeIndex].Value = Nodes[position.Posit].Value;
        //     Nodes[freeIndex].Next = Nodes[position.Posit].Next;

        //     // В старую первую ячейку записываем новый элемент
        //     Nodes[position.Posit].Value = item;
        //     Nodes[position.Posit].Next = freeIndex;

        //     // Обновляем _space
        //     _space.Posit = nextFree;

        //     // Если это была первая вставка (список был пустой)
        //     if (_start.Posit == -1)
        //     {
        //         _start.Posit = position.Posit;
        //     }
        // }
        // Вставка в ЛЮБУЮ позицию (начало, середину)
    }
    
    /// <summary>
    /// Находит позицию первого вхождения элемента в списке
    /// </summary>
    /// <param name="item">Элемент для поиска</param>
    /// <returns>Позиция элемента или End() если не найден</returns>
    public Position Locate(T item)
    {
        int current = _start.Posit;
        while (current != -1)
        {
            if (Nodes[current].Value.Equals(item))
                return new Position(current);
            current = Nodes[current].Next;
        }
        return _end;  // элемент не найден
    }
    
    /// <summary>
    /// Возвращает элемент в указанной позиции
    /// </summary>
    /// <param name="position">Позиция элемента</param>
    /// <returns>Элемент в указанной позиции</returns>
    /// <exception cref="Exception">Если позиция неверная или равна End()</exception>
    public T Retrieve(Position position)
    {
        if (position.Posit == _end.Posit || !CheckPosition(position.Posit))
            throw new Exception("Неверная позиция!");

        return Nodes[position.Posit].Value;
    }

    /// <summary>
    /// Удаляет элемент в указанной позиции
    /// </summary>
    /// <param name="position">Позиция элемента для удаления</param>
    /// <exception cref="Exception">Если позиция неверная</exception>
    public void Delete(Position position)
    {
        if (position.Posit < 0) throw new Exception("Данная позиция отсутствует в списке");
        int tmp;

        // Удаляем первый элемент
        if (position.Posit == _start.Posit)
        {
            tmp = _space.Posit;
            _space = _start; // освободили ячейку
            _start = new Position(Nodes[_start.Posit].Next); // новый старт
            Nodes[_space.Posit].Next = tmp;  // добавляем освободившуюся ячейку в список свободных

            return;
        }


        int prev = GetPrevious(position.Posit);
        if (prev == -1) throw new Exception("Данная позиция отсутствует в списке");

        // Обновляем ссылку предыдущего элемента
        Nodes[prev].Next = Nodes[position.Posit].Next;

        // Добавляем освободившуюся ячейку в список свободных
        tmp = _space.Posit;
        _space.Posit = position.Posit;
        Nodes[_space.Posit].Next = tmp;
        // Удаляем не первый элемент
        // int prev = GetPrevious(position.Posit);
        // if (prev == -1) throw new Exception("Данная позиция отсутствует в списке");

        // int current = Nodes[prev].Next;
        // Nodes[prev].Next = Nodes[current].Next;  // исключаем элемент из цепочки

        // tmp = _space.Posit;
        // _space = new Position(current);  // освобождаем ячейку

        // Nodes[_space.Posit].Next = tmp;  // добавляем в список свободных
    }
    
    /// <summary>
    /// Возвращает позицию следующего элемента после указанной позиции
    /// </summary>
    /// <param name="position">Текущая позиция</param>
    /// <returns>Следующая позиция или End() если текущая последняя</returns>
    public Position Next(Position position)
    {
        if (position.Posit == End().Posit)
        {
            return new Position(-1);  // после End() нет следующего
        }

        return new Position(Nodes[position.Posit].Next);
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

        // Соединяем конец списка данных с началом свободных ячеек
        Nodes[LastPos()].Next = _space.Posit;
        // Все ячейки списка становятся свободными
        _space = _start;
        // Список становится пустым
        _start = new Position(-1);
    }
    
    /// <summary>
    /// Возвращает позицию первого элемента списка
    /// </summary>
    /// <returns>Позиция первого элемента или End() если список пуст</returns>
    public Position First()
    {
        return IsEmpty() ? _end : new Position(_start.Posit);
    }

    /// <summary>
    /// Выводит все элементы списка в консоль
    /// </summary>
    public void PrintList()
    {
        if (_start.Posit == -1)
        {
            Console.WriteLine("Список пуст");
            return;
        }

        int cur = _start.Posit;

        while (cur != -1)
        {
            if (cur < 0 || cur >= Nodes.Length)
            {
                throw new Exception($"Invalid position index: {cur}");
            }

            Console.WriteLine(Nodes[cur].Value?.ToString() ?? "null");

            int nextIndex = Nodes[cur].Next;
            cur = nextIndex;
        }

        Console.WriteLine();
    }

    //=== ВСПОМОГАТЕЛЬНЫЕ ПРИВАТНЫЕ МЕТОДЫ ===

    /// <summary>
    /// Находит индекс последнего элемента в списке
    /// </summary>
    /// <returns>Индекс последнего элемента или -1 если список пуст</returns>
    private int LastPos()
    {
        int cur = _start.Posit;
        int prev = -1;
        while (cur != -1)
        {
            prev = cur;
            cur = Nodes[cur].Next;
        }
        return prev;
    }
    
    /// <summary>
    /// Проверяет, пуст ли список
    /// </summary>
    /// <returns>true если список пуст, иначе false</returns>
    private bool IsEmpty()
    {
        return _start.Posit == -1;
    }

    /// <summary>
    /// Находит индекс последнего элемента (аналогично LastPos)
    /// </summary>
    /// <returns>Индекс последнего элемента</returns>
    private int FindLastIndex()
    {
        if (_start.Posit == -1) return -1;

        int current = _start.Posit;
        while (Nodes[current].Next != -1)
        {
            current = Nodes[current].Next;
        }
        return current;
    }

    /// <summary>
    /// Находит индекс предыдущего элемента относительно указанного
    /// </summary>
    /// <param name="index">Индекс текущего элемента</param>
    /// <returns>Индекс предыдущего элемента или -1 если не найден</returns>
    private int GetPrevious(int index)
    {
        int current = _start.Posit;
        int previous = -1;
        while (current != -1)
        {
            if (current == index) return previous;
            previous = current;
            current = Nodes[current].Next;
        }
        return -1;
    }
    
    /// <summary>
    /// Проверяет, существует ли позиция в списке
    /// </summary>
    /// <param name="index">Индекс для проверки</param>
    /// <returns>true если позиция существует, иначе false</returns>
    private bool CheckPosition(int index)
    {
        if (index == _end.Posit) return false;  // End() не считается валидной позицией для операций
        int current = _start.Posit;
        while (current != -1)
        {
            if (current == index) return true;
            current = Nodes[current].Next;
        }
        return false;
    }
}