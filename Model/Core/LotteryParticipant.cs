using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class LotteryParticipant : Person
    {
        public string Id { get; private set; }
        public List<Ticket> Tickets { get; private set; }

        public LotteryParticipant(string fullName) : base(fullName)
        {
            Tickets = new List<Ticket>();
            Id = Guid.NewGuid().ToString();
        }

        public void Print()
        {
            Console.WriteLine($"У участника {FullName}: Balance {Balance}, Greed {Greed}");
        }
    }
}