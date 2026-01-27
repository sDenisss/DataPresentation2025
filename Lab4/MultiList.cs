using Lab4.HashArrays;
using Lab4.Models;
using System.Text;

namespace Lab4.Models;

public class MultiList : IMultiList
{
    private readonly CourseHashArray courseHashArray;
    private readonly StudentHashArray studentHashArray;

    public MultiList()
    {
        courseHashArray = new CourseHashArray();
        studentHashArray = new StudentHashArray();
    }

    // addStudentToCourse(s, c) – добавить студента s на курс c
    public void AddStudentToCourse(char[] studentName, char[] courseName)
    {
        // Ищем студента
        Student? student = studentHashArray.FindStudent(studentName);
        if (student == null)
        {
            studentHashArray.AddStudent(studentName);
            student = studentHashArray.FindStudent(studentName);
        }

        // Ищем курс
        Course? course = courseHashArray.FindCourse(courseName);
        if (course == null)
        {
            courseHashArray.AddCourse(courseName);
            course = courseHashArray.FindCourse(courseName);
        }

        if (student == null || course == null)
            return;

        // Проверяем, не записан ли уже студент на этот курс
        if (FindEnrollment(student, course) != null)
            return;

        // Создаем новую связь
        Link link = new Link(null!, null!);

        // Добавляем в цепочку студента
        // Если у студента нет связей, link указывает на самого студента
        // Иначе link указывает на первую существующую связь
        if (student.FirstEnrollment == null)
            link.SetNextStudentLink(student);  // Ссылка на самого студента
        else
            link.SetNextStudentLink(student.FirstEnrollment);
        
        student.FirstEnrollment = link;

        // Добавляем в цепочку курса
        // Если у курса нет связей, link указывает на сам курс
        // Иначе link указывает на первую существующую связь
        if (course.FirstEnrollment == null)
            link.SetNextCourseLink(course);  // Ссылка на сам курс
        else
            link.SetNextCourseLink(course.FirstEnrollment);
        
        course.FirstEnrollment = link;
    }

    // removeStudentFromCourse(s, c) – удалить студента s c курса c
    public void RemoveStudentFromCourse(char[] studentName, char[] courseName)
    {
        Student? student = studentHashArray.FindStudent(studentName);
        if (student == null) return;

        Course? course = courseHashArray.FindCourse(courseName);
        if (course == null) return;

        RemoveEnrollment(student, course);
    }

    // ===== УДАЛЕНИЕ СВЯЗИ =====
    private void RemoveEnrollment(Student student, Course course)
    {
        // Находим регистрационную связь
        Link? registration = FindEnrollment(student, course);
        if (registration == null) return;

        // Удаляем из цепочки студента
        RemoveFromStudentChain(student, registration);
        
        // Удаляем из цепочки курса
        RemoveFromCourseChain(course, registration);
    }

    private void RemoveFromStudentChain(Student student, Link registration)
    {
        // Ищем предыдущую связь в цепочке студента
        Link? prev = null;
        Link? current = student.FirstEnrollment as Link;

        while (current != null)
        {
            if (current == registration)
            {
                // Нашли удаляемую связь
                if (prev == null)
                {
                    // Удаляем первую связь
                    if (registration.NextStudentLink is Student)
                        student.FirstEnrollment = null;  // Больше нет связей
                    else
                        student.FirstEnrollment = registration.NextStudentLink as Link;
                }
                else
                {
                    // Удаляем из середины/конца
                    prev.SetNextStudentLink(registration.NextStudentLink);
                }
                return;
            }

            // Переход к следующей связи
            Base? next = current.NextStudentLink;
            if (next is Student) break;  // Дошли до конца цепочки
            prev = current;
            current = next as Link;
        }
    }

