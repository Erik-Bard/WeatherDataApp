using CsvHelper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
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
            var read = Reader();
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
                    foreach (var item in read)
                    {
                        if (item.Error != null)
                        {
                            continue;
                        }
                        else
                        {
                            context.CsvModelClasses.Add(item.Result);
                        }
                    }
                }
                context.SaveChanges();
            }
        }
        public static List<TinyCsvParser.Mapping.CsvMappingResult<CsvModelClass>> Reader()
        {
            TextReader reader = new StreamReader("TempFuktData.csv");
            // Här har vi lagt in en culture info då datan har använt '.' istället för ',' i csv filen
            var csvReader = new CsvReader(reader, System.Globalization.CultureInfo.CreateSpecificCulture("sv-se"));
            csvReader.GetRecords<CsvModelClass>();

            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            // denna rad kopplas till CsvReadMapper.cs klassen där infon är lagd i sina specifika delar
            var csvParser = new CsvParser<CsvModelClass>(csvParserOptions, new CsvReadMapper());
            csvParser.ReadFromFile("TempFuktData.csv", Encoding.UTF8);
            var fixedEntry = csvParser
                        .ReadFromFile(csv_file_path, Encoding.ASCII)
                        .Skip(1)
                        //.Distinct()
                        .ToList();
            reader.Dispose();
            return fixedEntry;
        }
    }
}
