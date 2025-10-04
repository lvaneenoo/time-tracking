using System.Diagnostics.CodeAnalysis;

internal sealed class TrackedDate : IComparable<TrackedDate>, IEquatable<TrackedDate>
{
    private static readonly DateOnly MinValue = new(2025, 1, 1);

    private readonly DateOnly _value;

    private TrackedDate(DateOnly value)
    {
        _value = value;
    }

    public DayOfWeek DayOfWeek => _value.DayOfWeek;

    public static bool TryParse(string s, [NotNullWhen(true)] out TrackedDate? result)
    {
        if (!DateOnly.TryParseExact(s, "yyyy-MM-dd", out DateOnly value))
        {
            result = null;
            return false;
        }

        if (!IsInRange(value))
        {
            result = null;
            return false;
        }

        result = new TrackedDate(value);
        return true;
    }

    private static bool IsInRange(DateOnly value) => MinValue <= value;

    public int CompareTo(TrackedDate? other) => other is null ? 1 : _value.CompareTo(other._value);
    public bool Equals(TrackedDate? other) => other is not null && _value == other._value;
    public override bool Equals(object? obj) => Equals(obj as TrackedDate);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString("yyyy-MM-dd");

    public static bool operator ==(TrackedDate? a, TrackedDate? b) => a is not null && a.Equals(b);
    public static bool operator !=(TrackedDate? a, TrackedDate? b) => !(a == b);

    public static bool operator <(TrackedDate? a, TrackedDate? b) => a is not null && a.CompareTo(b) < 0;
    public static bool operator >(TrackedDate? a, TrackedDate? b) => a is not null && a.CompareTo(b) > 0;
    public static bool operator <=(TrackedDate? a, TrackedDate? b) => a is not null && a.CompareTo(b) <= 0;
    public static bool operator >=(TrackedDate? a, TrackedDate? b) => a is not null && a.CompareTo(b) >= 0;

    public static implicit operator DateOnly(TrackedDate a) => a._value;
}
