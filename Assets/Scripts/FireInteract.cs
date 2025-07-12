using UnityEngine;
using UnityEngine.UI;

public class FireInteract : MonoBehaviour
{
    public GameObject fireObject;
    public GameObject uiTextObject;
    public Text uiText;
    public DayNightCycle dayNightCycle;
    public HealthSystem playerHealth; // Sağlık sistemi (sen tanımlayacaksın)
    public Animator playerAnimator; // Oyuncunun Animator'u
    private bool isInRange = false;
    private bool fireIsLit = false;  // Ateş yakıldı mı?

    public int requiredWood = 3;
    public int requiredMushroom = 1;
    public int healAmount = 20; // Can artış miktarı
    public DayNightCycle hunger;
    void Start()
    {
        fireObject.SetActive(false);
        uiTextObject.SetActive(false);
    }

    void Update()
    {
        if (!isInRange) return;

        int playerWood = InventoryManager.Instance.GetItemAmount("odun");
        int playerMushroom = InventoryManager.Instance.GetItemAmount("mantar");

        if (!fireIsLit)
        {
            uiText.text = $"Odun: {playerWood}/{requiredWood} (E yak)";

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (playerWood >= requiredWood)
                {
                    InventoryManager.Instance.RemoveItem("odun", requiredWood);
                    fireObject.SetActive(true);
                    fireIsLit = true;
                    uiText.text = "Ateş yakıldı!";
                    Debug.Log("Ateş yakıldı!");
                }
                else
                {
                    uiText.text = "Yeterli odunun yok!";
                }
            }
        }
        else // Ateş zaten yanıyor → yemek pişirme zamanı
        {
            uiText.text = $"Mantar: {playerMushroom}/{requiredMushroom} (E pişir)";

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (playerMushroom >= requiredMushroom)
                {
                    InventoryManager.Instance.RemoveItem("mantar", requiredMushroom);
                    // Açlık artır
                    dayNightCycle.IncreaseHunger(5); // hungerAmount: örneğin 20
                    playerAnimator.SetTrigger("Eat");
                    uiText.text = "Mantar pişirildi +20 Açlık!";
                    Debug.Log("Mantar pişirildi +20 Açlık");
                    uiText.text = "Mantar pişirildi +20 Can!";
                    Debug.Log("Mantar pişirildi +20 Can");
                }
                else
                {
                    uiText.text = "Yeterli mantarın yok!";
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dayNightCycle.EnterWarmZone();
            isInRange = true;
            uiTextObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dayNightCycle.ExitWarmZone();
            isInRange = false;
            uiTextObject.SetActive(false);
        }
    }
}
