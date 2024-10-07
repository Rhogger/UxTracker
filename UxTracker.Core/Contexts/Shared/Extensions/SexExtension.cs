using UxTracker.Core.Contexts.Account.Enums;

namespace UxTracker.Core.Contexts.Shared.Extensions;

public static class SexExtension
{
    public static Sex ToSex(this string value)
    {
        return Enum.TryParse(value, out Sex sex) ? sex : throw new ArgumentException($"Valor inválido: {value}");
    }

    public static string ToString(this Sex sex)
    {
        return sex.ToString();
    }
}