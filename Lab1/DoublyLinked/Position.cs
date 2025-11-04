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
}