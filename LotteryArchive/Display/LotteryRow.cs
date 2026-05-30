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
}
