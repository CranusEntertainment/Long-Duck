using UnityEngine;
using System.Collections.Generic;

public class BulletSpawner : MonoBehaviour
{
    public List<GameObject> bulletPrefabs; // Mermi prefablarýný liste olarak ayarla
    public float spawnInterval = 5f; // 5 saniye aralýkla spawn olacak

    void Start()
    {
        InvokeRepeating("SpawnBullet", 0f, spawnInterval); // Ýlk anda ve her 5 saniyede bir mermi oluþtur
    }

    void SpawnBullet()
    {
        if (bulletPrefabs.Count == 0) return; // Liste boþsa çýk

        // Rastgele bir mermi prefabý seç
        int randomIndex = Random.Range(0, bulletPrefabs.Count);
        GameObject randomBullet = bulletPrefabs[randomIndex];

        // Seçilen mermiyi oluþtur
        Instantiate(randomBullet, transform.position, transform.rotation);
    }
}
