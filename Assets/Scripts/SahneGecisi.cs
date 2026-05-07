using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneGecisi : MonoBehaviour
{
    [Tooltip("Geçmek istediğiniz sahnenin tam adını buraya yazın.")]
    public string gidilecekSahneAdi;

    [Tooltip("Siyah ekran panelinin üstündeki Animator bileşenini sürükleyin.")]
    public Animator gecisAnimator;

    private bool icerideMi = false;
    private bool gecisBasladiMi = false;

    void Update()
    {
        // Karakter içerideyken 'E' tuşuna basarsa ve henüz geçiş başlamadıysa...
        if (icerideMi && Input.GetKeyDown(KeyCode.E) && !gecisBasladiMi)
        {
            // Zamanlayıcılı geçişi başlat
            StartCoroutine(SahneYukleCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            icerideMi = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            icerideMi = false;
    }

    IEnumerator SahneYukleCoroutine()
    {
        gecisBasladiMi = true; // Tekrar E'e basılmasını engellemek için kilitliyoruz.

        // 1. Ekranı yavaşça karartan animasyonun Trigger'ını çalıştır
        gecisAnimator.SetTrigger("KararmaBasla");

        // 2. Animasyonun bitmesini bekle (Eğer animasyonu 1. saniyede bitirdiysen buraya 1f yaz)
        yield return new WaitForSeconds(1f);

        // 3. Ekran simsiyah olduktan sonra arka planda yeni sahneyi yükle
        SceneManager.LoadScene(gidilecekSahneAdi);
    }
}
