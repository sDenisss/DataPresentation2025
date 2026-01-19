using Lab3.CloseHash;
using Lab3.OpenHash;
// using Dictionary = Lab3.CloseHash.Dictionary;
using Dictionary = Lab3.OpenHash.Dictionary;

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
        F("Den");
        F("Andrey Mironov Sergeevich");
        U("Some bad guy");
        
        Console.WriteLine("После добавления:");
        Question("Den");  // Good
        Question("Some bad guy");  // Bad
        Question("Неизвестный");  // Неизвестый
        
        Console.WriteLine();

        // 2. Коллизии (имена с одинаковым хешем)
        F("ab");  // hash=57
        F("ba");  // hash=57 (коллизия!)
        
        Console.WriteLine("Коллизии:");
        Question("ab");
        Question("ba");
        Console.WriteLine();

        // 3. Переход между списками - сначала добавляем, потом меняем
        Console.WriteLine("Переход good → bad:");
        F("Иванов");  // Сначала добавляем как good
        Question("Иванов");  // Good
        U("Иванов");         // Становится Bad
        Question("Иванов");  // Теперь Bad
        Console.WriteLine();

        // 4. Конфликт: один в обоих списках
        F("Конфликтный");
        U("Конфликтный");  // Должен переместиться в bad
        
        Console.WriteLine("Конфликтный случай:");
        Question("Конфликтный");  // Должен быть только в bad
        Console.WriteLine();

        // 5. Дубликаты (многократное добавление) - должно остаться один раз
        F("Дубль");
        F("Дубль");
        F("Дубль");
        
        Console.WriteLine("После трёх F(Дубль):");
        Question("Дубль");
        Console.WriteLine();

        // 6. Граничные случаи
        F("");  // Пустое имя
        F("ОченьДлинноеИмяБольшеДесятиСимволов");
        
        Console.WriteLine("Граничные случаи:");
        Question("");
        Question("ОченьДлинноеИмяБольшеДесятиСимволов");
        Console.WriteLine();

        // 7. Тест что bad может стать good
        Console.WriteLine("Переход bad → good:");
        U("БывшийПлохой");
        Question("БывшийПлохой");  // Bad
        F("БывшийПлохой");         // Становится Good
        Question("БывшийПлохой");  // Теперь Good
        Console.WriteLine();

        // 8. Финальные списки
        Console.WriteLine("Финальные списки:");
        P();

        // 9. Очистка
        goodguys.Makenull();
        badguys.Makenull();
        
        Console.WriteLine("После очистки:");
        P();
        Question("Den");  // Должен быть "Не найден" после очистки
        
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