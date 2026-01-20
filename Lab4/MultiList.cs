using Lab3.CloseHash;  // Используем готовую реализацию хеш-таблицы из 3-й лабораторной

namespace Lab4.Models;
public class MultiList : IMultiList
{
    // Размер хеш-таблицы (фиксированный)
    private const int Capacity = 5;
    
    // Две хеш-таблицы: для студентов и для курсов
    private readonly Student?[] _students = new Student?[Capacity];
    private readonly Course?[] _courses = new Course?[Capacity];

    // addStudentToCourse(s, c) – добавить студента s на курс c.
    public void AddStudentToCourse(string studentName, string courseName)
    {
        // Находим студента
        var student = FindStudent(studentName);

        // Находим курс
        var course = FindCourse(courseName);

        // Проверяем, не записан ли уже студент на этот курс
        if (FindEnrollment(student!, course!) != null) 
            return;

        // Создаем новую связь (узел мультисписка)
        var link = new Link(student!, course!);

        // Добавляем связь в список студента (вставка в начало)
        link.NextStudentLink = student!.FirstEnrollment;
        student.FirstEnrollment = link;

        // Добавляем связь в список курса (вставка в начало)
        link.NextCourseLink = course!.FirstEnrollment;
        course.FirstEnrollment = link;
    }

    // removeStudentFromCourse(s, c) – удалить студента s c курса c.
    public void RemoveStudentFromCourse(string studentName, string courseName)
    {
        // Проверяем существование студента
        var student = FindStudent(studentName);

        // Проверяем существование курса
        var course = FindCourse(courseName);

        // Удаляем связь между студентом и курсом
        RemoveEnrollment(student!, course!);
    }

    // Внутренний метод удаления связи между студентом и курсом
    private void RemoveEnrollment(Student student, Course course)
    {
        // Удаляем связь из списка курсов студента
        Link? prevS = null;                       // Предыдущий узел в списке студента
        var currS = student.FirstEnrollment;      // Начинаем с первого элемента

        // Проходим по списку курсов студента
        while (currS != null)
        {
            // Нашли нужный курс
            if (currS.Course == course)
            {
                // Удаляем узел из списка
                if (prevS == null)
                    student.FirstEnrollment = currS.NextStudentLink; // Удаляем первый элемент
                else
                    prevS.NextStudentLink = currS.NextStudentLink;   // Удаляем из середины/конца
                break;                           // Завершаем поиск в списке студента
            }

            // Переходим к следующему узлу
            prevS = currS;
            currS = currS.NextStudentLink;
        }

        // Если связь не найдена - исключение
        if (currS == null)
            return;  

        // Удаляем связь из списка студентов курса (симметричная операция)
        Link? prevC = null;                       // Предыдущий узел в списке курса
        var currC = course.FirstEnrollment;       // Начинаем с первого элемента

        while (currC != null)
        {
            // Нашли нужного студента
            if (currC.Student == student)
            {
                // Удаляем узел из списка
                if (prevC == null)
                    course.FirstEnrollment = currC.NextCourseLink; // Первый элемент
                else
                    prevC.NextCourseLink = currC.NextCourseLink;   // Не первый элемент
                return;                           // Завершаем операцию
            }

            // Следующий узел
            prevC = currC;
            currC = currC.NextCourseLink;
        }
    }

    // removeStudent(s) – удалить студента s со всех курсов.
    public void RemoveStudent(string studentName)
    {
        // Находим студента
        var student = FindStudent(studentName);

        // Удаляем все связи студента со всеми курсами
        var link = student!.FirstEnrollment;
        while (link != null)
        {
            var next = link.NextStudentLink;      // Сохраняем ссылку на следующий элемент
            RemoveEnrollment(student, link.Course); // Удаляем текущую связь
            link = next;                          // Переходим к следующей связи
        }

        // Удаляем студента из хеш-таблицы
        RemoveStudentFromTable(studentName);
    }

