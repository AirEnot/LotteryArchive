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
            var dtos = items.Select(p => LotteryParticipantDTO.FromDomain(p)).ToList();

            var extraTypes = new Type[] { typeof(WinningTicketDTO) };
            var xmlSerializer = new XmlSerializer(typeof(List<LotteryParticipantDTO>), extraTypes);

            File.WriteAllText(path, string.Empty);
            using (var sw = new StreamWriter(path))
            {
                xmlSerializer.Serialize(sw, dtos);
            }
        }

        

        public override IEnumerable<LotteryParticipant> Deserialize(string path)
        {
            if (!File.Exists(path)) 
                return new List<LotteryParticipant>();

            var extraTypes = new Type[] { typeof(WinningTicketDTO) };
            var xmlSerializer = new XmlSerializer(typeof(List<LotteryParticipantDTO>), extraTypes);

            using (var sr = new StreamReader(path))
            {
                var dtos = (List<LotteryParticipantDTO>)xmlSerializer.Deserialize(sr);
                
                return dtos?.Select(dto => dto.ToDomain()).ToList() ?? new List<LotteryParticipant>();
            }
        }
    }
}