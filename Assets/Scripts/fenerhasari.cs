using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FenerHasari : MonoBehaviour
{
    void Awake()
    {
        // Hasar alanı, ışığın görünen şekliyle birebir aynı olsun: ışıkta görünen her düşman etkilenir.
        // (Elle çizilen çarpışma alanı ışıktan kısa kalıyordu; hüzmenin ucundaki düşmanlar etkilenmiyordu.)
        var isik = GetComponent<Light2D>();
        var alan = GetComponent<PolygonCollider2D>();
        if (isik == null || alan == null || isik.lightType != Light2D.LightType.Freeform) return;
        Vector3[] sekil = isik.shapePath;
        if (sekil == null || sekil.Length < 3) return;
        var noktalar = new Vector2[sekil.Length];
        for (int i = 0; i < sekil.Length; i++) noktalar[i] = sekil[i];
        alan.pathCount = 1;
        alan.SetPath(0, noktalar);
    }

    void OnTriggerEnter2D(Collider2D temasEden)
    {
        if (temasEden.gameObject.CompareTag("Dusman"))
        {
            // Işığa yakalanan düşman donar, artık zarar veremez ve 2 saniyede sönerek yok olur.
            DusmanSonumu.Baslat(temasEden.gameObject, 2f);
        }
    }
}
