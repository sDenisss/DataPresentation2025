namespace Lab4.Models;
public class Student : Base
{
    public char[] Name { get; }
    public Link? FirstEnrollment { get; set; }

    public Student(char[] name)
    {
        Name = name;
    }
    
    public override bool IsHasNext { get => false; }
}
