using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float speed = 3f; // Y�lan�n d�z ilerleme h�z�
    public float swayAmplitude = 0.5f; // Sa�-sol sapma genli�i
    public float swayFrequency = 1f; // Sa�-sol sapma s�kl���

    private Transform player; // Oyuncunun Transform'u
    private float movementTimer = 0f; // Hareket i�in zamanlay�c�

    private void Start()
    {
        // Oyuncuyu sahnede otomatik olarak bul
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
            FollowPlayerWithSway(); // Hafif sa�-sol sapmayla oyuncuyu takip et
        }
    }

    private void FollowPlayerWithSway()
    {
        // Y�lan�n oyuncuya do�ru olan y�n�n� belirle
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Sa�-sol hareket i�in sin�s dalgas� ekle
        movementTimer += Time.deltaTime * swayFrequency;
        Vector2 swayOffset = new Vector2(-directionToPlayer.y, directionToPlayer.x) * Mathf.Sin(movementTimer) * swayAmplitude;

        // Yeni pozisyonu hesapla (do�rusal hareket + sapma)
        Vector2 targetPosition = (Vector2)transform.position + directionToPlayer * speed * Time.deltaTime + swayOffset;

        // Yeni pozisyona ta��n
        transform.position = targetPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // E�er y�lan oyuncunun mermisiyle �arp���rsa yok ol
        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject); // Y�lan� yok et
            Destroy(collision.gameObject); // Oyuncunun mermisini yok et
        }
    }
}
