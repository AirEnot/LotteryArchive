using Newtonsoft.Json;

namespace Model.Core
{
    public partial class Ticket : ITicket
    {
        public int Price { get; private set; }

        [JsonConstructor]
        public Ticket(int id, bool IsSold, string lotteryName, int price, string participantId = null) 
            : this(id, IsSold, lotteryName, participantId)
        {
            Price = price;
        }
    }
}
