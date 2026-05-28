using System.Runtime.CompilerServices;
using Model.Core;

namespace LotteryArchive.Services;

public class AppState
{
    private readonly HashSet<Lottery> _drawnLotteries = new(ReferenceEqualityComparer.Instance);

    public LotteryController Controller { get; } = new();

    public StorageFormat CurrentFormat { get; private set; } = StorageFormat.Json;

    public void LoadOnStartup()
    {
        Controller.TryLoadFromStorage(Controller.LoadFromJson);
        SyncDrawnStateFromLotteries();
    }

    public void SaveOnExit()
    {
        Controller.TrySaveToStorage(Controller.SaveAsJson);
        Controller.TrySaveToStorage(Controller.SaveAsXml);
    }

    public void SaveCurrentFormat()
    {
        Controller.TrySaveToStorage(GetSaveAction(CurrentFormat));
    }

    public void SwitchFormat(StorageFormat newFormat)
    {
        if (newFormat == CurrentFormat)
        {
            return;
        }

        Controller.TrySaveToStorage(GetSaveAction(CurrentFormat));
        CurrentFormat = newFormat;
        Controller.TrySaveToStorage(GetSaveAction(CurrentFormat));
    }

    public bool IsLotteryDrawn(Lottery lottery) =>
        _drawnLotteries.Contains(lottery) || lottery.LotteryTickets.Any(t => t.IsSold);

    public void MarkLotteryDrawn(Lottery lottery) => _drawnLotteries.Add(lottery);

    public void SyncDrawnStateFromLotteries()
    {
        foreach (var lottery in Controller.AllLotteries)
        {
            if (lottery.LotteryTickets.Any(t => t.IsSold))
            {
                _drawnLotteries.Add(lottery);
            }
        }
    }

    public bool TryUpdateParticipant(LotteryParticipant participant, long balance, int greed)
    {
        if (balance < 0 || greed is < 0 or > 100)
        {
            return false;
        }

        var people = Controller.AllPeople;
        var index = people.FindIndex(p => p.Id == participant.Id);
        if (index < 0)
        {
            return false;
        }

        var updated = new LotteryParticipant(
            participant.Id,
            participant.FullName,
            balance,
            greed,
            participant.TotalSpent,
            participant.TotalWon,
            participant.Tickets);

        people[index] = updated;
        return true;
    }

    private Action GetSaveAction(StorageFormat format) =>
        format == StorageFormat.Json ? Controller.SaveAsJson : Controller.SaveAsXml;

    private sealed class ReferenceEqualityComparer : IEqualityComparer<Lottery>
    {
        public static ReferenceEqualityComparer Instance { get; } = new();

        public bool Equals(Lottery? x, Lottery? y) => ReferenceEquals(x, y);

        public int GetHashCode(Lottery obj) => RuntimeHelpers.GetHashCode(obj);
    }
}
