namespace Lab4.Models;
public class Student : Base
{
    public string Name { get; }
    public Link? FirstEnrollment { get; set; }

    public Student(string name)
    {
        Name = name;
    }
    
    public override string ToString() => Name;
}
