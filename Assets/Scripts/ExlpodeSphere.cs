using UnityEngine;
using System.Collections;

public class ExplodeCircle : MonoBehaviour
{
    public float destroyAfterSeconds = 1f; // Çarpmazsa 1 saniye sonra yok olacak
    public GameObject particle; // Patlama efekt objesi
    private bool hasExploded = false; // Patlama yapýldý mý kontrolü için

    void Start()
    {
        // Eðer mermi 1 saniye boyunca bir yere çarpmazsa patlamayý tetikle
        StartCoroutine(AutoDestroyAfterTime(destroyAfterSeconds));
    }

    // Mermi bir objeye çarptýðýnda patlamayý tetikle
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasExploded)
        {
            TriggerExplosion();
        }
    }

    // Mermi bir trigger objesine çarptýðýnda da patlamayý tetik
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasExploded)
        {
            TriggerExplosion();
        }
    }

    // Eðer hiçbir yere çarpmazsa patlama 1 saniye sonra tetiklenecek
    IEnumerator AutoDestroyAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (!hasExploded)
        {
            TriggerExplosion();
        }
    }

    public void TriggerExplosion()
    {
        if (hasExploded) return; // Eðer patlama zaten tetiklendiyse bir daha tetikleme
        hasExploded = true;

        CreateParticle(); // Particle oluþtur
    }

    void CreateParticle()
    {
        // Particle objesini oluþtur
        Instantiate(particle, transform.position, Quaternion.identity);

        // Bu nesneyi yok et
        Destroy(gameObject);
    }
}
