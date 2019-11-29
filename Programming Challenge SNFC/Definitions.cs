using Programming_Challenge_SNFC.Models;
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

        /// <summary>
        /// A list of states and the income tax rate for that state
        /// </summary>
        public static readonly List<TaxRate> STATETAXRATES = new TaxRate[]{ new TaxRate(){ State = "UT", rate = 5.0 },
                                                                            new TaxRate(){ State = "WY", rate = 5.0 },
                                                                            new TaxRate(){ State = "NV", rate = 5.0 },
                                                                            new TaxRate(){ State = "CO", rate = 6.5 },
                                                                            new TaxRate(){ State = "ID", rate = 6.5 },
                                                                            new TaxRate(){ State = "AZ", rate = 6.5 },
                                                                            new TaxRate(){ State = "OR", rate = 6.5 },
                                                                            new TaxRate(){ State = "WA", rate = 7.0 },
                                                                            new TaxRate(){ State = "NM", rate = 7.0 },
                                                                            new TaxRate(){ State = "TX", rate = 7.0 }
        }.ToList();

        /// <summary>
        /// The definition for the federal income tax rate
        /// </summary>
        public static readonly double FEDERALTAXRATE = 15.0;
    }
}