    // removeCourse(c) – удалить всех студентов с курса c.
    public void RemoveCourse(string courseName)
    {
        // Находим курс
        var course = FindCourse(courseName);

        // Удаляем всех студентов с курса
        var link = course!.FirstEnrollment;
        while (link != null)
        {
            var next = link.NextCourseLink;       // Сохраняем следующую связь
            RemoveEnrollment(link.Student, course); // Удаляем связь студента с курсом
            link = next;                          // Переходим дальше
        }

        // Удаляем курс из хеш-таблицы
        RemoveCourseFromTable(courseName);
    }

    // printCoursesOfStudent(s) – вывести список курсов, посещаемых студентом s.
    public void PrintCoursesOfStudent(string studentName)
    {
        // Ищем студента по имени в хеш-таблице
        var student = FindStudent(studentName);
        
        // Если студент не найден - завершаем выполнение метода
        // Это предотвращает обращение к null-ссылке в дальнейшем коде
        if (student == null) return;  

        // Выводим заголовок с именем студента
        Console.Write($"{studentName}: ");
        
        // Получаем первую связь студента с курсом
        // Первая связь ведет к первому курсу в списке студента
        var link = student.FirstEnrollment;
        
        // Если у студента нет ни одного курса
        if (link == null)
        {
            // Просто выводим пустую строку и завершаем метод
            Console.WriteLine();
            return;
        }

        // Проходим по всему списку курсов студента
        // Список организован как односвязный список через NextStudentLink
        while (link != null)
        {
            // Выводим название текущего курса
            Console.Write($"{link.Course.Name}");
            
            // Переходим к следующей связи (следующему курсу)
            link = link.NextStudentLink;
            
            // Если это не последний курс - добавляем разделитель
            if (link != null)
                Console.Write(", ");
        }
        
        // Переход на новую строку после вывода всех курсов
        Console.WriteLine();
    }

    // printStudentsOfCourse(c) – вывести список всех студентов посещающих курс c.
    public void PrintStudentsOfCourse(string courseName)
    {
        // Ищем курс по имени в хеш-таблице курсов
        var course = FindCourse(courseName);
        
        // Если курс не найден - завершаем выполнение
        if (course == null) return;  

        // Выводим заголовок с названием курса
        Console.Write($"{courseName}: ");
        
        // Получаем первую связь курса со студентом
        // Первая связь ведет к первому студенту в списке курса
        var link = course.FirstEnrollment;
        
        // Если на курсе нет ни одного студента
        if (link == null)
        {
            // Выводим пустую строку и завершаем
            Console.WriteLine();
            return;
        }

        // Проходим по всему списку студентов курса
        // Список организован как односвязный список через NextCourseLink
        while (link != null)
        {
            // Выводим имя текущего студента
            Console.Write($"{link.Student.Name}");
            
            // Переходим к следующей связи (следующему студенту)
            link = link.NextCourseLink;
            
            // Если это не последний студент - добавляем разделитель
            if (link != null)
                Console.Write(", ");
        }
        
        // Переход на новую строку после вывода всех студентов
        Console.WriteLine();
    }
        
    // // Объект словаря из Lab3
    // private readonly Dictionary _dictionary = new();

    // Хеш-функция: вычисляет индекс в таблице по строке
    private int Hash(string name)
    {
        int sum = 0;                              // Инициализируем сумму кодов символов
        
        // Проходим по всем символам строки до конца строки (до '\0')
        for (int i = 0; i < name.Length && name[i] != '\0'; i++)
            sum += name[i];                       // Суммируем ASCII-коды символов
        
        return sum % Capacity;                    // Возвращаем остаток от деления на размер таблицы
    }

    // Функция линейного пробирования: возвращает следующий индекс
    private int HashNext(int index)
    {
        return (index + 1) % Capacity;            // Переход к следующей ячейке по кругу
    }

    // -----------------------
    // Операции с таблицами
    // -----------------------

