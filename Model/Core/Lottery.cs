using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Lottery
    {
        public int TicketsPrice { get; private set; }

        public int TicketsCount { get; private set; }

        public List<Ticket> LotteryTickets { get; private set; }
    }
}
