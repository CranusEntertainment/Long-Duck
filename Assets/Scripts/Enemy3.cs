using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float speed = 3f; // Düþmanýn hareket hýzý
    public float directionChangeInterval = 2f; // Yön deðiþim aralýðý
    private Vector2 randomDirection; // Rastgele bir yön
    private float directionChangeTimer = 0f; // Yön deðiþim zamanlayýcýsý

    private void Start()
    {
        // Ýlk hareket yönünü rastgele belirle
        SetRandomDirection();
    }

    private void Update()
    {
        // Yön deðiþtirme zamanýný kontrol et
        directionChangeTimer += Time.deltaTime;
        if (directionChangeTimer >= directionChangeInterval)
        {
            SetRandomDirection(); // Yeni bir rastgele yön belirle
            directionChangeTimer = 0f; // Zamanlayýcýyý sýfýrla
        }

        // Rastgele yön boyunca hareket et
        MoveInRandomDirection();
    }

    private void SetRandomDirection()
    {
        // Yönü rastgele bir vektör olarak belirle
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void MoveInRandomDirection()
    {
        // Düþmaný rastgele yön boyunca hareket ettir
        transform.position += (Vector3)(randomDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer düþman oyuncuya çarparsa
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy3: Oyuncuya çarptým!"); // Debug mesajý
            Destroy(gameObject); // Düþmaný yok et
        }
        // Eðer düþman mermiye çarparsa
        else if (collision.CompareTag("Bullet"))
        {
            Debug.Log("Enemy3: Mermi tarafýndan vuruldum!"); // Debug mesajý
            Destroy(gameObject); // Düþmaný yok et
            Destroy(collision.gameObject); // Mermiyi yok et
        }
    }
}
