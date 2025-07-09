using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    public Transform player;          // Karakterin transform'u
    public Transform gunTransform;    // Tabancanýn transform'u
    public float radius = 1.5f;       // Karakterle tabanca arasýndaki mesafe

    private SpriteRenderer gunSpriteRenderer; // Tabancanýn SpriteRenderer bileþeni

    void Start()
    {
        // Tabancanýn SpriteRenderer bileþenini al
        gunSpriteRenderer = gunTransform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Mouse pozisyonunu dünya koordinatlarýna çevir
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z eksenini sýfýrla (2D için)

        // Karakterden mouse'a doðru yön vektörünü hesapla
        Vector3 direction = (mousePosition - player.position).normalized;

        // Tabancanýn karakter etrafýnda dairesel hareketle pozisyonunu belirle
        Vector3 desiredPosition = player.position + direction * radius; // Karakterin etrafýnda konum
        gunTransform.position = desiredPosition; // Pozisyonu direkt ayarla, Lerp kullanýlmýyor

        // Tabancanýn rotasyonunu mouse yönüne ayarla
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunTransform.rotation = Quaternion.Euler(0, 0, angle);

        // Karakterin saða/sola dönmesini saðla
        if (direction.x > 0)
        {
            gunSpriteRenderer.flipX = true; // Saða bak
            gunSpriteRenderer.flipY = false;
        }
        else if (direction.x < 0)
        {
            gunSpriteRenderer.flipX = true; // Sola bak
            gunSpriteRenderer.flipY = true;
        }
    }
}
