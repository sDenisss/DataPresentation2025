using Lab4.Models;

namespace Lab4.HashArrays;
public class StudentHashArray
{
    private const int Capacity = 5;
    private readonly Student?[] _studentsTable;

    public StudentHashArray()
    {
        _studentsTable = new Student?[Capacity];
    }

    // Добавить студента
    public void AddStudent(char[] name)
    {
        int index = Hash(name);
        
        for (int i = 0; i < Capacity; i++)
        {
            if (_studentsTable[index] == null)
            {
                _studentsTable[index] = new Student(name);
                return;
            }
            
            if (ArraysEqual(_studentsTable[index]!.Name, name))
                return; // Уже существует
            
            index = (index + 1) % Capacity;
        }
    }

    // Найти студента
    public Student? FindStudent(char[] name)
    {
        int index = Hash(name);
        
        for (int i = 0; i < Capacity; i++)
        {
            var student = _studentsTable[index];
            
            if (student == null)
                return null;
                
            if (ArraysEqual(student.Name, name))
                return student;
                
            index = (index + 1) % Capacity;
        }
        
        return null;
    }

    // Удалить студента
    public void RemoveStudent(char[] name)
    {
        int index = Hash(name);
        
        for (int i = 0; i < Capacity; i++)
        {
            if (_studentsTable[index] == null)
                return;
                
            if (ArraysEqual(_studentsTable[index]!.Name, name))
            {
                _studentsTable[index] = null;
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

    // Получить всех студентов
    public Student?[] GetAllStudents() => _studentsTable;
}