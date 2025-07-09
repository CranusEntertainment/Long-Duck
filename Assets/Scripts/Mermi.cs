using UnityEngine;

public class Mermi : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Mermiyi sürekli ileri doðru hareket ettir
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        // Ekranýn dýþýna çýktýðýnda mermiyi yok et  
        Destroy(gameObject);
    }
}
