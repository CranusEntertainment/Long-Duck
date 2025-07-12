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
        if (string.IsNullOrEmpty(itemName) || amount <= 0)
            return;

        if (inventory.ContainsKey(itemName))
            inventory[itemName] += amount;
        else
            inventory[itemName] = amount;

        Debug.Log($"{itemName} eklendi. Þu an: {inventory[itemName]}");

        UpdateUI();
    }

    public void RemoveItem(string itemName, int amount = 1)
    {
        if (string.IsNullOrEmpty(itemName) || amount <= 0)
            return;

        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] -= amount;
            if (inventory[itemName] <= 0)
                inventory[itemName] = 0;

            Debug.Log($"{itemName} çýkarýldý. Þu an: {inventory[itemName]}");

            UpdateUI();
        }
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

    private void UpdateUI()
    {
        InventoryUIManager uiManager = FindObjectOfType<InventoryUIManager>();
        if (uiManager != null)
            uiManager.UpdateUI();
    }
}
