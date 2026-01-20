using Lab4.Models;
namespace Lab4.Program;
public class Program
{
    public static void Main()
    {
        var multiList = new MultiList();

        // Add students
        multiList.AddStudent("Alice");  
        multiList.AddStudent("Bob");
        multiList.AddStudent("Charlie");

        // Add courses
        multiList.AddCourse("Math");
        multiList.AddCourse("Physics");
        multiList.AddCourse("CS");

        // Enrollments - добавить студентов на курсы
        multiList.AddStudentToCourse("Alice", "Math");
        multiList.AddStudentToCourse("Alice", "CS");
        multiList.AddStudentToCourse("Bob", "Math");
        multiList.AddStudentToCourse("Bob", "Physics");
        multiList.AddStudentToCourse("Charlie", "CS");

        multiList.PrintAll();

        Console.WriteLine();

        // Remove one enrollment
        multiList.RemoveStudentFromCourse("Bob", "Math"); 

        // Remove student (should remove all his enrollments)
        multiList.RemoveStudent("Alice");

        // Remove course (should remove all enrollments to it)
        multiList.RemoveCourse("CS");

        Console.WriteLine();
        multiList.PrintAll();

        Console.WriteLine();

        // Используем новые методы для вывода
        Console.WriteLine("Students in Physics:");
        multiList.PrintStudentsOfCourse("Physics"); 
        
        Console.WriteLine("\nCourses of Bob:");
        multiList.PrintCoursesOfStudent("Bob");
    }
}