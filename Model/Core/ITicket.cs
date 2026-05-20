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
        int participantId { get; }
        bool isSold { get; }

        string Print();
    }
}
