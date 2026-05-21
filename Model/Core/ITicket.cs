using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ITicket
    {
        int Id { get; }
        string participantId { get; }
        bool isSold { get; }

        string Print();
    }
}
