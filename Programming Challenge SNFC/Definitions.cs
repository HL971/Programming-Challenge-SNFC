using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programming_Challenge_SNFC
{
    /// <summary>
    /// A class to store static definitions such as valid states or paycodes
    /// </summary>
    static class Definitions
    {
        /// <summary>
        /// A list of valid pay codes used to determine payrate for an employee
        /// </summary>
        public static readonly List<char> VALIDPAYCODES = new char[]{'H', 'S'}.ToList();

        /// <summary>
        /// A list of codes for the states where the company currently has employees
        /// </summary>
        public static readonly List<string> VALIDSTATES = new string[]{ "UT", "WY", "NV", "CO", "ID", "AZ", "OR", "WA", "NM", "TX" }.ToList();
    }
}
