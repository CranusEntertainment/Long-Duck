using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 4f; // Takip hýzý
    public GameObject explosionEffectPrefab; // Patlama efekti prefab'ý

    private Transform player; // Oyuncunun Transform'u

    private void Start()
    {
        // Oyuncuyu otomatik olarak bul
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Oyuncu bulunamadý! 'Player' tag'ine sahip bir obje sahnede mevcut mu?");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            FollowPlayer(); // Karakteri takip et
        }
    }

    private void FollowPlayer()
    {
        // Oyuncuya doðru hareket et
        Vector2 direction = (player.position - transform.position).normalized; // Yön vektörü
        transform.position += (Vector3)direction * speed * Time.deltaTime; // Hareket
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Oyuncuya çarptýðýnda
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy2: Oyuncuya çarptým!"); // Debug mesajý göster

            TriggerExplosion(); // Patlama efekti oluþtur
            Destroy(gameObject); // Enemy2'yi yok et
        }

        // Mermiye çarptýðýnda
        if (collision.CompareTag("Bullet"))
        {
            Debug.Log("Enemy2: Bir mermi tarafýndan vuruldum!"); // Debug mesajý göster

            TriggerExplosion(); // Patlama efekti oluþtur
            Destroy(gameObject); // Enemy2'yi yok et
            Destroy(collision.gameObject); // Mermiyi yok et
        }
    }

    private void TriggerExplosion()
    {
        // Patlama efekti oluþtur
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}
