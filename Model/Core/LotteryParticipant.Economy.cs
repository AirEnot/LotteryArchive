using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class LotteryParticipant : Person
    {
        public long Balance { get; private set; }
        public int Greed { get; private set; }

        public LotteryParticipant(string fullName, int balance, int greed) : base(fullName)
        {
            Balance = balance;
            Greed = greed;
        }

        public void BuyTicket(Lottery lottery)
        {
            int safeGreed = Math.Clamp(Greed, 0, 100);
            long moneyAvailableToSpend = Balance * 100 / safeGreed;
            int ticketsToBuy = moneyAvailableToSpend / lottery.TicketPrice;
            for (int i = 0; i < ticketsToBuy; i++)
            {
                if (Balance >= lottery.TicketPrice)
                {
                    bool isSold = lottery.SellTicket(this);

                    if (!isSold) break;
                    Balance -= lottery.TicketPrice;
                }
                else
                {
                    break;
                }
            }
        }

        
    }
}
