using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float speed = 3f; // D��man�n hareket h�z�
    public float directionChangeInterval = 2f; // Y�n de�i�im aral���
    private Vector2 randomDirection; // Rastgele bir y�n
    private float directionChangeTimer = 0f; // Y�n de�i�im zamanlay�c�s�

    private void Start()
    {
        // �lk hareket y�n�n� rastgele belirle
        SetRandomDirection();
    }

    private void Update()
    {
        // Y�n de�i�tirme zaman�n� kontrol et
        directionChangeTimer += Time.deltaTime;
        if (directionChangeTimer >= directionChangeInterval)
        {
            SetRandomDirection(); // Yeni bir rastgele y�n belirle
            directionChangeTimer = 0f; // Zamanlay�c�y� s�f�rla
        }

        // Rastgele y�n boyunca hareket et
        MoveInRandomDirection();
    }

    private void SetRandomDirection()
    {
        // Y�n� rastgele bir vekt�r olarak belirle
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void MoveInRandomDirection()
    {
        // D��man� rastgele y�n boyunca hareket ettir
        transform.position += (Vector3)(randomDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // E�er d��man oyuncuya �arparsa
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy3: Oyuncuya �arpt�m!"); // Debug mesaj�
            Destroy(gameObject); // D��man� yok et
        }
        // E�er d��man mermiye �arparsa
        else if (collision.CompareTag("Bullet"))
        {
            Debug.Log("Enemy3: Mermi taraf�ndan vuruldum!"); // Debug mesaj�
            Destroy(gameObject); // D��man� yok et
            Destroy(collision.gameObject); // Mermiyi yok et
        }
    }
}
