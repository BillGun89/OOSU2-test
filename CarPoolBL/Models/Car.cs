using System;
using System.Collections.Generic;

namespace CarPoolBL.Models
{
    public class Car
    {
        public string RegNo {
            get; private set;
        }

        public bool IsAvailable {
            get; internal set;
        }

        public float HourlyCost {
            get; private set;
        }

        public float KmCost {
            get; private set;
        }

        internal Car(string regNo, bool isAvailable, float hourlyCost, float kmCost)
        {
            RegNo = regNo;
            IsAvailable = isAvailable;
            HourlyCost = hourlyCost;
            KmCost = kmCost;
        }
    }
}
