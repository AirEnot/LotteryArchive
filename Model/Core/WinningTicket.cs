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
        public WinningTicket(int id, bool isSold, int price, long prizeAmount) : base(id, isSold, price)
        {
            PrizeAmount = prizeAmount;
        }
    }
}
