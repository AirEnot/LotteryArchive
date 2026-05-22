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
        public void Draw(List<LotteryParticipant> participants)
        {
            var LotteryResult = CheckIfLotteryValid();

            LotteryResult(participants);
        }
        public Action<List<LotteryParticipant>> CheckIfLotteryValid()
        {
            int count = 0;
            for (int i = 0; i < TicketsCount; i++)
            {
                if (LotteryTickets[i].isSold)
                {
                    count++;
                }
            }
            if ((int)count < TicketsCount * 0.25)
            {
                return CancelLottery;
            }
            return GiveAway;
        }

        public void GiveAway(List<LotteryParticipant> participants)
        {
            foreach (var ticket in LotteryTickets)
            {
                participants.Find(x => x.Id == ticket.ParticipantId)?.GetPrize(ticket);
            }

        }
        public void CancelLottery(List<LotteryParticipant> participants)
        {
            foreach (var ticket in LotteryTickets)
            {
                participants.Find(x => x.Id == ticket.ParticipantId)?.ReturnMoney(ticket);
            }
        }
    }
}

