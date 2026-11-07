using UnityEngine;
using UnityEngine.Audio; // Mixer için şart
using UnityEngine.UI;    // Slider için şart

public class SoundManager : MonoBehaviour
{
    public AudioMixer mainMixer; // Inspector'dan Mixer'ı buraya sürükle
    public Slider musicSlider;   // Senin o mor slider'ı buraya sürükle

    void Start()
    {
        // Oyun açıldığında slider'ın değerini mixer'a eşitle
        SetVolume(musicSlider.value);
    }

    public void SetVolume(float sliderValue)
    {
        // Audio Mixer -80 ile 20 arası çalışır. 
        // Log10 kullanarak sesi kulağa daha doğal gelecek şekilde ayarlıyoruz.
        mainMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}