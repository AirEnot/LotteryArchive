using System.Xml.Serialization;
using Model.Core;

namespace Model.Data
{
    // МАГИЯ ДЛЯ XML: Говорим сериализатору, что в списке билетов могут прятаться выигрышные!
    [XmlInclude(typeof(WinningTicketDTO))]
    public class TicketDTO
    {
        public int Id { get; set; }
        public string ParticipantId { get; set; }
        public bool IsSold { get; set; }
        public string LotteryName { get; set; }
        public int Price { get; set; }

        public TicketDTO() { }

        public static TicketDTO FromDomain(ITicket t)
        {
            if (t is WinningTicket wt)
            {
                return new WinningTicketDTO
                {
                    Id = wt.Id,
                    ParticipantId = wt.ParticipantId,
                    IsSold = wt.IsSold,
                    LotteryName = wt.LotteryName,
                    Price = wt.Price,
                    PrizeAmount = wt.PrizeAmount
                };
            }

            return new TicketDTO
            {
                Id = t.Id,
                ParticipantId = t.ParticipantId,
                IsSold = t.IsSold,
                LotteryName = t.LotteryName,
                Price = t.Price
            };
        }

        public virtual ITicket ToDomain()
        {
            return new Ticket(Id, IsSold, LotteryName, Price, ParticipantId);
        }
    }

    // Наследник для выигрышного билета
    public class WinningTicketDTO : TicketDTO
    {
        public long PrizeAmount { get; set; }

        public override Ticket ToDomain()
        {
            return new WinningTicket(Id, IsSold, LotteryName, Price, PrizeAmount, ParticipantId);
        }
    }
}