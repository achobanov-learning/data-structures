namespace Common;

public class Printable
{
    private readonly string _value;
    private readonly ConsoleColor _color;

    public Printable(char character, ConsoleColor color = ConsoleColor.White) : this(character.ToString(), color)
    {
    }
    public Printable(string value, ConsoleColor color = ConsoleColor.White)
    {
        _value = value;
        _color = color;
        Length = value.Length;
    }

    public int Length { get; }

    public override string ToString()
    {
        return _value;
    }

    public void Print()
    {
        var hasColor = _color != ConsoleColor.White;
        if (hasColor)
        {
            Console.ForegroundColor = _color;
        }
        Console.Write(_value);
        if (hasColor)
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public static implicit operator Printable(char character)
    {
        return new Printable(character);
    }
    public static implicit operator Printable(string value)
    {
        return new Printable(value);
    }
}
