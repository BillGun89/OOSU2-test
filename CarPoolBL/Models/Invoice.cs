using System;

namespace CarPoolBL.Models
{
    public class Invoice
    {
        public Reservation Reservation {
            get; private set;
        }

        public float DistanceDriven {
            get; internal set;
        }

        public DateTime Returned {
            get; internal set;
        }

        public float Price {
            get; private set;
        }

        internal Invoice(Reservation r, float distance, DateTime returned)
        {
            Reservation = r;
            DistanceDriven = distance;
            Returned = returned;
            Price = (float)(r.Car.HourlyCost * (returned - r.From).TotalMinutes / 60.0 +
                            r.Car.KmCost * distance);
        }
    }
}
