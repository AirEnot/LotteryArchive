using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class Person : IPerson
    {
        public string FullName { get; set; }
        public Person(string fullName)
        {
            FullName = fullName;
        }
        public List<ITicket> Tickets { get; set; } = new List<ITicket>();

        public void AddWinTcket(WinningTicket ticket)
        {
            Tickets.Add(ticket);
        }
        public override string ToString() => FullName;
    }
}
