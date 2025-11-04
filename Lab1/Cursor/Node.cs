namespace Lab1.Cursor;

public class Node<T>
{
    public T? Value { get; set; }
    public int Next { get; set; }

    public Node(){}
}