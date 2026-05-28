namespace Model.Data
{
    public abstract class FileManager : IFileManager, IFileLifeController
    {
        private string _name;
        private string _folderPath;
        private string _fileName;
        private string _fileExtension;

        public string Name => _name;
        public string FolderPath => _folderPath;
        public string FileName => _fileName;
        public string FullPath
        {
            get
            {
                return Path.Combine(FolderPath, _fileName);
            }
        }

        public FileManager(string name)
        {
            _name = name;
            _folderPath = null;
            _fileName = _name;
            _fileExtension = "txt";
        }

        public FileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            _name = name;
            _folderPath = folderPath;
            _fileName = fileName;
            _fileExtension = fileExtension;
        }

        public void SelectFolder(string folderPath)
        {
            _folderPath = folderPath;
        }

        public void ChangeFileName(string fileName)
        {
            _fileName = fileName;
        }

        public void ChangeFileFormat(string fileExtension)
        {
            string oldPath = FullPath;
            string content = File.Exists(oldPath) ? File.ReadAllText(oldPath) : string.Empty;

            _fileExtension = fileExtension;

            EnsureFolderExists();
            File.WriteAllText(FullPath, content);

            if (!string.Equals(oldPath, FullPath, StringComparison.Ordinal) && File.Exists(oldPath))
                File.Delete(oldPath);
        }

        public virtual void CreateFile()
        {
            EnsureFolderExists();

            if (File.Exists(FullPath))
                return;

            File.Create(FullPath).Close();
        }

        public virtual void DeleteFile()
        {
            string path = FullPath;

            if (File.Exists(path))
                File.Delete(path);
        }

        public virtual void EditFile(string text)
        {
            EnsureFolderExists();
            File.WriteAllText(FullPath, text);
        }

        public virtual void ChangeFileExtension(string fileExtension)
        {
            string oldPath = FullPath;
            string content = File.Exists(oldPath) ? File.ReadAllText(oldPath) : string.Empty;

            _fileExtension = fileExtension;

            EnsureFolderExists();
            File.WriteAllText(FullPath, content);

            if (!string.Equals(oldPath, FullPath, StringComparison.Ordinal) && File.Exists(oldPath))
                File.Delete(oldPath);
        }

        private void EnsureFolderExists()
        {
            if (!string.IsNullOrEmpty(FolderPath) && !Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
        }
    }
}
