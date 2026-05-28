using System.Windows;
using System.Windows.Controls;
using LotteryArchive.Views;

namespace LotteryArchive
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Navigate(new MainPage());
        }

        public void Navigate(Page page)
        {
            RootFrame.Navigate(page);
        }
    }
}