using UnityEngine;
using System.Collections;

public class GunRecoil : MonoBehaviour
{
    public GameObject bulletPrefab;  // Oluþturulacak mermi prefabý
    public Transform gunTip;         // Tabancanýn ucu (merminin oluþacaðý yer)

    public void Shoot()
    {
        // Tabancanýn ucunda mermi prefabýný oluþtur
        if (bulletPrefab != null && gunTip != null)
        {
            Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
        }
    }
}
 