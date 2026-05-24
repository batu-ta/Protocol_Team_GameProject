using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ZeroGEnemyWander : MonoBehaviour
{
    [Header("Yerçekimsiz Hareket Ayarları")]
    public float moveSpeed = 3f;            // Uçuş hızı
    public float directionChangeTime = 4f;  // Kaç saniyede bir sıkılıp kendi isteğiyle yön değiştirecek

    private Rigidbody2D rb;
    private Vector2 currentDirection;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Yerçekimsiz kuşbakışı ortam ayarlarını kodla otomatik yapalım
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;    // Havada sürtünme sıfır (karakter yavaşlamaz)

        // Kendi etrafında fırıldak gibi dönmesini engelle
        rb.freezeRotation = true;

        // Oyuna başladığında ilk yönü ve hızını belirle
        PickRandomDirection();
    }

    void Update()
    {
        // Zamanlayıcı geri sayımı
        timer -= Time.deltaTime;

        // Süre dolduğunda rastgele yeni bir yön seç (böcek veya hayalet gibi rastgele gezinme hissi için)
        if (timer <= 0f)
        {
            PickRandomDirection();
        }
    }

    void FixedUpdate()
    {
        // Düşmana sürekli olarak belirlenen yönde hız ver
        // Uygulanan kuvvet (velocity) yerçekimi 0 olduğu için asla yavaşlamadan sabit hızla gider
        rb.linearVelocity = currentDirection * moveSpeed;
    }

    void PickRandomDirection()
    {
        // 360 derece rastgele bir açı/yön belirle
        currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        timer = directionChangeTime; // Süreyi yeniden başlat
    }

    // Bir duvara çarptığında burası çalışır
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Çarptığı duvarın yüzey açısını (normalini) al
        Vector2 wallNormal = collision.contacts[0].normal;

        // Geliş açımızı duvarın açısına göre yansıt (Tam bir bilardo topu sekmesi sağlar!)
        currentDirection = Vector2.Reflect(currentDirection, wallNormal).normalized;

        // Çarptıktan ve sektikten hemen sonra birden yönünü kendi kendine değiştirmesin diye süreyi sıfırla
        timer = directionChangeTime;
    }
}
