using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    public float lifetime = 2f; // Merminin otomatik olarak yok olma s�resi
    private Rigidbody2D rb;
    public float bulletDamage = 10f; // Merminin verece�i hasar
    private void Start()
    {
        // Merminin belirli bir s�re sonra yok olmas�n� sa�la
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector2 direction, float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �arp���lan nesneyi al
        GameObject other = collision.gameObject;

        // E�er �arp��t���m�z nesne d��man ise
        if (other.CompareTag("Player"))
        {
            // HealthSystem'e eri� ve hasar ver
            HealthSystem.Instance.TakeDamage(bulletDamage);
            Debug.Log("Tower mermisi player'a �arpt� ! ");
             Destroy(gameObject);
        }

       
        // Mermiyi yok et
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        // E�er mermi "Player" tag'ine sahip bir nesneye �arparsa yok ol
        if (other.CompareTag("Player"))
        {
            // �arpma durumunda, burada ek efektler veya hasar i�lemleri yap�labilir
            Destroy(gameObject);
            
        }
    }
}
