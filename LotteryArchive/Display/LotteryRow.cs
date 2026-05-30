using Model.Core;

namespace LotteryArchive.Display;

public class LotteryRow
{
    public LotteryRow(Lottery lottery)
    {
        Lottery = lottery;
        IsDrawn = lottery.IsDrawn;
        Name = lottery.Name;
        TicketsCount = lottery.TicketsCount;
        TicketsPrice = lottery.TicketsPrice;
        PrizePoolText = "Призовой фонд: " + lottery.PrizePool;
        StatusText = IsDrawn ? "Разыграна" : "Активна";
        CardOpacity = IsDrawn ? 0.75 : 1.0;
    }

    public Lottery Lottery { get; }

    public string Name { get; }

    public int TicketsCount { get; }

    public int TicketsPrice { get; }

    public string PrizePoolText { get; }

    public bool IsDrawn { get; }

    public string StatusText { get; }

    public double CardOpacity { get; }

    public static bool operator <(LotteryRow left, LotteryRow right)
    {
        return Compare(left, right) < 0;
    }

    public static bool operator >(LotteryRow left, LotteryRow right)
    {
        return Compare(left, right) > 0;
    }

    private static int Compare(LotteryRow left, LotteryRow right)
    {
        if (ReferenceEquals(left, right))
        {
            return 0;
        }

        if (left is null)
        {
            return -1;
        }

        if (right is null)
        {
            return 1;
        }

        int statusCompare = left.IsDrawn.CompareTo(right.IsDrawn);
        if (statusCompare != 0)
        {
            return statusCompare;
        }

        return string.Compare(left.Name, right.Name);
    }
}
