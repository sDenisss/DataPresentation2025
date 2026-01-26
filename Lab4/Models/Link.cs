namespace Lab4.Models;
public class Link : Base
{
    public Student? Student { get; set; }
    public Course? Course { get; set; }
    public Base? NextStudentLink { get; set; }
    public Base? NextCourseLink { get; set; }

    public Link(Base nextStudentLink, Base nextCourseLink)
    {
        NextStudentLink = nextStudentLink;
        NextCourseLink = nextCourseLink;
    }
    
    // Методы для установки связей
    internal void SetNextStudentLink(Link? link) => NextStudentLink = link;
    internal void SetNextCourseLink(Link? link) => NextCourseLink = link;

    public override bool IsHasNext { get => true; }
    
}