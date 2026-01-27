using Lab4.HashArrays;
using Lab4.Models;
using System.Text;

namespace Lab4.Models;

public class MultiList : IMultiList
{
    // Хеш-таблица для хранения курсов
    private readonly CourseHashArray courseHashArray;
    // Хеш-таблица для хранения студентов
    private readonly StudentHashArray studentHashArray;

    // Конструктор, инициализирует хеш-таблицы
    public MultiList()
    {
        courseHashArray = new CourseHashArray();
        studentHashArray = new StudentHashArray();
    }

    // addStudentToCourse(s, c) – добавить студента s на курс c
    public void AddStudentToCourse(char[] studentName, char[] courseName)
    {
        // Поиск студента в хеш-таблице
        Student? student = studentHashArray.FindStudent(studentName);
        // Если студент не найден - добавляем его
        if (student == null)
        {
            studentHashArray.AddStudent(studentName);
            student = studentHashArray.FindStudent(studentName);
        }

        // Поиск курса в хеш-таблице
        Course? course = courseHashArray.FindCourse(courseName);
        // Если курс не найден - добавляем его
        if (course == null)
        {
            courseHashArray.AddCourse(courseName);
            course = courseHashArray.FindCourse(courseName);
        }

        // Проверка на null после добавления
        if (student == null || course == null)
            return;

        // Проверка: студент уже записан на этот курс?
        if (FindEnrollment(student, course) != null)
            return;

        // Создание новой связи Link между студентом и курсом
        Link link = new Link(null!, null!);

        // Добавление связи в список студента (вставка в начало)
        if (student.FirstEnrollment == null)
            link.SetNextStudentLink(student);  // Если список пуст - ссылка на самого студента
        else
            link.SetNextStudentLink(student.FirstEnrollment);  // Иначе ссылка на предыдущий первый элемент
        
        student.FirstEnrollment = link;  // Новый элемент становится первым

        // Добавление связи в список курса (вставка в начало)
        if (course.FirstEnrollment == null)
            link.SetNextCourseLink(course);  // Если список пуст - ссылка на сам курс
        else
            link.SetNextCourseLink(course.FirstEnrollment);  // Иначе ссылка на предыдущий первый элемент
        
        course.FirstEnrollment = link;  // Новый элемент становится первым
    }

    // removeStudentFromCourse(s, c) – удалить студента s c курса c
    public void RemoveStudentFromCourse(char[] studentName, char[] courseName)
    {
        // Поиск студента по имени
        Student? student = studentHashArray.FindStudent(studentName);
        if (student == null) return;  // Студент не найден

        // Поиск курса по имени
        Course? course = courseHashArray.FindCourse(courseName);
        if (course == null) return;  // Курс не найден

        // Удаление связи между студентом и курсом
        RemoveEnrollment(student, course);
    }

    // ===== УДАЛЕНИЕ СВЯЗИ =====
    private void RemoveEnrollment(Student student, Course course)
    {
        // Поиск регистрационной связи
        Link? registration = FindEnrollment(student, course);
        if (registration == null) return;  // Связь не найдена

        // Удаление из цепочки студента
        RemoveFromStudentChain(student, registration);
        
        // Удаление из цепочки курса
        RemoveFromCourseChain(course, registration);
    }

    // Удаление связи из цепочки студента
    private void RemoveFromStudentChain(Student student, Link registration)
    {
        // Поиск предыдущей связи в цепочке
        Link? prev = null;
        Link? current = student.FirstEnrollment as Link;

        // Проход по цепочке студента
        while (current != null)
        {
            if (current == registration)  // Нашли удаляемую связь
            {
                if (prev == null)  // Удаляем первый элемент
                {
                    // Если следующий элемент - сам студент, значит цепочка пуста
                    if (registration.NextStudentLink is Student)
                        student.FirstEnrollment = null;  // Очищаем ссылку
                    else
                        student.FirstEnrollment = registration.NextStudentLink as Link;  // Следующий становится первым
                }
                else  // Удаляем не первый элемент
                {
                    // Пропускаем удаляемую связь
                    prev.SetNextStudentLink(registration.NextStudentLink);
                }
                return;  // Завершаем удаление
            }

            // Переход к следующему элементу цепочки
            Base? next = current.NextStudentLink;
            if (next is Student) break;  // Достигли конца цепочки
            prev = current;
            current = next as Link;
        }
    }

    // Удаление связи из цепочки курса
    private void RemoveFromCourseChain(Course course, Link registration)
    {
        // Поиск предыдущей связи в цепочке
        Link? prev = null;
        Link? current = course.FirstEnrollment as Link;

        // Проход по цепочке курса
        while (current != null)
        {
            if (current == registration)  // Нашли удаляемую связь
            {
                if (prev == null)  // Удаляем первый элемент
                {
                    // Если следующий элемент - сам курс, значит цепочка пуста
                    if (registration.NextCourseLink is Course)
                        course.FirstEnrollment = null;  // Очищаем ссылку
                    else
                        course.FirstEnrollment = registration.NextCourseLink as Link;  // Следующий становится первым
                }
                else  // Удаляем не первый элемент
                {
                    // Пропускаем удаляемую связь
                    prev.SetNextCourseLink(registration.NextCourseLink);
                }
                return;  // Завершаем удаление
            }

            // Переход к следующему элементу цепочки
            Base? next = current.NextCourseLink;
            if (next is Course) break;  // Достигли конца цепочки
            prev = current;
            current = next as Link;
        }
    }

