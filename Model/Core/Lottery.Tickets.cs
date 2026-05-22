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
                return false;
            }

            _lastTicketId++;
            _lotteryTickets[_lastTicketId].SellToParticipant(participant);
            participant.GetTicket(_lotteryTickets[_lastTicketId]);

            return true;
        }
    }
}
