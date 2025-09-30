namespace Lab1.DoublyLinked;
public class Position<T>
{
    public Node<T>? Posit;
    public Position() {}
    public Position(Node<T>? posit)
    {
        Posit = posit;
    }
}