using UnityEngine;
using System.Collections.Generic;

public class BulletSpawner : MonoBehaviour
{
    public List<GameObject> bulletPrefabs; // Mermi prefablar�n� liste olarak ayarla
    public float spawnInterval = 5f; // 5 saniye aral�kla spawn olacak

    void Start()
    {
        InvokeRepeating("SpawnBullet", 0f, spawnInterval); // �lk anda ve her 5 saniyede bir mermi olu�tur
    }

    void SpawnBullet()
    {
        if (bulletPrefabs.Count == 0) return; // Liste bo�sa ��k

        // Rastgele bir mermi prefab� se�
        int randomIndex = Random.Range(0, bulletPrefabs.Count);
        GameObject randomBullet = bulletPrefabs[randomIndex];

        // Se�ilen mermiyi olu�tur
        Instantiate(randomBullet, transform.position, transform.rotation);
    }
}
