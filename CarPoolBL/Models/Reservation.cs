using System;
using System.Collections.Generic;

namespace CarPoolBL.Models
{
    public class Reservation
    {
        public Member Member {
            get; private set;
        }

        public Car Car {
            get; private set;
        }

        public DateTime From {
            get; private set;
        }

        internal Reservation(Member m, Car c, DateTime from)
        {
            Member = m;
            Car = c;
            From = from;
        }
    }
}
