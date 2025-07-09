using UnityEngine;

public class Mermi : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Mermiyi s�rekli ileri do�ru hareket ettir
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        // Ekran�n d���na ��kt���nda mermiyi yok et  
        Destroy(gameObject);
    }
}
