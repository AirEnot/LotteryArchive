using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LotteryArchive.Helpers;
using Model.Core;

namespace LotteryArchive.Views;

public partial class CreateLotteryPage : Page
{
    private const string PrizePoolTooSmall = "Призовой фонд слишком мал";
    private Regex _digitsOnly = new Regex("^[0-9]+$");

    public CreateLotteryPage()
    {
        InitializeComponent();
    }

    private void CreateButton_OnClick(object sender, RoutedEventArgs e)
    {
        string message;
        if (!CheckForm(out message))
        {
            ValidationText.Text = message;
            ValidationText.Visibility = Visibility.Visible;
            return;
        }

        Lottery lottery = new Lottery(
            LotteryNameBox.Text.Trim(),
            int.Parse(TicketsCountBox.Text),
            long.Parse(PrizePoolBox.Text));

        App.State.Controller.AddLottery(lottery);
        App.State.SaveCurrentFormat();
        GoToPage(new MainPage());
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        GoToPage(new MainPage());
    }

    private void NumericBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !_digitsOnly.IsMatch(e.Text);
    }

    private void NumericBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        string message;
        if (!CheckNumbers(out message))
        {
            ValidationText.Text = message;
            ValidationText.Visibility = Visibility.Visible;
        }
        else
        {
            ValidationText.Visibility = Visibility.Collapsed;
        }
    }

    private bool CheckForm(out string message)
    {
        message = "";

        if (string.IsNullOrWhiteSpace(LotteryNameBox.Text))
        {
            message = "Введите название лотереи.";
            return false;
        }

        string lotteryName = LotteryNameBox.Text.Trim();
        foreach (Lottery lottery in App.State.Controller.AllLotteries)
        {
            if (string.Equals(lottery.Name, lotteryName, System.StringComparison.OrdinalIgnoreCase))
            {
                message = "Лотерея с таким названием уже существует.";
                return false;
            }
        }

        if (!CheckNumbers(out message))
        {
            return false;
        }

        int tickets = int.Parse(TicketsCountBox.Text);
        long prizePool = long.Parse(PrizePoolBox.Text);

        if (tickets > 1.5 * prizePool)
        {
            message = PrizePoolTooSmall;
            return false;
        }

        return true;
    }

    private bool CheckNumbers(out string message)
    {
        message = "";

        if (!string.IsNullOrWhiteSpace(TicketsCountBox.Text))
        {
            int tickets;
            if (!int.TryParse(TicketsCountBox.Text, out tickets))
            {
                message = "Введите корректное количество билетов.";
                return false;
            }
            if (InputLimits.IsNumberTooBig(tickets))
            {
                message = InputLimits.TooBigMessage("Количество билетов");
                return false;
            }
        }

        if (!string.IsNullOrWhiteSpace(PrizePoolBox.Text))
        {
            long prizePool;
            if (!long.TryParse(PrizePoolBox.Text, out prizePool))
            {
                message = "Введите корректный призовой фонд.";
                return false;
            }
            if (InputLimits.IsNumberTooBig(prizePool))
            {
                message = InputLimits.TooBigMessage("Призовой фонд");
                return false;
            }
        }

        if (string.IsNullOrWhiteSpace(TicketsCountBox.Text))
        {
            message = "Введите количество билетов.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(PrizePoolBox.Text))
        {
            message = "Введите призовой фонд.";
            return false;
        }

        int ticketsCount = int.Parse(TicketsCountBox.Text);
        long pool = long.Parse(PrizePoolBox.Text);
        if (ticketsCount > 1.5 * pool)
        {
            message = PrizePoolTooSmall;
            return false;
        }

        return true;
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
