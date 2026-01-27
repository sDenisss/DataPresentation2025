namespace Lab4.Models;
public class Link : Base
{
    public Base? NextStudentLink { get; set; }
    public Base? NextCourseLink { get; set; }

    public Link(Base nextStudentLink, Base nextCourseLink)
    {
        NextStudentLink = nextStudentLink;
        NextCourseLink = nextCourseLink;
    }
    
    // Методы для установки связей
    internal void SetNextStudentLink(Base? link) => NextStudentLink = link;
    internal void SetNextCourseLink(Base? link) => NextCourseLink = link;

    public override bool IsHasNext { get => true; }
    
}