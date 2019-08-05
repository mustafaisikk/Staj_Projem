using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using My_Library;

namespace Staj_Projem_console
{
    class MainClass
    {
        public static List<string> Filelist;
        public static List<string> NotFound_V;
        public static int FileLen_G;
        public static LogElement[] LogElements;
        public static List<int> Year_list;
        public static List<XElementsP> XElementsPL;
        public static void Main(string[] args)
        {
            string FilePath_G = @"/home/mustafa/Masaüstü/sh/YeniDizin/logs";
            //args[0];
            string TcTLO_N = @"/home/mustafa/Belgeler/son/YeniDizin/LogTextList.TcTLO";
            //args[1];
            string FilePath = @"/home/mustafa/Belgeler/son/YeniDizin/log.csv";
            //args[2];

            Staj_Projem_ARGS ARGS = new Staj_Projem_ARGS();

            Filelist = ARGS.Filelist_C(FilePath_G);

            if (Filelist.Count == 0)
            {
                Console.WriteLine("Dosyanın İçeriği Boştur Lütfen Başka Bir Dosya Seçiniz.");
                return;
            }

            foreach (string var in Filelist)
            {
                FileStream fs = File.OpenRead(var);
                FileLen_G += (int)fs.Length / 32;
                fs.Close();
            }
            LogElements = new LogElement[FileLen_G];

            NotFound_V = null;
            int i = 0;

            LogElements = ARGS.FileOpen(Filelist); 

            Year_list = new List<int>();

            for (i = 0; i < (FileLen_G); i++)
            {
                if (Year_list.Contains(LogElements[i].LogTime.Year) == false)
                {
                    Year_list.Add(LogElements[i].LogTime.Year);
                }
            }
            Year_list.Sort();


            string file = @"" + FilePath;
            StreamWriter file_str = new StreamWriter(file);

            XElementsPL = ARGS.Xelement_C(TcTLO_N);

            for (i = 0; i < FileLen_G; i++)
            {
                file_str.WriteLine(LogElements[i].LogİD + ";" + LogElements[i].LogTime + ";" + LogElements[i].Logtype + ";" + LogElements[i].LibCode + ";" + LogElements[i].UnitNo + ";" +
                        LogElements[i].EventCode + ";" + EventValue_O(i),LogElements[i].EventValue);
            }

            file_str.Close();

            Console.WriteLine(ARGS.NotFound_L(NotFound_V));
            Console.ReadKey();
        }

        public static string EventValue_O(int indis)
        {
            string temp = LogElements[indis].EventCode + " bulunamadı";
            string temp1 = "\"" + LogElements[indis].EventCode + "\"";

            foreach (XElementsP var in XElementsPL)
            {
                if (var.ID == temp1)
                {
                    temp = var.Default;
                    break;
                }
            }

            if (NotFound_V == null)
            {
                NotFound_V = new List<string>();
                NotFound_V.Clear();
            }

            string temp2 = temp;
            int NotFound_Temp = temp2.IndexOf("bulunamadı", 0, temp2.Length, StringComparison.Ordinal);

            if (NotFound_Temp != -1)
            {
                if (NotFound_V.Contains(temp2) != true)
                {
                    NotFound_V.Add(temp2);
                }
            }

            return temp;
        }
    }
}
