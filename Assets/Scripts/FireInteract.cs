using UnityEngine;
using UnityEngine.UI;

public class FireInteract : MonoBehaviour
{
    public GameObject fireObject;      // Yanan ate� objesi
    public GameObject uiTextObject;    // UI yaz�s�n� i�eren GameObject
    public Text uiText;                // UI yaz�s� (�rnek: "Odun: 10/3")

    private bool isInRange = false;
    public int requiredWood = 3;

    void Start()
    {
        fireObject.SetActive(false);      // Ate� kapal� ba�lar
        uiTextObject.SetActive(false);   // UI gizli ba�lar
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
                    // Odunu d��
                    InventoryManager.Instance.RemoveItem("odun", requiredWood);

                    // Ate�i yak
                    fireObject.SetActive(true);

                    // UI kapat
                    uiTextObject.SetActive(false);

                    // Bir daha ate� yak�lmas�n istersen:
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
