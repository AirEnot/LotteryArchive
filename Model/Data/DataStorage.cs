using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class DataStorage<T>
    {
        private SerializeManager<T> _manager;

        public void ChangeManager(SerializeManager<T> manager)
        {
            _manager = manager;
        }

        public void Save(IEnumerable<T> items)
        {
            if (_manager == null) throw new InvalidOperationException("Менеджер не задан");
            _manager.Serialize(items, _manager.FullPath);
        }

        public List<T> Load()
        {
            if (_manager == null) throw new InvalidOperationException("Менеджер не задан");
            return _manager.Deserialize(_manager.FullPath).ToList();
        }
    }
}
