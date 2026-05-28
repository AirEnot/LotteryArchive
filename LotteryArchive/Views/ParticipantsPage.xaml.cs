using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Model.Core;

namespace LotteryArchive.Views;

public partial class ParticipantsPage : Page
{
    private static readonly Regex DigitsRegex = new("^[0-9]+$");
    private bool _suppressGreedUpdate;

    public ParticipantsPage()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        RefreshParticipants();
    }

    private void RefreshParticipants()
    {
        var participants = App.State.Controller.AllPeople;
        var selectedId = (ParticipantsList.SelectedItem as LotteryParticipant)?.Id;

        ParticipantsList.ItemsSource = null;
        ParticipantsList.ItemsSource = participants;

        if (!string.IsNullOrEmpty(selectedId))
        {
            ParticipantsList.SelectedItem = participants.FirstOrDefault(p => p.Id == selectedId);
        }

        var hasParticipants = participants.Count > 0;
        ParticipantsList.Visibility = hasParticipants ? Visibility.Visible : Visibility.Collapsed;
        EmptyParticipantsText.Visibility = hasParticipants ? Visibility.Collapsed : Visibility.Visible;
        if (!hasParticipants)
        {
            EditPanel.Visibility = Visibility.Collapsed;
        }
    }

    private void ParticipantsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ParticipantsList.SelectedItem is not LotteryParticipant participant)
        {
            EditPanel.Visibility = Visibility.Collapsed;
            return;
        }

        EditPanel.Visibility = Visibility.Visible;
        SelectedParticipantText.Text = $"Редактирование: {participant.FullName}";
        BalanceEditBox.Text = participant.Balance.ToString();

        _suppressGreedUpdate = true;
        GreedSlider.Value = participant.Greed;
        GreedValueText.Text = participant.Greed.ToString();
        _suppressGreedUpdate = false;
    }

    private void GreedSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (_suppressGreedUpdate)
        {
            return;
        }

        GreedValueText.Text = ((int)GreedSlider.Value).ToString();
    }

    private void DeleteParticipantButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (ParticipantsList.SelectedItem is not LotteryParticipant participant)
        {
            MessageBox.Show("Выберите участника для удаления.", "Удаление", MessageBoxButton.OK,
                MessageBoxImage.Information);
            return;
        }

        var confirm = MessageBox.Show(
            $"Удалить участника «{participant.FullName}»?",
            "Удаление участника",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (confirm != MessageBoxResult.Yes)
        {
            return;
        }

        App.State.Controller.RemoveParticipant(participant);
        App.State.SaveCurrentFormat();
        EditPanel.Visibility = Visibility.Collapsed;
        RefreshParticipants();
    }

    private void SaveChangesButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (ParticipantsList.SelectedItem is not LotteryParticipant participant)
        {
            MessageBox.Show("Выберите участника в списке.", "Редактирование", MessageBoxButton.OK,
                MessageBoxImage.Information);
            return;
        }

        if (!long.TryParse(BalanceEditBox.Text, out var balance) || balance < 0)
        {
            MessageBox.Show("Введите корректный неотрицательный баланс.", "Редактирование", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        var greed = (int)GreedSlider.Value;
        if (!App.State.TryUpdateParticipant(participant, balance, greed))
        {
            MessageBox.Show("Не удалось сохранить изменения.", "Редактирование", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        App.State.SaveCurrentFormat();
        RefreshParticipants();
    }

    private void NumericBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !DigitsRegex.IsMatch(e.Text);
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        Navigate(new MainPage());
    }

    private void AddParticipantButton_OnClick(object sender, RoutedEventArgs e)
    {
        Navigate(new CreateParticipantPage());
    }

    private static void Navigate(Page page)
    {
        if (Application.Current.MainWindow is MainWindow window)
        {
            window.Navigate(page);
        }
    }
}
