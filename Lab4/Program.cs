using Lab4.Models;
namespace Lab4.Program;
public class Program
{
    public static void Main()
    {
        var multiList = new MultiList();

        char[] name1 = {'A', 'l', 'i', 'c', 'e'};
        char[] name2 = {'B', 'o', 'b'};
        char[] name3 = {'C', 'h', 'a', 'r', 'l', 'i', 'e'};

        char[] course1 = {'M', 'a', 't', 'h'};
        char[] course2 = {'P', 'h', 'y', 's'};
        char[] course3 = {'C', 'S'};

        // Add students
        multiList.AddStudent(name1);  
        multiList.AddStudent(name2);  
        multiList.AddStudent(name3);  

        // Add courses
        multiList.AddCourse(course1);
        multiList.AddCourse(course2);
        multiList.AddCourse(course3);


        // Enrollments - добавить студентов на курсы
        multiList.AddStudentToCourse(name1, course1);
        multiList.AddStudentToCourse(name1, course3);
        multiList.AddStudentToCourse(name2, course1);
        multiList.AddStudentToCourse(name2, course2);
        multiList.AddStudentToCourse(name3, course3);

        multiList.PrintAll();

        Console.WriteLine();

        // Remove one enrollment
        multiList.RemoveStudentFromCourse(name2, course1); 

        // Remove student (should remove all his enrollments)
        multiList.RemoveStudent(name1);

        // Remove course (should remove all enrollments to it)
        multiList.RemoveCourse(course3);

        Console.WriteLine();
        multiList.PrintAll();

        Console.WriteLine();

        // Используем новые методы для вывода
        Console.WriteLine("Students in Physics:");
        multiList.PrintStudentsOfCourse(course2); 
        
        Console.WriteLine("\nCourses of Bob:");
        multiList.PrintCoursesOfStudent(name2);
    }
}