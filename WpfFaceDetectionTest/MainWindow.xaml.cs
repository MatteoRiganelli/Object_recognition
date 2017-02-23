using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;

using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections; //per ArrayList
using System.Collections.Generic;

namespace WpfFaceDetectionTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {

        private Capture capture;
        private CascadeClassifier cascade_bicchiere;
        private CascadeClassifier cascade_latte;
        private CascadeClassifier cascade_tazza;
        private CascadeClassifier cascade_banana;
        private CascadeClassifier cascade_tazzaGialla;
        DispatcherTimer timer;
        double framenumber = 0; // per contare il numero dei frame

        //per gestione coordinate e altro riguardante gli oggetti nella scaena - vedi classe Oggetto() per maggiorni informazioni
        //---------------------------------
        Oggetto bicchiere = new Oggetto();
        Oggetto latte = new Oggetto();
        Oggetto tazza = new Oggetto();
        Oggetto banana = new Oggetto();
        Oggetto tazzaGialla = new Oggetto();
        //----------------------------------
        //CONTA OCCORRENZE OGGETTI in ogni Frame
        //----------------------------------
        int contaBicchieri = 0;
        int contaLatte = 0;
        int contaTazza = 0;
        int contaBanana = 0;
        int contaTazzaGialla = 0;
        //----------------------------------
        List<int> listBicc = null;
        List<int> listlatte = null;
        List<int> listTazza = null;
        List<int> listBanana = null;
        List<int> listTazzaGialla = null;

        private Font font = new Font("Arial", 12, System.Drawing.FontStyle.Bold); //creates new font


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            capture = new Capture();

            listBicc = new List<int>();
            listlatte = new List<int>();
            listTazza = new List<int>();
            listBanana = new List<int>();
            listTazzaGialla = new List<int>();

