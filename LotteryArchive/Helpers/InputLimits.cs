namespace LotteryArchive.Helpers;

public static class InputLimits
{
    public const int MaxNumber = 100000;
    public const int MaxNameLength = 40;
    public const int MinGreed = 0;
    public const int MaxGreed = 100;

    public static bool IsNumberTooBig(int value)
    {
        return value > MaxNumber;
    }

    public static bool IsNumberTooBig(long value)
    {
        return value > MaxNumber;
    }

    public static string TooBigMessage(string fieldName)
    {
        return "Слишком большое значение «" + fieldName + "».";
    }
}
