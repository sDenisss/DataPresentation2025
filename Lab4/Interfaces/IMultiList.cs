public interface IMultiList
{
    void AddStudentToCourse(char[] s, char[] c);
    void RemoveStudentFromCourse(char[] s, char[] c);
    void RemoveStudent(char[] s);
    void RemoveCourse(char[] c);
    void PrintStudentsOfCourse(char[] c);
    void PrintCoursesOfStudent(char[] s);
}