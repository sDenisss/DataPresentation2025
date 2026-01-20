namespace Lab4.Models;
public class Course : Base
{
    public string Name { get; }
    public Link? FirstEnrollment { get; set; }

    public Course(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}