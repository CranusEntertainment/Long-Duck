using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab'�
    public List<Transform> firePoints; // Ate� noktalar� (birden fazla destekleniyor)
    public float bulletSpeed = 10f;

    public GunRecoil gungun;
    public Slider fireRateSlider; // At�� h�z�n� g�steren slider

    private int fireLevel = 0; // Ate� h�z�n�n seviyesi (1-7)
    private bool isShooting = false;
    private Coroutine fireCoroutine;
    private Coroutine slowDownCoroutine; // H�z d���rme Coroutine�i
    public TopDownCharacterController karakter;

    private float originalMoveSpeed; // Karakterin orijinal hareket h�z�
    private float originalDashSpeed; // Karakterin orijinal dash h�z�
    private bool isUpgraded = false; // Upgrade durumunu takip etmek i�in
    public List<Transform> additionalFirePoints; // Upgrade sonras� a��lacak firePoints

    private float shootHoldTime = 0f; // Sol t�k�n ne kadar s�reyle bas�l� tutuldu�unu izlemek i�in

    private void Start()
    {
        karakter = GetComponent<TopDownCharacterController>();

        // Ba�lang�� h�zlar�n� kaydet
        originalMoveSpeed = karakter.moveSpeed;
        originalDashSpeed = karakter.dashSpeed;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Sol t�k bas�l� tutuluyor
        {
            shootHoldTime += Time.deltaTime; // Bas�l� tutma s�resini art�r

            if (isShooting == false)
            {
                isShooting = true;

                if (fireCoroutine == null)
                    fireCoroutine = StartCoroutine(ShootRoutine()); // S�rekli ate� etme i�lemini ba�lat�r
            }

            // E�er 2 saniye bas�l� tutulduysa h�zlar� d���r
            if (shootHoldTime >= 2f && slowDownCoroutine == null)
            {
                slowDownCoroutine = StartCoroutine(SlowDown());
            }
        }
        else if (Input.GetMouseButtonUp(0)) // Sol t�k b�rak�ld���nda
        {
            isShooting = false;

            // H�zlar� orijinal de�erlerine d�nd�r
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

            ResetFireLevel(); // Ate� h�z� bar�n� s�f�rla
            shootHoldTime = 0f; // Bas�l� tutma s�resini s�f�rla
        }
    }

    IEnumerator SlowDown()
    {
        // H�zlar� d���r
        karakter.dashSpeed = 5;
        karakter.moveSpeed = 1;
        yield break; // Coroutine hemen biter, h�zlar an�nda d���r�l�r
    }

    IEnumerator ShootRoutine()
    {
        fireLevel = 0; // Ba�lang��ta ate� h�z� seviyesini s�f�rla
        UpdateFireRateSlider();

        while (isShooting)
        {
            Shoot();
            gungun.Shoot(); // Ate�leme ve geri tepme

            if (fireLevel < 7) // Ate� seviyesi maksimuma ula�ana kadar artmaya devam eder
            {
                fireLevel++; // Her saniyede bir fireLevel�� art�r
                UpdateFireRateSlider();
            }

            // Ate� etme aral���n� fireLevel�a g�re ayarla
            yield return new WaitForSeconds(1.0f / (fireLevel > 0 ? fireLevel : 1));
        }
    }

    void Shoot()
    {
        // Upgrade yap�lmadan �nce yaln�zca ilk firePoint'ten ate� et
        if (!isUpgraded)
        {
            FireFromPoint(firePoints[0]);
        }
        else
        {
            // T�m aktif firePoints'ten mermi ate�le
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
        rb.velocity = firePoint.right * bulletSpeed; // Mermiyi ate� y�n�ne g�nder
    }

    void UpdateFireRateSlider()
    {
        fireRateSlider.value = fireLevel; // Slider'� ate� seviyesine g�re g�ncelle
    }

    void ResetFireLevel()
    {
        fireLevel = 0; // Ate� seviyesini s�f�rla
        UpdateFireRateSlider(); // Slider'� g�ncelle
    }

    // Upgrade al�nd���nda �a�r�lacak fonksiyon
    public void ActivateUpgrade()
    {
        if (!isUpgraded)
        {
            isUpgraded = true;

            // Mevcut firePoints�e ek olarak se�ti�iniz firePoints�i aktif hale getir
            foreach (Transform additionalPoint in additionalFirePoints)
            {
                firePoints.Add(additionalPoint);
            }
        }
    }
}
