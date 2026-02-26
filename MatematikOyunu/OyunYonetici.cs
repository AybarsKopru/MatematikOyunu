using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MatematikOyunu
{
    public static class OyunYonetici
    {
        public static OyunVerisi Veriler { get; private set; }
        private static readonly string dosyaYolu = "oyunverisi.txt";
        private static Random rastgele = new Random();

        static OyunYonetici()
        {
            VerileriYukle();
        }

        public static void OyunuKaydet(int sonrakiSeviye)
        {
            Veriler.KayitliOyunlar.Add(new KayitNoktasi { Seviye = sonrakiSeviye, Tarih = DateTime.Now });
            Veriler.KayitliOyunlar = Veriler.KayitliOyunlar.OrderByDescending(k => k.Tarih).Take(5).ToList();
            VerileriKaydet();
        }

        public static void SkorEkle(int puan, int seviye)
        {
            Veriler.SkorTablosu.Add(new SkorKaydi { Puan = puan, Seviye = seviye });
            Veriler.SkorTablosu = Veriler.SkorTablosu.OrderByDescending(s => s.Puan).Take(10).ToList();
            VerileriKaydet();
        }

        public static void VerileriKaydet()
        {
            try
            {
                List<string> satirlar = new List<string>();
                satirlar.Add("[KAYITLAR]");
                foreach (var kayit in Veriler.KayitliOyunlar)
                {
                    satirlar.Add($"{kayit.Seviye}|{kayit.Tarih:G}");
                }
                satirlar.Add("[SKORLAR]");
                foreach (var skor in Veriler.SkorTablosu)
                {
                    satirlar.Add($"{skor.Puan}|{skor.Seviye}");
                }
                File.WriteAllLines(dosyaYolu, satirlar);
            }
            catch (Exception ex) { MessageBox.Show("Veri kaydedilemedi: " + ex.Message); }
        }

        public static void VerileriYukle()
        {
            Veriler = new OyunVerisi();
            if (!File.Exists(dosyaYolu)) return;

            try
            {
                var satirlar = File.ReadAllLines(dosyaYolu);
                bool kayitOkumaModu = false, skorOkumaModu = false;
                foreach (var satir in satirlar)
                {
                    if (string.IsNullOrWhiteSpace(satir)) continue;
                    if (satir == "[KAYITLAR]") { kayitOkumaModu = true; skorOkumaModu = false; continue; }
                    if (satir == "[SKORLAR]") { skorOkumaModu = true; kayitOkumaModu = false; continue; }

                    if (kayitOkumaModu)
                    {
                        var p = satir.Split('|');
                        if (p.Length == 2 && int.TryParse(p[0], out int seviye) && DateTime.TryParse(p[1], out DateTime tarih))
                            Veriler.KayitliOyunlar.Add(new KayitNoktasi { Seviye = seviye, Tarih = tarih });
                    }
                    else if (skorOkumaModu)
                    {
                        var p = satir.Split('|');
                        if (p.Length == 2 && int.TryParse(p[0], out int puan) && int.TryParse(p[1], out int seviye))
                            Veriler.SkorTablosu.Add(new SkorKaydi { Puan = puan, Seviye = seviye });
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Veri okunamadı: " + ex.Message); }
        }

        public static int YildizHesapla(int dogruSayisi)
        {
            if (dogruSayisi >= 19) return 3;
            if (dogruSayisi >= 16) return 2;
            if (dogruSayisi >= 11) return 1;
            return 0;
        }

        public static List<Soru> SeviyeSorulariniUret(int seviye, int soruAdedi)
        {
            List<Soru> sorular = new List<Soru>();
            for (int i = 0; i < soruAdedi; i++)
            {
                sorular.Add(TekSoruUret(seviye));
            }
            return sorular;
        }

        private static Soru TekSoruUret(int seviye)
        {
            Soru soru = new Soru();
            int islemIndex;
            switch (seviye)
            {
                case 1: soru.Sayi1 = rastgele.Next(1, 21); soru.Sayi2 = rastgele.Next(1, 21); soru.Islem = (IslemTipi)rastgele.Next(0, 2); break;
                case 2: soru.Sayi1 = rastgele.Next(20, 101); soru.Sayi2 = rastgele.Next(20, 101); soru.Islem = (IslemTipi)rastgele.Next(0, 2); break;
                case 3: islemIndex = rastgele.Next(0, 3); soru.Islem = (IslemTipi)islemIndex; if (soru.Islem == IslemTipi.Carpma) { soru.Sayi1 = rastgele.Next(2, 11); soru.Sayi2 = rastgele.Next(2, 11); } else { soru.Sayi1 = rastgele.Next(1, 101); soru.Sayi2 = rastgele.Next(1, 101); } break;
                case 4: soru.Islem = rastgele.Next(0, 2) == 0 ? IslemTipi.Carpma : IslemTipi.Bolme; if (soru.Islem == IslemTipi.Carpma) { soru.Sayi1 = rastgele.Next(5, 16); soru.Sayi2 = rastgele.Next(5, 16); } else { int bolen = rastgele.Next(2, 11); int sonuc = rastgele.Next(2, 11); soru.Sayi1 = bolen * sonuc; soru.Sayi2 = bolen; } break;
                case 5: default: islemIndex = rastgele.Next(0, 4); soru.Islem = (IslemTipi)islemIndex; if (soru.Islem == IslemTipi.Bolme) { int bolen = rastgele.Next(2, 16); int sonuc = rastgele.Next(5, 21); soru.Sayi1 = bolen * sonuc; soru.Sayi2 = bolen; } else if (soru.Islem == IslemTipi.Carpma) { soru.Sayi1 = rastgele.Next(10, 21); soru.Sayi2 = rastgele.Next(10, 21); } else { soru.Sayi1 = rastgele.Next(100, 501); soru.Sayi2 = rastgele.Next(100, 501); } break;
            }
            if (soru.Islem == IslemTipi.Cikarma && soru.Sayi1 < soru.Sayi2) { int temp = soru.Sayi1; soru.Sayi1 = soru.Sayi2; soru.Sayi2 = temp; }
            else if (soru.Islem == IslemTipi.Bolme && (soru.Sayi2 == 0 || soru.Sayi1 % soru.Sayi2 != 0)) { int bolen = rastgele.Next(2, 11); int sonuc = rastgele.Next(2, 11); soru.Sayi1 = bolen * sonuc; soru.Sayi2 = bolen; }
            switch (soru.Islem)
            { 
                case IslemTipi.Toplama: soru.DogruCevap = soru.Sayi1 + soru.Sayi2; break;
                case IslemTipi.Cikarma: soru.DogruCevap = soru.Sayi1 - soru.Sayi2; break;
                case IslemTipi.Carpma: soru.DogruCevap = soru.Sayi1 * soru.Sayi2; break;
                case IslemTipi.Bolme: soru.DogruCevap = soru.Sayi1 / soru.Sayi2; break;
            }
            return soru;
        }

        public static string GetIslemSembolu(IslemTipi islem)
        {
            switch (islem)
            {
                case IslemTipi.Toplama: return "+";
                case IslemTipi.Cikarma: return "-";
                case IslemTipi.Carpma: return "x";
                case IslemTipi.Bolme: return "÷";
                default: return "?";
            }
        }
    }
}