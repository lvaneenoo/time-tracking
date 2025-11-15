using System.Diagnostics.CodeAnalysis;

internal sealed class TimeSheetEntryId
    : IComparable<TimeSheetEntryId>,
      IEquatable<TimeSheetEntryId>,
      IParsable<TimeSheetEntryId>
{
    private readonly long _value;

    internal TimeSheetEntryId(long value)
    {
        _value = value;
    }

    public static TimeSheetEntryId Parse(string s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string? s,
                                IFormatProvider? provider,
                                [MaybeNullWhen(false)] out TimeSheetEntryId result)
    {
        if (!long.TryParse(s, out long value) || value <= 0)
        {
            result = null;
            return false;
        }

        result = new TimeSheetEntryId(value);
        return true;
    }

    public int CompareTo(TimeSheetEntryId? other) => other is null ? 1 : _value.CompareTo(other._value);
    public bool Equals(TimeSheetEntryId? other) => other is not null && _value == other._value;
    public override bool Equals(object? obj) => Equals(obj as TimeSheetEntryId);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString();

    public static bool operator ==(TimeSheetEntryId? a, TimeSheetEntryId? b) => a is not null && a.Equals(b);
    public static bool operator !=(TimeSheetEntryId? a, TimeSheetEntryId? b) => !(a == b);

    public static bool operator <(TimeSheetEntryId? a, TimeSheetEntryId? b) => a is not null && a.CompareTo(b) < 0;
    public static bool operator >(TimeSheetEntryId? a, TimeSheetEntryId? b) => a is not null && a.CompareTo(b) > 0;
    public static bool operator <=(TimeSheetEntryId? a, TimeSheetEntryId? b) => a is not null && a.CompareTo(b) <= 0;
    public static bool operator >=(TimeSheetEntryId? a, TimeSheetEntryId? b) => a is not null && a.CompareTo(b) >= 0;

    public static implicit operator long(TimeSheetEntryId a) => a._value;
}
