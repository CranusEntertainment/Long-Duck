using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 20f; // Verilecek hasar miktar�
    public GameObject explosionEffectPrefab; // Patlama efekti prefab'�


    private void Start()
    {
        Destroy(gameObject,0.8f);
    }

    void OnCollisionEnter(Collision collision)
    {
        // �arp���lan nesneyi al
        GameObject other = collision.gameObject;

        // E�er �arp��t���m�z nesne d��man ise
        if (other.CompareTag("Enemy"))
        {
            // D��man�n 'healthBar' scriptine eri�
            //healthBar enemyHealth = other.GetComponent<healthBar>();

            // E�er d��man�n healthBar'� varsa hasar ver
           /* if (enemyHealth != null)
            {
                enemyHealth.takedamage(damage);
            }
           */
        }

        // �arpma noktas�
        ContactPoint contact = collision.contacts[0];
        Vector3 hitPoint = contact.point;

        // Patlama efekti olu�tur
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, hitPoint, Quaternion.identity);
        }

        // Mermiyi yok et
        Destroy(gameObject);
    }
}