    private void RemoveFromCourseChain(Course course, Link registration)
    {
        // Ищем предыдущую связь в цепочке курса
        Link? prev = null;
        Link? current = course.FirstEnrollment as Link;

        while (current != null)
        {
            if (current == registration)
            {
                // Нашли удаляемую связь
                if (prev == null)
                {
                    // Удаляем первую связь
                    if (registration.NextCourseLink is Course)
                        course.FirstEnrollment = null;  // Больше нет связей
                    else
                        course.FirstEnrollment = registration.NextCourseLink as Link;
                }
                else
                {
                    // Удаляем из середины/конца
                    prev.SetNextCourseLink(registration.NextCourseLink);
                }
                return;
            }

            // Переход к следующей связи
            Base? next = current.NextCourseLink;
            if (next is Course) break;  // Дошли до конца цепочки
            prev = current;
            current = next as Link;
        }
    }

    // removeStudent(s) – удалить студента s со всех курсов
    public void RemoveStudent(char[] studentName)
    {
        Student? student = studentHashArray.FindStudent(studentName);
        if (student == null) return;

        // Проходим по всем связям студента
        Link? link = student.FirstEnrollment as Link;
        while (link != null)
        {
            // Находим курс по цепочке
            Base? courseLink = link.NextCourseLink;
            while (courseLink != null && courseLink is Link)
            {
                courseLink = ((Link)courseLink).NextCourseLink;
            }

            if (courseLink is Course course)
            {
                // Удаляем связь с курса
                RemoveFromCourseChain(course, link);
            }

            // Переход к следующей связи студента
            Base? next = link.NextStudentLink;
            if (next is Student) break;
            link = next as Link;
        }

        // Очищаем все связи студента
        student.FirstEnrollment = null;
        
        // Удаляем студента из хеш-таблицы
        studentHashArray.RemoveStudent(studentName);
    }

    // removeCourse(c) – удалить всех студентов с курса c
    public void RemoveCourse(char[] courseName)
    {
        Course? course = courseHashArray.FindCourse(courseName);
        if (course == null) return;

        // Проходим по всем связям курса
        Link? link = course.FirstEnrollment as Link;
        while (link != null)
        {
            // Находим студента по цепочке
            Base? studentLink = link.NextStudentLink;
            while (studentLink != null && studentLink is Link)
            {
                studentLink = ((Link)studentLink).NextStudentLink;
            }

            if (studentLink is Student student)
            {
                // Удаляем связь со студента
                RemoveFromStudentChain(student, link);
            }

            // Переход к следующей связи курса
            Base? next = link.NextCourseLink;
            if (next is Course) break;
            link = next as Link;
        }

        // Очищаем все связи курса
        course.FirstEnrollment = null;
        
        // Удаляем курс из хеш-таблицы
        courseHashArray.RemoveCourse(courseName);
    }

    // printCoursesOfStudent(s) – вывести список курсов, посещаемых студентом s
    public void PrintCoursesOfStudent(char[] studentName)
    {
        Student? student = studentHashArray.FindStudent(studentName);
        if (student == null) return;

        Console.Write($"{CharArrayToString(studentName)}: ");

        Link? link = student.FirstEnrollment as Link;
        bool first = true;

        while (link != null)
        {
            // Ищем курс по цепочке
            Base? courseLink = link.NextCourseLink;
            while (courseLink != null && courseLink is Link)
            {
                courseLink = ((Link)courseLink).NextCourseLink;
            }

            if (courseLink is Course course)
            {
                if (!first) Console.Write(", ");
                Console.Write(CharArrayToString(course.Name));
                first = false;
            }

            // Переход к следующей связи
            Base? next = link.NextStudentLink;
            if (next is Student) break;
            link = next as Link;
        }

        Console.WriteLine();
    }

    // printStudentsOfCourse(c) – вывести список всех студентов посещающих курс c
    public void PrintStudentsOfCourse(char[] courseName)
    {
        Course? course = courseHashArray.FindCourse(courseName);
        if (course == null) return;

        Console.Write($"{CharArrayToString(courseName)}: ");

        Link? link = course.FirstEnrollment as Link;
        bool first = true;

        while (link != null)
        {
            // Ищем студента по цепочке
            Base? studentLink = link.NextStudentLink;
            while (studentLink != null && studentLink is Link)
            {
                studentLink = ((Link)studentLink).NextStudentLink;
            }

            if (studentLink is Student student)
            {
                if (!first) Console.Write(", ");
                Console.Write(CharArrayToString(student.Name));
                first = false;
            }

            // Переход к следующей связи
            Base? next = link.NextCourseLink;
            if (next is Course) break;
            link = next as Link;
        }

        Console.WriteLine();
    }

