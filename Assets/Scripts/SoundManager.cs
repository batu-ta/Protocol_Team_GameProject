using UnityEngine;
using UnityEngine.Audio; // Mixer için þart
using UnityEngine.UI;    // Slider için þart

public class SoundManager : MonoBehaviour
{
    public AudioMixer mainMixer; // Inspector'dan Mixer'ý buraya sürükle
    public Slider musicSlider;   // Senin o mor slider'ý buraya sürükle

    void Start()
    {
        // Oyun açýldýðýnda slider'ýn deðerini mixer'a eþitle
        SetVolume(musicSlider.value);
    }

    public void SetVolume(float sliderValue)
    {
        // Audio Mixer -80 ile 20 arasý çalýþýr. 
        // Log10 kullanarak sesi kulaða daha doðal gelecek þekilde ayarlýyoruz.
        mainMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}