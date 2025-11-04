
namespace Lab1.Interfaces;

public interface IList<T, P> where P : IPosition
{
    /// <summary>Возвращает позицию после последнего элемента</summary>
    P End();
    
    /// <summary>Вставляет элемент перед указанной позицией</summary>
    void Insert(T item, P position);
    
    /// <summary>Находит позицию первого вхождения элемента</summary>
    P Locate(T item);
    
    /// <summary>Возвращает элемент в указанной позиции</summary>
    T Retrieve(P position);
    
    /// <summary>Удаляет элемент в указанной позиции</summary>
    void Delete(P position);
    
    /// <summary>Возвращает следующую позицию</summary>
    P Next(P position);
    
    /// <summary>Возвращает предыдущую позицию</summary>
    P Previous(P position);
    
    /// <summary>Очищает список</summary>
    void Makenull();
    
    /// <summary>Возвращает позицию первого элемента</summary>
    P First();
    
    /// <summary>Выводит список в консоль</summary>
    void PrintList();
}