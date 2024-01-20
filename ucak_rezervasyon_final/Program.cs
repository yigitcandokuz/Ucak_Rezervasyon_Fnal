using ucak_rezervasyon_final;

class Program
{
    static List<Ucak> ucaklar = new List<Ucak>();
    static List<Lokasyon> lokasyonlar = new List<Lokasyon>();
    static List<Ucus> ucuslar = new List<Ucus>();
    static List<Rezervasyon> rezervasyonlar = new List<Rezervasyon>();
    static JsonHelper jsonHelper = new JsonHelper();

    static void Main(string[] args)
    {
        

        while (true)
        {
            Console.WriteLine("1. Uçuşları Listele");
            Console.WriteLine("2. Yeni Uçuş Ekle");
            Console.WriteLine("3. Rezervasyon Yap");
            Console.WriteLine("4. Kaydet ve Çıkış");

            Console.Write("Seçiminizi yapınız: ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    UcuslariListele();
                    break;
                case "2":
                    YeniUcusEkle();
                    break;
                case "3":
                    RezervasyonYap();
                    break;
                case "4":
                  
                    KaydetVeCikis(@"C:\Veri\veriler.json");
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }
        }
    }

    static void KaydetVeCikis(string dosyaAdi)
    {
        try
        {      
            string dizin = Path.GetDirectoryName(dosyaAdi);
            if (!Directory.Exists(dizin))
            {
                Directory.CreateDirectory(dizin);
            }

           
            var tumVeriler = new
            {
                Ucaklar = ucaklar,
                Lokasyonlar = lokasyonlar,
                Ucuslar = ucuslar,
                Rezervasyonlar = rezervasyonlar
            };

           
            jsonHelper.VeriyiJsonOlustur(tumVeriler, dosyaAdi);

            Console.WriteLine($"Veriler '{dosyaAdi}' dosyasına başarıyla kaydedildi.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Veri kaydetme hatası: {ex.Message}");
        }
    }




    static void UcuslariListele()
    {
        Console.WriteLine("Tüm Uçuşlar:");
        foreach (var ucus in ucuslar)
        {
            Console.WriteLine($"Kalkış: {ucus.KalkisYeri.Havaalani} - Varış: {ucus.VarisYeri.Havaalani} - Saat: {ucus.Saat}");
        }
    }

    static void YeniUcusEkle()
    {
        Console.WriteLine("Yeni Uçuş Ekle");

        
        Console.Write("Uçak Model: ");
        string ucakModel = Console.ReadLine();
        Console.Write("Uçak Marka: ");
        string ucakMarka = Console.ReadLine();
        Console.Write("Uçak Seri No: ");
        string ucakSeriNo = Console.ReadLine();
        Console.Write("Uçak Koltuk Kapasitesi: ");
        int ucakKoltukKapasitesi;
        while (!int.TryParse(Console.ReadLine(), out ucakKoltukKapasitesi) || ucakKoltukKapasitesi <= 0)
        {
            Console.WriteLine("Geçersiz giriş. Lütfen pozitif bir tam sayı girin.");
            Console.Write("Uçak Koltuk Kapasitesi: ");
        }

        Ucak yeniUcak = new Ucak
        {
            Model = ucakModel,
            Marka = ucakMarka,
            SeriNo = ucakSeriNo,
            KoltukKapasitesi = ucakKoltukKapasitesi
        };

       
        Console.Write("Kalkış Şehir: ");
        string kalkisHavaalani = Console.ReadLine();

        Console.Write("Varış Şehir: ");
        string varisHavaalani = Console.ReadLine();

        Lokasyon kalkisLokasyon = new Lokasyon
        {
            Havaalani = kalkisHavaalani,
            AktifPasif = true 
        };

        Lokasyon varisLokasyon = new Lokasyon
        {
            Havaalani = varisHavaalani,
            AktifPasif = true 
        };

        
        Console.Write("Uçuş Saati (örn: 2024-01-13T14:30): ");
        DateTime ucusSaat;
        while (!DateTime.TryParse(Console.ReadLine(), out ucusSaat))
        {
            Console.WriteLine("Geçersiz tarih formatı. Lütfen doğru bir tarih girin.");
            Console.Write("Uçuş Saati: ");
        }

        Ucus yeniUcus = new Ucus
        {
            KalkisYeri = kalkisLokasyon,
            VarisYeri = varisLokasyon,
            Saat = ucusSaat,
            UcakBilgisi = yeniUcak
        };

        ucuslar.Add(yeniUcus);

        Console.WriteLine("Yeni uçuş başarıyla eklendi!");

    }

    static void RezervasyonYap()
    {
        Console.WriteLine("Rezervasyon Yap");

       
        Console.WriteLine("Uçuşları Listele:");
        UcuslariListele();

        Console.Write("Rezervasyon yapmak istediğiniz uçuşun Saat değerini girin: ");
        string secilenUcusSaat = Console.ReadLine();

       
        Ucus secilenUcus = ucuslar.FirstOrDefault(u => u.Saat.ToString("yyyy-MM-ddTHH:mm") == secilenUcusSaat);

        if (secilenUcus == null)
        {
            Console.WriteLine("Belirtilen saatte bir uçuş bulunamadı.");
            return;
        }

       
        Console.Write("Ad: ");
        string ad = Console.ReadLine();
        Console.Write("Soyad: ");
        string soyad = Console.ReadLine();
        Console.Write("Yaş: ");
        int yas;
        while (!int.TryParse(Console.ReadLine(), out yas) || yas <= 0)
        {
            Console.WriteLine("Geçersiz giriş. Lütfen pozitif bir tam sayı girin.");
            Console.Write("Yaş: ");
        }

       
        if (secilenUcus.UcakBilgisi.KoltukKapasitesi <= secilenUcus.RezervasyonSayisi)
        {
            Console.WriteLine("Üzgünüz, uçak dolu. Rezervasyon yapılamıyor.");
            return;
        }

        
        Rezervasyon yeniRezervasyon = new Rezervasyon
        {
            UcusBilgisi = secilenUcus,
            Ad = ad,
            Soyad = soyad,
            Yas = yas
        };

        rezervasyonlar.Add(yeniRezervasyon);
        secilenUcus.RezervasyonSayisi++;

        Console.WriteLine("Rezervasyon başarıyla oluşturuldu!");
    }
}
