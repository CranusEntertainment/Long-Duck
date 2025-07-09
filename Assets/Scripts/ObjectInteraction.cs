using UnityEngine;
using UnityEngine.UI; // Eðer UI elemanlarýný kullanacaksanýz bunu ekleyin.

public class ObjectInteraction : MonoBehaviour
{
    public GameObject image;
    public string itemName; // Objenin ismi ya da tanýmý
    private bool isInRange = false;
   

    void Start()
    {
        image.SetActive(false);
         // InventoryManager scriptini bul
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager.Instance.AddItem(itemName, 1); // Envantere ekle
            Destroy(gameObject); // Obje yok edilir
            image.SetActive(false);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            image.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            image.SetActive(false);
        }
    }
}

