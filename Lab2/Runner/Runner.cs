public class Runner
{
    public static void RunTests2()
    {
        
    }
    public static void RunTests()
    {
        Console.WriteLine("1. Stack Array");
        Console.WriteLine("2. Stack ATD");
        Console.WriteLine("3. Stack LinkedList");
        Console.WriteLine("4. Queue Array");
        Console.WriteLine("5. Queue ATD");
        Console.WriteLine("6. Queue CircularLinkedList");
        Console.WriteLine("7. Map LinkedList");
        Console.WriteLine("8. RunAllTests");
        Console.WriteLine("9. Exit");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                Lab2.Stack.Tests.Array.RunTest();
                break;
                
            case "2":
                Lab2.Stack.Tests.ATDList.RunTest();
                break;
                
            case "3":
                Lab2.Stack.Tests.LinkedList.RunTest();
                break;
                
            case "4":
                Lab2.Queue.Tests.Array.RunTest();
                break;
                
            case "5":
                Lab2.Queue.Tests.ATDList.RunTest();
                break;
                
            case "6":
                Lab2.Queue.Tests.CircularLinkedList.RunTest();
                break;
                
            case "7":
                Lab2.Map.Tests.LinkedList.RunTest();
                break;
                
            case "8":
                RunAllTests();
                return;

            case "9":
                Console.WriteLine("Exiting...");
                return;
                
            default:
                Console.WriteLine("Invalid choice! Please enter a number from 1 to 9.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                RunTests(); // Recursive call to show menu again
                break;
        }

        // After running tests, ask if user wants to run more tests
        Console.WriteLine("\n\nPress any key to return to menu...");
        Console.ReadKey();
        Console.Clear();
        RunTests();
    }

    private static void RunAllTests()
    {
        Lab2.Stack.Tests.Array.RunTest();
        Lab2.Stack.Tests.ATDList.RunTest();
        Lab2.Stack.Tests.LinkedList.RunTest();
        Lab2.Queue.Tests.Array.RunTest();
        Lab2.Queue.Tests.ATDList.RunTest();
        Lab2.Queue.Tests.CircularLinkedList.RunTest();
        Lab2.Map.Tests.LinkedList.RunTest();
    }
}