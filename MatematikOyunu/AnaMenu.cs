using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MatematikOyunu
{
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
        }

        private void AnaMenu_Load(object sender, EventArgs e)
        {
            ArayuzuGuncelle();
        }

        private void ArayuzuGuncelle()
        {   //skorları büyükten küçüğe sıralar
            var enIyiSkorlar = OyunYonetici.Veriler.SkorTablosu.OrderByDescending(s => s.Puan).Take(3).ToList();
            lblSkorBirinci.Text = enIyiSkorlar.Count > 0 ? $"1. {enIyiSkorlar[0].Puan} Puan (Seviye {enIyiSkorlar[0].Seviye})" : "1. -";
            lblSkorIkinci.Text = enIyiSkorlar.Count > 1 ? $"2. {enIyiSkorlar[1].Puan} Puan (Seviye {enIyiSkorlar[1].Seviye})" : "2. -";
            lblSkorUcuncu.Text = enIyiSkorlar.Count > 2 ? $"3. {enIyiSkorlar[2].Puan} Puan (Seviye {enIyiSkorlar[2].Seviye})" : "3. -";

            btnDevam.Enabled = OyunYonetici.Veriler.KayitliOyunlar.Any();
        }

        private void btnYeniOyun_Click(object sender, EventArgs e)
        {
            Oyna(1);
        }

        private void btnDevam_Click(object sender, EventArgs e)
        {
            var kayitlar = OyunYonetici.Veriler.KayitliOyunlar;
            if (!kayitlar.Any())
            {
                MessageBox.Show("Devam edilecek bir oyun bulunamadı.", "Bilgi");
                return;
            }

            string liste = "Devam etmek istediğiniz oyunu seçin:\n\n";
            for (int i = 0; i < kayitlar.Count; i++)
            {
                liste += $"{i + 1}. Seviye {kayitlar[i].Seviye} ({kayitlar[i].Tarih:g})\n";
            }

            string secimStr = Microsoft.VisualBasic.Interaction.InputBox(liste, "Kayıtlı Oyun Seç", "1");
            if (int.TryParse(secimStr, out int secim) && secim > 0 && secim <= kayitlar.Count)
            {
                int baslanacakSeviye = kayitlar[secim - 1].Seviye;
                Oyna(baslanacakSeviye);
            }
        }

        private void Oyna(int baslangicSeviyesi)
        {
            int mevcutSeviye = baslangicSeviyesi;
            while (mevcutSeviye <= 5)
            {
                this.Hide();
                using (OyunFormu oyunFormu = new OyunFormu(mevcutSeviye))
                {
                    oyunFormu.ShowDialog();
                    ArayuzuGuncelle();
                    this.Show();

                    if (oyunFormu.SeviyeGecildi)
                    {
                        mevcutSeviye++;
                        if (mevcutSeviye > 5)
                        {
                            MessageBox.Show("Tebrikler! Oyunu tamamladınız!", "Oyun bitti");
                            break;
                        }
                        else
                        {
                            var devamMi = MessageBox.Show($"Tebrikler, {mevcutSeviye}. seviyeye geçtiniz! Devam etmek istiyor musunuz?", "Seviye Atlandı", MessageBoxButtons.YesNo);
                            if (devamMi == DialogResult.No) break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void SkorDetaylariGoster(object sender, EventArgs e)
        {
            if (!OyunYonetici.Veriler.SkorTablosu.Any())
            {
                MessageBox.Show("Henüz skor kaydedilmemiş.", "Skor Tablosu");
                return;
            }
            string detaylar = "TÜM SKORLAR\n\n";
            var siraliSkorlar = OyunYonetici.Veriler.SkorTablosu;
            foreach (var skor in siraliSkorlar)
            {
                detaylar += $"{skor.Puan} Puan (Seviye {skor.Seviye})\n";
            }
            MessageBox.Show(detaylar, "Detaylı Skor Tablosu");
        }
    }
}