using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LotteryArchive.Display;
using Model.Core;

namespace LotteryArchive.Views;

public partial class StatisticsPage : Page
{
    private List<ParticipantChartRow> _rows = new List<ParticipantChartRow>();

    public StatisticsPage()
    {
        InitializeComponent();
        Loaded += StatisticsPage_Loaded;
    }

    private void StatisticsPage_Loaded(object sender, RoutedEventArgs e)
    {
        _rows = new List<ParticipantChartRow>();

        foreach (LotteryParticipant person in App.State.Controller.AllPeople)
        {
            _rows.Add(new ParticipantChartRow(person));
        }

        ParticipantsCheckList.ItemsSource = _rows;
        UpdateChart();
    }

    private void ParticipantCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        CheckBox checkBox = sender as CheckBox;
        ParticipantChartRow row = checkBox.DataContext as ParticipantChartRow;
        if (row == null)
        {
            return;
        }

        row.IsSelected = checkBox.IsChecked == true;
        UpdateChart();
    }

    private void UpdateChart()
    {
        List<ParticipantChartRow> selected = new List<ParticipantChartRow>();

        foreach (ParticipantChartRow row in _rows)
        {
            if (row.IsSelected)
            {
                selected.Add(row);
            }
        }

        StatsChart.SetData(selected);
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        MainWindow window = Application.Current.MainWindow as MainWindow;
        if (window != null)
        {
            window.Navigate(new MainPage());
        }
    }
}
