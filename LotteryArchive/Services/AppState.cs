using Model.Core;

namespace LotteryArchive.Services;

public class AppState
{
    public LotteryController Controller { get; } = new();

    public StorageFormat CurrentFormat { get; set; } = StorageFormat.Json;

    public void LoadOnStartup(StorageFormat format)
    {
        CurrentFormat = format;
        if (format == StorageFormat.Json)
        {
            Controller.TryLoadFromStorage(Controller.LoadFromJson);
        }
        else
        {
            Controller.TryLoadFromStorage(Controller.LoadFromXml);
        }
    }

    public void SaveOnExit()
    {
        Controller.TrySaveToStorage(Controller.SaveAsJson);
        Controller.TrySaveToStorage(Controller.SaveAsXml);
    }

    public void SaveCurrentFormat()
    {
        Action save = CurrentFormat == StorageFormat.Json
            ? Controller.SaveAsJson
            : Controller.SaveAsXml;

        Controller.TrySaveToStorage(save);
    }

    public void SwitchFormat(StorageFormat newFormat)
    {
        if (newFormat == CurrentFormat)
        {
            return;
        }

        SaveCurrentFormat();

        CurrentFormat = newFormat;

        SaveCurrentFormat();
    }
}