    // Добавление нового студента
    public void AddStudent(char[] name) => studentHashArray.AddStudent(name);

    // Добавление нового курса
    public void AddCourse(char[] name) => courseHashArray.AddCourse(name);

    // GetStudentCourses - возвращает массив названий курсов студента
    public char[][] GetStudentCourses(char[] studentName)
    {
        Student? student = studentHashArray.FindStudent(studentName);
        if (student == null) return Array.Empty<char[]>();

        List<char[]> courses = new List<char[]>();
        Link? link = student.FirstEnrollment as Link;

        while (link != null)
        {
            // Ищем курс по цепочке
            Base? courseLink = link.NextCourseLink;
            while (courseLink != null && courseLink is Link)
            {
                courseLink = ((Link)courseLink).NextCourseLink;
            }

            if (courseLink is Course course)
            {
                courses.Add(course.Name);
            }

            // Переход к следующей связи
            Base? next = link.NextStudentLink;
            if (next is Student) break;
            link = next as Link;
        }

        return courses.ToArray();
    }

    // GetCourseStudents - возвращает массив имен студентов курса
    public char[][] GetCourseStudents(char[] courseName)
    {
        Course? course = courseHashArray.FindCourse(courseName);
        if (course == null) return Array.Empty<char[]>();

        List<char[]> students = new List<char[]>();
        Link? link = course.FirstEnrollment as Link;

        while (link != null)
        {
            // Ищем студента по цепочке
            Base? studentLink = link.NextStudentLink;
            while (studentLink != null && studentLink is Link)
            {
                studentLink = ((Link)studentLink).NextStudentLink;
            }

            if (studentLink is Student student)
            {
                students.Add(student.Name);
            }

            // Переход к следующей связи
            Base? next = link.NextCourseLink;
            if (next is Course) break;
            link = next as Link;
        }

        return students.ToArray();
    }

    // Вывод всех связей "студент-курс"
    public void PrintAll()
    {
        Console.WriteLine("=== Все связи ===");

        Student?[] allStudents = studentHashArray.GetAllStudents();
        foreach (Student? student in allStudents)
        {
            if (student == null) continue;

            Link? link = student.FirstEnrollment as Link;
            while (link != null)
            {
                // Ищем курс по цепочке
                Base? courseLink = link.NextCourseLink;
                while (courseLink != null && courseLink is Link)
                {
                    courseLink = ((Link)courseLink).NextCourseLink;
                }

                if (courseLink is Course course)
                {
                    Console.WriteLine($"{CharArrayToString(student.Name)} -> {CharArrayToString(course.Name)}");
                }

                // Переход к следующей связи
                Base? next = link.NextStudentLink;
                if (next is Student) break;
                link = next as Link;
            }
        }
    }

    // ===== поиск конкретной связи между студентом и курсом =====
    private Link? FindEnrollment(Student student, Course course)
    {
        Link? link = student.FirstEnrollment as Link;

        while (link != null)
        {
            // Ищем курс по цепочке
            Base? courseLink = link.NextCourseLink;
            while (courseLink != null && courseLink is Link)
            {
                courseLink = ((Link)courseLink).NextCourseLink;
            }

            if (courseLink == course)
                return link;

            // Переход к следующей связи
            Base? next = link.NextStudentLink;
            if (next is Student) break;
            link = next as Link;
        }

        return null;
    }

    // Вспомогательный метод: преобразование char[] в string
    private string CharArrayToString(char[] array)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < array.Length && array[i] != '\0'; i++)
        {
            sb.Append(array[i]);
        }
        return sb.ToString();
    }
}