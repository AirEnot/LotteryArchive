using System.Collections.Generic;
using System.Linq;
using Model.Core;

namespace Model.Data
{
    public class LotteryDTO
    {
        public string Name { get; set; }
        public int TicketsCount { get; set; }
        public long PrizePool { get; set; }
        public List<TicketDTO> LotteryTickets { get; set; }

        public LotteryDTO() { }

        public static LotteryDTO FromDomain(Lottery l)
        {
            return new LotteryDTO
            {
                Name = l.Name,
                TicketsCount = l.TicketsCount,
                PrizePool = l.PrizePool,
                LotteryTickets = l.LotteryTickets.Select(t => TicketDTO.FromDomain(t)).ToList()
            };
        }

        public Lottery ToDomain()
        {
            var restoredTickets = LotteryTickets?.Select(t => t.ToDomain()).ToList();
            return new Lottery(Name, TicketsCount, PrizePool, restoredTickets);
        }
    }
}