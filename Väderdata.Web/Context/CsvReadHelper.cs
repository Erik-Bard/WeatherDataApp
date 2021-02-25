using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Väderdata.Web.Data;

namespace Väderdata.Web.Context
{                   /// <summary>
                    ///   den här klassen så läser vi in väder datan i csv fil format, och sedan returnerar en variabel med all info till
                    ///   datainitializer klassen där den läggs in i databasen
                    /// </summary>
    public class CsvReadHelper
    {
        // Declare csv-file as a string to call on when needed
        public static string csv_file_path = "TempFuktData.csv";
        public static void CsvBuilder(WeatherContext context)
        {
            // Read in sorted data from method and save in variable
            var read = ReadCsv();

            // Ensure the DB exists and doesnt have any data in the table we want to populate
            context.Database.EnsureCreated();
            if (context.CsvModelClasses.Any())
            {
                Console.WriteLine("\nData already exists in database. Please remove old data before inserting new.");
            }
            else
            {
                // Check if read (our list of information from csv) contains anything 
                // and if null it returns console writeline
                if (read == null)
                {
                    Console.WriteLine("\n\nThis is the end of the sequence");
                }
                // if our db with csv-file info is empty we bulk insert
                // using Nuget-Package
                else
                {
                    context.CsvModelClasses.BulkInsert(read);
                }
            }
        }
        public static List<CsvModelClass> ReadCsv()
        {
            // Open stream to file, read all data
            List<string> CsvData = File.ReadAllLines(csv_file_path).Distinct().ToList();
            List<CsvModelClass> SortedList = new List<CsvModelClass>();
            foreach (string item in CsvData)
            {
                // Divide string inputs from file and remove commas
                string[] values = item.Split(',');
                CsvModelClass csvClass = new CsvModelClass();
                //Give properties value and parse values from string seperated array
                try
                {
                    csvClass.Datum = DateTime.Parse(values[0]);
                    csvClass.Plats = values[1];
                    var variable = values[2].Replace('.', ',')
                                            .Replace('−', '-');
                    csvClass.Temp = double.Parse(variable);
                    csvClass.Luftfuktighet = int.Parse(values[3]);
                    Console.WriteLine($"{csvClass.Datum}, {variable}");
                    SortedList.Add(csvClass);
                }
                // Catch format error and set temp to 0 because double cant be null
                catch (FormatException)
                {
                    string s = values[2];
                    csvClass.Temp = 0;
                }
                // Catch Overflow during assert and set temp to 0 because double cant be null
                catch (OverflowException)
                {
                    csvClass.Temp = 0;
                }
                // Catch other errors and display to user via console
                catch (Exception em)
                {
                    Console.WriteLine(em.Message);
                }
            }
            return SortedList;
        }
    }
}
