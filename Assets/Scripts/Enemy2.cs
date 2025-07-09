using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 4f; // Takip h�z�
    public GameObject explosionEffectPrefab; // Patlama efekti prefab'�

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
            Debug.LogError("Oyuncu bulunamad�! 'Player' tag'ine sahip bir obje sahnede mevcut mu?");
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
        // Oyuncuya do�ru hareket et
        Vector2 direction = (player.position - transform.position).normalized; // Y�n vekt�r�
        transform.position += (Vector3)direction * speed * Time.deltaTime; // Hareket
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Oyuncuya �arpt���nda
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy2: Oyuncuya �arpt�m!"); // Debug mesaj� g�ster

            TriggerExplosion(); // Patlama efekti olu�tur
            Destroy(gameObject); // Enemy2'yi yok et
        }

        // Mermiye �arpt���nda
        if (collision.CompareTag("Bullet"))
        {
            Debug.Log("Enemy2: Bir mermi taraf�ndan vuruldum!"); // Debug mesaj� g�ster

            TriggerExplosion(); // Patlama efekti olu�tur
            Destroy(gameObject); // Enemy2'yi yok et
            Destroy(collision.gameObject); // Mermiyi yok et
        }
    }

    private void TriggerExplosion()
    {
        // Patlama efekti olu�tur
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}