    // removeStudent(s) – удалить студента s со всех курсов
    public void RemoveStudent(char[] studentName)
    {
        // Поиск студента
        Student? student = studentHashArray.FindStudent(studentName);
        if (student == null) return;  // Студент не найден

        // Проход по всем связям студента
        Link? link = student.FirstEnrollment as Link;
        while (link != null)
        {
            // Поиск курса через цепочку связей
            Base? courseLink = link.NextCourseLink;
            while (courseLink != null && courseLink is Link)
            {
                courseLink = ((Link)courseLink).NextCourseLink;
            }

            // Если нашли курс - удаляем связь с его стороны
            if (courseLink is Course course)
            {
                RemoveFromCourseChain(course, link);
            }

            // Переход к следующей связи студента
            Base? next = link.NextStudentLink;
            if (next is Student) break;  // Достигли конца цепочки
            link = next as Link;
        }

        // Очистка всех связей студента
        student.FirstEnrollment = null;
        
        // Удаление студента из хеш-таблицы
        studentHashArray.RemoveStudent(studentName);
    }

    // removeCourse(c) – удалить всех студентов с курса c
    public void RemoveCourse(char[] courseName)
    {
        // Поиск курса
        Course? course = courseHashArray.FindCourse(courseName);
        if (course == null) return;  // Курс не найден

        // Проход по всем связям курса
        Link? link = course.FirstEnrollment as Link;
        while (link != null)
        {
            // Поиск студента через цепочку связей
            Base? studentLink = link.NextStudentLink;
            while (studentLink != null && studentLink is Link)
            {
                studentLink = ((Link)studentLink).NextStudentLink;
            }

            // Если нашли студента - удаляем связь с его стороны
            if (studentLink is Student student)
            {
                RemoveFromStudentChain(student, link);
            }

            // Переход к следующей связи курса
            Base? next = link.NextCourseLink;
            if (next is Course) break;  // Достигли конца цепочки
            link = next as Link;
        }

        // Очистка всех связей курса
        course.FirstEnrollment = null;
        
        // Удаление курса из хеш-таблицы
        courseHashArray.RemoveCourse(courseName);
    }

    // printCoursesOfStudent(s) – вывести список курсов, посещаемых студентом s
    public void PrintCoursesOfStudent(char[] studentName)
    {
        // Поиск студента
        Student? student = studentHashArray.FindStudent(studentName);
        if (student == null) return;  // Студент не найден

        // Вывод заголовка с именем студента
        Console.Write($"{CharArrayToString(studentName)}: ");

        // Начало цепочки связей студента
        Link? link = student.FirstEnrollment as Link;
        bool first = true;  // Флаг для правильной расстановки запятых

        // Проход по всем связям студента
        while (link != null)
        {
            // Поиск курса через цепочку связей
            Base? courseLink = link.NextCourseLink;
            while (courseLink != null && courseLink is Link)
            {
                courseLink = ((Link)courseLink).NextCourseLink;
            }

            // Если нашли курс - выводим его название
            if (courseLink is Course course)
            {
                if (!first) Console.Write(", ");  // Запятая перед всеми, кроме первого
                Console.Write(CharArrayToString(course.Name));
                first = false;  // Первый элемент выведен
            }

            // Переход к следующей связи
            Base? next = link.NextStudentLink;
            if (next is Student) break;  // Достигли конца цепочки
            link = next as Link;
        }

        Console.WriteLine();  // Переход на новую строку
    }

    // printStudentsOfCourse(c) – вывести список всех студентов посещающих курс c
    public void PrintStudentsOfCourse(char[] courseName)
    {
        // Поиск курса
        Course? course = courseHashArray.FindCourse(courseName);
        if (course == null) return;  // Курс не найден

        // Вывод заголовка с названием курса
        Console.Write($"{CharArrayToString(courseName)}: ");

        // Начало цепочки связей курса
        Link? link = course.FirstEnrollment as Link;
        bool first = true;  // Флаг для правильной расстановки запятых

        // Проход по всем связям курса
        while (link != null)
        {
            // Поиск студента через цепочку связей
            Base? studentLink = link.NextStudentLink;
            while (studentLink != null && studentLink is Link)
            {
                studentLink = ((Link)studentLink).NextStudentLink;
            }

            // Если нашли студента - выводим его имя
            if (studentLink is Student student)
            {
                if (!first) Console.Write(", ");  // Запятая перед всеми, кроме первого
                Console.Write(CharArrayToString(student.Name));
                first = false;  // Первый элемент выведен
            }

            // Переход к следующей связи
            Base? next = link.NextCourseLink;
            if (next is Course) break;  // Достигли конца цепочки
            link = next as Link;
        }

        Console.WriteLine();  // Переход на новую строку
    }

