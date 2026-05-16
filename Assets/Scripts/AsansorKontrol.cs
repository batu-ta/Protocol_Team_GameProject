using UnityEngine;

public class AsansorKontrol : MonoBehaviour
{
    [Header("Animasyon Ayarları")]
    public Animator asansorAnim;
    private bool asansorAcildiMi = false;

    [Header("Etkileşim (UI) Ayarları")]
    [Tooltip("Asansörün yanındayken çıkacak Bilgi Kutucuğunu buraya sürükleyin")]
    public GameObject bilgiKutucuguUI; // UI objemizi tanımladığımız yer

    void Start()
    {
        // Oyun ilk başladığında bilgi kutucuğunu gizlediğinden emin olalım
        if (bilgiKutucuguUI != null)
        {
            bilgiKutucuguUI.SetActive(false);
        }
    }

    // Karakter asansörün önündeki alana GİRDİĞİNDE çalışır
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Giren obje Player ise
        if (other.CompareTag("Player"))
        {
            // Asansör ilk kez açılacaksa animasyonu oynat
            if (asansorAnim != null && !asansorAcildiMi)
            {
                asansorAnim.SetTrigger("AsansorAc");
                asansorAcildiMi = true; // Sadece 1 kere açılsın
            }

            // Yanına geldiği sürece Bilgi Kutucuğu (UI) görünür olsun
            if (bilgiKutucuguUI != null)
            {
                bilgiKutucuguUI.SetActive(true);
            }
        }
    }

    // Karakter asansörün önündeki alandan ÇIKIP UZAKLAŞTIĞINDA çalışır
    private void OnTriggerExit2D(Collider2D other)
    {
        // Çıkan obje Player ise, yani oyuncu uzaklaştıysa
        if (other.CompareTag("Player"))
        {
            // Bilgi Kutucuğunu (Interact UI'ını) kapatarak gizle
            if (bilgiKutucuguUI != null)
            {
                bilgiKutucuguUI.SetActive(false);
            }
        }
    }
}
