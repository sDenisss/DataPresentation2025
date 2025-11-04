using Lab1.Interfaces;

namespace Lab1.Cursor;
public class Position : IPosition
{
    public int Posit;
    public Position() {}
    public Position(int posit)
    {
        Posit = posit;
    }
}