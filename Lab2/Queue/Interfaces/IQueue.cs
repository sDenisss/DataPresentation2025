namespace Lab2.Queue.Interfaces;

public interface IQueue<T>
{
    void MakeNull();
    T Front();
    T Dequeue();
    void Enqueue(T x);
    bool Empty();
    bool Full();
}