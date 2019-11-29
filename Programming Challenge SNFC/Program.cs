using Programming_Challenge_SNFC.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Programming_Challenge_SNFC
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Please include a valid output location for the textfiles in the args.");
            }
            else if (args.Length > 1)
            {
                Console.WriteLine("Please only include a single argument in the args. It should be a valid output location for the textfiles.");
            }
            else
            {
                List<Employee> employees = null;
                Console.WriteLine("Gathering employee information from file");
                try
                {
                    employees = Utility.collectEmployeeInformation();
                }
                catch(Exception e)
                {
                    Console.WriteLine("An error occurred while collecting employee data from the file: " + e.Message);
                }

                if (employees != null)
                {
                    List<PayCheck> payChecks = null;
                    Console.WriteLine("Generating paycheck info from employee info");
                    try
                    {
                        payChecks = Utility.generatePaycheckInfo(employees);
                        Console.WriteLine("Sorting paycheck info");
                        payChecks = Utility.sortPaychecks(payChecks);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An error occurred while generating paycheck info: " + e.Message);
                    }

                    if (payChecks != null)
                    {
                        Console.WriteLine("Writing paycheck info to file");
                        try
                        {
                            Utility.printPaycheckInfo(args[0], payChecks);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An error occurred while writing paycheck info to " + args[0] + Definitions.REQUIREMENTONEFILENAME + ": " + e.Message);
                        }
                    }

                    if (payChecks != null)
                    {
                        Console.WriteLine("Writing top earners info to file");
                        try
                        {

                        }
                        catch
                        {
                            Console.WriteLine("An error occurred while writing top earner info to " + args[0] + Definitions.REQUIREMENTTWOFILENAME + ": " + e.Message);
                        }
                    }
                }
            }
        }
    }
}
