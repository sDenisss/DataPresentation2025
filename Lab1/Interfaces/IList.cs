// using Lab1.Cursor;

// namespace Lab1.Interfaces;

// public interface IList<T> where T : IEquatable<T>
// {
//     /// <summary>Возвращает позицию после последнего элемента</summary>
//     object End();
    
//     /// <summary>Вставляет элемент перед указанной позицией</summary>
//     void Insert(T item, Position position);
    
//     /// <summary>Находит позицию первого вхождения элемента</summary>
//     object Locate(T item);
    
//     /// <summary>Возвращает элемент в указанной позиции</summary>
//     T Retrieve(object position);
    
//     /// <summary>Удаляет элемент в указанной позиции</summary>
//     void Delete(object position);
    
//     /// <summary>Возвращает следующую позицию</summary>
//     object Next(object position);
    
//     /// <summary>Возвращает предыдущую позицию</summary>
//     object Previous(object position);
    
//     /// <summary>Очищает список</summary>
//     void Makenull();
    
//     /// <summary>Возвращает позицию первого элемента</summary>
//     object First();
    
//     /// <summary>Выводит список в консоль</summary>
//     void PrintList();
// }