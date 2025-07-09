using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance
    public GunController GunController;
    public TopDownCharacterController CharacterController; // Karakter controller referansý
    public Text scoreText;
    public GameObject panel; // Panel referansý

    private int score = 0;
    private bool isPanelActive = false; // Panelin açýk olup olmadýðýný kontrol eder

    public float speedIncrement = 2f; // Hýz artýþ miktarý (editörden ayarlanabilir)
    private bool speedUpgraded = false; // Hýzýn artýrýlýp artýrýlmadýðýný kontrol eder
    private bool trailEnabled = false; // Trail Renderer etkinleþme durumu

    private void Awake()
    {
        // Eðer daha önce instance oluþmadýysa, bu instance yap
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
        // Skor kontrolü
        if (score >= 1) // Eðer skor 1'e ulaþýrsa hýzý arttýr
        {
            Debug.Log("Skor 1'e ulaþtý, hýz arttýrýlýyor...");
            UpgradeSpeed();
        }

        if (score >= 2 && !trailEnabled) // Eðer skor 2 olursa Trail Renderer etkinleþir
        {
            Debug.Log("Skor 2'ye ulaþtý, Trail Renderer etkinleþtiriliyor...");
            EnableTrail();
        }
        if (score >= 3) // Eðer skor 3 olursa maksimum can 200 olarak ayarlanýr
        {
            Debug.Log("Skor 3'e ulaþýldý, maksimum can 200'e ayarlanýyor...");
            HealthSystem.Instance.maxHitPoint = 200f; // Maksimum can 200 olarak ayarlandý
           // HealthSystem.Instance.hitPoint = 200f;   // Mevcut can da maksimum deðere eþitleniyor
            //HealthSystem.Instance.UpdateGraphics(); // UI güncelleniyor
        }
        if (score >= 4) // Eðer skor 3 olursa maksimum can 200 olarak ayarlanýr
        {
            Debug.Log("skor 4'e ulaþýldý silah güçlendi ! ");
            GunController.ActivateUpgrade();                                    
        }
        if (score >= 5) // Eðer skor 3 olursa maksimum can 200 olarak ayarlanýr
        {
            Debug.Log("skor 4'e ulaþýldý silah güçlendi ! ");
            GunController.ActivateUpgrade();
        }
        if (score >= 6) // Eðer skor 3 olursa maksimum can 200 olarak ayarlanýr
        {
            Debug.Log("skor 4'e ulaþýldý silah güçlendi ! ");
            GunController.ActivateUpgrade();
        }
        if (score >= 7) // Eðer skor 3 olursa maksimum can 200 olarak ayarlanýr
        {
            Debug.Log("skor 4'e ulaþýldý silah güçlendi ! ");
            GunController.ActivateUpgrade();
        }
        if (score >= 8) // Eðer skor 3 olursa maksimum can 200 olarak ayarlanýr
        {
            Debug.Log("skor 4'e ulaþýldý silah güçlendi ! ");
            GunController.ActivateUpgrade();
        }
        if (score >= 9) // Eðer skor 3 olursa maksimum can 200 olarak ayarlanýr
        {
            Debug.Log("skor 4'e ulaþýldý silah güçlendi ! ");
            GunController.ActivateUpgrade();
        }
        if (score >= 10) // Eðer skor 3 olursa maksimum can 200 olarak ayarlanýr
        {
            Debug.Log("skor 4'e ulaþýldý silah güçlendi ! ");
            GunController.ActivateUpgrade();
        }
        if (score >= 11) // Eðer skor 3 olursa maksimum can 200 olarak ayarlanýr
        {
            Debug.Log("skor 4'e ulaþýldý silah güçlendi ! ");
            GunController.ActivateUpgrade();
        }

        // Tab tuþuna b asýldýðýnda paneli aç/kapa
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
            Debug.Log($"Hýz güncellendi: {CharacterController.moveSpeed}"); // Gerçek deðeri kontrol et
            speedUpgraded = true;
        }
        else
        {
            Debug.LogError("CharacterController referansý eksik!");
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

        // Paneli baþlangýçta kapalý yap
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
            isPanelActive = !isPanelActive; // Mevcut durumu tersine çevir
            panel.SetActive(isPanelActive); // Paneli aç/kapa
        }
    }
}
