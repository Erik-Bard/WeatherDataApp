using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Väderdata.Web.Data;

namespace Väderdata.Web.Context
{
    public class CsvReader
    {
        public static string csv_file_path = "../TempFuktData.csv";
        public static DataTable GetDataTabletFromCSVFile()
        {
            DataTable csvData = new DataTable();
            try
            {
                TextFieldParser csvReader = new TextFieldParser("../TempFuktData.csv");
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                string[] colFields = csvReader.ReadFields();
                foreach (string column in colFields)
                {
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;
                    csvData.Columns.Add(datecolumn);
                }
                while (!csvReader.EndOfData)
                {
                    string[] fieldData = csvReader.ReadFields();
                    for (int i = 0; i < fieldData.Length; i++)
                    {
                        if (fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }
                    csvData.Rows.Add(fieldData);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return csvData;
        }
        public static void InsertDataAsBulk(DataTable csvFileData)
        {
            using (SqlConnection dbConnection = new SqlConnection("Data Source=ProductHost;Initial Catalog=WeatherDataDb;Integrated Security=True;"))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = "";
                    foreach (var column in csvFileData.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    s.WriteToServer(csvFileData);
                }
            }
        }
    }
}
