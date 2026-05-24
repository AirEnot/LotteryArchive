using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class WinningTicket : Ticket
    {
        public long PrizeAmount { get; private set; }
        public WinningTicket(int id, bool IsSold, string lotteryName,int price, long prizeAmount, string participantId = null)
            : base(id, IsSold, lotteryName, price, participantId)
        {
            PrizeAmount = prizeAmount;
        }
    }
}
