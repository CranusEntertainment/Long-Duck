using UnityEngine;
using UnityEngine.UI; // E�er UI elemanlar�n� kullanacaksan�z bunu ekleyin.

public class ObjectInteraction : MonoBehaviour
{
    public GameObject image;
    public string itemName; // Objenin ismi ya da tan�m�
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

