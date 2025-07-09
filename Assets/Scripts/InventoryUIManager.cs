using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject itemTextPrefab; // Text prefab
    public Transform contentParent;   // UI panelin objesi

    private Dictionary<string, Text> itemTexts = new Dictionary<string, Text>();

    void Start()
    {
        UpdateUI(); // Oyun baþýnda bir kere çaðýr (boþ olsa bile)
    }

    public void UpdateUI()
    {
        Dictionary<string, int> inventory = InventoryManager.Instance.GetInventory();

        foreach (KeyValuePair<string, int> pair in inventory)
        {
            if (itemTexts.ContainsKey(pair.Key))
            {
                itemTexts[pair.Key].text = $"{pair.Key}: {pair.Value}";
            }
            else
            {
                GameObject newTextObj = Instantiate(itemTextPrefab, contentParent);
                Text textComponent = newTextObj.GetComponent<Text>();
                textComponent.text = $"{pair.Key}: {pair.Value}";
                itemTexts.Add(pair.Key, textComponent);
            }
        }
    }
}
