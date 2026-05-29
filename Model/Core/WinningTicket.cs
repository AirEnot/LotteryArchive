using Newtonsoft.Json;

namespace Model.Core
{
    public class WinningTicket : Ticket
    {
        public long PrizeAmount { get; private set; }

        [JsonConstructor]
        public WinningTicket(int id, bool IsSold, string lotteryName,int price, long prizeAmount, string participantId = null)
            : base(id, IsSold, lotteryName, price, participantId)
        {
            PrizeAmount = prizeAmount;
        }
    }
}
