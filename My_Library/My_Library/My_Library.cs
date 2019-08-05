using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace My_Library
{
    public class LogElement
    {
        public int LogİD { get; set; }
        public DateTime LogTime { get; set; }
        public short Logtype { get; set; }
        public string LibCode { get; set; }
        public short UnitNo { get; set; }
        public short EventCode { get; set; }
        public string EventValue { get; set; }

        public LogElement(byte[] Bytes)
        {
            int j, k = 0;
            this.LogİD = BitConverter.ToInt32(Bytes, 0);
            this.LogTime = DateTimeOffset.FromUnixTimeSeconds(BitConverter.ToInt32(Bytes, 4)).UtcDateTime;
            this.Logtype = BitConverter.ToInt16(Bytes, 8);
            for (j = 10; j < 16; j++)
            {
                if (Bytes[j] != 0)
                    k++;
                else
                    break;
            }
            this.LibCode = System.Text.Encoding.ASCII.GetString(Bytes, 10, k);
            this.UnitNo = BitConverter.ToInt16(Bytes, 16);
            this.EventCode = BitConverter.ToInt16(Bytes, 18);
            k = 0;
            for (j = 20; j < 32; j++)
            {
                if (Bytes[j] != 0)
                    k++;
                else
                    break;
            }
            this.EventValue = System.Text.Encoding.ASCII.GetString(Bytes, 20, k);
        }
    }

    public class WL_Change
    {
        public DateTime Changing_Time;
        public double Change_time;
        public string value;


        public WL_Change(DateTime dateTime, double Change_time, String value)
        {
            this.Changing_Time = dateTime;
            this.Change_time = Change_time;
            this.value = value;
        }

    }

    public class XElementsP
    {
        public string ID { get; set; }
        public string Default { get; set; }
    }

    public class Staj_Projem_ARGS
    {
        public List<string> Filelist;
        public List<string> NotFound_V;
        public static int FileLen_G;
        public LogElement[] LogElements;
        public List<XElementsP> XElementsPL;


        public LogElement[] FileOpen(List<string> Filelist)
        {

            foreach (string var in Filelist)
            {
                FileStream fs = File.OpenRead(var);
                FileLen_G += (int)fs.Length / 32;
                fs.Close();
            }
            LogElements = new LogElement[FileLen_G];

            NotFound_V = null;
            int n = 0;
            int i = 0, j = 0;

            foreach (string var in Filelist)
            {
                FileStream fs = File.OpenRead(var);
                int FileLen = (int)fs.Length;
                byte[] bytes = new byte[FileLen];

                fs.Read(bytes, 0, bytes.Length);
                byte[] dizi = new byte[32];
                long k = FileLen;
                int l = 0;
                for (i = 0; i < (FileLen / 32); i++)
                {
                    for (j = 0; j < 32; j++)
                    {
                        if (l <= k)
                        {
                            dizi[j] = bytes[l];
                            l++;
                        }
                    }
                    if (n <= FileLen_G)
                    {
                        LogElements[n] = new LogElement(dizi);
                        n++;
                    }
                }

                fs.Close();
            }

            return LogElements;
        }

        public string NotFound_L(List<string> NotFound_V)
        {
            string text = "";
            foreach (string var in NotFound_V)
            {
                text += var + " \n";
            }
            return text;

        }

        public List<XElementsP> Xelement_C(string TcTLO_N)
        {
            string path = @"" + TcTLO_N;
            string xml = File.ReadAllText(path);
            XElementsPL = new List<XElementsP>();
            xml = xml.Replace("%s", "{0}");

            XElementsPL = XElement.Parse(xml).Descendants("o").Where(xe => xe.Elements("v").Any(x => (string)x.Attribute("n") == "TextID")).Select(xe => new XElementsP
            {
                ID = (string)xe.Elements("v").First(x => (string)x.Attribute("n") == "TextID"),
                Default = (string)xe.Elements("v").First(x => (string)x.Attribute("n") == "TextDefault"),
            }).ToList();

            return XElementsPL;
        }

        public List<string> Filelist_C(string FilePath_G)
        {
            Filelist = new List<string>();
            string[] dizindekiDosyalar = Directory.GetFiles(FilePath_G);
            foreach (string var in dizindekiDosyalar)
            {
                FileInfo fileInfo = new FileInfo(var);
                int NotFound_Temp = fileInfo.Name.IndexOf(".log", 0, fileInfo.Name.Length, StringComparison.Ordinal);

                if (NotFound_Temp != -1)
                {
                    int NotFound_Temp1 = fileInfo.Name.IndexOf("1970", 0, fileInfo.Name.Length, StringComparison.Ordinal);
                    if (NotFound_Temp1 == -1)
                    {
                        Filelist.Add(fileInfo.FullName);
                    }
                }
            }

            return Filelist;
        }
    }
}
