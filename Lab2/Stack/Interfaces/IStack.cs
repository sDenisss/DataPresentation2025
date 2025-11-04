namespace Lab2.Stack.Interfaces;

public interface IStack<T>
{
    void MakeNull();
    T Top();
    T Pop();
    void Push(T x);
    bool Empty();
    bool Full();
}