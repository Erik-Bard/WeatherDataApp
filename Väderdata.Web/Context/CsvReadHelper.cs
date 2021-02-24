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
        public static string csv_file_path = "TempFuktData.csv";
        public static void CsvBuilder(WeatherContext context)
        {
            var read = ReadCsv();
            // Ensure the DB exists and doesnt have any data in the table we want to populate
            context.Database.EnsureCreated();
            if (context.CsvModelClasses.Any())
            {
                Console.WriteLine("\nData already exists in database. Please remove old data before inserting new.");
            }
            else
            {
                if (read == null)
                {
                    Console.WriteLine("\n\nThis is the end of the sequence");
                }
                else
                {
                    context.CsvModelClasses.BulkInsert(read);
                }
            }
        }
        public static List<CsvModelClass> ReadCsv()
        {
            string path = "TempFuktData.csv";
            StreamReader sr = new StreamReader(path);
            Console.WriteLine("hej");
            Console.WriteLine();
            List<string> CsvData = File.ReadAllLines(path).Distinct().ToList();
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
            sr.Close();
            return SortedList;
        }
    }
}
