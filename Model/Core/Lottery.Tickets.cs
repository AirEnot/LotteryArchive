using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Lottery
    {
        private int _lastTicketId = -1;

        public bool SellTicket(LotteryParticipant participant)
        {
            if (_lastTicketId + 1 >= TicketsCount)
            {
                return false; // No more tickets available
            }

            if (participant.Balance < TicketsPrice)
            {
                return false; // Not enough balance to buy a ticket
            }

            _lastTicketId++;
            LotteryTickets[_lastTicketId].SellToParticipant(participant);
            participant.GetTicket(LotteryTickets[_lastTicketId]);

            return true;
        }
    }
}
