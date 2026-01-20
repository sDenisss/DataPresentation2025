public interface IMultiList
{
    void AddStudentToCourse(string s, string c);
    void RemoveStudentFromCourse(string s, string c);
    void RemoveStudent(string s);
    void RemoveCourse(string c);
    void PrintStudentsOfCourse(string c);
    void PrintCoursesOfStudent(string s);
}