using UnityEngine;

public class BossSnakeBehavior : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform'u
    public float zigzagSpeed = 3f; // Zigzag hareket h�z�
    public float zigzagAmplitude = 1f; // Zigzag genli�i
    public float zigzagFrequency = 2f; // Zigzag s�kl���
    public GameObject destroyEffectPrefab; // Yok olma efekti i�in prefab

    private void Update()
    {
        ZigzagFollowPlayer(); // Zigzag hareketi ile oyuncuyu takip et
    }

    private void ZigzagFollowPlayer()
    {
        // Y�lan�n oyuncuya do�ru olan y�n�n� belirle
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Zigzag hareketi i�in dalgalanma ekle
        float zigzagOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        Vector2 perpendicularOffset = new Vector2(-directionToPlayer.y, directionToPlayer.x) * zigzagOffset;

        // Zigzag pozisyonu hesapla
        Vector2 newPosition = (Vector2)transform.position + directionToPlayer * zigzagSpeed * Time.deltaTime + perpendicularOffset;

        // Y�lan� yeni pozisyona ta��
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // E�er y�lan oyuncunun mermisiyle �arp���rsa yok ol
        if (collision.CompareTag("Bullet"))
        {
            DestroyBoss(); // Yok olma fonksiyonunu �a��r
            Destroy(collision.gameObject); // Oyuncunun mermisini yok et
        }
    }

    private void DestroyBoss()
    {
        // Yok olma efekti olu�tur
        if (destroyEffectPrefab != null)
        {
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        }

        // Y�lan� yok et
        Destroy(gameObject);
    }
}
