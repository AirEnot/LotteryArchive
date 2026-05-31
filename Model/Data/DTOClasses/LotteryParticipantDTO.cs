using System.Collections.Generic;
using System.Linq;
using Model.Core;

namespace Model.Data
{
    public class LotteryParticipantDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public long Balance { get; set; }
        public int Greed { get; set; }
        public long TotalSpent { get; set; }
        public long TotalWon { get; set; }
        public List<TicketDTO> Tickets { get; set; }

        public LotteryParticipantDTO() { }
        public static LotteryParticipantDTO FromDomain(LotteryParticipant p)
        {
            return new LotteryParticipantDTO
            {
                Id = p.Id,
                FullName = p.FullName,
                Balance = p.Balance,
                Greed = p.Greed,
                TotalSpent = p.TotalSpent,
                TotalWon = p.TotalWon,
                Tickets = p.Tickets.Select(t => TicketDTO.FromDomain(t)).ToList()
            };
        }

        public LotteryParticipant ToDomain()
        {
            var restoredTickets = Tickets?.Select(t => t.ToDomain()).ToList();

            return new LotteryParticipant(
                Id, 
                FullName, 
                Balance, 
                Greed, 
                TotalSpent, 
                TotalWon, 
                restoredTickets!
            );
        }
    }
}