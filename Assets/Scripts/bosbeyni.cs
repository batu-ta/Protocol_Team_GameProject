using UnityEngine;
using UnityEngine.SceneManagement;

public class bossbeyni : MonoBehaviour
{
    // --- YENİ: Düşman fabrikasına haber uçuran tabela ---
    public static bool bossOldu = false; 

    public float hiz = 2f;
    public float canSuresi = 60f;
    public float gorusMesafesi = 10f; 
    public Transform oyuncu;
    
    private float fenerAltindaKalmaSuresi = 0f;
    private bool fenerDegiyorMu = false;
    private Rigidbody2D rb;
    private Vector3 orijinalOlcek;

    void Start()
    {
        // Oyun her başladığında tabelayı "yaşıyor" yapalım
        bossOldu = false; 

        rb = GetComponent<Rigidbody2D>();
        orijinalOlcek = transform.localScale;

        if (oyuncu == null)
        {
            GameObject bulunanOyuncu = GameObject.FindGameObjectWithTag("Player");
            if(bulunanOyuncu != null) oyuncu = bulunanOyuncu.transform;
        }
    }

    void FixedUpdate()
    {
        if (oyuncu != null)
        {
            float mesafe = Vector2.Distance(transform.position, oyuncu.position);

            // Oyuncu menzile girdiyse takip başlasın
            if (mesafe <= gorusMesafesi)
            {
                Vector2 yon = (oyuncu.position - transform.position).normalized;
                rb.linearVelocity = yon * hiz;

                // Yön kontrolü (Sola bakan orijinal sprite için)
                if (yon.x > 0.1f) 
                    transform.localScale = new Vector3(-orijinalOlcek.x, orijinalOlcek.y, orijinalOlcek.z);
                else if (yon.x < -0.1f) 
                    transform.localScale = new Vector3(orijinalOlcek.x, orijinalOlcek.y, orijinalOlcek.z);
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    void Update()
    {
        if (fenerDegiyorMu)
        {
            fenerAltindaKalmaSuresi += Time.deltaTime;
            
            // Boss fenerle yok olduğunda
            if (fenerAltindaKalmaSuresi >= canSuresi)
            {
                BossuGercektenOldur();
            }
        }
    }

    void BossuGercektenOldur()
    {
        // Önce tabelaya "Boss öldü" yazıyoruz ki fabrika sussun
        bossOldu = true; 
        Debug.Log("Zafer! Boss öldü ve ordu dağıldı.");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fener")) fenerDegiyorMu = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fener")) fenerDegiyorMu = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gorusMesafesi);
    }
}