using System.Collections;
using UnityEngine;

// Fenerin ışığına yakalanan düşmanın son anları: olduğu yerde donar, titreyerek söner ve yok olur.
// Işığa yakalandığı andan itibaren oyuncuya zarar veremez; feneri doğru tutan oyuncu ödüllendirilir.
public class DusmanSonumu : MonoBehaviour
{
    float sure = 2f;

    public static void Baslat(GameObject dusman, float sure)
    {
        if (dusman == null || dusman.GetComponent<DusmanSonumu>() != null) return;
        dusman.AddComponent<DusmanSonumu>().sure = Mathf.Max(0.1f, sure);
    }

    IEnumerator Start()
    {
        // Hareketi durdur: düşmanın hangi hareket betiği varsa kapat.
        foreach (var betik in GetComponents<MonoBehaviour>())
        {
            if (betik is SuzulenDusman || betik is DusmanTakip || betik is ZeroGEnemyWander) betik.enabled = false;
        }
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        // Artık oyuncuya değip onu öldüremez.
        foreach (var carpisma in GetComponents<Collider2D>())
        {
            if (!carpisma.isTrigger) carpisma.enabled = false;
        }
        var anim = GetComponent<Animator>();
        if (anim != null) anim.speed = 0.25f;

        var gorseller = GetComponentsInChildren<SpriteRenderer>();
        var renkler = new Color[gorseller.Length];
        for (int i = 0; i < gorseller.Length; i++) renkler[i] = gorseller[i].color;

        Vector3 merkez = transform.position;
        float t = 0f;
        while (t < sure)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / sure);
            transform.position = merkez + (Vector3)(Random.insideUnitCircle * 0.08f * (1f - k));
            for (int i = 0; i < gorseller.Length; i++)
            {
                if (gorseller[i] == null) continue;
                Color c = Color.Lerp(renkler[i], Color.white, k * 0.6f);
                c.a = renkler[i].a * (1f - k);
                gorseller[i].color = c;
            }
            yield return null;
        }
        Destroy(gameObject);
    }
}
