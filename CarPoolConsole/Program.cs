using System;
using System.Collections.Generic;

using CarPoolBL;
using CarPoolBL.Models;

namespace CarPoolConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Main();
        }

        private Program()
        {
            carPool = new CarPool();
        }

        private void Main()
        {
            Console.WriteLine("Welcome to the CarPool!");
            while (true) {
                try {
                    if (LogIn()) {
                        Console.WriteLine("You are now logged in {0}.", carPool.LoggedIn.Name);
                        MainMenu();
                        // For now the MainMenu() isn't used to choose anything.
                        if (carPool.LoggedIn.Reserved == null) {
                            ReserveCar();
                        } else {
                            ReturnCar();
                        }
                    } else {
                        Console.WriteLine("Failed to log in.");
                    }
                } catch (Exception e) {
                    Console.WriteLine("ERROR: " + e.Message);
                }
            }
        }

        private bool LogIn()
        {
            string idToParse = "";
            int id;
            while (!int.TryParse(idToParse, out id)) {
                Console.WriteLine("Write your member ID: ");
                idToParse = Console.ReadLine();
            }
            Console.WriteLine("Write your password: ");
            string password = Console.ReadLine();

            return carPool.LogIn(id, password);
        }

        private void MainMenu()
        {
            Console.WriteLine("Main menu:");
        }

        private void ReserveCar()
        {
            IList<Car> available = carPool.GetAvailableCars();
            int i = 1;
            foreach (Car c in available) {
                Console.Write("{0}. ", i++);
                Write(c);
            }

            Console.WriteLine("Enter the number of the car you want to book: ");
            string noToParse = "";
            int no;
            while (!int.TryParse(noToParse, out no) || !(0 < no && no <= available.Count)) {
                Console.WriteLine("Write your car's number: ");
                noToParse = Console.ReadLine();
            }
            Reservation r = carPool.ReserveCar(available[no - 1]);
            Write(r);
            Console.WriteLine();
        }

        private void ReturnCar()
        {
            Reservation toReturn = carPool.LoggedIn.Reserved;
            Console.WriteLine("You return the car " + toReturn.Car.RegNo + ".");
            string distanceToParse = "";
            float distance;
            while (!float.TryParse(distanceToParse, out distance) || distance < 0.0f) {
                Console.Write("Enter the distance driven in km: ");
                distanceToParse = Console.ReadLine();
            }
            Invoice i = carPool.ReturnCar(distance);
            Console.WriteLine("The car " + toReturn.Car.RegNo + " is returned.");
            Console.WriteLine("It was reserved from " + i.Reservation.From + " to " + i.Returned + ".");
            Console.WriteLine("Price: " + i.Price + " kr.");
        }

        private void Write(Car c)
        {
            Console.WriteLine(" {0}", c.RegNo);
        }

        private void Write(Reservation r)
        {
            Console.WriteLine(" RegNo: {0} reserved on {1} by {2}.",
                              r.Car.RegNo, r.From, r.Member.Name);
        }

        private CarPool carPool;
    }
}
