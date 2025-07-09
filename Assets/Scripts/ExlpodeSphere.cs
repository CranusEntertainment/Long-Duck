using UnityEngine;
using System.Collections;

public class ExplodeCircle : MonoBehaviour
{
    public float destroyAfterSeconds = 1f; // �arpmazsa 1 saniye sonra yok olacak
    public GameObject particle; // Patlama efekt objesi
    private bool hasExploded = false; // Patlama yap�ld� m� kontrol� i�in

    void Start()
    {
        // E�er mermi 1 saniye boyunca bir yere �arpmazsa patlamay� tetikle
        StartCoroutine(AutoDestroyAfterTime(destroyAfterSeconds));
    }

    // Mermi bir objeye �arpt���nda patlamay� tetikle
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasExploded)
        {
            TriggerExplosion();
        }
    }

    // Mermi bir trigger objesine �arpt���nda da patlamay� tetik
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasExploded)
        {
            TriggerExplosion();
        }
    }

    // E�er hi�bir yere �arpmazsa patlama 1 saniye sonra tetiklenecek
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
        if (hasExploded) return; // E�er patlama zaten tetiklendiyse bir daha tetikleme
        hasExploded = true;

        CreateParticle(); // Particle olu�tur
    }

    void CreateParticle()
    {
        // Particle objesini olu�tur
        Instantiate(particle, transform.position, Quaternion.identity);

        // Bu nesneyi yok et
        Destroy(gameObject);
    }
}
