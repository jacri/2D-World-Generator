using UnityEngine;

public class HotbarSlot : InventorySlot
{
    // ===== Public Variables =====================================================================

    public GameObject activeIndicator;

    // ===== Public Functions =====================================================================

    public Item Activate ()
    {
        activeIndicator.SetActive(true);
        return GetComponentInChildren<Item>();
    }

    public void Deactivate ()
    {
        activeIndicator.SetActive(false);
    }
}