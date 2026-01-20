namespace Lab4.Models;
public class Link : Base
{
    public Student Student { get; }
    public Course Course { get; }

    public Link? NextStudentLink { get; set; }
    public Link? NextCourseLink { get; set; }

    public Link(Student student, Course course)
    {
        Student = student;
        Course = course;
    }

    public override string ToString() => ($"{Student.Name} -> {Course.Name}");
    
}