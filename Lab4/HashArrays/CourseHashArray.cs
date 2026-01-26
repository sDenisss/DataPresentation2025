using Lab4.Models;

namespace Lab4.HashArrays;
public class CourseHashArray
{
    private const int Capacity = 5;
    private readonly Course?[] _coursesTable;

    public CourseHashArray()
    {
        _coursesTable = new Course?[Capacity];
    }

    // Добавить курс
    public void AddCourse(string name)
    {
        char[] nameChars = name.ToCharArray();
        int index = Hash(nameChars);
        
        for (int i = 0; i < Capacity; i++)
        {
            if (_coursesTable[index] == null)
            {
                _coursesTable[index] = new Course(nameChars);
                return;
            }
            
            if (ArraysEqual(_coursesTable[index]!.Name, nameChars))
                return; // Уже существует
            
            index = (index + 1) % Capacity;
        }
    }

    // Найти курс
    public Course? FindCourse(string name)
    {
        char[] nameChars = name.ToCharArray();
        int index = Hash(nameChars);
        
        for (int i = 0; i < Capacity; i++)
        {
            var course = _coursesTable[index];
            
            if (course == null)
                return null;
                
            if (ArraysEqual(course.Name, nameChars))
                return course;
                
            index = (index + 1) % Capacity;
        }
        
        return null;
    }

    // Удалить курс
    public void RemoveCourse(string name)
    {
        char[] nameChars = name.ToCharArray();
        int index = Hash(nameChars);
        
        for (int i = 0; i < Capacity; i++)
        {
            if (_coursesTable[index] == null)
                return;
                
            if (ArraysEqual(_coursesTable[index]!.Name, nameChars))
            {
                _coursesTable[index] = null;
                return;
            }
            
            index = (index + 1) % Capacity;
        }
    }

    // Хеш-функция
    private int Hash(char[] name)
    {
        int sum = 0;
        for (int i = 0; i < name.Length && name[i] != '\0'; i++)
            sum += name[i];
        return sum % Capacity;
    }

    // Сравнение char массивов
    private bool ArraysEqual(char[] arr1, char[] arr2)
    {
        if (arr1.Length != arr2.Length) return false;
        for (int i = 0; i < arr1.Length; i++)
            if (arr1[i] != arr2[i]) return false;
        return true;
    }

    // Получить все курсы
    public Course?[] GetAllCourses() => _coursesTable;
}