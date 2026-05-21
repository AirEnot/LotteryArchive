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

        public LotteryParticipant(string fullName, int balance, int greed) : this(fullName)
        {
            Balance = balance;
            Greed = greed;
        }

        public void BuyTicket(Lottery lottery)
        {
            int ticketsToBuy = lottery.TicketsCount * Greed / 100;

            for (int i = 0; i < ticketsToBuy; i++)
            {
                if (Balance >= lottery.TicketsPrice)
                {
                    bool isSold = lottery.SellTicket(this);

                    if (!isSold) break;
                    Balance -= lottery.TicketsPrice;
                }
                else
                {
                    break;
                }
            }
        }

        public void ChangeGreed(int greed)
        {
            Greed = greed * 100 / 10000;
        }
    }
}
