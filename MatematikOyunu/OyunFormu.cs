using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MatematikOyunu
{
    public partial class OyunFormu : Form
    {
        private List<Label> soruEtiketleri = new List<Label>();
        private List<TextBox> cevapKutulari = new List<TextBox>();
        private List<CheckBox> pasKutulari = new List<CheckBox>();
        private List<GroupBox> soruGruplari = new List<GroupBox>();

        private List<Soru> seviyeSorulari;
        private List<Soru> pasGecilenSorular = new List<Soru>();

        private int mevcutSeviye;
        private int mevcutBlok = 1;
        private int toplamPuan = 0;
        private int kalanSure = 300;

        private bool pasModu = false;
        private int pasModuMevcutSayfa = 0;
        public bool SeviyeGecildi { get; private set; } = false;

        public OyunFormu(int seviye)
        {
            InitializeComponent();
            this.mevcutSeviye = seviye;
        }

        private void OyunFormu_Load(object sender, EventArgs e)
        {
            KontrolleriListelereYukle();
            seviyeSorulari = OyunYonetici.SeviyeSorulariniUret(mevcutSeviye, 20);
            MevcutBlokSorulariniGoster();
            timer1.Start();
        }

        private void KontrolleriListelereYukle()
        {
            soruGruplari.AddRange(new GroupBox[] { gbSoru1, groupBox1, groupBox2, groupBox3, groupBox4 });
            soruEtiketleri.AddRange(new Label[] { lblSoru1, label1, label2, label3, label4 });
            cevapKutulari.AddRange(new TextBox[] { txtSoru1, textBox1, textBox2, textBox3, textBox4 });
            pasKutulari.AddRange(new CheckBox[] { rbPas, radioButton1, radioButton2, radioButton3, radioButton4 });

            for (int i = 0; i < 5; i++)
            {
                cevapKutulari[i].KeyPress += SadeceSayiGirisi;
                cevapKutulari[i].TextChanged += CevapKutusu_TextChanged;
                pasKutulari[i].CheckedChanged += PasKutusu_CheckedChanged;
            }
        }

        private void PasKutusu_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox seciliPasKutusu = sender as CheckBox;
            int index = pasKutulari.IndexOf(seciliPasKutusu);

            if (index != -1)
            {
                cevapKutulari[index].Enabled = !seciliPasKutusu.Checked;
                if (seciliPasKutusu.Checked)
                {
                    cevapKutulari[index].Clear();
                }
            }
        }

        private void CevapKutusu_TextChanged(object sender, EventArgs e)
        {
            TextBox degisenKutu = sender as TextBox;
            int index = cevapKutulari.IndexOf(degisenKutu);

            if (index != -1)
            {
                pasKutulari[index].Enabled = string.IsNullOrEmpty(degisenKutu.Text);
            }
        }

        private void MevcutBlokSorulariniGoster()
        {
            this.Text = $"Seviye {mevcutSeviye}";
            lblBlok.Text = pasModu ? $"PAS GEÇİLENLER (Sayfa {pasModuMevcutSayfa + 1})" : $"BLOK: {mevcutBlok}";

            List<Soru> gosterilecekSorular = pasModu
                ? pasGecilenSorular.Skip(pasModuMevcutSayfa * 5).Take(5).ToList()
                : seviyeSorulari.Skip((mevcutBlok - 1) * 5).Take(5).ToList();

            for (int i = 0; i < 5; i++)
            {
                if (i < gosterilecekSorular.Count)
                {
                    soruEtiketleri[i].Text = $"{gosterilecekSorular[i].Sayi1} {OyunYonetici.GetIslemSembolu(gosterilecekSorular[i].Islem)} {gosterilecekSorular[i].Sayi2} = ?";
                    cevapKutulari[i].Clear();
                    cevapKutulari[i].Enabled = true;
                    pasKutulari[i].Checked = false;
                    pasKutulari[i].Enabled = true;
                    soruGruplari[i].Visible = true;
                }
                else
                {
                    soruGruplari[i].Visible = false;
                }
            }
        }

        private void btnSonraki_Click(object sender, EventArgs e)
        {
            if (!MevcutBlokTamamlandiMi())
            {
                MessageBox.Show("Lütfen devam etmeden önce bu bloktaki tüm soruları cevaplayın veya pas geçin.", "Eksik Cevap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CevaplariDegerlendir();

            if (pasModu)
            {
                int toplamPasSayfasi = (pasGecilenSorular.Count + 4) / 5;
                if (pasModuMevcutSayfa < toplamPasSayfasi - 1)
                {
                    pasModuMevcutSayfa++;
                    MevcutBlokSorulariniGoster();
                }
                else
                {
                    OyunuBitir();
                }
                return;
            }

            if (mevcutBlok < 4)
            {
                mevcutBlok++;
                MevcutBlokSorulariniGoster();
                if (mevcutBlok == 4) btnSonraki.Text = "Bitir";
            }
            else
            {
                if (pasGecilenSorular.Any())
                {
                    pasModu = true;
                    int toplamPasSayfasi = (pasGecilenSorular.Count + 4) / 5;
                    btnSonraki.Text = toplamPasSayfasi > 1 ? "Sonraki Sayfa" : "Oyunu Bitir";

                    MevcutBlokSorulariniGoster();
                }
                else
                {
                    OyunuBitir();
                }
            }
        }

        private bool MevcutBlokTamamlandiMi()
        {
            List<Soru> gosterilenSorular = pasModu
                ? pasGecilenSorular.Skip(pasModuMevcutSayfa * 5).Take(5).ToList()
                : seviyeSorulari.Skip((mevcutBlok - 1) * 5).Take(5).ToList();

            for (int i = 0; i < gosterilenSorular.Count; i++)
            {
                bool cevapVerilmis = !string.IsNullOrWhiteSpace(cevapKutulari[i].Text);
                bool pasGecilmis = pasKutulari[i].Checked;
                if (!cevapVerilmis && !pasGecilmis)
                {
                    return false;
                }
            }
            return true;
        }

        private void CevaplariDegerlendir()
        {
            List<Soru> degerlendirilecekSorular = pasModu
                ? pasGecilenSorular.Skip(pasModuMevcutSayfa * 5).Take(5).ToList()
                : seviyeSorulari.Skip((mevcutBlok - 1) * 5).Take(5).ToList();

            for (int i = 0; i < degerlendirilecekSorular.Count; i++)
            {
                if (pasKutulari[i].Checked)
                {
                    if (!pasModu)
                    {
                        if (!pasGecilenSorular.Contains(degerlendirilecekSorular[i]))
                        {
                            pasGecilenSorular.Add(degerlendirilecekSorular[i]);
                        }
                    }
                }
                else if (int.TryParse(cevapKutulari[i].Text, out int cevap))
                {
                    if (cevap == degerlendirilecekSorular[i].DogruCevap)
                    {
                        toplamPuan += 5;
                    }
                }
            }
        }

        private void OyunuBitir()
        {
            timer1.Stop();
            int dogruSayisi = toplamPuan / 5;
            SeviyeGecildi = dogruSayisi >= 11;
            int yildizSayisi = OyunYonetici.YildizHesapla(dogruSayisi);

            string mesaj = $"Seviye {mevcutSeviye} Bitti!\nToplam Puan: {toplamPuan} ({dogruSayisi}/20 doğru)\n\n";

            if (SeviyeGecildi)
            {
                mesaj += $"Tebrikler! Seviyeyi {yildizSayisi} yıldız ile geçtiniz.";
                OyunYonetici.SkorEkle(toplamPuan, mevcutSeviye);
                if (mevcutSeviye < 5)
                {
                    OyunYonetici.OyunuKaydet(mevcutSeviye + 1);
                }
            }
            else
            {
                mesaj += "Maalesef, yeterli doğru sayısına ulaşamadınız.";
            }

            MessageBox.Show(mesaj, "Seviye Sonu");
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            kalanSure--;
            lblKalanSure.Text = $"SÜRE: {(kalanSure / 60):D2}:{(kalanSure % 60):D2}";
            if (kalanSure <= 0)
            {
                timer1.Stop();
                MessageBox.Show("Süre doldu!", "Oyun Bitti");
                OyunuBitir();
            }
        }

        private void SadeceSayiGirisi(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
                e.Handled = true;
        }
    }
}