using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Model.Core;

namespace LotteryArchive.Views;

public partial class LotteryDetailsPage : Page
{
    private readonly Lottery _lottery;

    public LotteryDetailsPage(Lottery lottery)
    {
        _lottery = lottery;
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        FillLotteryInfo();
    }

    private void FillLotteryInfo()
    {
        var isDrawn = App.State.IsLotteryDrawn(_lottery);

        TitleText.Text = _lottery.Name;
        NameText.Text = $"Название: {_lottery.Name}";
        TicketsCountText.Text = $"Количество билетов: {_lottery.TicketsCount}";
        PrizePoolText.Text = $"Призовой фонд: {_lottery.PrizePool}";
        TicketPriceText.Text = $"Цена билета: {_lottery.TicketsPrice}";
        SoldTicketsText.Text = $"Продано билетов: {_lottery.LotteryTickets.Count(t => t.IsSold)}";

        if (isDrawn)
        {
            StatusText.Text = "Статус: лотерея разыграна";
            StatusText.Foreground = new SolidColorBrush(Color.FromRgb(117, 117, 117));
            DrawButton.IsEnabled = false;
            DrawButton.Content = "Уже разыграна";
        }
        else
        {
            StatusText.Text = "Статус: активна";
            StatusText.Foreground = new SolidColorBrush(Color.FromRgb(46, 125, 50));
            DrawButton.IsEnabled = true;
            DrawButton.Content = "Разыграть";
        }
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        Navigate(new MainPage());
    }

    private void DrawButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (App.State.IsLotteryDrawn(_lottery))
        {
            return;
        }

        var controller = App.State.Controller;
        controller.SellTicketsInLottery(_lottery);
        var success = controller.DrawLottery(_lottery);

        App.State.MarkLotteryDrawn(_lottery);

        MessageBox.Show(
            success ? "Розыгрыш прошел успешно." : "Розыгрыш отменен: продано недостаточно билетов.",
            "Результат розыгрыша",
            MessageBoxButton.OK,
            success ? MessageBoxImage.Information : MessageBoxImage.Warning);

        FillLotteryInfo();
        App.State.SaveCurrentFormat();
    }

    private static void Navigate(Page page)
    {
        if (Application.Current.MainWindow is MainWindow window)
        {
            window.Navigate(page);
        }
    }
}
