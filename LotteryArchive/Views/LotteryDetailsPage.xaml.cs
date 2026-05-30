using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Model.Core;

namespace LotteryArchive.Views;

public partial class LotteryDetailsPage : Page
{
    private Lottery _lottery;

    public LotteryDetailsPage(Lottery lottery)
    {
        _lottery = lottery;
        InitializeComponent();
        Loaded += LotteryDetailsPage_Loaded;
    }

    private void LotteryDetailsPage_Loaded(object sender, RoutedEventArgs e)
    {
        FillInfo();
    }

    private void FillInfo()
    {
        bool isDrawn = _lottery.IsDrawn;
        int soldCount = 0;

        foreach (var ticket in _lottery.LotteryTickets)
        {
            if (ticket.IsSold)
            {
                soldCount++;
            }
        }

        TitleText.Text = _lottery.Name;
        NameText.Text = "Название: " + _lottery.Name;
        TicketsCountText.Text = "Количество билетов: " + _lottery.TicketsCount;
        PrizePoolText.Text = "Призовой фонд: " + _lottery.PrizePool;
        TicketPriceText.Text = "Цена билета: " + _lottery.TicketsPrice;
        SoldTicketsText.Text = "Продано билетов: " + soldCount;

        if (isDrawn)
        {
            StatusText.Text = "Статус: лотерея разыграна";
            StatusText.Foreground = new SolidColorBrush(Color.FromRgb(155, 155, 181));
            DrawButton.IsEnabled = false;
            DrawButton.Content = "Уже разыграна";
        }
        else
        {
            StatusText.Text = "Статус: активна";
            StatusText.Foreground = new SolidColorBrush(Color.FromRgb(0, 229, 160));
            DrawButton.IsEnabled = true;
            DrawButton.Content = "Разыграть";
        }
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        GoToPage(new MainPage());
    }

    private void DeleteLotteryButton_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBoxResult answer = MessageBox.Show(
            "Удалить лотерею «" + _lottery.Name + "»?",
            "Удаление",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (answer != MessageBoxResult.Yes)
        {
            return;
        }

        App.State.Controller.RemoveLottery(_lottery);
        App.State.SaveCurrentFormat();
        GoToPage(new MainPage());
    }

    private void DrawButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (_lottery.IsDrawn)
        {
            return;
        }

        App.State.Controller.SellTicketsInLottery(_lottery);
        bool success = App.State.Controller.DrawLottery(_lottery);

        _lottery.MarkAsDrawn();

        if (success)
        {
            MessageBox.Show("Розыгрыш прошел успешно.", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
            MessageBox.Show("Розыгрыш отменен: продано недостаточно билетов.", "Результат", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        App.State.SaveCurrentFormat();
        FillInfo();
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
