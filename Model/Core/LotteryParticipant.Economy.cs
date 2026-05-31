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

        public LotteryParticipant(string fullName, long balance, int greed) : this(fullName)
        {
            Balance = balance;
            TotalSpent = 0;
            TotalWon = 0;
            Greed = greed;
        }

        public void BuyTicket(Lottery lottery, int totalGreed)
        {
            if (totalGreed <= 0) return;

            double relativeShare = (double)Greed / totalGreed;

            int maxTicketsByGreed = (int)(lottery.TicketsCount * relativeShare);

            int maxTicketsByBalance = (int)(Balance / lottery.TicketsPrice);

            int ticketsToBuy = Math.Min(maxTicketsByGreed, maxTicketsByBalance);

            for (int i = 0; i < ticketsToBuy; i++)
            {
                if (Balance >= lottery.TicketsPrice)
                {
                    bool isSold = lottery.SellTicket(this);
                    if (!isSold) break; 
                    Balance -= lottery.TicketsPrice;
                    TotalSpent += lottery.TicketsPrice; }
                else
                {
                    break; 
                }
            }
        }

        public void GetTicket(ITicket ticket)
        {
            _tickets.Add(ticket);
        }

        public void GetPrize(ITicket ticket)
        {
            if (ticket is WinningTicket winningTicket)
            {
                long prize = winningTicket.PrizeAmount;
                Balance += prize;
                TotalWon += prize;
            }
        }

        public void ReturnMoney(ITicket ticket)
        {
            int ticketPrice = ticket.Price;
            int moneyToReturn = ticketPrice * 90 / 100;

            Balance += moneyToReturn;
            TotalSpent -= moneyToReturn;
        }
    }
}
