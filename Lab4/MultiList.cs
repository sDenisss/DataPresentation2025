using Lab4.HashArrays;
using Lab4.Models;

namespace Lab4.Models;

public class MultiList : IMultiList
{
    // Хеш-таблица курсов (хранит объекты Course)
    private readonly CourseHashArray courseHashArray;
    // Хеш-таблица студентов (хранит объекты Student)
    private readonly StudentHashArray studentHashArray;

    public MultiList()
    {
        courseHashArray = new CourseHashArray();
        studentHashArray = new StudentHashArray();
    }

    // addStudentToCourse(s, c) – добавить студента s на курс c
    public void AddStudentToCourse(string studentName, string courseName)
    {
        // Ищем студента в хеш-таблице
        var student = studentHashArray.FindStudent(studentName);
        // Если студента нет - добавляем
        if (student == null)
        {
            studentHashArray.AddStudent(studentName);
            student = studentHashArray.FindStudent(studentName);
        }

        // Ищем курс в хеш-таблице
        var course = courseHashArray.FindCourse(courseName);
        // Если курса нет - добавляем
        if (course == null)
        {
            courseHashArray.AddCourse(courseName);
            course = courseHashArray.FindCourse(courseName);
        }

        // Проверка на null (защита от ошибок)
        if (student == null || course == null)
            return;

        // Проверяем, не записан ли уже студент на этот курс
        if (FindEnrollment(student, course) != null)
            return;

        // Создаем новую связь (Link) между студентом и курсом
        Link link = new Link(null!, null!);  // Временная пустая связь

        // Добавляем связь в список курсов студента (вставка в начало)
        link.SetNextStudentLink(student.FirstEnrollment);  // Новая связь указывает на старый первый элемент
        student.FirstEnrollment = link;                     // Студент теперь указывает на новую связь

        // Добавляем связь в список студентов курса (вставка в начало)
        link.SetNextCourseLink(course.FirstEnrollment);     // Новая связь указывает на старый первый элемент
        course.FirstEnrollment = link;                      // Курс теперь указывает на новую связь

        // Сохраняем владельцев связи (студента и курс)
        link.Student = student;  // Устанавливаем ссылку на студента
        link.Course = course;    // Устанавливаем ссылку на курс
    }

    // removeStudentFromCourse(s, c) – удалить студента s c курса c
    public void RemoveStudentFromCourse(string studentName, string courseName)
    {
        // Ищем студента по имени
        var student = studentHashArray.FindStudent(studentName);
        if (student == null) return;  // Студент не найден

        // Ищем курс по имени
        var course = courseHashArray.FindCourse(courseName);
        if (course == null) return;  // Курс не найден

        // Удаляем связь между студентом и курсом
        RemoveEnrollment(student, course);
    }

    // ===== ВНУТРЕННЕЕ УДАЛЕНИЕ СВЯЗИ =====
    private void RemoveEnrollment(Student student, Course course)
    {
        // --- удаляем из цепочки студента ---
        Base? prevS = null;              // Предыдущий элемент в списке студента
        Base? currS = student.FirstEnrollment;  // Текущий элемент (начинаем с первого)

        // Проходим по всем курсам студента
        while (currS != null)
        {
            // Проверяем, что это Link и нужный курс
            if (currS is Link link && link.Course == course)
            {
                // Если удаляем первый элемент
                if (prevS == null)
                    student.FirstEnrollment = link.NextStudentLink as Link;  // Первым становится следующий
                else if (prevS is Link prevLink)  // Если удаляем не первый
                    prevLink.SetNextStudentLink(link.NextStudentLink as Link);  // Пропускаем текущий

                break;  // Завершаем поиск
            }

            // Переходим к следующему элементу
            prevS = currS;
            currS = (currS as Link)?.NextStudentLink;
        }

        // Если связь не найдена - выходим
        if (currS == null) return;

        // --- удаляем из цепочки курса ---
        Base? prevC = null;              // Предыдущий элемент в списке курса
        Base? currC = course.FirstEnrollment;  // Текущий элемент (начинаем с первого)

        // Проходим по всем студентам курса
        while (currC != null)
        {
            // Проверяем, что это Link и нужный студент
            if (currC is Link link && link.Student == student)
            {
                // Если удаляем первый элемент
                if (prevC == null)
                    course.FirstEnrollment = link.NextCourseLink as Link;  // Первым становится следующий
                else if (prevC is Link prevLink)  // Если удаляем не первый
                    prevLink.SetNextCourseLink(link.NextCourseLink as Link);  // Пропускаем текущий

                return;  // Завершаем операцию
            }

            // Переходим к следующему элементу
            prevC = currC;
            currC = (currC as Link)?.NextCourseLink;
        }
    }

    // removeStudent(s) – удалить студента s со всех курсов
    public void RemoveStudent(string studentName)
    {
        // Ищем студента по имени
        var student = studentHashArray.FindStudent(studentName);
        if (student == null) return;  // Студент не найден

        // Проходим по всем курсам студента
        Base? link = student.FirstEnrollment;
        while (link != null)
        {
            // Сохраняем ссылку на следующую связь
            var next = (link as Link)?.NextStudentLink;
            
            // Если это Link - удаляем связь
            if (link is Link enrollmentLink)
                RemoveEnrollment(student, enrollmentLink.Course!);

            // Переходим к следующей связи
            link = next;
        }

        // Удаляем студента из хеш-таблицы
        studentHashArray.RemoveStudent(studentName);
    }

