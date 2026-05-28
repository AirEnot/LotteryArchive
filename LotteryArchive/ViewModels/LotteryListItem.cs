using Model.Core;

namespace LotteryArchive.ViewModels;

public class LotteryListItem
{
    public LotteryListItem(Lottery lottery, bool isDrawn)
    {
        Lottery = lottery;
        IsDrawn = isDrawn;
    }

    public Lottery Lottery { get; }

    public string Name => Lottery.Name;

    public int TicketsCount => Lottery.TicketsCount;

    public long PrizePool => Lottery.PrizePool;

    public int TicketsPrice => Lottery.TicketsPrice;

    public bool IsDrawn { get; }

    public string StatusText => IsDrawn ? "Разыграна" : "Активна";

    public double CardOpacity => IsDrawn ? 0.55 : 1;
}
