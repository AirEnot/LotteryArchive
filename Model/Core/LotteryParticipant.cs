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

        private List<Ticket> _tickets = new();

        public List<Ticket> Tickets => _tickets.ToList();

        public LotteryParticipant(string fullName) : base(fullName)
        {
            Id = Guid.NewGuid().ToString();
        }

        public void Print()
        {
            Console.WriteLine($"У участника {FullName}: Balance {Balance}, Greed {Greed}");
        }
    }
}