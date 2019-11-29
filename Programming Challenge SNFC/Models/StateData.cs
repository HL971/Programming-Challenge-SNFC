using System;
using System.Collections.Generic;
using System.Text;

namespace Programming_Challenge_SNFC.Models
{
    public class StateData
    {
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public List<decimal> PayList { get; set; }
        public List<decimal> TaxList { get; set; }
        public List<int> HoursList { get; set; }

        private decimal medianPay()
        {
            decimal returnThis = 0.0M;

            // Sort data
            PayList.Sort();

            // Find center
            int roughcenter = PayList.Count % 2;

            if(roughcenter == 0) // Even number of entries
            {
                returnThis = (PayList[roughcenter] + PayList[roughcenter + 1]) / 2.0M;
            }
            else // Odd number of entries
            {
                returnThis = PayList[roughcenter + 1];
            }

            return returnThis;
        }

        private decimal totalStateTax()
        {
            decimal returnThis = 0.0M;

            foreach(decimal tax in TaxList)
            {
                returnThis = returnThis + tax;
            }

            return returnThis;
        }

        private int medianHoursWorked()
        {
            int returnThis = 0;

            // Sort data
            HoursList.Sort();

            // Find center
            int roughcenter = HoursList.Count % 2;

            if (roughcenter == 0) // Even number of entries
            {
                returnThis = (int)Math.Round((HoursList[roughcenter] + HoursList[roughcenter + 1]) / 2.0);
            }
            else // Odd number of entries
            {
                returnThis = HoursList[roughcenter + 1];
            }

            return returnThis;
        }

        public override string ToString()
        {
            return StateName + " "
                + medianHoursWorked() + " "
                + medianPay().ToString("C") + " "
                + totalStateTax().ToString("C");
        }

        public void addPayCheck(PayCheck payCheck)
        {
            PayList.Add(payCheck.NetPay);
            TaxList.Add(payCheck.StateTax);
            HoursList.Add(payCheck.TimeWorked);
        }

        public StateData(string stateCode)
        {
            if (Definitions.VALIDSTATES.Contains(stateCode))
            {
                this.StateCode = stateCode;

                foreach (string[] state in Definitions.STATENAMES)
                {
                    if (state[0] == stateCode)
                    {
                        StateName = state[1];
                        break;
                    }
                }

                PayList = new List<decimal>();
                TaxList = new List<decimal>();
                HoursList = new List<int>();
            }
            else
            {
                throw new ArgumentException("Error while creating new StateData object: stateCode not in defined list of valid states.");
            }
        }
    }
}
