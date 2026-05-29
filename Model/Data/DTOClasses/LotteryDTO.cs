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
        public bool IsDrawn { get; set; }
        public List<TicketDTO> LotteryTickets { get; set; }

        public LotteryDTO() { }

        public static LotteryDTO FromDomain(Lottery l)
        {
            return new LotteryDTO
            {
                Name = l.Name,
                TicketsCount = l.TicketsCount,
                PrizePool = l.PrizePool,
                IsDrawn = l.IsDrawn,
                LotteryTickets = l.LotteryTickets.Select(t => TicketDTO.FromDomain(t)).ToList()
            };
        }

        public Lottery ToDomain()
        {
            var restoredTickets = LotteryTickets?.Select(t => t.ToDomain()).ToList();
            var lottery = new Lottery(Name, TicketsCount, PrizePool, restoredTickets);
            if (IsDrawn)
                lottery.MarkAsDrawn();
            return lottery;
        }
    }
}