using Lab2.Queue.Array;
namespace Lab2.Program;

public class Program
{
    public static void Main()
    {
        Lab2.Stack.Tests.Array.RunTest();
        Lab2.Stack.Tests.ATDList.RunTest();
        Lab2.Stack.Tests.LinkedList.RunTest();
        Lab2.Queue.Tests.Array.RunTest();
        Lab2.Queue.Tests.ATDList.RunTest();
        Lab2.Queue.Tests.CircularLinkedList.RunTest();
        Lab2.Dictionary.Tests.LinkedList.RunTest();
    }
}