    // Попытка добавить студента в хеш-таблицу студентов
    private bool TryAddStudent(Student node)
    {
        int index = Hash(node.Name);              // Вычисляем начальный хеш для имени студента

        // Линейное пробирование: ищем свободную ячейку
        for (int i = 0; i < Capacity; i++)
        {
            if (_students[index] == null)
            {
                _students[index] = node;          // Нашли пустую ячейку - вставляем студента
                return true;                      // Успешно добавили
            }

            // Проверяем, нет ли уже студента с таким именем
            if (_students[index] != null &&
                _students[index]!.Name == node.Name)
            {
                // Студент уже существует - бросаем исключение
                return false;  
            }

            index = HashNext(index);              // Переходим к следующей ячейке
        }

        return false;                             // Не нашли свободной ячейки (таблица переполнена)
    }

    // Попытка добавить курс в хеш-таблицу курсов (аналогично TryAddStudent)
    private bool TryAddCourse(Course node)
    {
        int index = Hash(node.Name);              // Вычисляем хеш для названия курса

        for (int i = 0; i < Capacity; i++)
        {
            if (_courses[index] == null)
            {
                _courses[index] = node;           // Вставляем курс в пустую ячейку
                return true;                      // Успешно добавили
            }

            // Проверяем уникальность названия курса
            if (_courses[index] != null &&
                _courses[index]!.Name == node.Name)
            {
                return false;  
            }

            index = HashNext(index);              // Следующая ячейка
        }

        return false;                             // Таблица переполнена
    }

    // Поиск студента по имени в хеш-таблице
    private Student? FindStudent(string name)
    {
        int index = Hash(name);                   // Вычисляем начальный индекс

        // Линейное пробирование при поиске
        for (int i = 0; i < Capacity; i++)
        {
            var node = _students[index];          // Получаем элемент по индексу

            if (node == null)                     // Наткнулись на пустую ячейку
                return null;                      // Студент не найден

            // Проверка "node != null" избыточна - мы уже проверили выше
            if (node != null && node.Name == name)
                return node;                      // Нашли студента

            index = HashNext(index);              // Переходим дальше
        }

        return null;                              // Прошли всю таблицу - студента нет
    }

    // Поиск курса по имени (зеркально FindStudent)
    private Course? FindCourse(string name)
    {
        int index = Hash(name);                   // Начальный хеш для названия курса

        for (int i = 0; i < Capacity; i++)
        {
            var node = _courses[index];           // Элемент в текущей ячейке

            if (node == null)                     // Пустая ячейка - курс не найден
                return null;

            if (node != null && node.Name == name) // Избыточная проверка на null
                return node;                      // Нашли курс

            index = HashNext(index);              // Следующая позиция
        }

        return null;                              // Курс не найден
    }

    // Удаление студента из хеш-таблицы (логическое удаление - установка в null)
    private void RemoveStudentFromTable(string name)
    {
        int index = Hash(name);                   // Вычисляем индекс для удаления

        for (int i = 0; i < Capacity; i++)
        {
            if (_students[index] == null)         // Пустая ячейка - студент уже удален или не существовал
                return;

            // Нашли студента с нужным именем
            if (_students[index] != null && _students[index]!.Name == name)
            {
                _students[index] = null;          // Удаляем ссылку (логическое удаление)
                return;                           // Завершаем поиск
            }

            index = HashNext(index);              // Продолжаем поиск при коллизиях
        }
    }

    // Удаление курса из хеш-таблицы (аналогично RemoveStudentFromTable)
    private void RemoveCourseFromTable(string name)
    {
        int index = Hash(name);                   // Хеш для названия курса

        for (int i = 0; i < Capacity; i++)
        {
            if (_courses[index] == null)          // Курс не найден
                return;

            if (_courses[index] != null && _courses[index]!.Name == name)
            {
                _courses[index] = null;           // Удаляем курс
                return;                           // Выходим
            }

            index = HashNext(index);              // Продолжаем линейное пробирование
        }
    }

