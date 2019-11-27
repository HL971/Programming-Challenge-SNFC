using System;
using System.Collections.Generic;
using System.Text;

namespace Programming_Challenge_SNFC.Models
{
    /// <summary>
    /// Model for a given employee
    /// </summary>
    class Employee
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Paycode { get; set; }
        public decimal Payrate { get; set; }
        public DateTime StartDate { get; set; }
        public string StateCode { get; set; }
        public int TwoWeekHours { get; set; }

        /// <summary>
        /// Test Code
        /// </summary>
        public string testEmployee()
        {
            return Id + " " + FirstName + " " + LastName + " " + Paycode + " " + Payrate + " " + StartDate + " " + StateCode + TwoWeekHours;
        }
    }
}
