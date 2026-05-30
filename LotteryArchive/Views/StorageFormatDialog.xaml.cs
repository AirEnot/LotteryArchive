using System.Windows;
using LotteryArchive.Services;

namespace LotteryArchive.Views;

public partial class StorageFormatDialog : Window
{
    public StorageFormat SelectedFormat { get; private set; } = StorageFormat.Json;

    public StorageFormatDialog()
    {
        InitializeComponent();
    }

    private void JsonButton_OnClick(object sender, RoutedEventArgs e)
    {
        SelectedFormat = StorageFormat.Json;
        DialogResult = true;
    }

    private void XmlButton_OnClick(object sender, RoutedEventArgs e)
    {
        SelectedFormat = StorageFormat.Xml;
        DialogResult = true;
    }
}
