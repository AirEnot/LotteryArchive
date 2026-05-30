using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LotteryArchive.Helpers;
using Model.Core;

namespace LotteryArchive.Views;

public partial class CreateParticipantPage : Page
{
    private Regex _digitsOnly = new Regex("^[0-9]+$");

    public CreateParticipantPage()
    {
        InitializeComponent();
        GreedSlider.Value = 0;
        GreedValueText.Text = "0";
    }

    private void CreateButton_OnClick(object sender, RoutedEventArgs e)
    {
        string message;
        if (!CheckForm(out message))
        {
            ValidationText.Text = message;
            ValidationText.Visibility = Visibility.Visible;
            return;
        }

        long balance = long.Parse(BalanceBox.Text);
        int greed = (int)GreedSlider.Value;

        LotteryParticipant participant = new LotteryParticipant(FullNameBox.Text.Trim(), balance, greed);
        App.State.Controller.AddParticipant(participant);
        App.State.SaveCurrentFormat();
        GoToPage(new ParticipantsPage());
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        GoToPage(new ParticipantsPage());
    }

    private void NumericBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !_digitsOnly.IsMatch(e.Text);
    }

    private void BalanceBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(BalanceBox.Text))
        {
            ValidationText.Visibility = Visibility.Collapsed;
            return;
        }

        long balance;
        if (!long.TryParse(BalanceBox.Text, out balance))
        {
            return;
        }

        if (InputLimits.IsNumberTooBig(balance))
        {
            ValidationText.Text = InputLimits.TooBigMessage("Баланс");
            ValidationText.Visibility = Visibility.Visible;
        }
        else
        {
            ValidationText.Visibility = Visibility.Collapsed;
        }
    }

    private void GreedSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        GreedValueText.Text = ((int)GreedSlider.Value).ToString();
    }

    private bool CheckForm(out string message)
    {
        message = "";

        if (string.IsNullOrWhiteSpace(FullNameBox.Text))
        {
            message = "Введите ФИО участника.";
            return false;
        }

        long balance;
        if (!long.TryParse(BalanceBox.Text, out balance) || balance < 0)
        {
            message = "Введите корректный баланс.";
            return false;
        }

        if (InputLimits.IsNumberTooBig(balance))
        {
            message = InputLimits.TooBigMessage("Баланс");
            return false;
        }

        int greed = (int)GreedSlider.Value;
        if (greed < InputLimits.MinGreed || greed > InputLimits.MaxGreed)
        {
            message = "Жадность должна быть от 0 до 100.";
            return false;
        }

        return true;
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
