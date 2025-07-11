using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public Text odunText;
    public Text mantarText;
    public Text RockText;

    public void UpdateUI()
    {
        odunText.text = "" + InventoryManager.Instance.GetItemAmount("odun");
        mantarText.text = "" + InventoryManager.Instance.GetItemAmount("mantar");
        RockText.text = "" + InventoryManager.Instance.GetItemAmount("rock");
    }
}
