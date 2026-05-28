namespace Model.Data
{
    public abstract class SerializeManager<T> : FileManager, ISerializer<T>
    {
        public SerializeManager(string name) : base(name) { }

        public SerializeManager(string name, string folderPath, string fileName, string fileExtension )
            : base(name, folderPath, fileName, fileExtension) 
        {
            CreateFile();
        }


        public abstract void Serialize(IEnumerable<T> items, string path);
        public abstract IEnumerable<T> Deserialize(string path);
    }
}
