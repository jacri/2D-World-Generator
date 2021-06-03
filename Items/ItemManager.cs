using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // ===== Public Variables =====================================================================

    public Item item;

    // ===== Update ===============================================================================

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            item.StartUse();

        if (Input.GetMouseButtonUp(0))
            item.StopUse();
    }
}