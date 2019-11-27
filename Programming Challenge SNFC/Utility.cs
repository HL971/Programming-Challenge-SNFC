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
    class Utility
    {
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

                // Validate Paycode
                // Validate Payrate
                // Validate StartDate
                // Validate TwoWeekHours
            }
        }
    }
}
