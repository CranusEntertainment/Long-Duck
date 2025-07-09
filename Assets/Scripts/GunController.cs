using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab'ý
    public List<Transform> firePoints; // Ateþ noktalarý (birden fazla destekleniyor)
    public float bulletSpeed = 10f;

    public GunRecoil gungun;
    public Slider fireRateSlider; // Atýþ hýzýný gösteren slider

    private int fireLevel = 0; // Ateþ hýzýnýn seviyesi (1-7)
    private bool isShooting = false;
    private Coroutine fireCoroutine;
    private Coroutine slowDownCoroutine; // Hýz düþürme Coroutine’i
    public TopDownCharacterController karakter;

    private float originalMoveSpeed; // Karakterin orijinal hareket hýzý
    private float originalDashSpeed; // Karakterin orijinal dash hýzý
    private bool isUpgraded = false; // Upgrade durumunu takip etmek için
    public List<Transform> additionalFirePoints; // Upgrade sonrasý açýlacak firePoints

    private float shootHoldTime = 0f; // Sol týkýn ne kadar süreyle basýlý tutulduðunu izlemek için

    private void Start()
    {
        karakter = GetComponent<TopDownCharacterController>();

        // Baþlangýç hýzlarýný kaydet
        originalMoveSpeed = karakter.moveSpeed;
        originalDashSpeed = karakter.dashSpeed;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Sol týk basýlý tutuluyor
        {
            shootHoldTime += Time.deltaTime; // Basýlý tutma süresini artýr

            if (isShooting == false)
            {
                isShooting = true;

                if (fireCoroutine == null)
                    fireCoroutine = StartCoroutine(ShootRoutine()); // Sürekli ateþ etme iþlemini baþlatýr
            }

            // Eðer 2 saniye basýlý tutulduysa hýzlarý düþür
            if (shootHoldTime >= 2f && slowDownCoroutine == null)
            {
                slowDownCoroutine = StartCoroutine(SlowDown());
            }
        }
        else if (Input.GetMouseButtonUp(0)) // Sol týk býrakýldýðýnda
        {
            isShooting = false;

            // Hýzlarý orijinal deðerlerine döndür
            karakter.dashSpeed = originalDashSpeed;
            karakter.moveSpeed = originalMoveSpeed;

            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
                fireCoroutine = null;
            }

            if (slowDownCoroutine != null)
            {
                StopCoroutine(slowDownCoroutine);
                slowDownCoroutine = null;
            }

            ResetFireLevel(); // Ateþ hýzý barýný sýfýrla
            shootHoldTime = 0f; // Basýlý tutma süresini sýfýrla
        }
    }

    IEnumerator SlowDown()
    {
        // Hýzlarý düþür
        karakter.dashSpeed = 5;
        karakter.moveSpeed = 1;
        yield break; // Coroutine hemen biter, hýzlar anýnda düþürülür
    }

    IEnumerator ShootRoutine()
    {
        fireLevel = 0; // Baþlangýçta ateþ hýzý seviyesini sýfýrla
        UpdateFireRateSlider();

        while (isShooting)
        {
            Shoot();
            gungun.Shoot(); // Ateþleme ve geri tepme

            if (fireLevel < 7) // Ateþ seviyesi maksimuma ulaþana kadar artmaya devam eder
            {
                fireLevel++; // Her saniyede bir fireLevel’ý artýr
                UpdateFireRateSlider();
            }

            // Ateþ etme aralýðýný fireLevel’a göre ayarla
            yield return new WaitForSeconds(1.0f / (fireLevel > 0 ? fireLevel : 1));
        }
    }

    void Shoot()
    {
        // Upgrade yapýlmadan önce yalnýzca ilk firePoint'ten ateþ et
        if (!isUpgraded)
        {
            FireFromPoint(firePoints[0]);
        }
        else
        {
            // Tüm aktif firePoints'ten mermi ateþle
            foreach (Transform firePoint in firePoints)
            {
                FireFromPoint(firePoint);
            }
        }
    }

    private void FireFromPoint(Transform firePoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed; // Mermiyi ateþ yönüne gönder
    }

    void UpdateFireRateSlider()
    {
        fireRateSlider.value = fireLevel; // Slider'ý ateþ seviyesine göre güncelle
    }

    void ResetFireLevel()
    {
        fireLevel = 0; // Ateþ seviyesini sýfýrla
        UpdateFireRateSlider(); // Slider'ý güncelle
    }

    // Upgrade alýndýðýnda çaðrýlacak fonksiyon
    public void ActivateUpgrade()
    {
        if (!isUpgraded)
        {
            isUpgraded = true;

            // Mevcut firePoints’e ek olarak seçtiðiniz firePoints’i aktif hale getir
            foreach (Transform additionalPoint in additionalFirePoints)
            {
                firePoints.Add(additionalPoint);
            }
        }
    }
}
