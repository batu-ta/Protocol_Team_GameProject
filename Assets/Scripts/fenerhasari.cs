using UnityEngine;

public class FenerHasari : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D temasEden)
    {
        if (temasEden.gameObject.CompareTag("Dusman"))
        {
            // Virgül koyup 2f yazdık. Düşman 2 saniye sonra yok olucak.
            Destroy(temasEden.gameObject, 2f);
        }
    }
}