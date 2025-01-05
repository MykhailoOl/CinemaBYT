using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaBYT.Classes
{
    public class Timer
    {
        private DateTime dateTimeStart;
        private DateTime dateTimeEnd;
        Boolean isAvailable = true;
        private Seat seat;
        private Hall hall;
        private DateTime DateTimeStart
        {
            get { return dateTimeStart; }
            set { dateTimeStart = value; }
        }

        private DateTime DateTimeEnd
        {
            get { return dateTimeEnd; }
            set { dateTimeEnd = value; }
        }

        private bool IsAvailable
        {
            get { return isAvailable; }
            set { isAvailable = value; }
        }

        private Seat Seat
        {
            get { return seat; }
            set { seat = value; }
        }

        private Hall Hall
        {
            get { return hall; }
            set { hall = value; }
        }
    }

}
