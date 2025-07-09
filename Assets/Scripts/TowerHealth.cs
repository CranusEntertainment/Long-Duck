using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth;

    public Slider healthSlider; // Can bar� slider
    public Image animatedImage; // Animasyonlu image
    public Sprite[] animationFrames; // Animasyon i�in g�rseller
    public float frameRate = 0.1f; // Animasyon h�z�n� belirleyen s�re

    private int currentFrame = 0; // Mevcut animasyon karesi
    private float frameTimer;
    public GameObject explosionEffectPrefab; // Patlama efekti prefab'i
    public GameObject leftoverObjectPrefab; // Geriye b�rak�lacak obje prefab'i
    public GameObject hit;
    public ScoreManager skor;
    private void Start()
    {
        // Ba�lang��ta can� maksimum de�ere ayarla
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        // Animasyon i�in timer ba�lat
        frameTimer = frameRate;
    }

    private void Update()
    {
        // Can bar�n�n arka plan�nda animasyon olu�tur
        AnimateHealthImage();
    }

    private void AnimateHealthImage()
    {
        // Animasyonun h�z�n� kontrol et
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0)
        {
            frameTimer = frameRate;
            currentFrame = (currentFrame + 1) % animationFrames.Length; // S�radaki kareye ge� ve d�ng�sel yap
            animatedImage.sprite = animationFrames[currentFrame];
        }
    }

    public void TakeDamage(int damage)
    {
        // Can� azalt ve slider de�erini g�ncelle
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Can�n s�f�r�n alt�na inmesini �nle
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
        // Patlama efektini olu�tur
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // Geriye b�rak�lacak objeyi (peynir) olu�tur
        if (leftoverObjectPrefab != null)
        {
            Instantiate(leftoverObjectPrefab, transform.position, Quaternion.identity);
        }

        // Biraz bekledikten sonra Tower'� yok et
        Destroy(gameObject, 0.1f); // 0.1 saniye sonra yok et
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        // E�er tower, "Bullet" tag'ine sahip bir mermiyle �arp��t�ysa hasar al
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(hit, transform.position, Quaternion.identity);

            TakeDamage(10); // Her �arp��mada 10 hasar al
            Destroy(collision.gameObject); // Mermi yok olur
        }
    }
}
