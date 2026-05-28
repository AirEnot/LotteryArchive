using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LotteryArchive.Services;
using LotteryArchive.ViewModels;
using Model.Core;

namespace LotteryArchive.Views;

public partial class MainPage : Page
{
    private bool _suppressFormatChange;

    public MainPage()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _suppressFormatChange = true;
        FormatCombo.SelectedIndex = App.State.CurrentFormat == StorageFormat.Json ? 0 : 1;
        _suppressFormatChange = false;
        RefreshLotteries();
    }

    private void RefreshLotteries()
    {
        var items = App.State.Controller.AllLotteries
            .OrderBy(l => App.State.IsLotteryDrawn(l))
            .ThenBy(l => l.Name)
            .Select(l => new LotteryListItem(l, App.State.IsLotteryDrawn(l)))
            .ToList();

        LotteriesList.ItemsSource = items;

        var hasLotteries = items.Count > 0;
        LotteriesList.Visibility = hasLotteries ? Visibility.Visible : Visibility.Collapsed;
        EmptyLotteriesText.Visibility = hasLotteries ? Visibility.Collapsed : Visibility.Visible;
    }

    private void FormatCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_suppressFormatChange || FormatCombo.SelectedIndex < 0)
        {
            return;
        }

        var newFormat = FormatCombo.SelectedIndex == 0 ? StorageFormat.Json : StorageFormat.Xml;
        App.State.SwitchFormat(newFormat);

        _suppressFormatChange = true;
        FormatCombo.SelectedIndex = App.State.CurrentFormat == StorageFormat.Json ? 0 : 1;
        _suppressFormatChange = false;
    }

    private void StatisticsButton_OnClick(object sender, RoutedEventArgs e)
    {
        Navigate(new StatisticsPage());
    }

    private void ParticipantsButton_OnClick(object sender, RoutedEventArgs e)
    {
        Navigate(new ParticipantsPage());
    }

    private void CreateLotteryButton_OnClick(object sender, RoutedEventArgs e)
    {
        Navigate(new CreateLotteryPage());
    }

    private void LotteriesList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (LotteriesList.SelectedItem is LotteryListItem item)
        {
            Navigate(new LotteryDetailsPage(item.Lottery));
        }
    }

    private static void Navigate(Page page)
    {
        if (Application.Current.MainWindow is MainWindow window)
        {
            window.Navigate(page);
        }
    }
}
