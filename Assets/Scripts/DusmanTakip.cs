using UnityEngine;

public class DusmanTakip : MonoBehaviour
{
    public float hiz = 3f;
    private Transform oyuncu;
    private Rigidbody2D rb;
    
    // Yeni: Düşman doğduktan sonra kaç saniye şaşkın şaşkın beklesin?
    public float beklemeSuresi = 1f; 
    private float dogumZamani;

    void Start()
    {
        oyuncu = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        
        // Doğduğu anı kaydediyoruz
        dogumZamani = Time.time; 
    }

    void FixedUpdate() 
    {
        // Doğduğu andan itibaren o bekleme süresi geçene kadar kılını kıpırdatma
        if (Time.time < dogumZamani + beklemeSuresi)
        {
            rb.linearVelocity = Vector2.zero; // Olduğu yerde dursun
            return; // Aşağıdaki takip etme kısmına hiç geçme
        }

        // Süre dolunca av başlasın!
        if (oyuncu != null)
        {
            Vector2 yon = (oyuncu.position - transform.position).normalized;
            rb.linearVelocity = yon * hiz;
        }
    }
}