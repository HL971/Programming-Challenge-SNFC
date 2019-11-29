using System;
using System.Collections.Generic;
using System.Text;

namespace Programming_Challenge_SNFC.Models
{
    public class PayCheck
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal GrossPay { get; set; }
        public decimal FederalTax { get; set; }
        public decimal StateTax { get; set; }
        public decimal NetPay { get; set; }
        public string StateCode { get; set; }
        public int TimeWorked { get; set; }
        public DateTime EmployeeHireDate { get; set; }

        public override string ToString()
        {
            return Id + " "
                + FirstName + " "
                + LastName + " "
                + GrossPay.ToString("C") + " "
                + FederalTax.ToString("C") + " "
                + StateTax.ToString("C") + " "
                + NetPay.ToString("C");
        }

        // Kinda rough on the years worked here...TimeSpan doesn't have a total years option because it has no reference point for the start or end, and the length of a year is variable. We could make it more accurate, but that will need a lot more thought.
        public string printEarner()
        {
            return FirstName + " "
                + LastName + " "
                + ((DateTime.Now - EmployeeHireDate).TotalDays / 365) + " "
                + GrossPay;
        }


    }
}