    // Добавление нового студента
    public void AddStudent(char[] name) => studentHashArray.AddStudent(name);

    // Добавление нового курса
    public void AddCourse(char[] name) => courseHashArray.AddCourse(name);

    // GetStudentCourses - возвращает массив названий курсов студента
    public char[][] GetStudentCourses(char[] studentName)
    {
        // Поиск студента
        Student? student = studentHashArray.FindStudent(studentName);
        if (student == null) return Array.Empty<char[]>();  // Студент не найден

        // Список для хранения названий курсов
        List<char[]> courses = new List<char[]>();
        Link? link = student.FirstEnrollment as Link;

        // Проход по всем связям студента
        while (link != null)
        {
            // Поиск курса через цепочку связей
            Base? courseLink = link.NextCourseLink;
            while (courseLink != null && courseLink is Link)
            {
                courseLink = ((Link)courseLink).NextCourseLink;
            }

            // Если нашли курс - добавляем его название в список
            if (courseLink is Course course)
            {
                courses.Add(course.Name);
            }

            // Переход к следующей связи
            Base? next = link.NextStudentLink;
            if (next is Student) break;  // Достигли конца цепочки
            link = next as Link;
        }

        return courses.ToArray();  // Возвращаем массив курсов
    }

    // GetCourseStudents - возвращает массив имен студентов курса
    public char[][] GetCourseStudents(char[] courseName)
    {
        // Поиск курса
        Course? course = courseHashArray.FindCourse(courseName);
        if (course == null) return Array.Empty<char[]>();  // Курс не найден

        // Список для хранения имен студентов
        List<char[]> students = new List<char[]>();
        Link? link = course.FirstEnrollment as Link;

        // Проход по всем связям курса
        while (link != null)
        {
            // Поиск студента через цепочку связей
            Base? studentLink = link.NextStudentLink;
            while (studentLink != null && studentLink is Link)
            {
                studentLink = ((Link)studentLink).NextStudentLink;
            }

            // Если нашли студента - добавляем его имя в список
            if (studentLink is Student student)
            {
                students.Add(student.Name);
            }

            // Переход к следующей связи
            Base? next = link.NextCourseLink;
            if (next is Course) break;  // Достигли конца цепочки
            link = next as Link;
        }

        return students.ToArray();  // Возвращаем массив студентов
    }

    // Вывод всех связей "студент-курс"
    public void PrintAll()
    {
        Console.WriteLine("=== Все связи ===");

        // Получение всех студентов из хеш-таблицы
        Student?[] allStudents = studentHashArray.GetAllStudents();
        
        // Проход по всем студентам
        foreach (Student? student in allStudents)
        {
            if (student == null) continue;  // Пропуск пустых ячеек

            // Проход по всем связям студента
            Link? link = student.FirstEnrollment as Link;
            while (link != null)
            {
                // Поиск курса через цепочку связей
                Base? courseLink = link.NextCourseLink;
                while (courseLink != null && courseLink is Link)
                {
                    courseLink = ((Link)courseLink).NextCourseLink;
                }

                // Если нашли курс - выводим связь
                if (courseLink is Course course)
                {
                    Console.WriteLine($"{CharArrayToString(student.Name)} -> {CharArrayToString(course.Name)}");
                }

                // Переход к следующей связи
                Base? next = link.NextStudentLink;
                if (next is Student) break;  // Достигли конца цепочки
                link = next as Link;
            }
        }
    }

    // ===== поиск конкретной связи между студентом и курсом =====
    private Link? FindEnrollment(Student student, Course course)
    {
        // Начало цепочки связей студента
        Link? link = student.FirstEnrollment as Link;

        // Проход по всем связям студента
        while (link != null)
        {
            // Поиск курса через цепочку связей
            Base? courseLink = link.NextCourseLink;
            while (courseLink != null && courseLink is Link)
            {
                courseLink = ((Link)courseLink).NextCourseLink;
            }

            // Если нашли нужный курс - возвращаем связь
            if (courseLink == course)
                return link;

            // Переход к следующей связи
            Base? next = link.NextStudentLink;
            if (next is Student) break;  // Достигли конца цепочки
            link = next as Link;
        }

        return null;  // Связь не найдена
    }

    // Вспомогательный метод: преобразование char[] в string
    private string CharArrayToString(char[] array)
    {
        StringBuilder sb = new StringBuilder();
        // Копирование символов до конца строки или конца массива
        for (int i = 0; i < array.Length && array[i] != '\0'; i++)
        {
            sb.Append(array[i]);
        }
        return sb.ToString();
    }
}
