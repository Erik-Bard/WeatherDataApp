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
{
    public class CsvReadHelper
    {
        public static string csv_file_path = "TempFuktData.csv";
        public static List<TinyCsvParser.Mapping.CsvMappingResult<CsvModelClass>> Reader()
        {
            TextReader reader = new StreamReader("TempFuktData.csv");
            var csvReader = new CsvReader(reader, System.Globalization.CultureInfo.CreateSpecificCulture("sv-se"));
            csvReader.GetRecords<CsvModelClass>();

            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            var csvParser = new CsvParser<CsvModelClass>(csvParserOptions, new CsvReadMapper());
            csvParser.ReadFromFile("TempFuktData.csv", Encoding.UTF8);
            var fixedEntry = csvParser
                        .ReadFromFile(csv_file_path, Encoding.ASCII)
                        .ToList();
            reader.Dispose();
            return fixedEntry;
        }
    }
}
