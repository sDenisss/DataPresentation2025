namespace Lab2.Stack.ATDList;

public class Node
{
    public char? Value { get; set; }
    public Node? Next { get; set; }
    public Node? Previous { get; set; }

    public Node() { }
    public Node(char? value, Node? next, Node? previous)
    {
        Value = value;
        Next = next;
        Previous = previous;
    }
}