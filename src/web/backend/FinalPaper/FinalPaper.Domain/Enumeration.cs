using System.Reflection;

namespace FinalPaper.Domain.Utility; 

public abstract class Enumeration : IComparable
{
    public Enumeration()
    {
        Name = string.Empty;
    }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; }

    public int Id { get; set; }

    public int CompareTo(object? other)
    {
        return other is null ? -1 : Id.CompareTo(((Enumeration)other).Id);
    }

    public override string ToString()
    {
        return Name;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        return fields.Select(f => f.GetValue(null)).Cast<T>();
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        var otherValue = obj as Enumeration;
        if (otherValue is null)
            return false;

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
    {
        var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
        return absoluteDifference;
    }

    public static T FromValue<T>(int value) where T : Enumeration
    {
        var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
        return matchingItem;
    }

    public static T FromDisplayName<T>(string displayName) where T : Enumeration
    {
        var matchingItem = Parse<T, string>(displayName, "display name",
            item => item.Name.Equals(displayName, StringComparison.OrdinalIgnoreCase));
        return matchingItem;
    }

    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
            throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

        return matchingItem;
    }

    public static string FromIntValue(Type type, int value)
    {
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
        var matchingItems = fields.Select(f => f.GetValue(null)).Cast<Enumeration>();

        var matchingItem = matchingItems.FirstOrDefault(item => item.Id == value);

        if (matchingItem == null)
            throw new InvalidOperationException($"'{value}' is not a valid integer in {type}");

        return matchingItem.Name;
    }

    public static int FromStringValue(Type type, string value)
    {
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
        var matchingItems = fields.Select(f => f.GetValue(null)).Cast<Enumeration>();

        var matchingItem = matchingItems.FirstOrDefault(item => item.Name == value);

        if (matchingItem == null)
            throw new InvalidOperationException($"'{value}' is not a valid integer in {type}");

        return matchingItem.Id;
    }
}