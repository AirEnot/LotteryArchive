using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Lottery
    {
        public bool Draw(List<LotteryParticipant> participants)
        {
            var lotteryResult = CheckIfLotteryValid();
            lotteryResult(participants);
            return !ReferenceEquals(lotteryResult, CancelLottery);
        }
        public Action<List<LotteryParticipant>> CheckIfLotteryValid()
        {
            int count = 0;
            for (int i = 0; i < TicketsCount; i++)
            {
                if (_lotteryTickets[i].IsSold)
                {
                    count++;
                }
            }

            if (count * 4 <= TicketsCount)
            {
                return CancelLottery;
            }
            return GiveAway;
        }

        public void GiveAway(List<LotteryParticipant> participants)
        {
            foreach (var ticket in _lotteryTickets)
            {
                participants.Find(x => x.Id == ticket.ParticipantId)?.GetPrize(ticket);
            }

        }
        public void CancelLottery(List<LotteryParticipant> participants)
        {
            foreach (var ticket in _lotteryTickets)
            {
                participants.Find(x => x.Id == ticket.ParticipantId)?.ReturnMoney(ticket);
            }
        }
    }
}

