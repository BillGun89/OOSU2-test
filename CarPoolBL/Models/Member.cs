using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolBL.Models
{
    public class Member
    {
        public int MemberID {
            get; private set;
        }

        public string Name {
            get; private set;
        }

        public Reservation Reserved {
            // This is not very good if we had a real database. Search the database instead.
            get; internal set;
        }

        internal Member(int id, string password, string name)
        {
            MemberID = id;
            this.password = password;
            Name = name;
        }

        internal bool VerifyPassword(string given)
        {
            return password == given;
        }

        private string password;
    }
}
