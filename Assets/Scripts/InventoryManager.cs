using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(string itemName, int amount = 1)
    {
        InventoryUIManager uiManager = FindObjectOfType<InventoryUIManager>();
        if (uiManager != null)
            uiManager.UpdateUI();


        if (inventory.ContainsKey(itemName))
            inventory[itemName] += amount;
        else
            inventory[itemName] = amount;

        Debug.Log($"{itemName} eklendi. Þu an: {inventory[itemName]}");
    }

    public int GetItemAmount(string itemName)
    {
        if (inventory.ContainsKey(itemName))
            return inventory[itemName];
        return 0;
    }

    public Dictionary<string, int> GetInventory()
    {
        return inventory;
    }
}
