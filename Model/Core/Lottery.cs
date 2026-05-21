using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Lottery
    {
        private int _ticketsID;
        public int TicketsPrice
        {
            get
            {
                return (int)PrizePool * 2 / TicketsCount;
            }
        }

        public int TicketsCount { get; private set; }
        private long _winningTicketPrize;
        private int WinningTicketsCount
        {
            get
            {
                return (int)(TicketsCount*0.04);
            }
        }

        public long PrizePool {  get; private set; }

        public List<Ticket> LotteryTickets { get; private set; }

        public Lottery(int ticketsCount, long prizePool)
        {
            _ticketsID = 0;
            TicketsCount = ticketsCount;
            PrizePool = prizePool;
            CreateTickets();
        }

        private void CreateTickets()
        {
            LotteryTickets = new List<Ticket>(TicketsCount);
            _winningTicketPrize = PrizePool / WinningTicketsCount;
            for (int i = 0; i < WinningTicketsCount; i++)
            {
                LotteryTickets[i] = new WinningTicket(_ticketsID, false, TicketsPrice, _winningTicketPrize);
                _ticketsID++;
            }
            for (int i = WinningTicketsCount; i < TicketsCount; i++)
            {
                LotteryTickets[i] = new Ticket(_ticketsID, false);
                _ticketsID++;
            }
            LotteryTickets.Shuffle();
        }
    }
}
