using System.Windows;
using LotteryArchive.Services;

namespace LotteryArchive;

public partial class App : Application
{
    public static AppState State { get; } = new();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        State.LoadOnStartup();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        State.SaveOnExit();
        base.OnExit(e);
    }
}
