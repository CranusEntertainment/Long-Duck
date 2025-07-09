using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth;

    public Slider healthSlider; // Can barý slider
    public Image animatedImage; // Animasyonlu image
    public Sprite[] animationFrames; // Animasyon için görseller
    public float frameRate = 0.1f; // Animasyon hýzýný belirleyen süre

    private int currentFrame = 0; // Mevcut animasyon karesi
    private float frameTimer;
    public GameObject explosionEffectPrefab; // Patlama efekti prefab'i
    public GameObject leftoverObjectPrefab; // Geriye býrakýlacak obje prefab'i
    public GameObject hit;
    public ScoreManager skor;
    private void Start()
    {
        // Baþlangýçta caný maksimum deðere ayarla
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        // Animasyon için timer baþlat
        frameTimer = frameRate;
    }

    private void Update()
    {
        // Can barýnýn arka planýnda animasyon oluþtur
        AnimateHealthImage();
    }

    private void AnimateHealthImage()
    {
        // Animasyonun hýzýný kontrol et
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0)
        {
            frameTimer = frameRate;
            currentFrame = (currentFrame + 1) % animationFrames.Length; // Sýradaki kareye geç ve döngüsel yap
            animatedImage.sprite = animationFrames[currentFrame];
        }
    }

    public void TakeDamage(int damage)
    {
        // Caný azalt ve slider deðerini güncelle
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Canýn sýfýrýn altýna inmesini önle
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            DestroyTower();

            Debug.Log("Tower yok edildi ! ");
        }
    }

    private void DestroyTower()
    {
        skor.IncreaseScore(1);
        // Patlama efektini oluþtur
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // Geriye býrakýlacak objeyi (peynir) oluþtur
        if (leftoverObjectPrefab != null)
        {
            Instantiate(leftoverObjectPrefab, transform.position, Quaternion.identity);
        }

        // Biraz bekledikten sonra Tower'ý yok et
        Destroy(gameObject, 0.1f); // 0.1 saniye sonra yok et
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Eðer tower, "Bullet" tag'ine sahip bir mermiyle çarpýþtýysa hasar al
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(hit, transform.position, Quaternion.identity);

            TakeDamage(10); // Her çarpýþmada 10 hasar al
            Destroy(collision.gameObject); // Mermi yok olur
        }
    }
}
