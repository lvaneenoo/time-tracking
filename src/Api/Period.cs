using System.Diagnostics.CodeAnalysis;

internal sealed class Period : IComparable<Period>, IEquatable<Period>
{
    private Period(TimeOnly start, TimeOnly end)
    {
        Start = start;
        End = end;
    }

    public TimeOnly End { get; }
    public TimeOnly Start { get; }

    public static Period Create(TimeOnly start, TimeOnly end)
    {
        if (start < end)
        {
            return new Period(start, end);
        }

        throw new ArgumentException("[PLACEHOLDER]", nameof(end));
    }

    public static bool TryCreate(TimeOnly start, TimeOnly end, [NotNullWhen(true)] out Period? result)
    {
        if (start < end)
        {
            result = new Period(start, end);
            return true;
        }

        result = null;
        return false;
    }

    public int CompareTo(Period? other)
    {
        if (other is null)
        {
            return 1;
        }

        if (Start == other.Start)
        {
            return End == other.End ? 0 : End.CompareTo(other.End);
        }

        return Start.CompareTo(other.Start);
    }

    public bool Equals(Period? other) => other is not null && Start == other.Start && End == other.End;
    public override bool Equals(object? obj) => Equals(obj as Period);
    public override int GetHashCode() => HashCode.Combine(Start, End);
    public override string ToString() => $"{Start}-{End}";

    public static bool operator ==(Period? a, Period? b) => a is not null && a.Equals(b);
    public static bool operator !=(Period? a, Period? b) => !(a == b);

    public static bool operator <(Period? a, Period? b) => a is not null && a.CompareTo(b) < 0;
    public static bool operator >(Period? a, Period? b) => a is not null && a.CompareTo(b) > 0;
    public static bool operator <=(Period? a, Period? b) => a is not null && a.CompareTo(b) <= 0;
    public static bool operator >=(Period? a, Period? b) => a is not null && a.CompareTo(b) >= 0;
}
