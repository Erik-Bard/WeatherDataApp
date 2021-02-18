//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Väderdata.Web.Data;
//using System.Globalization;
//using Väderdata.Web.Context;

//namespace Väderdata.Web.Context
//{
//    internal class CSVMarcus
//    {
//        private static string csv_file_path = "TempFuktData.csv";

//        //private static WeatherContext _Context = new WeatherContext();
//        public static void ReadCsv()
//        {
//            StreamReader sr = new StreamReader(csv_file_path);
//            string Line = sr.ReadLine();
//            List<CsvModelClass> WxInfo = new List<CsvModelClass>();
//            while (Line != null)
//            {
//                var s = CreateInfo(Line);
//                WxInfo.Add(s);
//            }
//            foreach(var item in WxInfo)
//            {
//                _Context.CsvModelClasses.Add(item);
//            }
//        }
//        private static CsvModelClass CreateInfo(string line)
//        {
//            CultureInfo invC = CultureInfo.InvariantCulture;
//            string[] info = line.Split(',');
//            DateTime Date = DateTime.Parse(info[0]);
//            double temp = double.Parse(info[2], invC);
//            double humid = double.Parse(info[3]);
//            CsvModelClass WXData = new CsvModelClass { Datum = Date, Plats = info[1], Temp = temp, Luftfuktighet = humid };
//            return WXData;
//        }
//    }
//}
