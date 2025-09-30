namespace Lab1;

public class Addressee : IEquatable<Addressee>
{
    private const int MaxCharsInName = 20;
    private const int MaxCharsInAdresse = 50;
    private string Name { get; set; }
    private string Addresse { get; set; }
    // private char[] Name = new char[MaxCharsInName];
    // private char[] Adresse = new char[MaxCharsInAdresse];
    public Addressee(string name, string addresse)
    {
        Name = name.Length > MaxCharsInName ? name.Substring(0, MaxCharsInName) : name;
        Addresse = addresse.Length > MaxCharsInAdresse ? addresse.Substring(0, MaxCharsInAdresse) : addresse;
    }

    public bool Equals(Addressee? other)
    {
        if (other == null) return false;
        return other.Name == Name && other.Addresse == Addresse;
    }

    public override string ToString()
    {
        return $"{Name} - {Addresse}";
    }
}