using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class JsonHelper
{
    public void VeriyiJsonOlustur<T>(T veri, string dosyaAdi)
    {
        try
        {
            string json = JsonConvert.SerializeObject(veri, Formatting.Indented);
            File.WriteAllText(dosyaAdi, json);
            Console.WriteLine($"Veri başarıyla '{dosyaAdi}' dosyasına kaydedildi.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Veri kaydetme hatası: {ex.Message}");
        }
    }

    public T JsonOku<T>(string dosyaAdi)
    {
        try
        {
            string json = File.ReadAllText(dosyaAdi);
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JSON okuma hatası: {ex.Message}");
            return default(T);
        }
    }
}
