using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class LotteryParticipant : Person
    {
        public List<Ticket> Tickets { get; set; }

        public LotteryParticipant(string fullName) : base(fullName)
        {
            Tickets = new List<Ticket>();
        }
    }
}