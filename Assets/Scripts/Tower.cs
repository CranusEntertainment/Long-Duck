using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab Referansý
    public Transform spawnPoint; // Merminin çýkacaðý nokta
    public Vector2 shootDirection = Vector2.right; // Ateþleme yönü
    public float fireRate = 1f; // Ateþleme hýzý (saniye cinsinden)

    private float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        icebullet icebulletScript = bullet.GetComponent<icebullet>();

        if (icebulletScript != null)
        {
            icebulletScript.moveDirection = shootDirection.normalized; // Yönü ayarla
        }
    }
}
