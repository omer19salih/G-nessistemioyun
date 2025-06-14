using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Günessistemioyun
{
    public class BombaItem
    {
        public PictureBox PictureBox { get; set; }
        public Timer KacisTimer { get; set; }
    }

    public class bomba
    {
        private Timer bombaOlusturmaTimer;
        private List<BombaItem> bombalar = new List<BombaItem>();
        private Control parentControl;
        private Random random = new Random();
        private Action<int> puanAzaltCallback;

        public bomba(Control parent, Action<int> puanAzaltCallback)
        {
            this.parentControl = parent;
            this.puanAzaltCallback = puanAzaltCallback;

            // Bomba oluşturma
            bombaOlusturmaTimer = new Timer { Interval = 1000 };
            bombaOlusturmaTimer.Tick += BombaOlusturmaTimer_Tick;
            bombaOlusturmaTimer.Start();
        }

        private void BombaOlusturmaTimer_Tick(object sender, EventArgs e)
        {
            if (random.NextDouble() < 0.7)
            {
                PictureBox bombaPic = new PictureBox
                {
                    Image = Properties.Resources.bomba, // bomba.png olmalı Resources'a ekli
                    Size = new Size(50, 50),
                    Location = new Point(
                        random.Next(0, parentControl.Width - 50),
                        random.Next(0, parentControl.Height - 50)),
                    BackColor = Color.Transparent,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Visible = true
                };

                bombaPic.Click += Bomba_Click;

                Timer kacisTimer = new Timer { Interval = 1500 }; // 1.5 saniyede kaybolur
                kacisTimer.Tick += (s, args) =>
                {
                    kacisTimer.Stop();
                    var item = bombalar.FirstOrDefault(b => b.PictureBox == bombaPic);
                    if (item != null)
                    {
                        bombalar.Remove(item);
                        parentControl.Controls.Remove(item.PictureBox);
                        item.PictureBox.Dispose();
                    }
                };

                var bombaItem = new BombaItem
                {
                    PictureBox = bombaPic,
                    KacisTimer = kacisTimer
                };

                bombalar.Add(bombaItem);
                parentControl.Controls.Add(bombaPic);
                bombaPic.BringToFront();
                kacisTimer.Start();
            }
        }

        private void Bomba_Click(object sender, EventArgs e)
        {
            var bombaPic = sender as PictureBox;
            var bombaItem = bombalar.FirstOrDefault(b => b.PictureBox == bombaPic);

            if (bombaItem != null)
            {
                puanAzaltCallback?.Invoke(20); // Puan azaltılır
                bombaItem.KacisTimer.Stop();

                bombalar.Remove(bombaItem);
                parentControl.Controls.Remove(bombaItem.PictureBox);
                bombaItem.PictureBox.Dispose();
            }
        }

        public void Stop()
        {
            bombaOlusturmaTimer.Stop();

            foreach (var item in bombalar)
            {
                item.KacisTimer.Stop();
                parentControl.Controls.Remove(item.PictureBox);
                item.PictureBox.Dispose();
            }

            bombalar.Clear();
        }

        public void Start()
        {
            bombaOlusturmaTimer.Start();
        }
    }
}
