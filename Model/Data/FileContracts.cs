namespace Model.Data
{
    public interface IFileManager
    {
        string FolderPath { get; }
        string FileName { get; }
        string FullPath { get; }

        void SelectFolder(string folderPath);
        void ChangeFileName(string fileName);
        void ChangeFileFormat(string fileExtension);
    }

    public interface IFileLifeController
    {
        void CreateFile();
        void DeleteFile();
        void EditFile(string text);
        void ChangeFileExtension(string fileExtension);
    }

    public interface ISerializer<T> 
    {
        void Serialize(IEnumerable<T> items, string path);
        IEnumerable<T> Deserialize(string path);
    }
}
