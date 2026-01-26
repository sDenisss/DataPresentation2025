namespace Lab4.Models;
public class Course : Base
{
    public char[] Name { get; set; }
    public Link? FirstEnrollment { get; set; }

    public Course(char[] name)
    {
        Name = name;
    }

    public override bool IsHasNext { get => false; }
}