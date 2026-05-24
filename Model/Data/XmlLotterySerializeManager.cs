using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Model.Core;

namespace Model.Data
{
    // Наследуемся от твоего базового SerializeManager для Лотереи
    public class XmlLotterySerializeManager : SerializeManager<Lottery>
    {
        public XmlLotterySerializeManager(string folderPath, string fileName) 
            : base("XmlLotteryManager", folderPath, fileName, "xml") 
        { }

        // ОСНОВНОЙ МЕТОД СОХРАНЕНИЯ (Вызывается из DataStorage)
        public override void Serialize(IEnumerable<Lottery> items, string path)
        {
            // 1. Конвертируем доменные модели лотерей в DTO-структуры
            var dtos = items.Select(l => LotteryDTO.FromDomain(l)).ToList();

            // 2. Указываем массив известных типов для сохранения полиморфных списков билетов внутри лотереи
            var extraTypes = new Type[] { typeof(WinningTicketDTO) };
            var xmlSerializer = new XmlSerializer(typeof(List<LotteryDTO>), extraTypes);

            using (var sw = new StreamWriter(path))
            {
                xmlSerializer.Serialize(sw, dtos);
            }
        }

        // ОСНОВНОЙ МЕТОД ЗАГРУЗКИ (Вызывается из DataStorage)
        public override IEnumerable<Lottery> Deserialize(string path)
        {
            if (!File.Exists(path)) 
                return new List<Lottery>();

            var extraTypes = new Type[] { typeof(WinningTicketDTO) };
            var xmlSerializer = new XmlSerializer(typeof(List<LotteryDTO>), extraTypes);

            using (var sr = new StreamReader(path))
            {
                var dtos = (List<LotteryDTO>)xmlSerializer.Deserialize(sr);
                
                // 3. Восстанавливаем лотереи из DTO с сохранением внутреннего состояния счетчиков билетов
                return dtos?.Select(dto => dto.ToDomain()).ToList() ?? new List<Lottery>();
            }
        }
    }
}