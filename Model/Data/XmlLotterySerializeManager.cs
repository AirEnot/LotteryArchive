using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Model.Core;

namespace Model.Data
{
    public class XmlLotterySerializeManager : SerializeManager<Lottery>
    {
        public XmlLotterySerializeManager(string folderPath, string fileName) 
            : base("XmlLotteryManager", folderPath, fileName, "xml") 
        { }
        
        public override void Serialize(IEnumerable<Lottery> items, string path)
        {
            var dtos = items.Select(l => LotteryDTO.FromDomain(l)).ToList();

            var extraTypes = new Type[] { typeof(WinningTicketDTO) };
            var xmlSerializer = new XmlSerializer(typeof(List<LotteryDTO>), extraTypes);
            File.WriteAllText(path, string.Empty); // Очищаем файл перед записью
            using (var sw = new StreamWriter(path))
            {
                xmlSerializer.Serialize(sw, dtos);
            }
        }

        public override IEnumerable<Lottery> Deserialize(string path)
        {
            if (!File.Exists(path)) 
                return new List<Lottery>();

            var extraTypes = new Type[] { typeof(WinningTicketDTO) };
            var xmlSerializer = new XmlSerializer(typeof(List<LotteryDTO>), extraTypes);

            using (var sr = new StreamReader(path))
            {
                var dtos = (List<LotteryDTO>)xmlSerializer.Deserialize(sr);
                
                return dtos?.Select(dto => dto.ToDomain()).ToList() ?? new List<Lottery>();
            }
        }
    }
}