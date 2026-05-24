using UnityEngine;

public class DusmanCikarici : MonoBehaviour
{
    [Header("Ayarlar")]
    public GameObject dusmanPrefab;
    public Transform[] noktalar; // Sahneye koyduğun o boş objeleri buraya sürükle
    public float uretimHizi = 2f;
    public float guvenliMesafe = 10f; // Oyuncu bu mesafeden yakınsa o noktada doğmasınlar
    public Transform oyuncu;

    void Start()
    {
        // Oyun başlar başlamaz "Uret" fonksiyonunu periyodik olarak çağırmaya başlar
        InvokeRepeating("Uret", 0f, uretimHizi);
    }

    void Uret()
    {
        // 1. KONTROL: Boss öldü mü? 
        // Eğer boss öldüyse fabrika paydos eder, aşağıdaki hiçbir kod çalışmaz.
        if (bossbeyni.bossOldu == true)
        {
            Debug.Log("Fabrika: Boss öldüğü için üretim durduruldu.");
            return; 
        }

        // 2. ADIM: Listeden rastgele bir nokta seç
        if (noktalar.Length == 0) return; // Eğer nokta eklemeyi unuttuysan hata vermesin
        
        int rastgeleIndis = Random.Range(0, noktalar.Length);
        Transform secilenNokta = noktalar[rastgeleIndis];

        // 3. ADIM: Mesafe Kontrolü
        // Seçtiğimiz nokta oyuncunun dibinde mi?
        float mesafe = Vector2.Distance(secilenNokta.position, oyuncu.position);

        if (mesafe > guvenliMesafe)
        {
            // Mesafe güvenliyse düşmanı o noktada oluştur
            Instantiate(dusmanPrefab, secilenNokta.position, Quaternion.identity);
        }
        else
        {
            // Eğer seçilen nokta oyuncuya çok yakınsa, bu turda kimseyi çıkarma
            // (Bir sonraki uretimHizi saniyesinde şansımızı tekrar deneyeceğiz)
            Debug.Log("Seçilen nokta güvenli değil, üretim pas geçildi.");
        }
    }
}