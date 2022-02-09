using System;
using System.Collections.Generic;

using CarPoolBL.Models;

namespace CarPoolBL.Internals
{
    /// <summary>
    ///  This class is used to access the storage in the application.
    /// </summary>
    internal class UnitOfWork
    {
        public Repository<Car> CarRepository {
            get; private set;
        }

        public Repository<Member> MemberRepository {
            get; private set;
        }

        public Repository<Reservation> ReservationRepository {
            get; private set;
        }

        public Repository<Invoice> InvoiceRepository {
            get; private set;
        }

        /// <summary>
        ///  Create a new instance.
        /// </summary>
        internal UnitOfWork()
        {
            CarRepository = new Repository<Car>();
            MemberRepository = new Repository<Member>();
            ReservationRepository = new Repository<Reservation>();
            InvoiceRepository = new Repository<Invoice>();

            // Initialize the tables if this is the first UnitOfWork.
            if (MemberRepository.IsEmpty()) {
                Fill();
            }
        }

        /// <summary>
        ///  Save the changes made.
        /// </summary>
        public void Save()
        { }

        private void Fill()
        {
            CarRepository.Add(new Car("FEN292", true,  50.0f, 2.0f));
            CarRepository.Add(new Car("FEN232", false, 55.0f, 1.9f));
            CarRepository.Add(new Car("BAR345", true,  50.0f, 1.9f));
            CarRepository.Add(new Car("ZOO123", true,  45.0f, 1.8f));

            MemberRepository.Add(new Member(1, "foo", "AndersG"));
            MemberRepository.Add(new Member(2, "bar", "HansG"));

            ReservationRepository.Add
                (new Reservation(MemberRepository.FirstOrDefault(m => m.MemberID == 2),
                                 CarRepository.FirstOrDefault(c => c.RegNo == "FEN232"),
                                 DateTime.Now - TimeSpan.FromDays(3.9)));
            MemberRepository.FirstOrDefault(m => m.MemberID == 2).Reserved =
                ReservationRepository.FirstOrDefault(r => true);
        }
    }
}
