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
        public string ParticipantId { get; protected set; }
        public bool isSold { get ; private set; }

        public Ticket(int id, bool isSold)
        {
            Id = id;
            this.isSold = isSold;
        }

        public void SellToParticipant(LotteryParticipant participant)
        {
            if (isSold)
            {
                throw new InvalidOperationException("Ticket is already sold.");
            }
            this.ParticipantId = participant.Id;
            isSold = true;
        }

        public void AddOwner(string participantID)
        {
            ParticipantId = participantID;
        }
        public string Print()
        {
            return $"Ticket ID: {Id}, Participant ID: {ParticipantId}, Sold: {isSold}";
        }
    }
}
