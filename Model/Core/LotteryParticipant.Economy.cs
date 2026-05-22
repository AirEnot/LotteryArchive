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
        public long TotalSpent { get; private set; }
        public long TotalWon { get; private set; }

        public LotteryParticipant(string fullName, int balance) : this(fullName)
        {
            Balance = balance;
            TotalSpent = 0;
            TotalWon = 0;
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

        public void GetTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
        }

        public void GetPrize(Ticket ticket)
        {
            if (ticket is WinningTicket winningTicket)
            {
                long prize = winningTicket.PrizeAmount;
                Balance += prize;
                TotalWon += prize;
            }
        }

        public void ReturnMoney(Ticket ticket)
        {
            int ticketPrice = ticket.Price;
            int moneyToReturn = ticketPrice * 90 / 100;

            Balance += moneyToReturn;
            TotalSpent -= moneyToReturn;
        }
        public void ChangeGreed(int greed)
        {
            Greed = greed * 100 / 10000;
        }
    }
}
