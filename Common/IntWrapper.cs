namespace Common;

public class IntWrapper : IComparable<IntWrapper>
{
    private readonly int _number;

    public IntWrapper(int number)
    {
        _number = number;
    }

    public int CompareTo(IntWrapper other)
    {
        return _number.CompareTo(other._number);
    }

    public override string ToString()
    {
        return _number.ToString();
    }
}