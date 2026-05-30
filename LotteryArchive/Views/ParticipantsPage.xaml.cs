using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LotteryArchive.Helpers;
using Model.Core;

namespace LotteryArchive.Views;

public partial class ParticipantsPage : Page
{
    private Regex _digitsOnly = new Regex("^[0-9]+$");
    private bool _blockSliderEvent;

    public ParticipantsPage()
    {
        InitializeComponent();
        Loaded += ParticipantsPage_Loaded;
    }

    private void ParticipantsPage_Loaded(object sender, RoutedEventArgs e)
    {
        ShowParticipants();
    }

    private void ShowParticipants()
    {
        ParticipantsList.ItemsSource = null;
        ParticipantsList.ItemsSource = App.State.Controller.AllPeople;

        if (App.State.Controller.AllPeople.Count == 0)
        {
            ParticipantsList.Visibility = Visibility.Collapsed;
            EmptyParticipantsText.Visibility = Visibility.Visible;
            EditPanel.Visibility = Visibility.Collapsed;
        }
        else
        {
            ParticipantsList.Visibility = Visibility.Visible;
            EmptyParticipantsText.Visibility = Visibility.Collapsed;
        }
    }

    private void ParticipantsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        LotteryParticipant participant = ParticipantsList.SelectedItem as LotteryParticipant;
        if (participant == null)
        {
            EditPanel.Visibility = Visibility.Collapsed;
            return;
        }

        EditPanel.Visibility = Visibility.Visible;
        SelectedParticipantText.Text = "Редактирование: " + participant.FullName;
        BalanceEditBox.Text = participant.Balance.ToString();

        _blockSliderEvent = true;
        GreedSlider.Value = participant.Greed;
        GreedValueText.Text = participant.Greed.ToString();
        _blockSliderEvent = false;
    }

    private void GreedSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (_blockSliderEvent)
        {
            return;
        }
        GreedValueText.Text = ((int)GreedSlider.Value).ToString();
    }

    private void DeleteParticipantButton_OnClick(object sender, RoutedEventArgs e)
    {
        LotteryParticipant participant = ParticipantsList.SelectedItem as LotteryParticipant;
        if (participant == null)
        {
            MessageBox.Show("Выберите участника для удаления.");
            return;
        }

        MessageBoxResult answer = MessageBox.Show(
            "Удалить участника «" + participant.FullName + "»?",
            "Удаление",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (answer != MessageBoxResult.Yes)
        {
            return;
        }

        App.State.Controller.RemoveParticipant(participant);
        App.State.SaveCurrentFormat();
        EditPanel.Visibility = Visibility.Collapsed;
        ShowParticipants();
    }

    private void SaveChangesButton_OnClick(object sender, RoutedEventArgs e)
    {
        LotteryParticipant participant = ParticipantsList.SelectedItem as LotteryParticipant;
        if (participant == null)
        {
            MessageBox.Show("Выберите участника в списке.");
            return;
        }

        long balance;
        if (!long.TryParse(BalanceEditBox.Text, out balance) || balance < 0)
        {
            MessageBox.Show("Введите корректный баланс.");
            return;
        }

        if (InputLimits.IsNumberTooBig(balance))
        {
            MessageBox.Show(InputLimits.TooBigMessage("Баланс"));
            return;
        }

        int greed = (int)GreedSlider.Value;
        if (greed < InputLimits.MinGreed || greed > InputLimits.MaxGreed)
        {
            MessageBox.Show("Жадность должна быть от 0 до 100.");
            return;
        }

        int index = App.State.Controller.AllPeople.FindIndex(p => p.Id == participant.Id);
        if (index < 0)
        {
            MessageBox.Show("Участник не найден.");
            return;
        }

        LotteryParticipant updated = new LotteryParticipant(
            participant.Id,
            participant.FullName,
            balance,
            greed,
            participant.TotalSpent,
            participant.TotalWon,
            participant.Tickets);

        App.State.Controller.AllPeople[index] = updated;
        App.State.SaveCurrentFormat();
        ShowParticipants();
    }

    private void NumericBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !_digitsOnly.IsMatch(e.Text);
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        GoToPage(new MainPage());
    }

    private void AddParticipantButton_OnClick(object sender, RoutedEventArgs e)
    {
        GoToPage(new CreateParticipantPage());
    }

    private void GoToPage(Page page)
    {
        MainWindow window = Application.Current.MainWindow as MainWindow;
        if (window != null)
        {
            window.Navigate(page);
        }
    }
}
