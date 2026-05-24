using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Ticket : ITicket
    {
        public string LotteryName { get; private set; }
        public int Id { get ; private set; }
        public string ParticipantId { get; protected set; }
        public bool IsSold { get ; private set; }

        public Ticket(int id, bool isSold, string lotteryName, string participantId = null)
        {
            Id = id;
            IsSold = isSold;
            LotteryName = lotteryName;
            ParticipantId = participantId;
        }

        public void SellToParticipant(LotteryParticipant participant)
        {
            if (IsSold)
            {
                throw new InvalidOperationException("Ticket is already sold.");
            }
            this.ParticipantId = participant.Id;
            IsSold = true;
        }

        public string Print()
        {
            return $"Ticket ID: {Id}, Participant ID: {ParticipantId}, Sold: {IsSold}";
        }
    }
}
