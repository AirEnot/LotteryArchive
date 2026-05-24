using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Model.Core;

namespace Model.Data
{
    public class XmlParticipantSerializeManager : SerializeManager<LotteryParticipant>
    {
        public XmlParticipantSerializeManager(string folderPath, string fileName) 
            : base("XmlParticipantManager", folderPath, fileName, "xml") 
        { }

        public override void Serialize(IEnumerable<LotteryParticipant> items, string path)
        {
            // 1. Перегоняем настоящих участников в плоские DTO
            var dtos = items.Select(p => LotteryParticipantDTO.FromDomain(p)).ToList();

            // 2. Создаем стандартный XML-сериализатор для списка DTO
            // Передаем типы-наследники билетов (WinningTicketDTO), чтобы полиморфизм не сломался
            var extraTypes = new Type[] { typeof(WinningTicketDTO) };
            var xmlSerializer = new XmlSerializer(typeof(List<LotteryParticipantDTO>), extraTypes);

            // Исполняем запись через потоки (как в 10 лабе)
            using (var sw = new StreamWriter(path))
            {
                xmlSerializer.Serialize(sw, dtos);
            }
        }

        

        // ОСНОВНОЙ МЕТОД ЗАГРУЗКИ (Вызывается из DataStorage)
        public override IEnumerable<LotteryParticipant> Deserialize(string path)
        {
            if (!File.Exists(path)) 
                return new List<LotteryParticipant>();

            var extraTypes = new Type[] { typeof(WinningTicketDTO) };
            var xmlSerializer = new XmlSerializer(typeof(List<LotteryParticipantDTO>), extraTypes);

            using (var sr = new StreamReader(path))
            {
                var dtos = (List<LotteryParticipantDTO>)xmlSerializer.Deserialize(sr);
                
                // 3. Перегоняем DTO обратно в полноценные бизнес-объекты через новые конструкторы
                return dtos?.Select(dto => dto.ToDomain()).ToList() ?? new List<LotteryParticipant>();
            }
        }
    }
}