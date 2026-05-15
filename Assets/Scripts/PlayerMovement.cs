using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController2D : MonoBehaviour
{
    [Header("=== HAREKET AYARLARI ===")]
    public float hareketHizi = 8f;
    public float kosmaHizi = 14f;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 hareketGirdisi;
    private bool sagaBakiyor = true;
    private bool oluMu = false;

    private float geciciHiz;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        geciciHiz = hareketHizi;
    }

    void Update()
    {
        if (oluMu) return;

        // Shift'e basılıyken hızı ayarla
        if (Input.GetKey(KeyCode.LeftShift))
        {
            geciciHiz = kosmaHizi;
        }
        else
        {
            geciciHiz = hareketHizi;
        }

        hareketGirdisi.x = Input.GetAxisRaw("Horizontal");
        hareketGirdisi.y = Input.GetAxisRaw("Vertical");
        hareketGirdisi = hareketGirdisi.normalized;

        if (Input.GetButtonDown("Fire1")) { anim.SetTrigger("AtesEt"); }

        if (Input.GetKeyDown(KeyCode.K))
        {
            oluMu = true;
            anim.SetBool("Olu", true);
            rb.linearVelocity = Vector2.zero; // Unity 6 öncesi için rb.velocity
            return;
        }

        YonuCevir();

        // ---- ANİMASYON PARAMETRELERİ (GÜNCELLENDİ) ----

        // 1. Durma/Yürüme hissi için hız değerini her halükarda yolluyoruz.
        anim.SetFloat("Hiz", hareketGirdisi.magnitude);

        // 2. YENİ EKLENDİ: Karakter sadece tuşa bastığında değil, hareket ederken + shifte basarken KoşuyorBool'u True olur.
        bool yuruyorMu = hareketGirdisi.magnitude > 0f;
        bool kosmayaBasladi = Input.GetKey(KeyCode.LeftShift) && yuruyorMu;

        anim.SetBool("Kosuyor", kosmayaBasladi);
    }

    void FixedUpdate()
    {
        if (!oluMu)
        {
            rb.linearVelocity = hareketGirdisi * geciciHiz; // Unity 6 altındaysanız rb.velocity = hareketGirdisi * geciciHiz; kullanın
        }
    }

    private void YonuCevir()
    {
        if (sagaBakiyor && hareketGirdisi.x < 0f || !sagaBakiyor && hareketGirdisi.x > 0f)
        {
            sagaBakiyor = !sagaBakiyor;
            Vector3 lokalOlcek = transform.localScale;
            lokalOlcek.x *= -1f;
            transform.localScale = lokalOlcek;
        }
    }
}
