using UxTracker.Core.Contexts.Account.Enums;

namespace UxTracker.Core.Contexts.Account.Extensions;

public static class SexExtension
{
    public static string ToHumanize(this Sex sex) =>
        sex switch
        {
            Sex.Male => "Masculino",
            Sex.Female => "Feminino",
            Sex.PreferNotSay => "Prefiro não dizer",
            _ => throw new ArgumentOutOfRangeException(nameof(sex), sex, null)
        };
    
    public static Sex ToEnum(this string sex) =>
        sex switch
        {
            "Male" => Sex.Male,
            "Female" => Sex.Female,
            "PreferNotSay" => Sex.PreferNotSay,
            _ => throw new ArgumentOutOfRangeException(nameof(sex), sex, null)
        };
}