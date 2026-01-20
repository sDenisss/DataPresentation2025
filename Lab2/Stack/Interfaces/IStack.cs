namespace Lab2.Stack.Interfaces;

public interface IStack
{
    void MakeNull();
    char Top();
    char Pop();
    void Push(char x);
    bool Empty();
    bool Full();
}