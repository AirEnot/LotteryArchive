using System.Windows;
using System.Windows.Controls;
using LotteryArchive.ViewModels;

namespace LotteryArchive.Views;

public partial class StatisticsPage : Page
{
    private List<ParticipantSelectionItem> _items = [];

    public StatisticsPage()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _items = App.State.Controller.AllPeople
            .Select(p => new ParticipantSelectionItem(p))
            .ToList();

        foreach (var item in _items)
        {
            item.PropertyChanged += (_, _) => UpdateChart();
        }

        ParticipantsCheckList.ItemsSource = _items;
        UpdateChart();
    }

    private void UpdateChart()
    {
        StatsChart.SetData(_items);
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (Application.Current.MainWindow is MainWindow window)
        {
            window.Navigate(new MainPage());
        }
    }
}
