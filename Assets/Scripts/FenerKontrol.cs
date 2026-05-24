using UnityEngine;

public class FenerKontrol : MonoBehaviour
{
    private Camera anaKamera;

    [Header("Fener Ayarları")]
    [Tooltip("Açıp kapatacağınız Işık Objesini buraya sürükleyin")]
    public GameObject fenerIsigi; // Işık veren objemizi Unity'den buraya tanımlayacağız

    void Start()
    {

        anaKamera = Camera.main;
        // Oyun başladığı anda fener Işığı tanımlanmışsa, onu otomatik olarak kapatır.
        if (fenerIsigi != null)
        {
            fenerIsigi.SetActive(false); // false kapatmak, true açmak demektir
        }


        anaKamera = Camera.main;
    }

    void Update()
    {
       


        // 2. KISIM: FENERİ AÇIP KAPATMA (Yeni eklediğimiz kısım)
        // Eğer oyuncu 'F' tuşuna basarsa:
        if (Input.GetKeyDown(KeyCode.F))
        {
            // fenerIsigi objesi kodla eşleştirilmiş mi diye kontrol ediyoruz (hata almamak için)
            if (fenerIsigi != null)
            {
                // Objenin şu anki durumunun TAM TERSİNE çevir. 
                // Açıksa (!true = false) kapatır, kapalıysa (!false = true) açar.
                bool suAnkiDurum = fenerIsigi.activeSelf;
                fenerIsigi.SetActive(!suAnkiDurum);
            }
            else
            {
                Debug.LogWarning("Fener Isigi değişkenine Inspector'dan objeyi sürüklemediniz!");
            }
        }
    }
}
