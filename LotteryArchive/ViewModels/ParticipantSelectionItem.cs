using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model.Core;

namespace LotteryArchive.ViewModels;

public class ParticipantSelectionItem : INotifyPropertyChanged
{
    private bool _isSelected;

    public ParticipantSelectionItem(LotteryParticipant participant)
    {
        Participant = participant;
    }

    public LotteryParticipant Participant { get; }

    public string FullName => Participant.FullName;

    public long TotalSpent => Participant.TotalSpent;

    public long TotalWon => Participant.TotalWon;

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value)
            {
                return;
            }

            _isSelected = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
