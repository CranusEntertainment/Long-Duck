using UnityEngine;

public class CraftingZoneTrigger : MonoBehaviour
{
    public GameObject craftPanel; // UI'daki craft panelin

    private void Start()
    {
        if (craftPanel != null)
            craftPanel.SetActive(false); // Baþta kapalý
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (craftPanel != null)
                craftPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (craftPanel != null)
                craftPanel.SetActive(false);
        }
    }
}
