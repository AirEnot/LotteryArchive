using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using LotteryArchive.Display;

namespace LotteryArchive.Controls;

public partial class SimpleBarChart : UserControl
{
    private const double MinGroupWidth = 70;

    private Brush _spentColor = new SolidColorBrush(Color.FromRgb(220, 92, 76));
    private Brush _wonColor = new SolidColorBrush(Color.FromRgb(56, 142, 96));
    private Brush _textColor = new SolidColorBrush(Color.FromRgb(90, 90, 90));

    private List<ParticipantChartRow> _rows = new List<ParticipantChartRow>();

    public SimpleBarChart()
    {
        InitializeComponent();
        Loaded += SimpleBarChart_Loaded;
        SizeChanged += SimpleBarChart_SizeChanged;
    }

    public void SetData(List<ParticipantChartRow> rows)
    {
        _rows = rows;
        DrawChart();
    }

    private void SimpleBarChart_Loaded(object sender, RoutedEventArgs e)
    {
        DrawChart();
    }

    private void SimpleBarChart_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        DrawChart();
    }

    private void DrawChart()
    {
        ChartCanvas.Children.Clear();

        if (_rows.Count == 0)
        {
            EmptyText.Visibility = Visibility.Visible;
            ChartScroll.Visibility = Visibility.Collapsed;
            return;
        }

        EmptyText.Visibility = Visibility.Collapsed;
        ChartScroll.Visibility = Visibility.Visible;

        double top = 20;
        double bottomSpace = 30;
        double chartHeight = 200;
        double left = 10;

        double totalWidth = _rows.Count * MinGroupWidth;
        if (totalWidth < 300)
        {
            totalWidth = 300;
        }

        ChartCanvas.Width = totalWidth;
        double groupWidth = totalWidth / _rows.Count;
        double barWidth = 22;

        long maxValue = 1;
        foreach (ParticipantChartRow row in _rows)
        {
            if (row.TotalSpent > maxValue)
            {
                maxValue = row.TotalSpent;
            }
            if (row.TotalWon > maxValue)
            {
                maxValue = row.TotalWon;
            }
        }

        for (int i = 0; i < _rows.Count; i++)
        {
            ParticipantChartRow row = _rows[i];
            double center = left + groupWidth * i + groupWidth / 2;

            double spentX = center - barWidth - 2;
            double wonX = center + 2;

            DrawOneBar(spentX, row.TotalSpent, maxValue, chartHeight, top, barWidth, _spentColor);
            DrawOneBar(wonX, row.TotalWon, maxValue, chartHeight, top, barWidth, _wonColor);

            string shortName = row.FullName;
            if (shortName.Length > 8)
            {
                shortName = shortName.Substring(0, 7) + "…";
            }

            TextBlock nameLabel = new TextBlock();
            nameLabel.Text = shortName;
            nameLabel.FontSize = 10;
            nameLabel.Foreground = _textColor;
            nameLabel.Width = groupWidth - 4;
            nameLabel.TextAlignment = TextAlignment.Center;
            Canvas.SetLeft(nameLabel, left + groupWidth * i);
            Canvas.SetTop(nameLabel, top + chartHeight + 6);
            ChartCanvas.Children.Add(nameLabel);
        }

        DrawLegend(left, top + chartHeight + 22);
    }

    private void DrawOneBar(double x, long value, long maxValue, double chartHeight, double top, double width, Brush color)
    {
        double height = value / (double)maxValue * chartHeight;
        if (height < 2)
        {
            height = 2;
        }

        Rectangle bar = new Rectangle();
        bar.Width = width;
        bar.Height = height;
        bar.Fill = color;
        Canvas.SetLeft(bar, x);
        Canvas.SetTop(bar, top + chartHeight - height);
        ChartCanvas.Children.Add(bar);

        if (value > 0)
        {
            TextBlock valueText = new TextBlock();
            valueText.Text = value.ToString();
            valueText.FontSize = 9;
            valueText.Foreground = color;
            valueText.Width = width + 10;
            valueText.TextAlignment = TextAlignment.Center;
            Canvas.SetLeft(valueText, x - 5);
            Canvas.SetTop(valueText, top + chartHeight - height - 14);
            ChartCanvas.Children.Add(valueText);
        }
    }

    private void DrawLegend(double x, double y)
    {
        Rectangle spentBox = new Rectangle();
        spentBox.Width = 12;
        spentBox.Height = 12;
        spentBox.Fill = _spentColor;
        Canvas.SetLeft(spentBox, x);
        Canvas.SetTop(spentBox, y);

        TextBlock spentText = new TextBlock();
        spentText.Text = "Потрачено";
        spentText.FontSize = 11;
        spentText.Foreground = _textColor;
        Canvas.SetLeft(spentText, x + 16);
        Canvas.SetTop(spentText, y - 1);

        Rectangle wonBox = new Rectangle();
        wonBox.Width = 12;
        wonBox.Height = 12;
        wonBox.Fill = _wonColor;
        Canvas.SetLeft(wonBox, x + 100);
        Canvas.SetTop(wonBox, y);

        TextBlock wonText = new TextBlock();
        wonText.Text = "Выиграно";
        wonText.FontSize = 11;
        wonText.Foreground = _textColor;
        Canvas.SetLeft(wonText, x + 116);
        Canvas.SetTop(wonText, y - 1);

        ChartCanvas.Children.Add(spentBox);
        ChartCanvas.Children.Add(spentText);
        ChartCanvas.Children.Add(wonBox);
        ChartCanvas.Children.Add(wonText);
    }
}
