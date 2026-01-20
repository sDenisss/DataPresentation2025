using Lab1.Interfaces;

namespace Lab1.DoublyLinked;
public class Position<T> : IPosition
{
    public Node<T>? Posit;
    public Position() {}
    public Position(Node<T>? posit)
    {
        Posit = posit;
    }

    // public override bool Equals(object? obj)
    // {
    //     if (obj is null) return false;
        
    //     if (ReferenceEquals(this, obj)) return true;
        
    //     if (this.GetType() != obj.GetType()) return false;
        
    //     Position<T> other = (Position<T>)obj;
        
    //     if (Posit is null && other.Posit is null) return true;
    //     if (Posit is null || other.Posit is null) return false;
        
    //     return Posit == other.Posit;
    // }
}