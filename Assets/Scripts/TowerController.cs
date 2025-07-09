using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab'i
    public Transform spawnPoint; // Merminin do�aca�� nokta
    public Transform targetPoint; // Hedef nokta
    public float bulletSpeed = 5f; // Mermi h�z�
    public float fireRate = 1f; // At�� aral��� (saniye cinsinden)
    public float detectionRadius = 5f; // Hedef alg�lama yar��ap�

    public int maxHealth = 100; // Tower'�n maksimum can�
    private int currentHealth;

    

    private float nextFireTime = 0f; // Bir sonraki at���n zaman�

    private void Start()
    {
        // Ba�lang��ta tower'�n can�n� maksimum de�ere ayarla
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // E�er hedef nokta belirli bir mesafedeyse at�� yap
        if (targetPoint != null && Vector2.Distance(transform.position, targetPoint.position) <= detectionRadius)
        {
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void Fire()
    {
        // Mermiyi spawnpoint'ten olu�tur ve hedefe y�nlendir
        GameObject bulletInstance = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        TowerBullet bulletScript = bulletInstance.GetComponent<TowerBullet>();

        if (targetPoint != null && bulletScript != null)
        {
            // Mermiyi hedef noktaya y�nlendir
            Vector2 direction = (targetPoint.position - spawnPoint.position).normalized;
            bulletScript.SetDirection(direction, bulletSpeed);
        }
    }

   

  


    private void OnDrawGizmosSelected()
    {
        // Unity Editor'de alg�lama yar��ap�n� g�rselle�tir
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
