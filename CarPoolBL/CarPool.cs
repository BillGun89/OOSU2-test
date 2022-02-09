using System;
using System.Collections.Generic;
using System.Linq;

using CarPoolBL.Models;

namespace CarPoolBL
{
    /// <summary>
    ///  This is the facade of the business layer.
    /// </summary>
    public class CarPool
    {
        public Member LoggedIn {
            get; private set;
        }

        /// <summary>
        ///  The LogIn system operation.
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool LogIn(int memberId, string password)
        {
            unitOfWork = new Internals.UnitOfWork();
            Member m = unitOfWork.MemberRepository.FirstOrDefault(m => m.MemberID == memberId);
            if (m != null && m.VerifyPassword(password)) {
                LoggedIn = m;
                return true;
            }
            LoggedIn = null;
            return false;
        }

        /// <summary>
        ///  Get the list of available cars.
        /// </summary>
        /// <returns></returns>
        public IList<Car> GetAvailableCars()
        {
            if (LoggedIn == null) {
                throw new ApplicationException("there isn't anyone logged in.");
            }
            List<Car> available = new List<Car>();
            foreach (Car c in unitOfWork.CarRepository.Find(c => c.IsAvailable)) {
                available.Add(c);
            }
            return available;
        }

        /// <summary>
        ///  Reserve a choosen car.
        /// </summary>
        /// <param name="c">the car to reserve.</param>
        /// <returns>the reservation.</returns>
        public Reservation ReserveCar(Car c)
        {
            if (LoggedIn == null) {
                throw new ApplicationException("there isn't anyone logged in.");
            }
            if (LoggedIn.Reserved != null) {
                throw new ApplicationException("you have already a reserved car.");
            }
            if (!c.IsAvailable) {
                throw new ApplicationException("the car " + c.RegNo + " is already reserved by someone else.");
            }

            c.IsAvailable = false;

            Reservation r = new Reservation(LoggedIn, c, DateTime.Now);
            unitOfWork.ReservationRepository.Add(r);
            LoggedIn.Reserved = r;
            unitOfWork.Save();

            return r;
        }

        /// <summary>
        ///  Return a car.
        /// </summary>
        /// <param name="distanceDriven">the distance driven.</param>
        /// <returns>the invoice.</returns>
        public Invoice ReturnCar(float distanceDriven)
        {
            if (LoggedIn == null) {
                throw new ApplicationException("there isn't anyone logged in.");
            }
            if (LoggedIn.Reserved == null) {
                throw new ApplicationException("you don't have a reserved car.");
            }
            if (distanceDriven < 0.0) {
                throw new ApplicationException("distance driven cannot be negative.");
            }

            Reservation r = LoggedIn.Reserved;
            Invoice i = new Invoice(r, distanceDriven, DateTime.Now);

            unitOfWork.InvoiceRepository.Add(i);
            r.Car.IsAvailable = true;
            LoggedIn.Reserved = null;
            unitOfWork.Save();

            return i;
        }

        private Internals.UnitOfWork unitOfWork;
    }
}
