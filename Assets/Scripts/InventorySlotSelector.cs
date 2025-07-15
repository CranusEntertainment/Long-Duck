using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotSelector : MonoBehaviour
{
    public GameObject[] slotIndicators; // Her slotun üzerinde duran gösterge (mesela mavi ok sprite'ý)
    public Transform[] itemPoints;      // Oyuncunun elinde kullanýlacak pozisyonlar (her slot bir spell gibi)
    public GameObject characterPoint;   // Oyuncunun elindeki nokta nesnesi (gösterici)

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
        // Önce tüm slotlarý pasif yap
        for (int i = 0; i < slotIndicators.Length; i++)
        {
            slotIndicators[i].SetActive(i == currentSlotIndex);
        }

        // Karakterdeki point nesnesini güncelle
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
