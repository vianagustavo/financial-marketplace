namespace BCWalletManager.Application.Contracts;
public static class EnumParser
{
    public static T Parse<T>(string value) where T : Enum
    {
        return Enum.TryParse(typeof(T), value, true, out object? result)
            ? (T)result
            : throw new ArgumentException($"Invalid value '{value}' for enum type {typeof(T).Name}");
    }
}