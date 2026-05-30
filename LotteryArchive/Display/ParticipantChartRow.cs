using Model.Core;

namespace LotteryArchive.Display;

public class ParticipantChartRow
{
    public ParticipantChartRow(LotteryParticipant participant)
    {
        Participant = participant;
        FullName = participant.FullName;
        TotalSpent = participant.TotalSpent;
        TotalWon = participant.TotalWon;
        IsSelected = false;
    }

    public LotteryParticipant Participant { get; }

    public string FullName { get; }

    public long TotalSpent { get; }

    public long TotalWon { get; }

    public bool IsSelected { get; set; }
}
