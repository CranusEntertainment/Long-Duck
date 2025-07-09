using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    public Transform player;          // Karakterin transform'u
    public Transform gunTransform;    // Tabancan�n transform'u
    public float radius = 1.5f;       // Karakterle tabanca aras�ndaki mesafe

    private SpriteRenderer gunSpriteRenderer; // Tabancan�n SpriteRenderer bile�eni

    void Start()
    {
        // Tabancan�n SpriteRenderer bile�enini al
        gunSpriteRenderer = gunTransform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Mouse pozisyonunu d�nya koordinatlar�na �evir
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z eksenini s�f�rla (2D i�in)

        // Karakterden mouse'a do�ru y�n vekt�r�n� hesapla
        Vector3 direction = (mousePosition - player.position).normalized;

        // Tabancan�n karakter etraf�nda dairesel hareketle pozisyonunu belirle
        Vector3 desiredPosition = player.position + direction * radius; // Karakterin etraf�nda konum
        gunTransform.position = desiredPosition; // Pozisyonu direkt ayarla, Lerp kullan�lm�yor

        // Tabancan�n rotasyonunu mouse y�n�ne ayarla
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunTransform.rotation = Quaternion.Euler(0, 0, angle);

        // Karakterin sa�a/sola d�nmesini sa�la
        if (direction.x > 0)
        {
            gunSpriteRenderer.flipX = true; // Sa�a bak
            gunSpriteRenderer.flipY = false;
        }
        else if (direction.x < 0)
        {
            gunSpriteRenderer.flipX = true; // Sola bak
            gunSpriteRenderer.flipY = true;
        }
    }
}