    // removeCourse(c) – удалить всех студентов с курса c
    public void RemoveCourse(string courseName)
    {
        // Ищем курс по имени
        var course = courseHashArray.FindCourse(courseName);
        if (course == null) return;  // Курс не найден

        // Проходим по всем студентам курса
        Base? link = course.FirstEnrollment;
        while (link != null)
        {
            // Сохраняем ссылку на следующую связь
            var next = (link as Link)?.NextCourseLink;
            
            // Если это Link - удаляем связь
            if (link is Link enrollmentLink)
                RemoveEnrollment(enrollmentLink.Student!, course);

            // Переходим к следующей связи
            link = next;
        }

        // Удаляем курс из хеш-таблицы
        courseHashArray.RemoveCourse(courseName);
    }

    // printCoursesOfStudent(s) – вывести список курсов, посещаемых студентом s
    public void PrintCoursesOfStudent(string studentName)
    {
        // Ищем студента по имени
        var student = studentHashArray.FindStudent(studentName);
        if (student == null) return;  // Студент не найден

        // Выводим имя студента
        Console.Write($"{studentName}: ");

        // Проходим по всем курсам студента
        Base? link = student.FirstEnrollment;
        bool first = true;  // Флаг первого элемента (для правильной расстановки запятых)

        while (link != null)
        {
            if (link is Link enrollmentLink)
            {
                // Добавляем запятую перед всеми элементами, кроме первого
                if (!first) Console.Write(", ");
                // Выводим название курса (char[] -> string)
                Console.Write(new string(enrollmentLink.Course!.Name));
                first = false;  // Первый элемент уже выведен
            }

            // Переходим к следующему курсу
            link = (link as Link)?.NextStudentLink;
        }

        // Переход на новую строку
        Console.WriteLine();
    }

    // printStudentsOfCourse(c) – вывести список всех студентов посещающих курс c
    public void PrintStudentsOfCourse(string courseName)
    {
        // Ищем курс по имени
        var course = courseHashArray.FindCourse(courseName);
        if (course == null) return;  // Курс не найден

        // Выводим название курса
        Console.Write($"{courseName}: ");

        // Проходим по всем студентам курса
        Base? link = course.FirstEnrollment;
        bool first = true;  // Флаг первого элемента

        while (link != null)
        {
            if (link is Link enrollmentLink)
            {
                // Добавляем запятую перед всеми элементами, кроме первого
                if (!first) Console.Write(", ");
                // Выводим имя студента (char[] -> string)
                Console.Write(new string(enrollmentLink.Student!.Name));
                first = false;  // Первый элемент уже выведен
            }

            // Переходим к следующему студенту
            link = (link as Link)?.NextCourseLink;
        }

        // Переход на новую строку
        Console.WriteLine();
    }

    // Добавление нового студента
    public void AddStudent(string name) => studentHashArray.AddStudent(name);

    // Добавление нового курса
    public void AddCourse(string name) => courseHashArray.AddCourse(name);

    // GetStudentCourses - возвращает массив названий курсов студента
    public string[] GetStudentCourses(string studentName)
    {
        // Ищем студента по имени
        var student = studentHashArray.FindStudent(studentName);
        if (student == null) return Array.Empty<string>();  // Студент не найден

        // Список для хранения названий курсов
        var temp = new List<string>();
        // Проходим по всем курсам студента
        Base? link = student.FirstEnrollment;

        while (link != null)
        {
            if (link is Link enrollmentLink)
                // Добавляем название курса в список
                temp.Add(new string(enrollmentLink.Course!.Name));

            // Переходим к следующему курсу
            link = (link as Link)?.NextStudentLink;
        }

        // Возвращаем массив названий курсов
        return temp.ToArray();
    }

    // GetCourseStudents - возвращает массив имен студентов курса
    public string[] GetCourseStudents(string courseName)
    {
        // Ищем курс по имени
        var course = courseHashArray.FindCourse(courseName);
        if (course == null) return Array.Empty<string>();  // Курс не найден

        // Список для хранения имен студентов
        var temp = new List<string>();
        // Проходим по всем студентам курса
        Base? link = course.FirstEnrollment;

        while (link != null)
        {
            if (link is Link enrollmentLink)
                // Добавляем имя студента в список
                temp.Add(new string(enrollmentLink.Student!.Name));

            // Переходим к следующему студенту
            link = (link as Link)?.NextCourseLink;
        }

        // Возвращаем массив имен студентов
        return temp.ToArray();
    }

    // Вывод всех связей "студент-курс"
    public void PrintAll()
    {
        Console.WriteLine("=== Все связи ===");

        // Получаем всех студентов из хеш-таблицы
        var allStudents = studentHashArray.GetAllStudents();
        // Для каждого студента
        foreach (var student in allStudents)
        {
            if (student == null) continue;  // Пропускаем пустые ячейки

            // Проходим по всем курсам студента
            Base? link = student.FirstEnrollment;
            while (link != null)
            {
                if (link is Link enrollmentLink)
                    // Выводим связь: студент -> курс
                    Console.WriteLine($"{new string(student.Name)} -> {new string(enrollmentLink.Course!.Name)}");

                // Переходим к следующему курсу
                link = (link as Link)?.NextStudentLink;
            }
        }
    }

    // ===== поиск конкретной связи между студентом и курсом =====
    private Link? FindEnrollment(Student student, Course course)
    {
        // Проходим по всем курсам студента
        Base? link = student.FirstEnrollment;
        while (link != null)
        {
            // Проверяем, что это Link и нужный курс
            if (link is Link enrollmentLink && enrollmentLink.Course == course)
                return enrollmentLink;  // Нашли нужную связь

            // Переходим к следующему курсу
            link = (link as Link)?.NextStudentLink;
        }

        return null;  // Связь не найдена
    }
}