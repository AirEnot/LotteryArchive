using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class LotteryController
    {
        public List<LotteryParticipant> AllPeople { get; private set; }
        public List<Lottery> AllLotteries { get; private set; }

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
    }
}
