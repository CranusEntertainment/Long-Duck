using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab'i
    public Transform spawnPoint; // Merminin doðacaðý nokta
    public Transform targetPoint; // Hedef nokta
    public float bulletSpeed = 5f; // Mermi hýzý
    public float fireRate = 1f; // Atýþ aralýðý (saniye cinsinden)
    public float detectionRadius = 5f; // Hedef algýlama yarýçapý

    public int maxHealth = 100; // Tower'ýn maksimum caný
    private int currentHealth;

    

    private float nextFireTime = 0f; // Bir sonraki atýþýn zamaný

    private void Start()
    {
        // Baþlangýçta tower'ýn canýný maksimum deðere ayarla
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // Eðer hedef nokta belirli bir mesafedeyse atýþ yap
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
        // Mermiyi spawnpoint'ten oluþtur ve hedefe yönlendir
        GameObject bulletInstance = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        TowerBullet bulletScript = bulletInstance.GetComponent<TowerBullet>();

        if (targetPoint != null && bulletScript != null)
        {
            // Mermiyi hedef noktaya yönlendir
            Vector2 direction = (targetPoint.position - spawnPoint.position).normalized;
            bulletScript.SetDirection(direction, bulletSpeed);
        }
    }

   

  


    private void OnDrawGizmosSelected()
    {
        // Unity Editor'de algýlama yarýçapýný görselleþtir
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
