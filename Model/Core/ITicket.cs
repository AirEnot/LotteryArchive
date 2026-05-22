using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ITicket
    {
        string LotteryName { get; }
        int Id { get; }
        string ParticipantId { get;}
        bool IsSold { get; }
        string Print();
    }
}
