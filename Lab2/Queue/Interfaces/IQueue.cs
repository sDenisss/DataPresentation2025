namespace Lab2.Queue.Interfaces;

public interface IQueue
{
    void MakeNull();
    char Front();
    char Dequeue();
    void Enqueue(char x);
    bool Empty();
    bool Full();
}