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
        private const string JsonParticipantsFilePath = "participants.json";
        private const string JsonLotteriesFilePath = "lotteries.json";
        private const string XmlParticipantsFilePath = "participants.xml";
        private const string XmlLotteriesFilePath = "lotteries.xml";

        private const string LogFileName = "logging.txt";

        private static string DataDirectory = Path.GetFullPath(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Model\Data\SerializedData")
        );

        public List<LotteryParticipant> AllPeople { get; private set; }
        public List<Lottery> AllLotteries { get; private set; }

        private DataStorage<LotteryParticipant> _participantStorage = new DataStorage<LotteryParticipant>();
        private DataStorage<Lottery> _lotteryStorage = new DataStorage<Lottery>();

        private JsonSerializeManager<LotteryParticipant> _jsonParticipantManager 
            = new JsonSerializeManager<LotteryParticipant>(DataDirectory, JsonParticipantsFilePath);
        private JsonSerializeManager<Lottery> _jsonLotteryManager 
            = new JsonSerializeManager<Lottery>(DataDirectory, JsonLotteriesFilePath);

        private XmlParticipantSerializeManager _xmlParticipantManager 
            = new XmlParticipantSerializeManager(DataDirectory, XmlParticipantsFilePath);

        private XmlLotterySerializeManager _xmlLotteryManager 
            = new XmlLotterySerializeManager(DataDirectory, XmlLotteriesFilePath);

        public LotteryController()
        {
            AllPeople = new List<LotteryParticipant>();
            AllLotteries = new List<Lottery>();
            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }
        }

        public void AddParticipant(LotteryParticipant participant)
        {
            AllPeople.Add(participant);
        }

        public void AddLottery(Lottery lottery)
        {
            AllLotteries.Add(lottery);
        }

        public bool DrawLottery(Lottery lottery)
        {
            return lottery.Draw(AllPeople);
        }

        public void SellTicketsInLottery(Lottery lottery)
        {
            int totalGreed = AllPeople.Sum(p => p.Greed);
            foreach (LotteryParticipant person in AllPeople)
                person.BuyTicket(lottery, totalGreed);
        }

        public void RemoveLottery(Lottery lottery)
        {
            AllLotteries.Remove(lottery);
        }

        public void RemoveParticipant(LotteryParticipant participant)
        {
            AllPeople.Remove(participant);
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

        public void TrySaveToStorage(Action saveMethod)
        {
            try
            {
                saveMethod();
            }
            catch (Exception ex)
            {
                LogException("сохранение", ex);
            }
        }

        public void TryLoadFromStorage(Action loadMethod)
        {
            try
            {
                loadMethod();
            }
            catch (Exception ex)
            {
                LogException("загрузка", ex);
            }
        }

        private static void LogException(string operation, Exception ex)
        {
            try
            {
                var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogFileName);
                var entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Ошибка при {operation} данных:{Environment.NewLine}{ex}{Environment.NewLine}{new string('-', 60)}{Environment.NewLine}";
                File.AppendAllText(logPath, entry);
            }
            catch
            {
                
            }
        }
    }
}
