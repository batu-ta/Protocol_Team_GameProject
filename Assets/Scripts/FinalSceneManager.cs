using System.Collections;
using UnityEngine;
using TMPro;

public class FinalSceneManager : MonoBehaviour
{
    [Header("UI Ayarları")]
    public TextMeshProUGUI finalMetin;

    [TextArea(5, 10)]
    public string yazilacakMetin = "Subject: Eyes protocol,\n\nGöz <color=green>başarıyla</color> ele geçirildi. Güvenli alana geçiş yapılıyor, bütün anomaliler aktif.";

    public float yazmaHizi = 0.08f;

    [Header("Ses Ayarları")]
    public AudioSource sesKaynagi;
    public AudioClip daktiloSesi;

    void Start()
    {
        // 1. Önce bütün metni TextMeshPro'ya içindeki kodlarla gizlice yüklüyoruz
        finalMetin.text = yazilacakMetin;

        // 2. Başlangıçta görünür karakter sayısını 0 yapıyoruz (ekran boş görünecek)
        finalMetin.maxVisibleCharacters = 0;

        StartCoroutine(DaktiloEfekti());
    }

    IEnumerator DaktiloEfekti()
    {
        // TextMeshPro metni arka planda işlesin (renk kodlarını vb. algılasın)
        finalMetin.ForceMeshUpdate();

        // Sadece ekranda gözükecek olan gerçek harflerin sayısına bak
        int toplamHarf = finalMetin.textInfo.characterCount;
        int suAnkiGozuken = 0;

        while (suAnkiGozuken < toplamHarf)
        {
            suAnkiGozuken++;

            // Görünür harf sayısını 1 arttır
            finalMetin.maxVisibleCharacters = suAnkiGozuken;

            // Sıradaki harfi bul (Renk kodlarını ('<, >, vs.') umursamadan doğrudan okunan karakteri alır)
            char harf = finalMetin.textInfo.characterInfo[suAnkiGozuken - 1].character;

            // Boşluk hissini daha iyi vermek için ses kontrolü
            if (harf != ' ' && harf != '\n')
            {
                if (sesKaynagi != null && daktiloSesi != null)
                {
                    sesKaynagi.pitch = Random.Range(0.9f, 1.1f);
                    sesKaynagi.PlayOneShot(daktiloSesi);
                }
            }

            yield return new WaitForSeconds(yazmaHizi);
        }
    }
}
