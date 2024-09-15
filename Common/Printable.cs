namespace Common;

public class Printable
{
    private readonly char _character;
    private readonly ConsoleColor _color;

    public static Printable[] Combine(params Printable[][] printables)
    {
        return printables.SelectMany(x => x).ToArray();
    }
    public static Printable[] Create(string value, ConsoleColor color = ConsoleColor.White)
    {
        return value.Select(x => new Printable(x, color)).ToArray();
    }
    public static Printable[] Create(char value, ConsoleColor color = ConsoleColor.White)
    {
        return [ new Printable(value, color) ];
    }

    public Printable(char character, ConsoleColor color = ConsoleColor.White)
    {
        _character = character;
        _color = color;
    }

    public override string ToString()
    {
        var result = _character.ToString();
        if (_color != ConsoleColor.White)
        {
            result += $" {_color}";
        }
        return result;
    }

    public void Print()
    {
        var hasColor = _color != ConsoleColor.White;
        if (Console.ForegroundColor != _color)
        {
            Console.ForegroundColor = _color;
        }
        Console.Write(_character);
    }

    public static implicit operator Printable(char character)
    {
        return new Printable(character);
    }
}
