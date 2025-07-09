using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab Referans�
    public Transform spawnPoint; // Merminin ��kaca�� nokta
    public Vector2 shootDirection = Vector2.right; // Ate�leme y�n�
    public float fireRate = 1f; // Ate�leme h�z� (saniye cinsinden)

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
            icebulletScript.moveDirection = shootDirection.normalized; // Y�n� ayarla
        }
    }
}
