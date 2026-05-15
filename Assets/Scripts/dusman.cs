using UnityEngine;
using UnityEngine.SceneManagement;

public class SuzulenDusman : MonoBehaviour
{
    [Header("Hız Ayarları")]
    public float temelHiz = 2f;
    private float gercekHiz; // Her bir düşmanın kendine ait hızı olacak

    [Header("Sürü (Dalgalanma) Efekti")]
    public float dalgaHizi = 3f;     // Sağa-sola ne kadar sık kıvrılacağı
    public float dalgaBuyuklugu = 1f; // Ne kadar genişten alacağı
    private float rastgeleZamanFarki; // Hepsi aynı anda aynı yöne dönmesin diye fark

    private Transform playerTransform;

    void Start()
    {
        // 1. HİZ FARKI: Her canavar doğduğunda temel hızının üzerine çok ufak bir yavaşlık/hızlanır ekleriz (-0.5 ile +0.5 arası)
        // Böylece aynı anda doğsalar bile zamanla biri diğerini geçer, sıra bozulur.
        gercekHiz = temelHiz + Random.Range(-0.5f, 0.5f);

        // 2. SALINIM FARKI: Her düşmanın dalgalanması (kavisi) farklı saniyede başlar
        rastgeleZamanFarki = Random.Range(0f, 100f);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // A - Karakterimize doğru giden ana, DÜZ istikametimiz (Yön)
            Vector3 yurumeyonu = (playerTransform.position - transform.position).normalized;

            // B - Bu yöne dik olan 90 derecelik açı (Sağ-Sol ekseni)
            // 2D Oyun dünyasında dik açı Vector3(-Y, X, 0) formülüyle bulunur.
            Vector3 dikSolSagEkseni = new Vector3(-yurumeyonu.y, yurumeyonu.x, 0f);

            // C - Sinüs Dalgası Matematiği: Zamanı kullanarak yılan kavisini oluşturuyoruz
            float dalgalanmaMiktari = Mathf.Sin(Time.time * dalgaHizi + rastgeleZamanFarki) * dalgaBuyuklugu;

            // D - İki hareketi birleştir! 
            // (İleriye gitme kuvveti) + (Sağa sola kavis yapma kuvveti)
            Vector3 toplamHareket = (yurumeyonu * gercekHiz) + (dikSolSagEkseni * dalgalanmaMiktari);

            // E - Yeni pozisyonu uygula
            transform.position += toplamHareket * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Düşmana hasar verme kodumuz (Bu kısmı oyunu yeniden başlatmak yerine önceki verdiğim "HasarAl" koduyla değiştirebilirsiniz)
        if (collision.gameObject.CompareTag("Player"))
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }
    }
}
