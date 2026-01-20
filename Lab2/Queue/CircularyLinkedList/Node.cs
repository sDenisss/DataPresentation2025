namespace Lab2.Queue.CircularyLinkedList;

public class Node
{
    public char Value { get; set; }
    public Node Next { get; set; }

    public Node() { }
    public Node(char value, Node next)
    {
        Value = value;
        Next = next;
    }
}