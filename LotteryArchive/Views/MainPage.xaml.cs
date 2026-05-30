using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LotteryArchive.Display;
using LotteryArchive.Services;
using Model.Core;

namespace LotteryArchive.Views;

public partial class MainPage : Page
{
    private bool _blockFormatChange;

    public MainPage()
    {
        InitializeComponent();
        Loaded += MainPage_Loaded;
    }

    private void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        _blockFormatChange = true;
        if (App.State.CurrentFormat == StorageFormat.Json)
        {
            FormatCombo.SelectedIndex = 0;
        }
        else
        {
            FormatCombo.SelectedIndex = 1;
        }
        _blockFormatChange = false;

        ShowLotteries();
    }

    private void ShowLotteries()
    {
        List<LotteryRow> rows = new List<LotteryRow>();

        foreach (Lottery lottery in App.State.Controller.AllLotteries)
        {
            rows.Add(new LotteryRow(lottery));
        }

        rows.Sort(delegate (LotteryRow a, LotteryRow b)
        {
            if (a.IsDrawn != b.IsDrawn)
            {
                if (a.IsDrawn)
                {
                    return 1;
                }
                return -1;
            }
            return string.Compare(a.Name, b.Name);
        });

        LotteriesList.ItemsSource = rows;

        if (rows.Count == 0)
        {
            LotteriesList.Visibility = Visibility.Collapsed;
            EmptyLotteriesText.Visibility = Visibility.Visible;
        }
        else
        {
            LotteriesList.Visibility = Visibility.Visible;
            EmptyLotteriesText.Visibility = Visibility.Collapsed;
        }
    }

    private void FormatCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_blockFormatChange)
        {
            return;
        }

        StorageFormat newFormat = StorageFormat.Json;
        if (FormatCombo.SelectedIndex == 1)
        {
            newFormat = StorageFormat.Xml;
        }

        App.State.SwitchFormat(newFormat);
        ShowLotteries();
    }

    private void StatisticsButton_OnClick(object sender, RoutedEventArgs e)
    {
        GoToPage(new StatisticsPage());
    }

    private void ParticipantsButton_OnClick(object sender, RoutedEventArgs e)
    {
        GoToPage(new ParticipantsPage());
    }

    private void CreateLotteryButton_OnClick(object sender, RoutedEventArgs e)
    {
        GoToPage(new CreateLotteryPage());
    }

    private void LotteriesList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        LotteryRow row = LotteriesList.SelectedItem as LotteryRow;
        if (row != null)
        {
            GoToPage(new LotteryDetailsPage(row.Lottery));
        }
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
