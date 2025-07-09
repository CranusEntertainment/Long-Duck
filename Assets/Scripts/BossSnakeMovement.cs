using UnityEngine;

public class BossSnakeBehavior : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform'u
    public float zigzagSpeed = 3f; // Zigzag hareket hýzý
    public float zigzagAmplitude = 1f; // Zigzag genliði
    public float zigzagFrequency = 2f; // Zigzag sýklýðý
    public GameObject destroyEffectPrefab; // Yok olma efekti için prefab

    private void Update()
    {
        ZigzagFollowPlayer(); // Zigzag hareketi ile oyuncuyu takip et
    }

    private void ZigzagFollowPlayer()
    {
        // Yýlanýn oyuncuya doðru olan yönünü belirle
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Zigzag hareketi için dalgalanma ekle
        float zigzagOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        Vector2 perpendicularOffset = new Vector2(-directionToPlayer.y, directionToPlayer.x) * zigzagOffset;

        // Zigzag pozisyonu hesapla
        Vector2 newPosition = (Vector2)transform.position + directionToPlayer * zigzagSpeed * Time.deltaTime + perpendicularOffset;

        // Yýlaný yeni pozisyona taþý
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer yýlan oyuncunun mermisiyle çarpýþýrsa yok ol
        if (collision.CompareTag("Bullet"))
        {
            DestroyBoss(); // Yok olma fonksiyonunu çaðýr
            Destroy(collision.gameObject); // Oyuncunun mermisini yok et
        }
    }

    private void DestroyBoss()
    {
        // Yok olma efekti oluþtur
        if (destroyEffectPrefab != null)
        {
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        }

        // Yýlaný yok et
        Destroy(gameObject);
    }
}
