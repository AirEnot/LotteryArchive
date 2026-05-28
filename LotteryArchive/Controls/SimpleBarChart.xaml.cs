using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using LotteryArchive.ViewModels;

namespace LotteryArchive.Controls;

public partial class SimpleBarChart : UserControl
{
    private static readonly Brush SpentBrush = new SolidColorBrush(Color.FromRgb(220, 92, 76));
    private static readonly Brush WonBrush = new SolidColorBrush(Color.FromRgb(56, 142, 96));
    private static readonly Brush LabelBrush = new SolidColorBrush(Color.FromRgb(90, 90, 90));

    public SimpleBarChart()
    {
        InitializeComponent();
        Loaded += (_, _) => Redraw();
        SizeChanged += (_, _) => Redraw();
    }

    public void SetData(IReadOnlyList<ParticipantSelectionItem> items)
    {
        _items = items.Where(i => i.IsSelected).ToList();
        Redraw();
    }

    private List<ParticipantSelectionItem> _items = [];

    private void Redraw()
    {
        ChartCanvas.Children.Clear();

        if (_items.Count == 0 || ActualWidth < 40 || ActualHeight < 40)
        {
            EmptyText.Visibility = Visibility.Visible;
            return;
        }

        EmptyText.Visibility = Visibility.Collapsed;

        const double leftMargin = 12;
        const double bottomMargin = 70;
        const double topMargin = 24;
        const double legendHeight = 28;

        var chartWidth = ActualWidth - leftMargin - 12;
        var chartHeight = ActualHeight - topMargin - bottomMargin - legendHeight;

        var maxValue = _items.Max(i => Math.Max(i.TotalSpent, i.TotalWon));
        if (maxValue <= 0)
        {
            maxValue = 1;
        }

        var groupWidth = chartWidth / _items.Count;
        var barWidth = Math.Min(36, groupWidth / 3);

        for (var index = 0; index < _items.Count; index++)
        {
            var item = _items[index];
            var centerX = leftMargin + groupWidth * index + groupWidth / 2;

            DrawBar(centerX - barWidth - 4, item.TotalSpent, maxValue, chartHeight, topMargin, barWidth, SpentBrush);
            DrawBar(centerX + 4, item.TotalWon, maxValue, chartHeight, topMargin, barWidth, WonBrush);

            var label = new TextBlock
            {
                Text = TrimName(item.FullName),
                Foreground = LabelBrush,
                FontSize = 11,
                TextAlignment = TextAlignment.Center,
                Width = groupWidth - 4
            };

            Canvas.SetLeft(label, leftMargin + groupWidth * index);
            Canvas.SetTop(label, topMargin + chartHeight + 8);
            ChartCanvas.Children.Add(label);
        }

        DrawLegend(leftMargin, topMargin + chartHeight + 36);
    }

    private void DrawBar(double x, long value, long maxValue, double chartHeight, double top, double width, Brush brush)
    {
        var height = value / (double)maxValue * chartHeight;
        var rect = new Rectangle
        {
            Width = width,
            Height = Math.Max(2, height),
            Fill = brush,
            RadiusX = 3,
            RadiusY = 3
        };

        Canvas.SetLeft(rect, x);
        Canvas.SetTop(rect, top + chartHeight - rect.Height);
        ChartCanvas.Children.Add(rect);
    }

    private void DrawLegend(double x, double y)
    {
        AddLegendItem(x, y, SpentBrush, "Потрачено");
        AddLegendItem(x + 120, y, WonBrush, "Выиграно");
    }

    private void AddLegendItem(double x, double y, Brush brush, string text)
    {
        var colorBox = new Rectangle { Width = 14, Height = 14, Fill = brush, RadiusX = 2, RadiusY = 2 };
        Canvas.SetLeft(colorBox, x);
        Canvas.SetTop(colorBox, y);

        var label = new TextBlock { Text = text, FontSize = 12, Foreground = LabelBrush };
        Canvas.SetLeft(label, x + 20);
        Canvas.SetTop(label, y - 1);

        ChartCanvas.Children.Add(colorBox);
        ChartCanvas.Children.Add(label);
    }

    private static string TrimName(string name) =>
        name.Length <= 14 ? name : name[..12] + "…";
}
