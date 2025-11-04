namespace Lab2.Dictionary.LinkedList;

public class Node<TKey, TValue>
{
    public TKey? Key { get; set; }
    public TValue? Value { get; set; }
    public Node<TKey, TValue>? Next { get; set; }

    public Node() { }
    public Node(TKey? key, TValue? value, Node<TKey, TValue>? next = null)
    {
        Key = key;
        Value = value;
        Next = next;
    }
}