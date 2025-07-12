using UnityEngine;
using UnityEngine.UI;

public class FireInteract : MonoBehaviour
{
    public GameObject fireObject;      // Yanan ateþ objesi
    public GameObject uiTextObject;    // UI yazýsýný içeren GameObject
    public Text uiText;                // UI yazýsý (örnek: "Odun: 10/3")

    private bool isInRange = false;
    public int requiredWood = 3;

    void Start()
    {
        fireObject.SetActive(false);      // Ateþ kapalý baþlar
        uiTextObject.SetActive(false);   // UI gizli baþlar
    }

    void Update()
    {
        if (isInRange)
        {
            int playerWood = InventoryManager.Instance.GetItemAmount("odun");
            uiText.text = $"Odun: {playerWood}/{requiredWood}";

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (playerWood >= requiredWood)
                {
                    // Odunu düþ
                    InventoryManager.Instance.RemoveItem("odun", requiredWood);

                    // Ateþi yak
                    fireObject.SetActive(true);

                    // UI kapat
                    uiTextObject.SetActive(false);

                    // Bir daha ateþ yakýlmasýn istersen:
                    this.enabled = false;
                }
                else
                {
                    Debug.Log("Yeterli odunun yok!");
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            uiTextObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            uiTextObject.SetActive(false);
        }
    }
}
