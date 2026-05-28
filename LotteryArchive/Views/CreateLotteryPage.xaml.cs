using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Model.Core;

namespace LotteryArchive.Views;

public partial class CreateLotteryPage : Page
{
    private const string PrizePoolTooSmallMessage = "Призовой фонд слишком мал";
    private static readonly Regex DigitsRegex = new("^[0-9]+$");

    public CreateLotteryPage()
    {
        InitializeComponent();
    }

    private void CreateButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!ValidateForm(out var validationMessage))
        {
            ShowValidation(validationMessage);
            return;
        }

        var lottery = new Lottery(
            LotteryNameBox.Text.Trim(),
            int.Parse(TicketsCountBox.Text),
            long.Parse(PrizePoolBox.Text));

        App.State.Controller.AddLottery(lottery);
        App.State.SaveCurrentFormat();
        Navigate(new MainPage());
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        Navigate(new MainPage());
    }

    private void NumericBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !DigitsRegex.IsMatch(e.Text);
    }

    private void LotteryValidation_OnChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TicketsCountBox.Text) || string.IsNullOrWhiteSpace(PrizePoolBox.Text))
        {
            ValidationText.Visibility = Visibility.Collapsed;
            return;
        }

        if (!int.TryParse(TicketsCountBox.Text, out var tickets) || !long.TryParse(PrizePoolBox.Text, out var prizePool))
        {
            return;
        }

        if (tickets > 2L * prizePool)
        {
            ShowValidation(PrizePoolTooSmallMessage);
        }
        else
        {
            ValidationText.Visibility = Visibility.Collapsed;
        }
    }

    private bool ValidateForm(out string message)
    {
        message = string.Empty;
        if (string.IsNullOrWhiteSpace(LotteryNameBox.Text))
        {
            message = "Введите название лотереи.";
            return false;
        }

        if (!int.TryParse(TicketsCountBox.Text, out var tickets) || tickets <= 0)
        {
            message = "Количество билетов должно быть целым числом больше 0.";
            return false;
        }

        if (!long.TryParse(PrizePoolBox.Text, out var prizePool) || prizePool <= 0)
        {
            message = "Призовой фонд должен быть числом больше 0.";
            return false;
        }

        if (tickets > 2L * prizePool)
        {
            message = PrizePoolTooSmallMessage;
            return false;
        }

        return true;
    }

    private void ShowValidation(string message)
    {
        ValidationText.Text = message;
        ValidationText.Visibility = Visibility.Visible;
    }

    private static void Navigate(Page page)
    {
        if (Application.Current.MainWindow is MainWindow window)
        {
            window.Navigate(page);
        }
    }
}
