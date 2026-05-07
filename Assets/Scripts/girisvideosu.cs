using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MenuSinematik : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoPaneli; 
    
    // Sahne adını tam olarak "Outside" yaptık
    [SerializeField] private string oyunSahneAdi = "Outside"; 

    void Start()
    {
        // Video bittiğinde bu fonksiyon tetiklenecek
        videoPlayer.loopPointReached += SahneyeGec;
    }

    public void SinematigiBaslat()
    {
        if (videoPaneli != null)
        {
            videoPaneli.SetActive(true);
            videoPlayer.Play();
        }
    }

    void SahneyeGec(VideoPlayer vp)
    {
        Debug.Log("Video bitti, Outside sahnesine aktarılıyorsunuz...");
        SceneManager.LoadScene(oyunSahneAdi);
    }
}