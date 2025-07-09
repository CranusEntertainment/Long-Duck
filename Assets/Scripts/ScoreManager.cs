using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance
    public GunController GunController;
    public TopDownCharacterController CharacterController; // Karakter controller referans�
    public Text scoreText;
    public GameObject panel; // Panel referans�

    private int score = 0;
    private bool isPanelActive = false; // Panelin a��k olup olmad���n� kontrol eder

    public float speedIncrement = 2f; // H�z art�� miktar� (edit�rden ayarlanabilir)
    private bool speedUpgraded = false; // H�z�n art�r�l�p art�r�lmad���n� kontrol eder
    private bool trailEnabled = false; // Trail Renderer etkinle�me durumu

    private void Awake()
    {
        // E�er daha �nce instance olu�mad�ysa, bu instance yap
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Skor kontrol�
        if (score >= 1) // E�er skor 1'e ula��rsa h�z� artt�r
        {
            Debug.Log("Skor 1'e ula�t�, h�z artt�r�l�yor...");
            UpgradeSpeed();
        }

        if (score >= 2 && !trailEnabled) // E�er skor 2 olursa Trail Renderer etkinle�ir
        {
            Debug.Log("Skor 2'ye ula�t�, Trail Renderer etkinle�tiriliyor...");
            EnableTrail();
        }
        if (score >= 3) // E�er skor 3 olursa maksimum can 200 olarak ayarlan�r
        {
            Debug.Log("Skor 3'e ula��ld�, maksimum can 200'e ayarlan�yor...");
            HealthSystem.Instance.maxHitPoint = 200f; // Maksimum can 200 olarak ayarland�
           // HealthSystem.Instance.hitPoint = 200f;   // Mevcut can da maksimum de�ere e�itleniyor
            //HealthSystem.Instance.UpdateGraphics(); // UI g�ncelleniyor
        }
        if (score >= 4) // E�er skor 3 olursa maksimum can 200 olarak ayarlan�r
        {
            Debug.Log("skor 4'e ula��ld� silah g��lendi ! ");
            GunController.ActivateUpgrade();                                    
        }
        if (score >= 5) // E�er skor 3 olursa maksimum can 200 olarak ayarlan�r
        {
            Debug.Log("skor 4'e ula��ld� silah g��lendi ! ");
            GunController.ActivateUpgrade();
        }
        if (score >= 6) // E�er skor 3 olursa maksimum can 200 olarak ayarlan�r
        {
            Debug.Log("skor 4'e ula��ld� silah g��lendi ! ");
            GunController.ActivateUpgrade();
        }
        if (score >= 7) // E�er skor 3 olursa maksimum can 200 olarak ayarlan�r
        {
            Debug.Log("skor 4'e ula��ld� silah g��lendi ! ");
            GunController.ActivateUpgrade();
        }
        if (score >= 8) // E�er skor 3 olursa maksimum can 200 olarak ayarlan�r
        {
            Debug.Log("skor 4'e ula��ld� silah g��lendi ! ");
            GunController.ActivateUpgrade();
        }
        if (score >= 9) // E�er skor 3 olursa maksimum can 200 olarak ayarlan�r
        {
            Debug.Log("skor 4'e ula��ld� silah g��lendi ! ");
            GunController.ActivateUpgrade();
        }
        if (score >= 10) // E�er skor 3 olursa maksimum can 200 olarak ayarlan�r
        {
            Debug.Log("skor 4'e ula��ld� silah g��lendi ! ");
            GunController.ActivateUpgrade();
        }
        if (score >= 11) // E�er skor 3 olursa maksimum can 200 olarak ayarlan�r
        {
            Debug.Log("skor 4'e ula��ld� silah g��lendi ! ");
            GunController.ActivateUpgrade();
        }

        // Tab tu�una b as�ld���nda paneli a�/kapa
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePanel();
        }
    }

    public void UpgradeSpeed()
    {
        if (CharacterController != null)
        {
            CharacterController.moveSpeed = 10f;
            Debug.Log($"H�z g�ncellendi: {CharacterController.moveSpeed}"); // Ger�ek de�eri kontrol et
            speedUpgraded = true;
        }
        else
        {
            Debug.LogError("CharacterController referans� eksik!");
        }
    }

    public void EnableTrail()
    {
        if (CharacterController != null && CharacterController.dashTrail != null)
        {
            CharacterController.dashTrail.gameObject.SetActive(true); // Trail Renderer aktif hale getir
            trailEnabled = true;
        }
        else
        {
            Debug.LogError("Trail Renderer veya CharacterController eksik!");
        }
    }

    void Start()
    {
        UpdateScoreText();

        // Paneli ba�lang��ta kapal� yap
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void TogglePanel()
    {
        if (panel != null)
        {
            isPanelActive = !isPanelActive; // Mevcut durumu tersine �evir
            panel.SetActive(isPanelActive); // Paneli a�/kapa
        }
    }
}
