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
        public WinningTicket(int id, bool isSold, string lotteryName,int price, long prizeAmount) : base(id, isSold, lotteryName, price)
        {
            PrizeAmount = prizeAmount;
        }
    }
}
