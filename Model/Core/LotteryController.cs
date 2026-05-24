using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Model.Data;

namespace Model.Core
{
    public class LotteryController
    {
        //Надо дописать пути, создать отдельный класс Paths и прописать файлы
        public List<LotteryParticipant> AllPeople { get; private set; }
        public List<Lottery> AllLotteries { get; private set; }

        private DataStorage<LotteryParticipant> _participantStorage = new DataStorage<LotteryParticipant>();
        private DataStorage<Lottery> _lotteryStorage = new DataStorage<Lottery>();

        private JsonSerializeManager<LotteryParticipant> _jsonParticipantManager = new JsonSerializeManager<LotteryParticipant>(" ", " ");
        private JsonSerializeManager<Lottery> _jsonLotteryManager = new JsonSerializeManager<Lottery>(" ", " ");

        private XmlParticipantSerializeManager _xmlParticipantManager = new XmlParticipantSerializeManager(" ", " ");

        private XmlLotterySerializeManager _xmlLotteryManager = new XmlLotterySerializeManager(" ", " ");

        public LotteryController()
        {
            AllPeople = new List<LotteryParticipant>();
            AllLotteries = new List<Lottery>();
        }

        public void AddParticipant(LotteryParticipant participant)
        {
            AllPeople.Add(participant);
        }

        public void AddLottery(Lottery lottery)
        {
            AllLotteries.Add(lottery);
        }

        public void DrawLottery(Lottery lottery)
        {
            lottery.Draw(AllPeople);
        }

        public void SaveAsJson()
        {
            _participantStorage.ChangeManager(_jsonParticipantManager);
            _lotteryStorage.ChangeManager(_jsonLotteryManager);

            _participantStorage.Save(AllPeople);
            _lotteryStorage.Save(AllLotteries);
        }

        public void SaveAsXml()
        {
            _participantStorage.ChangeManager(_xmlParticipantManager);
            _lotteryStorage.ChangeManager(_xmlLotteryManager);

            _participantStorage.Save(AllPeople);
            _lotteryStorage.Save(AllLotteries);
        }

        public void LoadFromJson()
        {
            _participantStorage.ChangeManager(_jsonParticipantManager);
            _lotteryStorage.ChangeManager(_jsonLotteryManager);

            AllPeople = _participantStorage.Load();
            AllLotteries = _lotteryStorage.Load();
        }

        public void LoadFromXml()
        {
            _participantStorage.ChangeManager(_xmlParticipantManager);
            _lotteryStorage.ChangeManager(_xmlLotteryManager);

            AllPeople = _participantStorage.Load();
            AllLotteries = _lotteryStorage.Load();
        }
    }
}