    // -----------------------
    // Публичный интерфейс
    // -----------------------

    // Добавление нового студента (публичный метод)
    public void AddStudent(string name)
    {
        // Проверяем, нет ли уже студента с таким именем
        if (FindStudent(name) != null)
            return;

        // Создаем и добавляем студента
        TryAddStudent(new Student(name));
    }

    // Добавление нового курса (публичный метод)
    public void AddCourse(string name)
    {
        // Проверка существования курса
        if (FindCourse(name) != null)
            return;

        // Создаем и добавляем курс
        TryAddCourse(new Course(name));
    }

    // GetStudentCourses - возвращает массив названий курсов студента
    public string[] GetStudentCourses(string studentName)
    {
        // Ищем студента по имени
        var student = FindStudent(studentName);
        
        // Если студент не найден - возвращаем пустой массив
        // Это безопасная альтернатива возврату null
        if (student == null) return Array.Empty<string>();  

        // Подсчитываем количество курсов у студента
        // Нужно для создания массива правильного размера
        int count = 0;
        var temp = student.FirstEnrollment;
        while (temp != null)
        {
            count++;  // Увеличиваем счетчик
            temp = temp.NextStudentLink;  // Переход к следующему курсу
        }

        // Создаем массив строк для хранения названий курсов
        // Размер массива равен количеству курсов
        var result = new string[count];
        
        // Снова проходим по списку курсов, начиная с первого
        int i = 0;
        temp = student.FirstEnrollment;
        
        // Заполняем массив названиями курсов
        while (temp != null)
        {
            // Добавляем название курса в массив
            result[i++] = temp.Course.Name;
            
            // Переход к следующему курсу
            temp = temp.NextStudentLink;
        }

        // Возвращаем заполненный массив
        return result;
    }

    // GetCourseStudents - возвращает массив имен студентов курса
    public string[] GetCourseStudents(string courseName)
    {
        // Ищем курс по имени
        var course = FindCourse(courseName);
        
        // Если курс не найден - возвращаем пустой массив
        if (course == null) return Array.Empty<string>();  

        // Подсчитываем количество студентов на курсе
        int count = 0;
        var temp = course.FirstEnrollment;
        while (temp != null)
        {
            count++;  // Считаем студентов
            temp = temp.NextCourseLink;  // Переход к следующему студенту
        }

        // Создаем массив для хранения имен студентов
        var result = new string[count];
        
        // Заполняем массив именами студентов
        int i = 0;
        temp = course.FirstEnrollment;
        while (temp != null)
        {
            // Добавляем имя студента в массив
            result[i++] = temp.Student.Name;
            
            // Переход к следующему студенту
            temp = temp.NextCourseLink;
        }

        // Возвращаем массив с именами студентов
        return result;
    }

    // Вывод всех связей "студент-курс"
    public void PrintAll()
    {
        Console.WriteLine("=== All Enrollments ===");

        // Проходим по всем ячейкам таблицы студентов
        for (int i = 0; i < Capacity; i++)
        {
            var student = _students[i];

            // Пропускаем пустые ячейки
            if (student == null)
                continue;

            // Выводим все курсы данного студента
            var link = student.FirstEnrollment;
            while (link != null)
            {
                Console.WriteLine($"{student.Name} -> {link.Course.Name}"); // Связь
                link = link.NextStudentLink;       // Следующий курс студента
            }
        }
    }

    // Поиск конкретной связи между студентом и курсом
    private Link? FindEnrollment(Student student, Course course)
    {
        // Проходим по всем курсам студента
        var link = student.FirstEnrollment;
        while (link != null)
        {
            if (link.Course == course)            // Нашли нужный курс
                return link;                      // Возвращаем связь

            link = link.NextStudentLink;          // Переходим к следующему курсу
        }

        return null;                              // Связь не найдена
    }
}