using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotSelector : MonoBehaviour
{
    public GameObject[] slotIndicators; // Her slotun �zerinde duran g�sterge (mesela mavi ok sprite'�)
    public Transform[] itemPoints;      // Oyuncunun elinde kullan�lacak pozisyonlar (her slot bir spell gibi)
    public GameObject characterPoint;   // Oyuncunun elindeki nokta nesnesi (g�sterici)

    private int currentSlotIndex = 0;
    private int slotCount;

    void Start()
    {
        slotCount = slotIndicators.Length;
        UpdateSlotIndicator();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentSlotIndex = (currentSlotIndex + 1) % slotCount;
            UpdateSlotIndicator();
        }
    }
    

    void UpdateSlotIndicator()
    {
        // �nce t�m slotlar� pasif yap
        for (int i = 0; i < slotIndicators.Length; i++)
        {
            slotIndicators[i].SetActive(i == currentSlotIndex);
        }

        // Karakterdeki point nesnesini g�ncelle
        if (characterPoint != null && itemPoints.Length > currentSlotIndex)
        {
            characterPoint.transform.position = itemPoints[currentSlotIndex].position;
        }
    }

    public int GetCurrentSlotIndex()
    {
        return currentSlotIndex;
    }
}
