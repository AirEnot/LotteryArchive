using System.Windows;
using LotteryArchive.Services;
using LotteryArchive.Views;

namespace LotteryArchive;

public partial class App : Application
{
    public static AppState State { get; } = new();

    protected override void OnStartup(StartupEventArgs e)
    {
        ShutdownMode = ShutdownMode.OnExplicitShutdown;
        StorageFormat startupFormat = ChooseStartupFormat();
        State.LoadOnStartup(startupFormat);
        ShutdownMode = ShutdownMode.OnLastWindowClose;
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        State.SaveOnExit();
        base.OnExit(e);
    }

    private static StorageFormat ChooseStartupFormat()
    {
        StorageFormatDialog dialog = new StorageFormatDialog();
        bool? result = dialog.ShowDialog();

        return result == true ? dialog.SelectedFormat : StorageFormat.Json;
    }
}
