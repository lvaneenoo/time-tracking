using System.Diagnostics.CodeAnalysis;

public sealed class Comment : IComparable<Comment>, IEquatable<Comment>, IParsable<Comment>
{
    private const int MaxLength = 150;

    private readonly string _value;

    internal Comment(string value)
    {
        _value = value;
    }

    public static Comment Parse(string s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Comment result)
    {
        if (s is null || s.Length > MaxLength)
        {
            result = null;
            return false;
        }

        result = new Comment(s);
        return true;
    }

    public int CompareTo(Comment? other) => other is null ? 1 : _value.CompareTo(other._value);

    public bool Equals(Comment? other) => other is not null && _value == other._value;
    public override bool Equals(object? obj) => Equals(obj as Comment);

    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value;

    public static bool operator ==(Comment? a, Comment? b) => a is not null && a.Equals(b);
    public static bool operator !=(Comment? a, Comment? b) => !(a == b);

    public static bool operator <(Comment? a, Comment? b) => a is not null && a.CompareTo(b) < 0;
    public static bool operator >(Comment? a, Comment? b) => a is not null && a.CompareTo(b) > 0;
    public static bool operator <=(Comment? a, Comment? b) => a is not null && a.CompareTo(b) <= 0;
    public static bool operator >=(Comment? a, Comment? b) => a is not null && a.CompareTo(b) >= 0;

    public static implicit operator string(Comment a) => a.ToString();
}
