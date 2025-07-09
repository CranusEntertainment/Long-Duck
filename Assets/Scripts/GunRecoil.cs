using UnityEngine;
using System.Collections;

public class GunRecoil : MonoBehaviour
{
    public GameObject bulletPrefab;  // Olu�turulacak mermi prefab�
    public Transform gunTip;         // Tabancan�n ucu (merminin olu�aca�� yer)

    public void Shoot()
    {
        // Tabancan�n ucunda mermi prefab�n� olu�tur
        if (bulletPrefab != null && gunTip != null)
        {
            Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
        }
    }
}
 