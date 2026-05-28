using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Model.Core;

namespace LotteryArchive.Views;

public partial class CreateParticipantPage : Page
{
    private static readonly Regex DigitsRegex = new("^[0-9]+$");

    public CreateParticipantPage()
    {
        InitializeComponent();
    }

    private void CreateButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!ValidateForm(out var validationMessage))
        {
            ValidationText.Text = validationMessage;
            ValidationText.Visibility = Visibility.Visible;
            return;
        }

        var participant = new LotteryParticipant(
            FullNameBox.Text.Trim(),
            long.Parse(BalanceBox.Text),
            int.Parse(GreedBox.Text));

        App.State.Controller.AddParticipant(participant);
        App.State.SaveCurrentFormat();
        Navigate(new ParticipantsPage());
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        Navigate(new ParticipantsPage());
    }

    private void NumericBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !DigitsRegex.IsMatch(e.Text);
    }

    private bool ValidateForm(out string message)
    {
        message = string.Empty;
        if (string.IsNullOrWhiteSpace(FullNameBox.Text))
        {
            message = "Введите ФИО участника.";
            return false;
        }

        if (!long.TryParse(BalanceBox.Text, out var balance) || balance < 0)
        {
            message = "Баланс должен быть неотрицательным числом.";
            return false;
        }

        if (!int.TryParse(GreedBox.Text, out var greed) || greed is < 0 or > 100)
        {
            message = "Жадность должна быть в диапазоне от 0 до 100.";
            return false;
        }

        return true;
    }

    private static void Navigate(Page page)
    {
        if (Application.Current.MainWindow is MainWindow window)
        {
            window.Navigate(page);
        }
    }
}
