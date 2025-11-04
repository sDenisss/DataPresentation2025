namespace Lab2.Dictionary.Interfaces;

public interface IDictionary<TKey, TValue>
{
    void MakeNull();
    void Assign(TKey key, TValue value);
    bool Compute(TKey key, out TValue value);
    void Print();
}