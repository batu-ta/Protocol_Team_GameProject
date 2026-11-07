using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Oyuncunun ölümünü tek yerden yönetir: ölüm animasyonu, ağır çekim, kararan ekran,
// "ÖLDÜN" yazısı, ölüm sayacı ve sahnenin yeniden başlaması.
// Sahneye bir şey eklemeye gerek yok: ilk ölümde kendini kurar, sahne yeniden yüklenince kaybolur.
// Kullanım: OlumYoneticisi.OyuncuOldu("dusman");
public class OlumYoneticisi : MonoBehaviour
{
    // İleride ölüme tepki veren karakterler (Hades tarzı) bu kayıtları okuyabilir.
    public const string ToplamOlumAnahtari = "stat_toplam_olum";
    public const string SonOlumNedeniAnahtari = "stat_son_olum_nedeni";

    const float KararmaSuresi = 0.9f;    // ekranın kararma süresi (gerçek zaman)
    const float BeklemeSuresi = 1.1f;    // yazının ekranda kalma süresi
    const float AgirCekim = 0.35f;       // ölüm anındaki zaman yavaşlaması

    static OlumYoneticisi aktif;

    public static bool OlumSuruyor => aktif != null;
    public static int ToplamOlum => PlayerPrefs.GetInt(ToplamOlumAnahtari, 0);

    public static void OyuncuOldu(string neden)
    {
        if (aktif != null) return; // aynı anda iki düşman değerse tek ölüm sayılır
        var go = new GameObject("OlumYoneticisi");
        aktif = go.AddComponent<OlumYoneticisi>();
        aktif.StartCoroutine(aktif.OlumSahnesi(string.IsNullOrEmpty(neden) ? "bilinmiyor" : neden));
    }

    IEnumerator OlumSahnesi(string neden)
    {
        int sayi = ToplamOlum + 1;
        PlayerPrefs.SetInt(ToplamOlumAnahtari, sayi);
        PlayerPrefs.SetString(SonOlumNedeniAnahtari, neden);
        PlayerPrefs.Save();

        GameObject oyuncu = GameObject.FindGameObjectWithTag("Player");
        if (oyuncu != null)
        {
            var kontrol = oyuncu.GetComponent<PlayerController2D>();
            if (kontrol != null) kontrol.Oldur();
        }

        Image perde;
        Text baslik, alt;
        EkraniKur(sayi, out perde, out baslik, out alt);

        Time.timeScale = AgirCekim;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / KararmaSuresi;
            float k = Mathf.Clamp01(t);
            perde.color = new Color(0.04f, 0f, 0f, Mathf.Lerp(0f, 0.9f, k));
            float yaziAlfa = Mathf.Clamp01((k - 0.35f) / 0.65f);
            baslik.color = new Color(0.82f, 0.08f, 0.08f, yaziAlfa);
            alt.color = new Color(0.75f, 0.72f, 0.72f, yaziAlfa * 0.85f);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(BeklemeSuresi);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void EkraniKur(int sayi, out Image perde, out Text baslik, out Text alt)
    {
        var tuvalGo = new GameObject("OlumEkrani", typeof(Canvas), typeof(CanvasScaler));
        tuvalGo.transform.SetParent(transform, false);
        var tuval = tuvalGo.GetComponent<Canvas>();
        tuval.renderMode = RenderMode.ScreenSpaceOverlay;
        tuval.sortingOrder = 999;
        var olcek = tuvalGo.GetComponent<CanvasScaler>();
        olcek.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        olcek.referenceResolution = new Vector2(1920, 1080);

        perde = new GameObject("Perde", typeof(Image)).GetComponent<Image>();
        perde.transform.SetParent(tuvalGo.transform, false);
        TamEkran(perde.rectTransform);
        perde.color = new Color(0f, 0f, 0f, 0f);
        perde.raycastTarget = true; // ölüm sırasında arayüze tıklanmasın

        Font yaziTipi = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        baslik = Yazi(tuvalGo.transform, "ÖLDÜN", yaziTipi, 150, new Vector2(0f, 40f));
        alt = Yazi(tuvalGo.transform, "Ölüm sayısı: " + sayi, yaziTipi, 40, new Vector2(0f, -90f));
    }

    static Text Yazi(Transform ust, string metin, Font yaziTipi, int boyut, Vector2 konum)
    {
        var yazi = new GameObject("Yazi", typeof(Text)).GetComponent<Text>();
        yazi.transform.SetParent(ust, false);
        yazi.rectTransform.sizeDelta = new Vector2(1600f, boyut * 1.6f);
        yazi.rectTransform.anchoredPosition = konum;
        yazi.font = yaziTipi;
        yazi.fontSize = boyut;
        yazi.alignment = TextAnchor.MiddleCenter;
        yazi.text = metin;
        yazi.color = new Color(1f, 1f, 1f, 0f);
        yazi.raycastTarget = false;
        return yazi;
    }

    static void TamEkran(RectTransform rt)
    {
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    void OnDestroy()
    {
        if (aktif == this)
        {
            aktif = null;
            Time.timeScale = 1f; // sahne değişse bile oyun yavaş kalmasın
        }
    }
}
