namespace Lab2.Map.Interfaces;

public interface IMap
{
    void MakeNull();
    void Assign(char[] key, char[] value);
    bool Compute(char[] key, out char[] value);
    void Print();
}