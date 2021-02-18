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
        //public static DataTable GetDataTabletFromCSVFile()
        //{
        //    DataTable csvData = new DataTable();
        //    try
        //    {
        //        TextFieldParser csvReader = new TextFieldParser(csv_file_path);
        //        csvReader.SetDelimiters(new [] { "," });
        //        csvReader.HasFieldsEnclosedInQuotes = true;
        //        string[] colFields = csvReader.ReadFields();
        //        foreach (string column in colFields)
        //        {
        //            DataColumn datecolumn = new DataColumn(column);
        //            datecolumn.AllowDBNull = true;
        //            csvData.Columns.Add(datecolumn);
        //        }
        //        while (!csvReader.EndOfData)
        //        {
        //            string[] fieldData = csvReader.ReadFields();
        //            for (int i = 0; i < fieldData.Length; i++)
        //            {
        //                if (fieldData[i] == "")
        //                {
        //                    fieldData[i] = null;
        //                }
        //            }
        //            csvData.Rows.Add(fieldData);
        //        }
        //        csvReader.Close();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return csvData;
        //}

        public static List<TinyCsvParser.Mapping.CsvMappingResult<CsvModelClass>> Reader()
        {
            TextReader reader = new StreamReader("TempFuktData.csv");
            var csvReader = new CsvReader(reader, System.Globalization.CultureInfo.CreateSpecificCulture("sv-se"));
            var records = csvReader.GetRecords<CsvModelClass>();


            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            var csvParser = new CsvParser<CsvModelClass>(csvParserOptions, new CsvReadMapper());
            var entries = csvParser.ReadFromFile("TempFuktData.csv", Encoding.UTF8);
            var fixedEntry = csvParser
                        .ReadFromFile(csv_file_path, Encoding.ASCII)
                        .ToList();
            return fixedEntry;
        }
    }
}
