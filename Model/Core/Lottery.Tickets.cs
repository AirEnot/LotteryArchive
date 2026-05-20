using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Lottery
    {
        //public string LotteryName { get; set; }
        public int TicketsPrice { get; private set; }
        //public int TempVarNextTicketNumber { get; private set; } 
        public List<Ticket> LotteryTickets {  get; private set; }

        public bool SellTicket(LotteryParticipant participant)
        {
            return true;
            //    int maxTickets = 0;
            //    if (TempVarNextTicketNumber > maxTickets)
            //    {
            //        return false;
            //    }
            //    if (participant.Balance < TicketsPrice)
            //    {
            //        return false;
            //    }
            //    Ticket newTicket = new Ticket(TempVarNextTicketNumber)
            //    {
            //        Price = TicketPrice
            //    };

            //    // 4. Увеличиваем счетчик! Следующий билет получит номер +1
            //    _nextTicketNumber++;

            //    // 5. Передаем билет участнику через наш красивый перегруженный оператор +
            //    // Внутри оператора у участника спишутся деньги и билет добавится в его список
            //    participant += newTicket;

            //    // 6. Лотерея забирает корешок билета себе в архив (для будущего розыгрыша)
            //    SoldTickets.Add(newTicket);

            //    // 7. Если этого человека еще нет в списке участников данной лотереи - добавляем
            //    if (!Participants.Contains(participant))
            //    {
            //        Participants.Add(participant);
            //    }

            //    return true;
            //}

            //// 2-й вариант метода (ПЕРЕГРУЗКА): Продажа сразу нескольких билетов
            //public void SellTicket(LotteryParticipant participant, int count)
            //{
            //    for (int i = 0; i < count; i++)
            //    {
            //        // Пытаемся продать 1 билет.
            //        bool success = SellTicket(participant);

            //        // Если SellTicket вернул false (кончились билеты или деньги), 
            //        // мы просто прерываем цикл.
            //        if (!success) break;
            //    }
        }
    }
}
