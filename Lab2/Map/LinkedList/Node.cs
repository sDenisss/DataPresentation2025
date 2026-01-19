using Lab2.Program;
namespace Lab2.Map.LinkedList;

// Узел связного списка
public class Node<TKey, TValue>
{
    public Addressee Data;                     // Данные узла
    public Node<TKey, TValue> Next;            // Следующий узел

    // Конструктор с готовыми данными
    // public Node(Addressee data, Node<TKey, TValue> next)
    // {
    //     Data = data;
    //     Next = next;
    // }

    // Конструктор с массивами символов
    public Node(char[] name, char[] address, Node<TKey, TValue> next)
    {
        Data = new Addressee(name, address);
        Next = next;
    }
}