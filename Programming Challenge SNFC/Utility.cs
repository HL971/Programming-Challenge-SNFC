using Programming_Challenge_SNFC.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

                #region Validate Id
                if(separatedInput[0] == "")
                {
                    // Not accepting blank Ids
                    return null;
                }
                #endregion
                #region Validate FirstName
                if (separatedInput[1] == "")
                {
                    // Not accepting blank First Names
                    return null;
                }
                #endregion
                #region Validate LastName
                if (separatedInput[2] == "")
                {
                    // Not accepting blank First Names
                    return null;
                }
                #endregion
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
        #region getEmployeeById()
        /// <summary>
        /// getEmplyeeById()
        /// 
        /// Returns an employee object from a referenced employee list, based on a passed in employee Id
        /// May throw ArgumentNullException if a null value is passed as a parameter
        /// </summary>
        /// <param name="Id">The id for the employee being searched for</param>
        /// <param name="employees">The referenced list/table of employees</param>
        /// <returns>Null if the employee specified is not found, A valid Employee object otherwise</returns>
        public static Employee getEmployeeById(string Id, ref List<Employee> employees)
        {
            Employee returnThis = null;

            if(Id != null && employees != null)
            {
                foreach(Employee e in employees)
                {
                    if(e != null)
                    {
                        if(e.Id == Id)
                        {
                            returnThis = e;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (Id == null && employees == null)
                {
                    throw new ArgumentNullException("Error in Utilities.getEmployeeById: A null value was passed into both parameters");
                }
                else if (Id == null)
                {
                    throw new ArgumentNullException("Error in Utilities.getEmployeeById: A null value was passed into the Id parameter");
                }
                else if (employees == null)
                {
                    throw new ArgumentNullException("Error in Utilities.getEmployeeById: A null value was passed into the employees parameter");
                }
                else
                {
                    throw new Exception("Error in Utilities.getEmployeeById: An unexpected error has occurred");
                }
            }

            return returnThis;
        }
        #endregion
        #region collectEmployeeInformation()
        /// <summary>
        /// collectEmployeeInformation()
        /// 
        /// The method used to collect all employee information from the file and
        /// return it to the program
        /// </summary>
        /// <returns>Null if an error occurs in reading the file, a list of employee objects otherwise</returns>
        public static List<Employee> collectEmployeeInformation()
        {
            List<Employee> returnThis = new List<Employee>();

            try
            {
                using (StreamReader employeeFile = new StreamReader(Definitions.EMPLOYEESINFOLOCATION))
                {
                    string employeeLine;
                    int line = 1; // Used for debug purposes

                    while((employeeLine = employeeFile.ReadLine()) != null)
                    {
                        Employee employee = ParseEmployeeFromString(employeeLine);

                        if(employee != null)
                        {
                            returnThis.Add(employee);
                        }
                        else
                        {
                            Console.WriteLine("Line " + line + " in the file returned a null employee.");
                        }

                        line++;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error collecting employee information: " + e.Message);
                return null;
            }

            return returnThis;
        }
        #endregion
        #region generatePaycheckInfo()
        /// <summary>
        /// generatePaycheckInfo()
        /// 
        /// Generates paycheck info for every employee in a given list
        /// </summary>
        /// <param name="employees">The list of employees to generate information for</param>
        /// <returns>An unsorted list of paycheck information</returns>
        public static List<PayCheck> generatePaycheckInfo(List<Employee> employees)
        {
            if(employees != null)
            {
                List<PayCheck> returnThis = new List<PayCheck>();

                try
                {
                    foreach(Employee employee in employees)
                    {
                        // Calculate Grosspay
                        decimal grosspay = 0.0M;
                        if(employee.Paycode == 'S')
                        {
                            grosspay = employee.Payrate / 26.0M; // Assiuming a 52 week year, their salary will be split into 26 even pieces
                        }
                        else
                        {
                            if(employee.TwoWeekHours <= 80)
                            {
                                grosspay = employee.Payrate * employee.TwoWeekHours;
                            }
                            else
                            {
                                if(employee.TwoWeekHours <= 90)
                                {
                                    grosspay = employee.Payrate * 80 + (employee.Payrate * 1.5M * (employee.TwoWeekHours - 80));
                                }
                                else
                                {
                                    grosspay = employee.Payrate * 80 + (employee.Payrate * 1.5M * 10) + (employee.Payrate * 1.75M * (employee.TwoWeekHours - 90));
                                }
                            }
                        }

                        // Calculate state taxes
                        TaxRate taxrate = null;
                        foreach(TaxRate state in Definitions.STATETAXRATES)
                        {
                            if(state.State == employee.StateCode)
                            {
                                taxrate = state;
                                break;
                            }
                        }

                        decimal stateTax;
                        if(taxrate != null)
                        {
                            stateTax = grosspay * taxrate.rate;
                        }
                        else
                        {
                            throw new ArgumentNullException("Error in Utility.generatePaycheckInfo(): Employee state not found in taxrate table");
                        }

                        // Calculate federal taxes
                        decimal fedtax = grosspay * Definitions.FEDERALTAXRATE;

                        // Calculate net pay
                        decimal netpay = grosspay - (stateTax + fedtax);

                        // Build Paycheck and add to list
                        returnThis.Add(new PayCheck()
                        {
                            Id = employee.Id,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            GrossPay = grosspay,
                            StateTax = stateTax,
                            FederalTax = fedtax,
                            NetPay = netpay,
                            StateCode = employee.StateCode,
                            TimeWorked = employee.TwoWeekHours,
                            EmployeeHireDate = employee.StartDate
                        });
                    } // End foreach
                }
                catch(Exception e)
                {
                    throw new Exception("Error in Utility.generatePaycheckInfo(): " + e.Message);
                }

                return returnThis;
            }
            else
            {
                throw new ArgumentNullException("Error in Utility.generatePaycheckInfo(): Null value passed into employees method parameter");
            }
        }
        #endregion
        #region sortPaychecks()
        /// <summary>
        /// sortPaychecks()
        /// 
        /// Uses merge sort to sort a list of paychecks by gross pay descending
        /// </summary>
        /// <param name="payChecks">a referenced list of paychecks</param>
        public static List<PayCheck> sortPaychecks(List<PayCheck> payChecks)
        {
            if(payChecks.Count == 1)
            {
                return payChecks;
            }
            else
            {
                int halfcount = payChecks.Count / 2;
                List<PayCheck> firsthalf = sortPaychecks(payChecks.GetRange(0, halfcount));
                List<PayCheck> secondhalf = sortPaychecks(payChecks.GetRange(halfcount, payChecks.Count - halfcount));

                int i = 0, n = 0;
                List<PayCheck> returnThis = new List<PayCheck>();

                while(i < firsthalf.Count || n < secondhalf.Count)
                {
                    if(i == firsthalf.Count) // add rest of secondhalf to returnthis
                    {
                        returnThis.AddRange(secondhalf.GetRange(n, secondhalf.Count - n));
                        n = secondhalf.Count;
                    }
                    else if(n == secondhalf.Count) // add rest of firsthalf to returnthis
                    {
                        returnThis.AddRange(firsthalf.GetRange(i, firsthalf.Count - i));
                        i = firsthalf.Count;
                    }
                    else
                    {
                        if(firsthalf[i].GrossPay > secondhalf[n].GrossPay)
                        {
                            returnThis.Add(firsthalf[i]);
                            i++;
                        }
                        else
                        {
                            returnThis.Add(secondhalf[n]);
                            n++;
                        }
                    }
                }

                return returnThis;
            }
        }
        #endregion
        #region printPaycheckInfo()
        /// <summary>
        /// printPaycheckInfo()
        /// 
        /// Prints every paycheck in a given list to a file at a specified location
        /// The filename is hardcoded in Definitions.cs
        /// May throw a DirectoryNotFoundException if the given outputlocation is invlaid
        /// May throw an ArgumentNullException if either parameter is passed in as null
        /// May throw an Exception if any other error occurs
        /// </summary>
        /// <param name="outputlocation">The path to print the file to</param>
        /// <param name="paychecks">A list of paystubs to print to file</param>
        public static void printPaycheckInfo(string outputlocation, List<PayCheck> paychecks)
        {
            if (outputlocation != null && paychecks != null)
            {
                try
                {
                    using (StreamWriter output = new StreamWriter(outputlocation + Definitions.REQUIREMENTONEFILENAME))
                    {
                        foreach (PayCheck payCheck in paychecks)
                        {
                            output.WriteLine(payCheck.ToString());
                        }
                    }
                }
                catch (DirectoryNotFoundException dnfe)
                {
                    throw new DirectoryNotFoundException("Error in Utility.printPaycheckInfo()...The supplied output location " + outputlocation + " is invalid: " + dnfe.Message);
                }
                catch(ArgumentNullException ane)
                {
                    throw new Exception("Error in Utility.printPaycheckInfo(): " + ane.Message);
                }
                catch (Exception e)
                {
                    throw new Exception("An unanticipated error occurred in Utility.printPaycheckInfo() : " + e.Message);
                }
            }
            else
            {
                if(outputlocation == null && paychecks == null)
                {
                    throw new ArgumentNullException("Error in Utilities.printPaycheckInfo: A null value was passed into both parameters");
                }
                else if(outputlocation == null)
                {
                    throw new ArgumentNullException("Error in Utilities.printPaycheckInfo: A null value was passed into the outputlocation parameter");
                }
                else if(paychecks == null)
                {
                    throw new ArgumentNullException("Error in Utilities.printPaycheckInfo: A null value was passed into the paychecks parameter");
                }
                else
                {
                    throw new Exception("Error in Utilities.printPaycheckInfo: An unexpected error has occurred");
                }
            }
        }
        #endregion
        #region collectEarnerInfo()
        /// <summary>
        /// collectEarnerInfo()
        /// 
        /// Takes a list of paycheck information and gathers info about the top 15% of earners in the company
        /// Throws ArgumentNullException if a null value is passed into the payChecks parameter
        /// </summary>
        /// <param name="payChecks">A list of paycheck information for all employees</param>
        /// <returns>An unsorted list of the top earners in the company</returns>
        public static List<TopEarner> collectEarnerInfo(List<PayCheck> payChecks)
        {
            if(payChecks != null)
            {
                List<TopEarner> returnThis = new List<TopEarner>();

                // Do not assume that the list is sorted
                payChecks = sortPaychecks(payChecks);

                int numberOfTopEarners = (int)(Math.Round((double)(payChecks.Count) * 0.15));

                for(int i = 0; i < numberOfTopEarners; i++)
                {
                    returnThis.Add(new TopEarner()
                    {
                        FirstName = payChecks[i].FirstName,
                        LastName = payChecks[i].LastName,
                        Pay = payChecks[i].GrossPay,
                        YearsWorked = (int)Math.Floor((DateTime.Now - payChecks[i].EmployeeHireDate).TotalDays / 365)
                    });
                }

                return returnThis;
            }
            else
            {
                throw new ArgumentNullException("Error in collectEarnerInfo(): payChecks parameter was null");
            }
        }
        #endregion
        #region sortEarners()
        public static List<TopEarner> sortEarners(List<TopEarner> earners)
        {
            if(earners.Count == 1)
            {
                return earners;
            }
            else
            {
                int halfcount = earners.Count / 2;
                List<TopEarner> firstHalf = sortEarners(earners.GetRange(0, halfcount));
                List<TopEarner> secondHalf = sortEarners(earners.GetRange(halfcount, earners.Count - halfcount));

                int i = 0, n = 0;
                List<TopEarner> returnThis = new List<TopEarner>();

                while(i < firstHalf.Count || n < secondHalf.Count)
                {
                    if(n == secondHalf.Count)
                    {
                        returnThis.AddRange(firstHalf.GetRange(i, firstHalf.Count - i));
                        i = firstHalf.Count;
                    }
                    else if(i == firstHalf.Count)
                    {
                        returnThis.AddRange(secondHalf.GetRange(n, secondHalf.Count - n));
                        n = secondHalf.Count;
                    }
                    else
                    {
                        if(firstHalf[i].YearsWorked > secondHalf[n].YearsWorked)
                        {
                            returnThis.Add(firstHalf[i]);
                            i++;
                        }
                        else if(firstHalf[i].YearsWorked < secondHalf[n].YearsWorked)
                        {
                            returnThis.Add(secondHalf[n]);
                            n++;
                        }
                        else
                        {
                            int x = String.Compare(firstHalf[i].LastName, secondHalf[n].LastName);
                            if(x < 0)
                            {
                                returnThis.Add(firstHalf[i]);
                                i++;
                            }
                            else if(x > 0)
                            {
                                returnThis.Add(secondHalf[n]);
                                n++;
                            }
                            else
                            {
                                if(String.Compare(firstHalf[i].FirstName, secondHalf[n].FirstName) < 0)
                                {
                                    returnThis.Add(firstHalf[i]);
                                    i++;
                                }
                                else
                                {
                                    returnThis.Add(secondHalf[n]);
                                    n++;
                                }
                            }
                        }
                    }
                }

                return returnThis;
            }
        }
        #endregion
        #region printTopEarnerInfo()
        /// <summary>
        /// printTopEarnerInfo()
        /// 
        /// Prints the information about the given top earners to a file
        /// The filename is hardcoded in Definitions.cs
        /// Throws an ArgumentNullException if either of the parameters is given as null
        /// Throws a DirectoryNotFoundException if the given outputlocation is invalid
        /// Throws an Exception if any other error occurs
        /// </summary>
        /// <param name="outputlocation">The path to print the file to</param>
        /// <param name="earners">A list of the information for the top earners, it is assumed to be in order</param>
        public static void printTopEarnerInfo(string outputlocation, List<TopEarner> earners)
        {
            if (outputlocation != null && earners != null)
            {
                try
                {
                    using (StreamWriter output = new StreamWriter(outputlocation + Definitions.REQUIREMENTTWOFILENAME))
                    {
                        foreach (TopEarner earner in earners)
                        {
                            output.WriteLine(earner.ToString());
                        }
                    }
                }
                catch (DirectoryNotFoundException dnfe)
                {
                    throw new DirectoryNotFoundException("Error in Utility.printTopEarnerInfo()...The supplied output location " + outputlocation + " is invalid: " + dnfe.Message);
                }
                catch (ArgumentNullException ane)
                {
                    throw new Exception("Error in Utility.printTopEarnerInfo(): " + ane.Message);
                }
                catch (Exception e)
                {
                    throw new Exception("An unanticipated error occurred in Utility.printTopEarnerInfo() : " + e.Message);
                }

            }
            else
            {
                if (outputlocation == null && earners == null)
                {
                    throw new ArgumentNullException("Error in Utilities.printTopEarnerInfo: A null value was passed into both parameters");
                }
                else if (outputlocation == null)
                {
                    throw new ArgumentNullException("Error in Utilities.printTopEarnerInfo: A null value was passed into the outputlocation parameter");
                }
                else if (earners == null)
                {
                    throw new ArgumentNullException("Error in Utilities.printTopEarnerInfo: A null value was passed into the earners parameter");
                }
                else
                {
                    throw new Exception("Error in Utilities.printTopEarnerInfo: An unexpected error has occurred");
                }
            }
        }
        #endregion
        #region generateStateData()
        /// <summary>
        /// generateStateData()
        /// 
        /// Generates information about pay organised by state based on a passed in list of paycheck information.
        /// Throws an ArgumentNullException if the payCheck information is null
        /// </summary>
        /// <param name="payChecks">A list of paycheck data to generate state specific data from</param>
        /// <returns>A list of pay information by state</returns>
        public static List<StateData> generateStateData(List<PayCheck> payChecks)
        {
            if (payChecks != null)
            {
                List<StateData> returnThis = new List<StateData>();

                foreach(string statecode in Definitions.VALIDSTATES)
                {
                    returnThis.Add(new StateData(statecode));
                }

                foreach(PayCheck payCheck in payChecks)
                {
                    returnThis.Find(state => state.StateCode == payCheck.StateCode).addPayCheck(payCheck);
                }

                return returnThis;
            }
            else
            {
                throw new ArgumentNullException("Error in Utility.generateStateData(): Null value passed into payChecks parameter");
            }
        }
        #endregion
        #region sortStateData()
        /// <summary>
        /// sortStateData()
        /// 
        /// Uses a bubble sort to sort a list of state data alphabetically by state
        /// Throws ArgumentNullException if the stateData passed in is null
        /// </summary>
        /// <param name="stateData">The data to be sorted</param>
        /// <returns>A list of state data sorted alphabetically by state</returns>
        public static List<StateData> sortStateData(List<StateData> stateData)
        {
            if (stateData != null)
            {

                for (int n = stateData.Count - 2; n > 0; n--)
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (String.Compare(stateData[i].StateName, stateData[i + 1].StateName) > 0)
                        {
                            StateData holdThis = stateData[i];
                            stateData[i] = stateData[i + 1];
                            stateData[i + 1] = holdThis;
                        }
                    }
                }

                return stateData;
            }
            else
            {
                throw new ArgumentNullException("Error in Utility.sortStateData(): Null value passed into stateData parameter");
            }
        }
        #endregion
        #region printStateData()
        /// <summary>
        /// printStateData()
        /// 
        /// Prints the given state data to a file
        /// The filename is hardcoded in Definitions.cs
        /// Throws an ArgumentNullException if either of the parameters is given as null
        /// Throws a DirectoryNotFoundException if the given outputlocation is invalid
        /// Throws an Exception if any other error occurs
        /// </summary>
        /// <param name="outputlocation">The path to print the file to</param>
        /// <param name="stateData">A list of data to be printed, it is assumed to be sorted as desired before being passed in</param>
        public static void printStateData(string outputlocation, List<StateData> stateData)
        {
            if (outputlocation != null && stateData != null)
            {
                try
                {
                    using (StreamWriter output = new StreamWriter(outputlocation + Definitions.REQUIREMENTTHREEFILENAME))
                    {
                        foreach(StateData state in stateData)
                        {
                            output.WriteLine(state.ToString());
                        }
                    }
                }
                catch (DirectoryNotFoundException dnfe)
                {
                    throw new DirectoryNotFoundException("Error in Utility.printStateData()...The supplied output location " + outputlocation + " is invalid: " + dnfe.Message);
                }
                catch (ArgumentNullException ane)
                {
                    throw new Exception("Error in Utility.printStateData(): " + ane.Message);
                }
                catch (Exception e)
                {
                    throw new Exception("An unanticipated error occurred in Utility.printStateData() : " + e.Message);
                }
            }
            else
            {
                if (outputlocation == null && stateData == null)
                {
                    throw new ArgumentNullException("Error in Utilities.printStateData(): A null value was passed into both parameters");
                }
                else if (outputlocation == null)
                {
                    throw new ArgumentNullException("Error in Utilities.printStateData(): A null value was passed into the outputlocation parameter");
                }
                else if (stateData == null)
                {
                    throw new ArgumentNullException("Error in Utilities.printStateData(): A null value was passed into the stateData parameter");
                }
                else
                {
                    throw new Exception("Error in Utilities.printStateData(): An unexpected error has occurred");
                }
            }
        }
        #endregion
    }
}
