using System;
using System.Collections.Generic;

namespace MatematikOyunu
{
    public class KayitNoktasi
    {
        public int Seviye { get; set; }
        public DateTime Tarih { get; set; }
    }

    public class SkorKaydi
    {
        public int Puan { get; set; }
        public int Seviye { get; set; }
    }

    public class OyunVerisi
    {
        public List<KayitNoktasi> KayitliOyunlar { get; set; }
        public List<SkorKaydi> SkorTablosu { get; set; }

        public OyunVerisi()
        {
            KayitliOyunlar = new List<KayitNoktasi>();
            SkorTablosu = new List<SkorKaydi>();
        }
    }
}