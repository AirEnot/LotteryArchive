using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class WinningTicket : Ticket
    {
        public int PrizeAmount { get; private set; }
        public WinningTicket(int id, string participantId, bool isSold, int price, int prizeAmount) : base(id, participantId, isSold, price)
        {
            PrizeAmount = prizeAmount;
        }
    }
}
