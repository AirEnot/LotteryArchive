using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Ticket : ITicket
    {
        public int Id { get ; private set; }
        public string participantId { get; private set; }
        public bool isSold { get ; private set; }

        public Ticket(int id, string participantId, bool isSold)
        {
            Id = id;
            this.participantId = participantId;
            this.isSold = isSold;
        }

        public void SellToParticipant(LotteryParticipant participant)
        {
            if (isSold)
            {
                throw new InvalidOperationException("Ticket is already sold.");
            }
            this.participantId = participant.Id;
            isSold = true;
        }
        public string Print()
        {
            return $"Ticket ID: {Id}, Participant ID: {participantId}, Sold: {isSold}";
        }
    }
}
