using System;
using Gtk;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using My_Library;

public partial class MainWindow
{
    private Fixed fixed2;
    private Gtk.Button button6;
    private Gtk.Button button3;
    private Gtk.Button button2;
    private Gtk.Button button1;
    private Gtk.Button button4;
    private ScrolledWindow GtkScrolledWindow;
    private TextView textview3;
    private Gtk.Button button5;
    private Gtk.Button button7;
    private Gtk.Button button8;

    protected virtual void Build()
    {

        global::Stetic.Gui.Initialize(this);
        // Widget MainWindow
        this.Name = "MainWindow";
        this.Title = Mono.Unix.Catalog.GetString("LogToCSV");
        this.WindowPosition = (WindowPosition)4;
        this.Resizable = false;
        this.DefaultWidth = 0;
        this.DefaultHeight = 0;
        // Container child MainWindow.Gtk.Container+ContainerChild
        this.fixed2 = new Fixed
        {
            Name = "fixed2",
            HasWindow = false
        };
        // Container child fixed2.Gtk.Fixed+FixedChild
        this.button6 = new Gtk.Button
        {
            CanFocus = true,
            Name = "button6",
            UseUnderline = true,
            Label = global::Mono.Unix.Catalog.GetString("Dosya Seçiz")
        };
        this.fixed2.Add(this.button6);
        global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.fixed2[this.button6]));
        w1.X = 70;
        w1.Y = 40;
        button6.Clicked += new EventHandler(FileOpen);
        // Container child fixed2.Gtk.Fixed+FixedChild
        this.button3 = new Gtk.Button
        {
            CanFocus = true,
            Name = "button3",
            UseUnderline = true,
            Label = global::Mono.Unix.Catalog.GetString("Dosyayı Görüntüle")
        };
        this.fixed2.Add(this.button3);
        Fixed.FixedChild w2 = ((Fixed.FixedChild)(this.fixed2[this.button3]));
        w2.X = 70;
        w2.Y = 160;
        this.button3.Clicked += new EventHandler(FileList);
        // Container child fixed2.Gtk.Fixed+FixedChild
        this.button2 = new Gtk.Button
        {
            CanFocus = true,
            Name = "button2",
            UseUnderline = true,
            Label = global::Mono.Unix.Catalog.GetString("Konum Seçiniz")
        };
        this.fixed2.Add(this.button2);
        Fixed.FixedChild w3 = ((Fixed.FixedChild)(this.fixed2[this.button2]));
        w3.X = 70;
        w3.Y = 220;
        button2.Clicked += new EventHandler(PathOpen);

        // Container child fixed2.Gtk.Fixed+FixedChild
        this.button1 = new Gtk.Button
        {
            CanFocus = true,
            Name = "button1",
            UseUnderline = true,
            Label = global::Mono.Unix.Catalog.GetString("Dosya Oluştur")
        };
        this.fixed2.Add(this.button1);
        Fixed.FixedChild w4 = ((Fixed.FixedChild)(this.fixed2[this.button1]));
        w4.X = 70;
        w4.Y = 280;
        button1.Clicked += new EventHandler(FileCreate);
        // Container child fixed2.Gtk.Fixed+FixedChild
        this.GtkScrolledWindow = new ScrolledWindow
        {
            WidthRequest = 700,
            HeightRequest = 400,
            Name = "GtkScrolledWindow",
            ShadowType = ((global::Gtk.ShadowType)(1))
        };
        // Container child GtkScrolledWindow.Gtk.Container+ContainerChild
        this.textview3 = new TextView
        {
            CanFocus = true,
            Name = "textview3"
        };
        this.GtkScrolledWindow.Add(this.textview3);
        this.fixed2.Add(this.GtkScrolledWindow);
        Fixed.FixedChild w6 = (Fixed.FixedChild)this.fixed2[this.GtkScrolledWindow];
        w6.X = 265;
        w6.Y = 14;
        // Container child fixed2.Gtk.Fixed+FixedChild
        this.button4 = new Gtk.Button
        {
            CanFocus = true,
            Name = "button4",
            UseUnderline = true,
            Label = Mono.Unix.Catalog.GetString("TcTLO Seçiniz")
        };
        this.fixed2.Add(this.button4);
        Fixed.FixedChild w7 = (Fixed.FixedChild)this.fixed2[this.button4];
        w7.X = 70;
        w7.Y = 100;
        button4.Clicked += new EventHandler(TcTLO_O);
        // Container child fixed2.Gtk.Fixed + FixedChild
        this.button5 = new Gtk.Button
        {
            CanFocus = true,
            Name = "button5",
            UseUnderline = true,
            Label = global::Mono.Unix.Catalog.GetString("Bulunamayanlar")
        };
        this.fixed2.Add(this.button5);
        global::Gtk.Fixed.FixedChild w8 = ((global::Gtk.Fixed.FixedChild)(this.fixed2[this.button5]));
        w8.X = 10;
        w8.Y = 330;
        button5.Clicked += new EventHandler(NotFound_L);
        // Container child fixed2.Gtk.Fixed+FixedChild
        this.button7 = new Gtk.Button
        {
            CanFocus = true,
            Name = "button7",
            UseUnderline = true,
            Label = Mono.Unix.Catalog.GetString("Temizle")
        };
        this.fixed2.Add(this.button7);
        Fixed.FixedChild w9 = (Fixed.FixedChild)(this.fixed2[this.button7]);
        w9.X = 180;
        w9.Y = 380;
        button7.Clicked += new EventHandler(Clear);
        // Container child fixed2.Gtk.Fixed+FixedChild
        this.button8 = new Gtk.Button
        {
            CanFocus = true,
            Name = "button8",
            UseUnderline = true,
            Label = global::Mono.Unix.Catalog.GetString("Analiz Oluştur")
        };
        this.fixed2.Add(this.button8);
        Fixed.FixedChild w10 = (Fixed.FixedChild)(this.fixed2[this.button8]);
        w10.X = 10;
        w10.Y = 380;
        button8.Clicked += new EventHandler(Create_analysis);
        this.Add(this.fixed2);
        if ((this.Child != null))
        {
            this.Child.ShowAll();
        }
        this.Show();

        this.DeleteEvent += new DeleteEventHandler(this.OnDeleteEvent);
    }

    WL_Change WL_Change_Temp;
    LogElement[] LogElements;
    Staj_Projem_ARGS ARGS = new Staj_Projem_ARGS();

    public List<string> Filelist;
    public List<string> NotFound_V;
    List<WL_Change>[,] WL_Changes_6;
    List<WL_Change>[,] WL_Changes_4;
    List<WL_Change> WL_Changes_2;
    List<WL_Change> WL_Changes_Max;
    List<int> Year_list;
    List<XElementsP> XElementsPL;

    public string FilePath = "";
    public string FilePath_G = "";
    public static string TcTLO_N = "";

    public static int FileLen_G;
    public static double WL_Changes_double;
    public static int Day_Count;
    public static int Day_Count_Temp;
    public static int Water_Supply;

    private void FileOpen(object sender, EventArgs e)
    {


        FolderBrowserDialog fBrowser = new FolderBrowserDialog();

        if (fBrowser.ShowDialog() == DialogResult.OK)
        {
            FilePath_G = fBrowser.SelectedPath;
        }
        if (FilePath_G == "")
        {
            MessageBox.Show("Lütfen Bir Dosya Seçiniz");
            return;
        }
        MessageBox.Show(FilePath_G);

        Filelist = ARGS.Filelist_C(FilePath_G);

        if (Filelist.Count == 0)
        {
            MessageBox.Show("Dosyanın İçeriği Boştur Lütfen Tekrar Seçiniz.");
            FilePath_G = "";
            return;
        }
        FileLen_G = 0;
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
    }

    private void PathOpen(object sender, EventArgs e)
    {
        FolderBrowserDialog fBrowser = new FolderBrowserDialog();

        if (fBrowser.ShowDialog() == DialogResult.OK)
        {
            FilePath = fBrowser.SelectedPath;
        }
        if (FilePath == "")
        {
            MessageBox.Show("Lütfen Bir Konum Seçiniz.");
            return;
        }
        MessageBox.Show(FilePath);

    }

    private void FileCreate(object sender, EventArgs e)
    {
        if (Filelist == null)
        {
            MessageBox.Show("Lütfen Bir Dosya Seçiniz.");
            return;
        }

        else if (FilePath == "")
        {
            MessageBox.Show("Lütfen Bir Konum Seçiniz.");
            return;
        }
        else if (TcTLO_N == "")
        {
            MessageBox.Show("Lütfen Bir TcTLO Dosyası Seçiniz.");
            return;
        }
        else if (textview3.Buffer.Text == "")
        {
            MessageBox.Show("Lütfen Dosyayı Görüntüleyiniz.");
            return;
        }

        int i;
        WL_Changes_4 = null;
        WL_Changes_6 = null;

        string file = @"" + FilePath + "/Log.csv";
        StreamWriter file_str = new StreamWriter(file);


        for (i = 0; i < FileLen_G; i++)
        {

            file_str.WriteLine(LogElements[i].LogİD + ";" + LogElements[i].LogTime + ";" + LogElements[i].Logtype + ";" + LogElements[i].LibCode + ";" + LogElements[i].UnitNo + ";" +
                    LogElements[i].EventCode + ";" + EventValue_O(i));
        }

        file_str.Close();


    }

    private void FileList(object sender, EventArgs e)
    {

        if (Filelist == null)
        {
            MessageBox.Show("Lüen Bir Dosya Seçiz.");
            return;
        }
        if (TcTLO_N == "")
        {
            MessageBox.Show("Lüfen TcTLO Dosyasını Seçniz.");
            return;
        }
        if (NotFound_V == null)
        {
            NotFound_V = new List<string>();
        }
        else
        {
            NotFound_V.Clear();
        }
        int i;

        WL_Changes_4 = null;
        WL_Changes_6 = null;

        var iter = this.textview3.Buffer.GetIterAtLine(0);
        this.textview3.Buffer.InsertWithTags(ref iter, "");
        XElementsPL = ARGS.Xelement_C(TcTLO_N);

        for (i = 0; i < FileLen_G; i++)
        {
            string temp = "" + LogElements[i].LogİD + " --> " +
            LogElements[i].LogTime + " --> " + LogElements[i].Logtype + " --> " +
            LogElements[i].LibCode + " --> " + LogElements[i].UnitNo + " --> " +
            LogElements[i].EventCode + " --> " + EventValue_O(i) + "\n";
            this.textview3.Buffer.InsertWithTags(ref iter, "" + temp);
        }
    }

    private void TcTLO_O(object sender, EventArgs e)
    {
        OpenFileDialog fileN1 = new OpenFileDialog
        {
            Filter = "TcTLO Dosyası |*.TcTLO | Xml Dosyası |*.xml"
        };
        if (fileN1.ShowDialog() == DialogResult.OK)
        {
            TcTLO_N = fileN1.FileName;
        }
        if (TcTLO_N == "")
        {
            MessageBox.Show("Lütfen Bir TcTLO Dosyası Seçiniz.");
            return;
        }
        MessageBox.Show(TcTLO_N);

    }

    public string EventValue_O(int indis)
    {
        string temp = LogElements[indis].EventCode + " bulunamadı";
        int notFound;
        string temp1 = "\"" + LogElements[indis].EventCode + "\"";

        foreach (XElementsP var in XElementsPL)
        {
            if (var.ID == temp1)
            {
                notFound = var.Default.IndexOf("{0}", 0, var.Default.Length, StringComparison.Ordinal);
                if (notFound != -1)
                {
                    temp = var.Default.Replace("{0}", "" + LogElements[indis].EventValue);
                    break;
                }
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

        int xx, xy;
        if (WL_Changes_6 == null)
        {
            WL_Changes_6 = new List<WL_Change>[Year_list.Count, 13];

            for (xy = 0; xy < Year_list.Count; xy++)
            {
                if (WL_Changes_6[xy, 0] == null)
                {
                    for (xx = 0; xx < 13; xx++)
                    {
                        WL_Changes_6[xy, xx] = new List<WL_Change>();
                    }
                }
            }
        }

        if (WL_Changes_4 == null)
        {
            WL_Changes_4 = new List<WL_Change>[Year_list.Count, 13];

            for (xy = 0; xy < Year_list.Count; xy++)
            {
                if (WL_Changes_4[xy, 0] == null)
                {
                    for (xx = 0; xx < 13; xx++)
                    {
                        WL_Changes_4[xy, xx] = new List<WL_Change>();
                    }
                }
            }
        }

        if (WL_Changes_Max == null)
        {
            WL_Changes_Max = new List<WL_Change>();
        }

        if (WL_Changes_2 == null)
        {
            WL_Changes_2 = new List<WL_Change>();
        }

        if (Day_Count_Temp != LogElements[indis].LogTime.Day)
        {
            Day_Count++;
        }
        Day_Count_Temp = LogElements[indis].LogTime.Day;


        int Water_Supply_Temp = temp2.IndexOf("Su besleme açıldı.", 0, temp2.Length, StringComparison.Ordinal);
        if (Water_Supply_Temp != -1)
        {
            Water_Supply++;
        }

        int changed = temp2.IndexOf("Su seviyesi değişti. Yeni seviye:", 0, temp2.Length, StringComparison.Ordinal);
        if (changed != -1)
        {

            if (WL_Change_Temp == null)
            {
                WL_Change_Temp = new WL_Change(LogElements[indis].LogTime, 0, LogElements[indis].EventValue);
            }

            WL_Changes_double = (LogElements[indis].LogTime - WL_Change_Temp.Changing_Time).TotalSeconds;
            WL_Change wL_Change = new WL_Change(LogElements[indis].LogTime, WL_Changes_double, LogElements[indis].EventValue);

            if ((WL_Change_Temp.value == "6" && wL_Change.value == "4") || (WL_Change_Temp.value == "2" && wL_Change.value == "4"))
            {
                if (WL_Changes_double > 60 && WL_Changes_double < 70000)
                {
                    WL_Changes_4[Year_list.IndexOf(LogElements[indis].LogTime.Year), LogElements[indis].LogTime.Month].Add(wL_Change);
                }
            }
            else if (WL_Change_Temp.value == "4" && wL_Change.value == "6")
            {
                if (WL_Changes_double > 60 && WL_Changes_double < 70000)
                {
                    WL_Changes_6[Year_list.IndexOf(LogElements[indis].LogTime.Year), LogElements[indis].LogTime.Month].Add(wL_Change);
                }
            }
            if (WL_Changes_double > 70000)
            {
                WL_Changes_Max.Add(wL_Change);
            }
            if (wL_Change.value == "2")
            {
                WL_Changes_2.Add(wL_Change);
            }
            WL_Change_Temp = wL_Change;

        }

        return temp;
    }

    private void NotFound_L(object sender, EventArgs e)
    {
        if (Filelist == null)
        {
            MessageBox.Show("Lütfen Dosya Seçiniz.");
            return;
        }
        else if (textview3.Buffer.Text == "")
        {
            MessageBox.Show("Lütfen Dosyayı Görüntüleyiniz.");
            return;
        }

        else if (NotFound_V.Count == 0)
        {
            MessageBox.Show("Herşey Yolunda.");
            return;
        }

        MessageBox.Show(ARGS.NotFound_L(NotFound_V));

    }

    private void Clear(object sender, EventArgs e)
    {
        textview3.Buffer.Clear();
        Day_Count = 0;
        Day_Count_Temp = 0;
        Water_Supply = 0;
        NotFound_V.Clear();
    }

    public void Create_analysis(object sender, EventArgs e)
    {
        if (FilePath == "")
        {
            MessageBox.Show("Lüen Bir Konum Seçiniz.");
            return;
        }
        if (textview3.Buffer.Text == "")
        {
            MessageBox.Show("Lüen Dosyayı Okutunuz.");
            return;
        }
        string Output = "";

        int i, j;
        if (WL_Changes_6 != null && WL_Changes_4 != null)
        {
            string file2 = @"" + FilePath + "/WChanced.csv";
            StreamWriter file_str2 = new StreamWriter(file2);

            for (i = 0; i < Year_list.Count; i++)
            {
                for (j = 0; j < 13; j++)
                {
                    if (WL_Changes_6[i, j].Count != 0)
                    {
                        foreach (WL_Change var in WL_Changes_6[i, j])
                        {
                            file_str2.WriteLine(var.Changing_Time + ";" + var.Change_time + ";" + var.value);
                        }
                    }
                }
            }
            file_str2.WriteLine("\n\n\n");
            for (i = 0; i < Year_list.Count; i++)
            {
                for (j = 0; j < 13; j++)
                {
                    if (WL_Changes_4[i, j].Count != 0)
                    {
                        foreach (WL_Change var in WL_Changes_4[i, j])
                        {
                            file_str2.WriteLine(var.Changing_Time + ";" + var.Change_time + ";" + var.value);
                        }
                    }
                }
            }

            file_str2.WriteLine("\n\n\n");

            foreach (WL_Change var in WL_Changes_Max)
            {
                file_str2.WriteLine(var.Changing_Time + ";" + var.Change_time + ";" + var.value);
            }
            file_str2.WriteLine("\n\n\n");

            foreach (WL_Change var in WL_Changes_2)
            {
                file_str2.WriteLine(var.Changing_Time + ";" + var.Change_time + ";" + var.value);
            }
            file_str2.WriteLine("\n TOPLAM = "+WL_Changes_2.Count);

            file_str2.Close();
        }

        int All_Count = 0;
        double Avarange = 0;
        double STR_DV_L = 0;

        for (i = 0; i < Year_list.Count; i++)
        {
            for (j = 0; j < 13; j++)
            {
                All_Count += WL_Changes_6[i, j].Count;
                All_Count += WL_Changes_4[i, j].Count;
            }
        }

        for (i = 0; i < Year_list.Count; i++)
        {
            for (j = 0; j < 13; j++)
            {
                foreach (WL_Change var in WL_Changes_4[i, j])
                {
                    Avarange += var.Change_time / All_Count;
                }
                foreach (WL_Change var in WL_Changes_6[i, j])
                {
                    Avarange += var.Change_time / All_Count;
                }

                foreach (WL_Change var in WL_Changes_4[i, j])
                {
                    STR_DV_L += (var.Change_time - Avarange) * (var.Change_time - Avarange) / (All_Count - 1);
                }
                foreach (WL_Change var in WL_Changes_6[i, j])
                {
                    STR_DV_L += (var.Change_time - Avarange) * (var.Change_time - Avarange) / (All_Count - 1);
                }
            }
        }
        STR_DV_L = Math.Sqrt(STR_DV_L);


        Output += "Standart Sapma: " + TimeSpan.FromSeconds(STR_DV_L) + "\n"+
            "Ortalama : " + TimeSpan.FromSeconds(Avarange) + "\n\n\n"+
            "Toplam gün sayısı : " + Day_Count + " ---> Toplam Kayıt Sayısı " + LogElements.Count()+"\n"+
            "Ortalama Günlük Kayıt Sayısı : "+(LogElements.Count()/Day_Count)+" ---> Günlük : "+ ((LogElements.Count() / Day_Count)*32) + "Byte\n"+
            "100 MB Ortalama " + ((100 * 1048576) / ((LogElements.Count() / Day_Count) * 32)) + " Günde <--> "+ (((100 * 1048576) / ((LogElements.Count() / Day_Count) * 32)) / 365) +" Yılda Dolar\n\n" +
        	"Toplam gün sayısı : " + Day_Count + " ---> Toplam Su Besleme Sayısı: " + Water_Supply +"\n" +
        	"Ortalama Su Besleme Sayısı : "+ Water_Supply/Day_Count + "\n\n\n";

        
        double[,] Month_AVR = new double[Year_list.Count, 12];
        double[,] Month_Sdv = new double[Year_list.Count, 12];

        for (i = 0; i < Year_list.Count; i++)
        {
            for (j = 0; j < 13; j++)
            {
                if (WL_Changes_4[i, j].Count != 0)
                {
                    foreach (WL_Change var in WL_Changes_4[i, j])
                    {
                        Month_AVR[i, (j - 1)] += var.Change_time / All_Count;
                    }
                    foreach (WL_Change var in WL_Changes_6[i, j])
                    {
                        Month_AVR[i, (j - 1)] += var.Change_time / All_Count;
                    }
                    foreach (WL_Change var in WL_Changes_4[i, j])
                    {
                        Month_Sdv[i, (j - 1)] += (var.Change_time - Avarange) * (var.Change_time - Avarange) / (All_Count - 1);
                    }
                    foreach (WL_Change var in WL_Changes_6[i, j])
                    {
                        Month_Sdv[i, (j - 1)] += (var.Change_time - Avarange) * (var.Change_time - Avarange) / (All_Count - 1);
                    }
                    Month_Sdv[i, (j - 1)] = Math.Sqrt(Month_Sdv[i, (j - 1)]);
                    Output += "" + Year_list[i] + " Senesi " + j + ". Ayı :\n";
                    Output += "" + "Ortalama : " + TimeSpan.FromSeconds(Month_AVR[i, (j - 1)]) + "  -->  Standart Sapma : " + TimeSpan.FromSeconds(Month_Sdv[i, (j - 1)]) + "\n";
                }
            }
            Output += "\n";

        }


        MessageBox.Show("" + Output);
    }


}



