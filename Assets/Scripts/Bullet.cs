using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 20f; // Verilecek hasar miktarý
    public GameObject explosionEffectPrefab; // Patlama efekti prefab'ý


    private void Start()
    {
        Destroy(gameObject,0.8f);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Çarpýþýlan nesneyi al
        GameObject other = collision.gameObject;

        // Eðer çarpýþtýðýmýz nesne düþman ise
        if (other.CompareTag("Enemy"))
        {
            // Düþmanýn 'healthBar' scriptine eriþ
            //healthBar enemyHealth = other.GetComponent<healthBar>();

            // Eðer düþmanýn healthBar'ý varsa hasar ver
           /* if (enemyHealth != null)
            {
                enemyHealth.takedamage(damage);
            }
           */
        }

        // Çarpma noktasý
        ContactPoint contact = collision.contacts[0];
        Vector3 hitPoint = contact.point;

        // Patlama efekti oluþtur
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, hitPoint, Quaternion.identity);
        }

        // Mermiyi yok et
        Destroy(gameObject);
    }
}
