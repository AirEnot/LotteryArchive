using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Ticket : ITicket
    {
        public int Price { get; private set; }

        public Ticket(int id, string participantId, bool isSold, int price) : this(id, participantId, isSold)
        {
            Price = price;
        }
    }
}
