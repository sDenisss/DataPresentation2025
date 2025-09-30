namespace Lab1;

using Lab1.Cursor;
using Lab1.DoublyLinked;
// using CursorList = Lab1.Cursor.List<Addressee>;
// using DoublyLinked = Lab1.DoublyLinked.List<Addressee>;
using SomeList = Cursor.List<Addressee>;
using SomePosition = Lab1.Cursor.Position;

public class Program
{
    public static void Main()
    {
        SomeList list = new SomeList();
        WriteInList(list);
        list.PrintList();

        DeleteDuplicates(list);
        list.PrintList();

    }

    public static void WriteInList(SomeList list)
    {
        list.Insert(new Addressee("Анна Ковалева", "Москва, ул. Пушкина 15"), list.End());
        list.Insert(new Addressee("Андрей Миронов", "Санкт-Петербург, пр. Кронверкский 49"), list.End());
        list.Insert(new Addressee("Савин Денис", "Санкт-Петербург, пер. Вяземский 5-7"), list.First()); // first
        list.Insert(new Addressee("Савин Денис", "Санкт-Петербург, пер. Вяземский 5-7"), list.End()); // дубликат
        list.Insert(new Addressee("Дмитрий Соколов", "Санкт-Петербург, Невский пр. 25"), list.End());
        list.Insert(new Addressee("Елена Васнецова", "Казань, ул. Баумана 10"), list.End());
        list.Insert(new Addressee("Анна Ковалева", "Москва, ул. Пушкина 15"), list.End());  // дубликат
        list.Insert(new Addressee("Савин Денис", "Санкт-Петербург, пер. Вяземский 5-7"), list.End()); // дубликат
        list.Insert(new Addressee("Сергей Орлов", "Екатеринбург, ул. Мира 8"), list.End());
        list.Insert(new Addressee("Дмитрий Соколов", "Санкт-Петербург, Невский пр. 25"), list.End());  // дубликат
        list.Insert(new Addressee("Ольга Новикова", "Новосибирск, Красный пр. 12"), list.End());
        list.Insert(new Addressee("Андрей Миронов", "Санкт-Петербург, пр. Кронверкский 49"), list.End()); // дубликат
    }

    public static void DeleteDuplicates(SomeList list)
    {
        // Если список пустой или с одним элементом - выходим
        if (list.First().Posit == list.End().Posit)
            return;

        // Внешний цикл: проходим по всем элементам
        SomePosition current = list.First();
        
        while (current.Posit != list.End().Posit)
        {
            // Сохраняем следующий элемент перед внутренним циклом
            SomePosition nextCurrent = list.Next(current);
            
            // Получаем текущий элемент для сравнения
            Addressee currentItem = list.Retrieve(current);
            
            // Внутренний цикл: проверяем все последующие элементы
            SomePosition search = list.Next(current);
            
            while (search.Posit != list.End().Posit)
            {
                // Сохраняем следующий элемент перед возможным удалением
                SomePosition nextSearch = list.Next(search);
                
                // Сравниваем элементы
                if (currentItem.Equals(list.Retrieve(search)))
                {
                    // Удаляем дубликат
                    list.Delete(search);
                }
                
                // Переходим к следующему элементу
                search = nextSearch;
            }
            
            // Переходим к следующему элементу внешнего цикла
            current = nextCurrent;
        }
    }
}