using System;
using System.Collections.Generic;
using System.Text;

namespace Programming_Challenge_SNFC.Models
{
    class TopEarner
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int YearsWorked { get; set; }
        public decimal Pay { get; set; }

        public override string ToString()
        {
            return FirstName + " "
                + LastName + " "
                + YearsWorked + " "
                + Pay.ToString("C");
        }
    }
}
