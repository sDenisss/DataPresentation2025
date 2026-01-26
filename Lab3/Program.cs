using Lab3.CloseHash;
using Lab3.OpenHash;
using Dictionary = Lab3.CloseHash.Dictionary;
// using Dictionary = Lab3.OpenHash.Dictionary;

namespace Lab3.Program;
public class Program
{
    Dictionary goodguys = new Dictionary();
    Dictionary badguys = new Dictionary();
    public static void Main()
    {
        Program program = new Program();
        program.Run();
    }

    private void Run()
    {
        Console.WriteLine("=== ТЕСТ СИСТЕМЫ ОЗТ ===\n");

        // 1. Базовые операции
        // F("Den");
        // F("Andrey Mironov Sergeevich");
        // U("Some bad guy");
        
        // Console.WriteLine("После добавления:");
        // Question("Den");  // Good
        // Question("Some bad guy");  // Bad
        // Question("Неизвестный");  // Неизвестый
        
        Console.WriteLine();

        // 2. Коллизии (имена с одинаковым хешем)
        F("ab");  // hash=57
        F("ba");  // hash=57 (коллизия!)
        
        Console.WriteLine("Коллизии:");
        Question("ab");
        Question("ba");
        Console.WriteLine();

        // 8. Финальные списки
        Console.WriteLine("Финальные списки:");
        P();

        Console.WriteLine("\nChanges:");
        Console.WriteLine("Переход good → bad:");
        U("ba");  // Сначала добавляем как good
        Question("ba");  // Good
        U("ab");         // Становится Bad
        Question("ab");  // Теперь Bad
        Console.WriteLine();
        P();
        
        Console.WriteLine("\n=== ТЕСТ ЗАВЕРШЕН ===");
    }
    private void F(string name)
    {
        goodguys.Insert(ToCharArray(name));
        badguys.Delete(ToCharArray(name));
    }
    private void U(string name)
    {
        badguys.Insert(ToCharArray(name));
        goodguys.Delete(ToCharArray(name));
    }
    private void Question(string name)
    {
        if (goodguys.Member(ToCharArray(name)))
            Console.WriteLine("законодатель хороший человек");
        else if (badguys.Member(ToCharArray(name)))
            Console.WriteLine("законодатель плохой человек");
        else
            Console.WriteLine("Не найден");
    }

    private void P()
    {
        System.Console.WriteLine("goodguys: ");
        goodguys.Print();
        
        System.Console.WriteLine("badguys: ");
        badguys.Print();
    }

    private void E()
    {
        Console.WriteLine("Конечный список: ");
        P();
        Console.WriteLine("End");
    }

    private static char[] ToCharArray(string name)
    {
        // Просто конвертируем строку в массив
        return name.ToCharArray();
    }
}