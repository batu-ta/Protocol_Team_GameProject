using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MenuSinematik : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoPaneli;

    // Sahne adını tam olarak "Outside" yaptık
    [SerializeField] private string oyunSahneAdi = "Outside";

    [Tooltip("Video başladıktan kaç saniye sonra bir tuşla atlanabilsin (yanlışlıkla atlamayı önler).")]
    [SerializeField] private float atlamaGecikmesi = 0.4f;

    private bool gecildi = false;
    private float baslamaZamani = -1f;

    void Start()
    {
        // Video bittiğinde bu fonksiyon tetiklenecek
        videoPlayer.loopPointReached += SahneyeGec;
        // Video oynatılamazsa (dosya bozuk, platform desteklemiyor vb.) oyun takılmasın, devam etsin.
        videoPlayer.errorReceived += (vp, mesaj) =>
        {
            Debug.LogWarning("Giriş videosu oynatılamadı, oyuna geçiliyor: " + mesaj);
            SahneyeGec(vp);
        };
    }

    public void SinematigiBaslat()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // WebGL, oyuna gömülü videoları oynatamaz; videoyu atlayıp doğrudan oyuna geç.
        if (videoPlayer.source == VideoSource.VideoClip)
        {
            SahneyeGec(videoPlayer);
            return;
        }
#endif
        if (videoPaneli != null)
        {
            videoPaneli.SetActive(true);
            videoPlayer.Play();
            baslamaZamani = Time.unscaledTime;
        }
    }

    void Update()
    {
        // Video oynarken herhangi bir tuş ya da tıklama videoyu atlar.
        if (gecildi || baslamaZamani < 0f) return;
        if (Time.unscaledTime - baslamaZamani < atlamaGecikmesi) return;
        if (Input.anyKeyDown) SahneyeGec(videoPlayer);
    }

    void SahneyeGec(VideoPlayer vp)
    {
        if (gecildi) return;
        gecildi = true;
        Debug.Log("Video bitti, Outside sahnesine aktarılıyorsunuz...");
        SceneManager.LoadScene(oyunSahneAdi);
    }
}
