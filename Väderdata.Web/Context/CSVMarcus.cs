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
//        public static void ReadCsv(WeatherContext context)
//        {
//            StreamReader sr = new StreamReader(csv_file_path);
//            string Line = sr.ReadLine().ToString();
//            List<CsvModelClass> WxInfo = new List<CsvModelClass>();
//            int x = 0;
//            while (Line != null)
//            {
                
//                var v = CreateInfo(Line);
//                Console.WriteLine(x);
//                WxInfo.Add(v);
//                var t = from W in WxInfo
//                        orderby W.Datum ascending
//                        select W;
//                x++;
//                Line = sr.ReadLine();
//            }
//            sr.Close();
//            var csv = from c in WxInfo
//                      orderby c.Datum ascending
//                      select c;
//            foreach (var item in csv)
//            {
//                context.CsvModelClasses.Add(item);
//                context.SaveChanges();
//            }
//        }
//        private static CsvModelClass CreateInfo(string line)
//        {
//            CultureInfo invC = CultureInfo.InvariantCulture;
//            string[] info = line.Split(',');
//            DateTime Date = DateTime.Parse(info[0]);
//            string position = info[1].ToString();
//            double temp = double.Parse(info[2], invC);
//            Console.WriteLine(temp);
//            Console.WriteLine(Date);
//            double humid = double.Parse(info[3]);
//            CsvModelClass WXData = new CsvModelClass { Datum = Date, Plats = position, Temp = temp, Luftfuktighet = humid };
//            return WXData;
//        }
//    }
//}
