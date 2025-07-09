using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float speed = 3f; // Yýlanýn düz ilerleme hýzý
    public float swayAmplitude = 0.5f; // Sað-sol sapma genliði
    public float swayFrequency = 1f; // Sað-sol sapma sýklýðý

    private Transform player; // Oyuncunun Transform'u
    private float movementTimer = 0f; // Hareket için zamanlayýcý

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
            Debug.LogError("Oyuncu bulunamadý! 'Player' tag'ine sahip bir obje sahnede mevcut mu?");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            FollowPlayerWithSway(); // Hafif sað-sol sapmayla oyuncuyu takip et
        }
    }

    private void FollowPlayerWithSway()
    {
        // Yýlanýn oyuncuya doðru olan yönünü belirle
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Sað-sol hareket için sinüs dalgasý ekle
        movementTimer += Time.deltaTime * swayFrequency;
        Vector2 swayOffset = new Vector2(-directionToPlayer.y, directionToPlayer.x) * Mathf.Sin(movementTimer) * swayAmplitude;

        // Yeni pozisyonu hesapla (doðrusal hareket + sapma)
        Vector2 targetPosition = (Vector2)transform.position + directionToPlayer * speed * Time.deltaTime + swayOffset;

        // Yeni pozisyona taþýn
        transform.position = targetPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer yýlan oyuncunun mermisiyle çarpýþýrsa yok ol
        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject); // Yýlaný yok et
            Destroy(collision.gameObject); // Oyuncunun mermisini yok et
        }
    }
}
