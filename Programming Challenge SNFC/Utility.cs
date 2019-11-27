using Programming_Challenge_SNFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programming_Challenge_SNFC
{
    /// <summary>
    /// Class containing static utility methods for use throughout the program
    /// </summary>
    static class Utility
    {
        #region ParseEmployeeFromString()
        /// <summary>
        /// ParseEmployeeFromString()
        /// 
        /// Takes a line from Employees.txt, parses it and transforms it into an employee object
        /// </summary>
        /// <param name="input">A single line of text from Employees.txt, or a string of the same format</param>
        /// <returns>Null if the input string is null, Null if the input string is an invalid format, a valid Employee object otherwise</returns>
        public static Employee ParseEmployeeFromString(string input)
        {
            if(input == null)
            {
                return null;
            }
            else
            {
                List<string> separatedInput = input.Split(new char[] { ',' }).ToList();

                if(separatedInput.Count != 8) // There are either too few, or too many input items. The input is therefore an invalid format
                {
                    return null;
                }

                #region Validate Paycode
                char paycode;
                
                if(false == Char.TryParse(separatedInput[3], out paycode))
                {
                    // The paycode was unable to be parsed
                    return null;
                }

                if(!(Definitions.VALIDPAYCODES.Contains(paycode)))
                {
                    // The paycode is not a valid paycode
                    return null;
                }
                #endregion
                #region Validate Payrate
                decimal payrate;

                if(false == Decimal.TryParse(separatedInput[4], out payrate))
                {
                    // payrate failed to parse
                    return null;
                }
                #endregion
                #region Validate StartDate
                DateTime startDate;

                if(false == DateTime.TryParse(separatedInput[5], out startDate))
                {
                    // startDate was invalid and failed to parse
                    return null;
                }
                #endregion
                #region Validate StateCode
                if (!(Definitions.VALIDSTATES.Contains(separatedInput[6])))
                {
                    // The state code is invalid
                    return null;
                }
                #endregion
                #region Validate TwoWeekHours
                int twoWeekHours;

                if(false == Int32.TryParse(separatedInput[7], out twoWeekHours))
                {
                    // twoWeekHours was invalid and failed to parse
                    return null;
                }
                #endregion

                Employee returnThis;
                try
                {
                    returnThis = new Employee {
                        Id = separatedInput[0],
                        FirstName = separatedInput[1],
                        LastName = separatedInput[2],
                        Paycode = paycode,
                        Payrate = payrate,
                        StartDate = startDate,
                        StateCode = separatedInput[6],
                        TwoWeekHours = twoWeekHours
                    };
                }
                catch(Exception e)
                {
                    // An unanticipated error has occurred
                    return null;
                }

                return returnThis;
            }
        }
        #endregion
    }
}