            //inizializzazione dei classificatori
            //-------------------------------------------------------------------
            cascade_bicchiere = new CascadeClassifier(@"cascade/bicchiere.xml"); 
            cascade_latte = new CascadeClassifier(@"cascade/latte.xml");
            cascade_tazza = new CascadeClassifier(@"cascade/tazza.xml");
            cascade_banana = new CascadeClassifier(@"cascade/banana.xml");
            cascade_tazzaGialla = new CascadeClassifier(@"cascade/tazzaGialla.xml");   
            //------------------------------------------------------------------- 

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            // incremento il numero di frame +=1
            framenumber += capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_POS_FRAMES);

            // ottengo il frame corrente
            Image<Bgr, Byte> currentFrame = capture.QueryFrame();
            textBox.FontSize = 10;

            if (currentFrame != null)
            {

                Image<Gray, Byte> grayFrame = currentFrame.Convert<Gray, Byte>();
                //frame ruotato di 45 gradi
                Image<Gray, Byte> grayFrame45 = currentFrame.Rotate(45.0, new Emgu.CV.Structure.Bgr(System.Drawing.Color.Gray)).Convert<Gray, Byte>();
                Image<Gray, Byte> grayFrameLess45 = currentFrame.Rotate(-45.0, new Emgu.CV.Structure.Bgr(System.Drawing.Color.Gray)).Convert<Gray, Byte>();
                //-----------------------------------------------------------------------
                System.Drawing.Rectangle[] provaBicc = cascade_bicchiere.DetectMultiScale(grayFrame, 1.1, 5, new System.Drawing.Size(100, 100), System.Drawing.Size.Empty);
                System.Drawing.Rectangle[] provaBicc45 = cascade_bicchiere.DetectMultiScale(grayFrame45, 1.1, 5, new System.Drawing.Size(100, 100), System.Drawing.Size.Empty);
                System.Drawing.Rectangle[] provaBiccLess45 = cascade_bicchiere.DetectMultiScale(grayFrameLess45, 1.1, 5, new System.Drawing.Size(100, 100), System.Drawing.Size.Empty);

                System.Drawing.Rectangle[] provaLatte = cascade_latte.DetectMultiScale(grayFrame, 1.1, 5, new System.Drawing.Size(100, 100), System.Drawing.Size.Empty);
                System.Drawing.Rectangle[] provaTazza = cascade_tazza.DetectMultiScale(grayFrame, 1.3, 7, new System.Drawing.Size(105, 150), System.Drawing.Size.Empty);
                System.Drawing.Rectangle[] provaBanana = cascade_banana.DetectMultiScale(grayFrame, 1.5, 10, new System.Drawing.Size(100, 100), new System.Drawing.Size(400, 230));
                System.Drawing.Rectangle[] provaTazzaGialla = cascade_tazzaGialla.DetectMultiScale(grayFrame, 1.1, 3, new System.Drawing.Size(100, 100), System.Drawing.Size.Empty);
                //-----------------------------------------------------------------------
                Console.Write(provaBicc.Length.ToString());

                //valore ultimo numero del frame number, es: 12345 return 5
                int ultimoNumeroFrame = (int)(Math.Abs((framenumber % 10)));
                ultimoNumeroFrame = Math.Abs(ultimoNumeroFrame - 1);

                //BICCHIERE PLASTICA
                if (provaBicc.Length == 1 && bicchiere.get_Visible() == true)
                {
                    //incrementa occorrenza
                    if (contaBicchieri < 10)
                    {
                        contaBicchieri += 1;
                        listBicc.Add(1);
                        //int remove = Math.Min(list.Count, 1);
                        if (framenumber > 10)
                            listBicc.RemoveAt(0);
                        //list.RemoveRange(0, remove);

                    }
                    foreach (int i in listBicc)
                    {
                        textBox_List.AppendText(i.ToString());
                        if (textBox_List.Text.Length % 10 == 0)
                            textBox_List.AppendText(" - BICCHIERE\n");
                        textBox_List.ScrollToEnd();
                    }
                    textBox1_occurrencesBicc.Text = contaBicchieri.ToString();

                    if (textBox.Text != "") textBox.AppendText(Environment.NewLine);
                    currentFrame.Draw(provaBicc[0], new Bgr(System.Drawing.Color.Red), 2);
                    int X = provaBicc[0].X + (int)(provaBicc[0].Width / 2);
                    int Y = provaBicc[0].Y + (int)(provaBicc[0].Height / 2);
                    textBox.AppendText("PLASTIC_WHITE_GLASS. :\n " + provaBicc[0].Size.ToString() + " X: " + X + " Y: " + Y);

                    Graphics g = Graphics.FromImage(currentFrame.Bitmap);
                    String text = "PLASTIC\nWHITE_GLASS";
                    int tWidth = (int)g.MeasureString(text, font).Width;
                    int x;
                    if (tWidth >= provaBicc[0].Width)
                        x = provaBicc[0].Left - ((tWidth - provaBicc[0].Width) / 2);
                    else
                        x = (provaBicc[0].Width / 2) - (tWidth / 2) + provaBicc[0].Left;

                    g.DrawString(text, font, Brushes.White, new PointF(x, provaBicc[0].Top - 36));
                    //CONTROLLO CONFIDENZIALITA BICCHIERI
                    if (listBicc.Count >= 6)
                    {
                        Graphics g1 = Graphics.FromImage(currentFrame.Bitmap);
                        String quantiBicchieri = contaBicchieri.ToString();
                        String quantiNellaLista = (listBicc.Count - 1).ToString();

                        Console.Beep();

                        textBox1_nameObj.Text = "BICCHIERE";

                        g1.DrawString(quantiNellaLista, font, Brushes.Black, new PointF(X, Y));
                    }
                }
                else
                {
                    listBicc.Clear();
                    contaBicchieri -= 1;
                    listBicc.Add(0);
                    textBox1_occurrencesBicc.Text = contaBicchieri.ToString();
                    if (contaBicchieri <= 0)
                    {
                        contaBicchieri = 0;
                        if (textBox.Text != "") textBox.AppendText(Environment.NewLine);

                    }

                }

                // BICCHIERE PLASTICA RUOTATO 45 GRADI
                if (provaBicc45.Length == 1 && bicchiere.get_Visible() == true)
                {
                    if (textBox.Text != "") textBox.AppendText(Environment.NewLine);

                    textBox.AppendText("PLASTIC_WHITE_GLASS_45. :\n RUOTATO\n");

                    textBox1_nameObj.Text = "RUOTATO45";
                }

                // BICCHIERE PLASTICA RUOTATO -45 GRADI
                if (provaBiccLess45.Length == 1 && bicchiere.get_Visible() == true)
                {
                    if (textBox.Text != "") textBox.AppendText(Environment.NewLine);

                    textBox.AppendText("PLASTIC_WHITE_GLASS-45. :\n RUOTATO\n");
                    textBox1_nameObj.Text = "RUOTATO-45";
                }

                //SCATOLA LATTE
                if (provaLatte.Length == 1 && latte.get_Visible() == true)
                {
                    //incrementa occorrenza
                    if (contaLatte < 10)
                    {
                        contaLatte += 1;
                        listlatte.Add(1);
                        //int remove = Math.Min(list.Count, 1);
                        if (framenumber > 10)
                            listlatte.RemoveAt(0);
                        //list.RemoveRange(0, remove);

                    }
                    foreach (int i in listlatte)
                    {
                        textBox_List.AppendText(i.ToString());
                        if (textBox_List.Text.Length % 10 == 0)
                            textBox_List.AppendText("\n"); //LATTE
                        textBox_List.ScrollToEnd();
                    }
                    textBox1_occurrencesLatte.Text = contaLatte.ToString();


                    if (textBox.Text != "") textBox.AppendText(Environment.NewLine);
                    currentFrame.Draw(provaLatte[0], new Bgr(System.Drawing.Color.Cyan), 2);
                    int X = provaLatte[0].X + (int)(provaLatte[0].Width / 2);
                    int Y = provaLatte[0].Y + (int)(provaLatte[0].Height / 2);
                    textBox.AppendText("MILK. :\n " + provaLatte[0].Size.ToString() + " X: " + X + " Y: " + Y);

                    Graphics g = Graphics.FromImage(currentFrame.Bitmap);
                    String text = "MILK_CARTON";
                    int tWidth = (int)g.MeasureString(text, font).Width;
                    int x;
                    if (tWidth >= provaLatte[0].Width)
                        x = provaLatte[0].Left - ((tWidth - provaLatte[0].Width) / 2);
                    else
                        x = (provaLatte[0].Width / 2) - (tWidth / 2) + provaLatte[0].Left;

                    g.DrawString(text, font, Brushes.White, new PointF(x, provaLatte[0].Top - 41));
                    //CONTROLLO CONFIDENZIALITA LATTE
                    if (listlatte.Count >= 4)
                    {
                        Graphics g1 = Graphics.FromImage(currentFrame.Bitmap);
                        String quantoLatte = contaLatte.ToString();

                        Console.Beep();
                        textBox1_nameObj.Text = "LATTE";

                        g1.DrawString(quantoLatte, font, Brushes.Black, new PointF(X, Y));

                    }
                }
                else
                {
                    listlatte.Clear();
                    contaLatte -= 1;
                    listlatte.Add(0);
                    textBox1_occurrencesLatte.Text = contaLatte.ToString();
                    if (contaLatte <= 0)
                    {
                        contaLatte = 0;
                        if (textBox.Text != "") textBox.AppendText(Environment.NewLine);

                    }

                }

                //TAZZA BLU
                if (provaTazza.Length == 1 && tazza.get_Visible() == true)
                {
                    //incrementa occorrenza
                    if (contaTazza < 10)
                    {
                        contaTazza += 1;
                        listTazza.Add(1);
                        //int remove = Math.Min(list.Count, 1);
                        if (framenumber > 10)
                            listTazza.RemoveAt(0);
                        //list.RemoveRange(0, remove);

                    }
                    foreach (int i in listTazza)
                    {
                        textBox_List.AppendText(i.ToString());
                        if (textBox_List.Text.Length % 10 == 0)
                            textBox_List.AppendText("\n"); //TAZZA
                        textBox_List.ScrollToEnd();
                    }
                    textBox1_occurrencesTazza.Text = contaTazza.ToString();

                    if (textBox.Text != "") textBox.AppendText(Environment.NewLine);
                    currentFrame.Draw(provaTazza[0], new Bgr(System.Drawing.Color.Blue), 2);
                    int X = provaTazza[0].X + (int)(provaTazza[0].Width / 2);
                    int Y = provaTazza[0].Y + (int)(provaTazza[0].Height / 2);
                    textBox.AppendText("BLUE_CUP. :\n " + provaTazza[0].Size.ToString() + " X: " + X + " Y: " + Y);

                    Graphics g = Graphics.FromImage(currentFrame.Bitmap);
                    String text = "BLUE_CUP";
                    int tWidth = (int)g.MeasureString(text, font).Width;
                    int x;
                    if (tWidth >= provaTazza[0].Width)
                        x = provaTazza[0].Left - ((tWidth - provaTazza[0].Width) / 2);
                    else
                        x = (provaTazza[0].Width / 2) - (tWidth / 2) + provaTazza[0].Left;

                    g.DrawString(text, font, Brushes.White, new PointF(x, provaTazza[0].Top - 18));
                    //CONTROLLO CONFIDENZIALITA TAZZA
                    if (listTazza.Count >= 3)
                    {
                        Graphics g2 = Graphics.FromImage(currentFrame.Bitmap);
                        String quantoTazza = contaTazza.ToString();

                        Console.Beep();
                        textBox1_nameObj.Text = "TAZZA";

                        g2.DrawString(quantoTazza, font, Brushes.Black, new PointF(X, Y));

                    }
                }
                else
                {
                    listTazza.Clear();
                    contaTazza -= 1;
                    listTazza.Add(0);
                    textBox1_occurrencesTazza.Text = contaTazza.ToString();
                    if (contaTazza <= 0)
                    {
                        contaTazza = 0;
                        if (textBox.Text != "") textBox.AppendText(Environment.NewLine);

                    }

                }

                //BANANA               
                if (provaBanana.Length == 1 && banana.get_Visible() == true)
                {
                    //incrementa occorrenza
                    if (contaBanana < 10)
                    {
                        contaBanana += 1;
                        listBanana.Add(1);
                        //int remove = Math.Min(list.Count, 1);
                        if (framenumber > 10)
                            listBanana.RemoveAt(0);
                        //list.RemoveRange(0, remove);

                    }
                    foreach (int i in listBanana)
                    {
                        textBox_List.AppendText(i.ToString());
                        if (textBox_List.Text.Length % 10 == 0)
                            textBox_List.AppendText("\n"); //BANANA
                        textBox_List.ScrollToEnd();
                    }
                    textBox1_occurrencesBanana.Text = contaBanana.ToString();

                    if (textBox.Text != "") textBox.AppendText(Environment.NewLine);
                    currentFrame.Draw(provaBanana[0], new Bgr(System.Drawing.Color.Yellow), 2);
                    int X = provaBanana[0].X + (int)(provaBanana[0].Width / 2);
                    int Y = provaBanana[0].Y + (int)(provaBanana[0].Height / 2);
                    textBox.AppendText("BANANA. : " + provaBanana[0].Size.ToString() + " X: " + X + " Y: " + Y);

                    Graphics g = Graphics.FromImage(currentFrame.Bitmap);
                    String text = "BANANA";
                    int tWidth = (int)g.MeasureString(text, font).Width;
                    int x;
                    if (tWidth >= provaBanana[0].Width)
                        x = provaBanana[0].Left - ((tWidth - provaBanana[0].Width) / 2);
                    else
                        x = (provaBanana[0].Width / 2) - (tWidth / 2) + provaBanana[0].Left;

                    g.DrawString(text, font, Brushes.OrangeRed, new PointF(x, provaBanana[0].Top - 18));
                    //CONTROLLO CONFIDENZIALITA BANANA
                    if (contaBanana >= 6)
                    {
                        Graphics g3 = Graphics.FromImage(currentFrame.Bitmap);
                        String quantoBanana = contaBanana.ToString();

                        Console.Beep();
                        textBox1_nameObj.Text = "BANANA";

                        g3.DrawString(quantoBanana, font, Brushes.Black, new PointF(X, Y));


                    }
                }
                else
                {
                    listBanana.Clear();
                    contaBanana -= 1;
                    listBanana.Add(0);
                    textBox1_occurrencesBanana.Text = contaBanana.ToString();
                    if (contaBanana <= 0)
                    {
                        contaBanana = 0;
                        if (textBox.Text != "") textBox.AppendText(Environment.NewLine);

                    }

                }

                //TAZZA GIALLA
                if (provaTazzaGialla.Length > 0 && tazzaGialla.get_Visible() == true)
                {
                    //incrementa occorrenza
                    if (contaTazzaGialla < 10)
                    {
                        contaTazzaGialla += 1;
                        listTazzaGialla.Add(1);
                        //int remove = Math.Min(list.Count, 1);
                        if (framenumber > 10)
                            listTazzaGialla.RemoveAt(0);
                        //list.RemoveRange(0, remove);

                    }
                    foreach (int i in listTazzaGialla)
                    {
                        textBox_List.AppendText(i.ToString());
                        if (textBox_List.Text.Length % 10 == 0)
                            textBox_List.AppendText("\n"); //TAZZA GIALLA
                        textBox_List.ScrollToEnd();
                    }
                    textBox1_occurrencesTazzaGialla.Text = contaTazzaGialla.ToString();

                    if (textBox.Text != "") textBox.AppendText(Environment.NewLine);
                    currentFrame.Draw(provaTazzaGialla[0], new Bgr(System.Drawing.Color.Orange), 2);
                    int X = provaTazzaGialla[0].X + (int)(provaTazzaGialla[0].Width / 2);
                    int Y = provaTazzaGialla[0].Y + (int)(provaTazzaGialla[0].Height / 2);
                    textBox.AppendText("TAZZA_GIALLA :\n " + provaTazzaGialla[0].Size.ToString() + " X: " + X + " Y: " + Y);

                    Graphics g = Graphics.FromImage(currentFrame.Bitmap);
                    String text = "YELLOW_CUP";
                    int tWidth = (int)g.MeasureString(text, font).Width;
                    int x;
                    if (tWidth >= provaTazzaGialla[0].Width)
                        x = provaTazzaGialla[0].Left - ((tWidth - provaTazzaGialla[0].Width) / 2);
                    else
                        x = (provaTazzaGialla[0].Width / 2) - (tWidth / 2) + provaTazzaGialla[0].Left;

                    g.DrawString(text, font, Brushes.White, new PointF(x, provaTazzaGialla[0].Top - 18));
                    //CONTROLLO CONFIDENZIALITA TAZZA GIALLA
                    if (listTazzaGialla.Count >= 3)
                    {
                        Graphics g4 = Graphics.FromImage(currentFrame.Bitmap);
                        String quantoTazzaGialla = contaTazzaGialla.ToString();

                        Console.Beep();
                        textBox1_nameObj.Text = "TAZZA_G";

                        g4.DrawString(quantoTazzaGialla, font, Brushes.Black, new PointF(X, Y));

                    }
                }
                else
                {
                    listTazzaGialla.Clear();
                    contaTazzaGialla -= 1;
                    listTazzaGialla.Add(0);
                    textBox1_occurrencesTazzaGialla.Text = contaTazzaGialla.ToString();
                    if (contaTazzaGialla <= 0)
                    {
                        contaTazzaGialla = 0;
                        if (textBox.Text != "") textBox.AppendText(Environment.NewLine);

                    }

                }

                if (framenumber % 10 == 0) //ogni 15 frame
                    textBox1.Text = framenumber.ToString();

                textBox.ScrollToEnd();
                image1.Source = ToBitmapSource(currentFrame);

            }
        }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        public static BitmapSource ToBitmapSource(IImage image)
        {
            using (System.Drawing.Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }

        private void CupPLasticWhite_Click(object sender, System.EventArgs e)
        {
            //MenuItem mi = sender as MenuItem;
            if (bicchiere.get_Visible() == true)
                bicchiere.set_visible_false();
            else
                bicchiere.set_visible_true();
            //MessageBox.Show(mi.IsChecked.ToString());

        }

        private void Milk_Click(object sender, System.EventArgs e)
        {
            if (latte.get_Visible() == true)
                latte.set_visible_false();
            else
                latte.set_visible_true();

        }

        private void Tazza_Click(object sender, System.EventArgs e)
        {
            if (tazza.get_Visible() == true)
                tazza.set_visible_false();
            else
                tazza.set_visible_true();

        }
        private void Banana_Click(object sender, System.EventArgs e)
        {
            if (banana.get_Visible() == true)
                banana.set_visible_false();
            else
                banana.set_visible_true();

        }
        private void TazzaGialla_Click(object sender, System.EventArgs e)
        {
            if (tazzaGialla.get_Visible() == true)
                tazzaGialla.set_visible_false();
            else
                tazzaGialla.set_visible_true();

        }
    }
}

    

