using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    public float lifetime = 2f; // Merminin otomatik olarak yok olma süresi
    private Rigidbody2D rb;
    public float bulletDamage = 10f; // Merminin vereceði hasar
    private void Start()
    {
        // Merminin belirli bir süre sonra yok olmasýný saðla
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector2 direction, float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Çarpýþýlan nesneyi al
        GameObject other = collision.gameObject;

        // Eðer çarpýþtýðýmýz nesne düþman ise
        if (other.CompareTag("Player"))
        {
            // HealthSystem'e eriþ ve hasar ver
            HealthSystem.Instance.TakeDamage(bulletDamage);
            Debug.Log("Tower mermisi player'a çarptý ! ");
             Destroy(gameObject);
        }

       
        // Mermiyi yok et
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer mermi "Player" tag'ine sahip bir nesneye çarparsa yok ol
        if (other.CompareTag("Player"))
        {
            // Çarpma durumunda, burada ek efektler veya hasar iþlemleri yapýlabilir
            Destroy(gameObject);
            
        }
    }
}
