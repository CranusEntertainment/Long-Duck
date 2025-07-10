using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public string itemName;
    public GameObject image;
    private bool isInRange = false;

    void Start()
    {
        image.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager.Instance.AddItem(itemName, 1);
            Destroy(gameObject);
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
