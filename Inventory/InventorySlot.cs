using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    // ===== Public Variables =====================================================================

    public int index;
    public Item item;

    // ===== Public Functions =====================================================================

    public void AddItem (GameObject newItem)
    {
        newItem.transform.parent = transform;
        newItem.transform.localPosition = Vector3.zero;

        if (item != null)
            UpdateItem(newItem.GetComponent<Item>());

        else
            item = newItem.GetComponent<Item>();
    }

    public Item UpdateItem ()
    {
        if (item == null)
            return null;

        item.StopUse();
        Item temp = item;

        item.transform.parent = null;
        item = null;

        return temp;
    }

    public Item UpdateItem (Item newItem)
    {
        if (item != null)
            item.transform.parent = null;

        item.StopUse();
        Item temp = item;

        newItem.transform.parent = transform;
        newItem.transform.localPosition = Vector3.zero + new Vector3(0,0,-1);
        item = newItem;

        return temp;
    }
}