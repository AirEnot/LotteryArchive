using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Lottery
    {
        public string Name { get; private set; }
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
                return (int)(TicketsCount * 0.04);
            }
        }

        public long PrizePool { get; private set; }

        private List<Ticket> _lotteryTickets = new();

        public List<Ticket> LotteryTickets => _lotteryTickets.ToList();

        public Lottery(string name, int ticketsCount, long prizePool)
        {
            Name = name;
            _ticketsID = 0;
            TicketsCount = ticketsCount;
            PrizePool = prizePool;
            CreateTickets();    
        }
        public Lottery(string name, int ticketsCount, long prizePool, List<Ticket> tickets = null)
        {
            Name = name;
            TicketsCount = ticketsCount;
            PrizePool = prizePool;
            _lotteryTickets = tickets ?? new List<Ticket>();
            _winningTicketPrize = PrizePool / WinningTicketsCount;
            
            if (_lotteryTickets.Any())
            {
                _ticketsID = _lotteryTickets.Max(t => t.Id) + 1;
            }
            
            _lastTicketId = _lotteryTickets.FindLastIndex(t => t.IsSold);
        }

        private void CreateTickets()
        {
            _lotteryTickets = new List<Ticket>(TicketsCount);
            _winningTicketPrize = PrizePool / WinningTicketsCount;
            for (int i = 0; i < WinningTicketsCount; i++)
            {
                _lotteryTickets.Add(new WinningTicket(_ticketsID, false, Name, TicketsPrice, _winningTicketPrize));
                _ticketsID++;
            }
            for (int i = WinningTicketsCount; i < TicketsCount; i++)
            {
                _lotteryTickets.Add(new Ticket(_ticketsID, false, Name, TicketsPrice));
                _ticketsID++;
            }
            _lotteryTickets.Shuffle();
        }
    }
}